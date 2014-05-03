Imports Windows.Storage
Imports Microsoft.Phone.BackgroundTransfer
Imports System.IO
Imports System.IO.IsolatedStorage
Imports ICSharpCode.SharpZipLib.Zip
Imports System.Runtime.Serialization
Imports System.Collections.ObjectModel
Imports Coding4Fun.Toolkit.Controls
Imports System.ComponentModel

Public Class clsDatabaseIO



    Public Shared Function GetTheLatestDayInXmlFileFolder(ByRef tLatestDay As DateTime) As Boolean
        Try
            Dim _FilePaths As List(Of String) = clsDatabaseIO.GetAllXmlFilePath(GlobalVariables.gXmlFileFolderName)
            If _FilePaths.Count = 0 Then
                Return False
            End If

            tLatestDay = GetDateFromFilePath(_FilePaths.Item(0))
            Dim _TempDay As DateTime = Nothing
            Dim _ZeroTime As TimeSpan = New TimeSpan(0)
            For i As Integer = 0 To _FilePaths.Count - 1
                _TempDay = GetDateFromFilePath(_FilePaths.Item(i))
                If _TempDay - tLatestDay > _ZeroTime Then
                    tLatestDay = _TempDay
                End If
            Next
            Return True
        Catch ex As Exception

        End Try

        Return False
    End Function
    Private Shared Sub AddSuitableTableToSchedule(ByRef tSchedule As ObservableCollection(Of clsTrainsInOneDay), tTable As clsTrainsInOneDay)
        Dim _ZeroTime As TimeSpan = New TimeSpan(0)
        If tTable.mDate - DateTime.Now < _ZeroTime Then Exit Sub
        For i As Integer = 0 To tSchedule.Count - 1
            If tTable.mDate.Year = tSchedule.Item(i).mDate.Year And tTable.mDate.Month = tSchedule.Item(i).mDate.Month And tTable.mDate.Day = tSchedule.Item(i).mDate.Day Then
                Exit Sub
            End If
        Next
        tSchedule.Add(tTable)
    End Sub
    Public Shared Sub AddMoreXmlFromIsoStorage()
        Dim _FilePaths As List(Of String) = clsDatabaseIO.GetAllXmlFilePath(GlobalVariables.gXmlFileFolderName)

        Dim _FileDate As DateTime

        For i As Integer = 0 To _FilePaths.Count - 1
            _FileDate = GetDateFromFilePath(_FilePaths.Item(i))
            AddSuitableTableToSchedule(GlobalVariables.gSchedule, New clsTrainsInOneDay(_FileDate, GlobalVariables.gXmlFileFolderName + _FilePaths.Item(i), False, True))
        Next

    End Sub
    Public Shared Sub LoadXmlFromIsoStorage_BgWorker(ByRef tBgWorker As BackgroundWorker, funPgChanged As ProgressChangedEventHandler, funComplete As RunWorkerCompletedEventHandler)
        tBgWorker = New BackgroundWorker
        tBgWorker.WorkerReportsProgress = True
        AddHandler tBgWorker.DoWork, AddressOf LoadXmlFromIsoStorage_SingleThread
        AddHandler tBgWorker.ProgressChanged, funPgChanged
        AddHandler tBgWorker.RunWorkerCompleted, funComplete

        tBgWorker.RunWorkerAsync()
    End Sub 
    Public Shared Sub LoadXmlFromIsoStorage_SingleThread()
        Dim _FilePaths As List(Of String) = clsDatabaseIO.GetAllXmlFilePath(GlobalVariables.gXmlFileFolderName)

        Dim _FileDate As DateTime

        GlobalVariables.gSchedule.Clear()
        For i As Integer = 0 To _FilePaths.Count - 1
            _FileDate = GetDateFromFilePath(_FilePaths.Item(i))

            If _FileDate.Month = DateTime.Now.Month And _FileDate.Day = DateTime.Now.Day Then
                GlobalVariables.gSchedule.Add(New clsTrainsInOneDay(_FileDate, GlobalVariables.gXmlFileFolderName + _FilePaths.Item(i), True, True))
            Else
                GlobalVariables.gSchedule.Add(New clsTrainsInOneDay(_FileDate, GlobalVariables.gXmlFileFolderName + _FilePaths.Item(i), False, True))
            End If
        Next
         
    End Sub
    ''複製 Assets 中的 xml 到 指地的 xml folder  若 xml 的檔名日期 早於昨天 則不放入
    ''會檢查 xml folder 是否存在
    Public Shared Sub CopyDefaultTimeTableToXMLFolder()
        Dim _AllXmlFileinDefaultFolder As String() = Directory.GetFiles(GlobalVariables.gDefaultXmlFileFolder)
        Dim _Today As DateTime = New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)
        Dim _FileDay As DateTime = Nothing
        Dim _OneDay As TimeSpan = New TimeSpan(24, 0, 0)

        Dim _IsoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
        If _IsoStore.DirectoryExists(GlobalVariables.gXmlFileFolderName) = False Then
            _IsoStore.CreateDirectory(GlobalVariables.gXmlFileFolderName)
        End If

        For i As Integer = 0 To _AllXmlFileinDefaultFolder.Length - 1
            _FileDay = ConvertXmlFileNameToDateTime(_AllXmlFileinDefaultFolder(i))

            If _Today - _FileDay <= _OneDay Then
                Dim _DesFilePath As String = GlobalVariables.gXmlFileFolderName + _AllXmlFileinDefaultFolder(i).Substring(_AllXmlFileinDefaultFolder(i).Length - 12)
                CopyFileFromAssetsToIsoStorage(_AllXmlFileinDefaultFolder(i), _DesFilePath)
            End If
        Next

    End Sub

    Private Shared Function ConvertXmlFileNameToDateTime(tFilePath As String) As DateTime
        Dim _DateString As String = tFilePath.Substring(tFilePath.Length - 12)
        Dim _Year As String = _DateString.Substring(0, 4)
        Dim _Month As String = _DateString.Substring(4, 2)
        Dim _Day As String = _DateString.Substring(6, 2)

        Return New DateTime(_Year, _Month, _Day)
    End Function


    Public Shared Sub CopyFileFromAssetsToIsoStorage(tOriginalFilePath As String, tDesFilePath As String)
        If File.Exists(tOriginalFilePath) = False Then Exit Sub
        Dim _OriginalUri As Uri = New Uri(tOriginalFilePath, UriKind.Relative)
        Dim _OriginalSRI As System.Windows.Resources.StreamResourceInfo = Application.GetResourceStream(_OriginalUri)
        Dim _OriginalData As IO.Stream = _OriginalSRI.Stream

        Dim _IsoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
        If _IsoStore.FileExists(tDesFilePath) Then
            _IsoStore.DeleteFile(tDesFilePath)
        End If

        Dim _IsoStorage As IsolatedStorage.IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
        Dim _IsoStream As IsolatedStorageFileStream = _IsoStorage.CreateFile(tDesFilePath)
        _OriginalData.CopyTo(_IsoStream)

        _OriginalData.Dispose()
        _IsoStream.Dispose()
    End Sub

    ''DataContractorSerializer
    Public Shared Function ToMemoryStream(tDataContractorObj As Object) As MemoryStream
        Dim _Serializer As New DataContractSerializer(tDataContractorObj.GetType)
        Dim _ResultStream As New MemoryStream()
        _Serializer.WriteObject(_ResultStream, tDataContractorObj)
        Return _ResultStream
    End Function

    Public Shared Sub ToObject(InputStream As MemoryStream, ByRef tResult As Object)
        Dim _Serializer As New DataContractSerializer(tResult.GetType)
        tResult = _Serializer.ReadObject(InputStream)
    End Sub

    '' MemoryStream File IO
    Public Shared Sub SaveMemoryStream(tFilePath As String, tInputStreamList As List(Of MemoryStream))
        Dim _FS As New FileStream(tFilePath, FileMode.Create, System.IO.FileAccess.Write)

        Dim _CurrentMemoryStreamLength As Integer = 0
        Dim _bytes(0) As Byte
        Dim _tempInpurStream As MemoryStream
        For i As Integer = 0 To tInputStreamList.Count - 1
            _tempInpurStream = tInputStreamList.Item(i)
            _tempInpurStream.Read(_bytes, _CurrentMemoryStreamLength, _tempInpurStream.Length)
            _CurrentMemoryStreamLength += _tempInpurStream.Length
            _tempInpurStream.Close()
        Next
        'ms.Read(bytes, 0, (int)ms.Length);
        _FS.Write(_bytes, 0, _bytes.Length)
        _FS.Close()
    End Sub

    Public Shared Function ReadMomoryStream(tFilePath As String, tObjStreamLength As Integer) As List(Of MemoryStream)
        Dim _FS As New FileStream(tFilePath, FileMode.Open, System.IO.FileAccess.Read)
        Dim _CurrentMemoryStreamLength As Integer = 0
        Dim _Result As New List(Of MemoryStream)
        Dim _bytes(0) As Byte
        Dim _tempMS As MemoryStream
        Do Until _CurrentMemoryStreamLength >= _FS.Length
            _FS.Read(_bytes, _CurrentMemoryStreamLength, tObjStreamLength)
            _tempMS = New MemoryStream
            _tempMS.Read(_bytes, 0, tObjStreamLength)
            _Result.Add(_tempMS)
        Loop

        Return _Result
    End Function


    Public Shared Function IsFileExist(tFilePath As String) As Boolean
        Dim _IsoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
        Return _IsoStore.FileExists(tFilePath)
    End Function
    Public Shared Function GetAllXmlFilePath(tXmlFolder As String) As List(Of String)
        Dim _result As New List(Of String)
        Dim _FilePathArray() As String
        Dim _IsoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
        If Not _IsoStore.DirectoryExists(tXmlFolder) Then Return _result
        _FilePathArray = _IsoStore.GetFileNames(tXmlFolder + "*")
        For i As Integer = 0 To _FilePathArray.Length - 1
            _result.Add(_FilePathArray(i))
        Next
        Return _result
    End Function
    Public Shared Function GetTheLatestDate(tFilePathList As List(Of String)) As DateTime
        If tFilePathList Is Nothing OrElse tFilePathList.Count = 0 Then Return Nothing
        Dim _LatestDate As DateTime = GetDateFromFilePath(tFilePathList.Item(0))
        Dim _tempDate As DateTime
        For i As Integer = 1 To tFilePathList.Count - 1
            _tempDate = GetDateFromFilePath(tFilePathList.Item(i))
            If _LatestDate < _tempDate Then
                _LatestDate = _tempDate
            End If
        Next
        Return _LatestDate
    End Function


    Public Shared Function LoadAllTrains(tFilePath As String) As List(Of clsTrain)
        Dim _DateString As String = tFilePath.Substring(tFilePath.Length - 12)
        Dim _Year As String = _DateString.Substring(0, 4)
        Dim _Month As String = _DateString.Substring(4, 2)
        Dim _Day As String = _DateString.Substring(6, 2)

        Dim ResultTrain As New List(Of clsTrain)
        ' Dim a As FileStream = File.OpenRead("Assets/TimeTable/20131005.xml")
        Dim FS As FileStream = File.OpenRead(tFilePath)
        Dim Reader As XmlReader = XmlReader.Create(FS)

        Dim tempTrain As clsTrain = Nothing
        Dim _CarClass, _Cripple, _Dinning, _Line, _LineDir, _Note, _OverNightStn, _Package, _Route, _Train, _Type As String
        Dim tempTimeInfoList As ObservableCollection(Of clsTimeInfo) = Nothing

        While Reader.Read()
            If Reader.IsStartElement() Then
                If Reader.Name = "TrainInfo" Then
                    If tempTimeInfoList IsNot Nothing Then
                        tempTrain = New clsTrain(_Year, _Month, _Day, _CarClass, _Cripple, _Dinning, _Line, _LineDir, _Note, _OverNightStn, _Package, _Route, _Train, _Type, tempTimeInfoList)
                        ResultTrain.Add(tempTrain)
                        tempTrain = Nothing
                        tempTimeInfoList = Nothing
                    End If
                    _CarClass = Reader("CarClass")
                    _Cripple = Reader("Cripple")
                    _Dinning = Reader("Dinning")
                    _Line = Reader("Line")
                    _LineDir = Reader("LineDir")
                    _Note = Reader("Note")
                    _OverNightStn = Reader("OverNightStn")
                    _Package = Reader("Package")
                    _Route = Reader("Route")
                    _Train = Reader("Train")
                    _Type = Reader("Type")
                    tempTimeInfoList = New ObservableCollection(Of clsTimeInfo)
                ElseIf Reader.Name = "TimeInfo" Then
                    tempTimeInfoList.Add(New clsTimeInfo(_Year, _Month, _Day, Reader("ARRTime"), Reader("DEPTime"), Reader("Order"), Reader("Route"), Reader("Station")))
                End If
            End If
        End While

        Return ResultTrain
    End Function

    Public Shared Sub ClearStorage()
        Dim _IsoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication
        If _IsoStore.FileExists(GlobalVariables.gZipTimeTableFilePath) Then _IsoStore.DeleteFile(GlobalVariables.gZipTimeTableFilePath)
        If _IsoStore.FileExists(GlobalVariables.gZipTimeTableFileTempPath) Then _IsoStore.DeleteFile(GlobalVariables.gZipTimeTableFileTempPath)
        Dim _FilePath As List(Of String) = clsDatabaseIO.GetAllXmlFilePath(GlobalVariables.gXmlFileFolderName)
        For i As Integer = 0 To _FilePath.Count - 1
            If _IsoStore.FileExists(GlobalVariables.gXmlFileFolderName + _FilePath.Item(i)) Then
                _IsoStore.DeleteFile(GlobalVariables.gXmlFileFolderName + _FilePath.Item(i))
            End If
        Next
    End Sub

    Public Shared Function GetDateFromFilePath(tFilePath As String) As DateTime
        tFilePath = tFilePath.Substring(tFilePath.Length - 12)
        Dim _yearString As String = tFilePath.Substring(0, 4)
        Dim _monthString As String = tFilePath.Substring(4, 2)
        Dim _dateString As String = tFilePath.Substring(6, 2)
        Return New DateTime(_yearString, _monthString, _dateString)
    End Function

    Public Shared Sub AddTimeTableFormIsolationStorageToDB(tTimeTableFilePath As String)
        Dim _tempTrainsInOneDate As clsTrainsInOneDay
        Dim _tempDate As DateTime
        _tempDate = GetDateFromFilePath(tTimeTableFilePath)
        _tempTrainsInOneDate = New clsTrainsInOneDay(_tempDate, tTimeTableFilePath, True, True)
        GlobalVariables.gSchedule.Add(_tempTrainsInOneDate)
    End Sub


    Public Shared Function LoadAllTimeTablesFormIsolatedStorage(tTimeTableFilePathList As List(Of String)) As List(Of clsTrainsInOneDay)
        Dim _result As New List(Of clsTrainsInOneDay)
        Dim _tempTrainsInOneDate As clsTrainsInOneDay
        Dim _tempDate As DateTime
        For i As Integer = 0 To tTimeTableFilePathList.Count - 1
            _tempDate = GetDateFromFilePath(tTimeTableFilePathList.Item(i))
            _tempTrainsInOneDate = New clsTrainsInOneDay(_tempDate, tTimeTableFilePathList.Item(i), True, True)
            _result.Add(_tempTrainsInOneDate)
        Next
        Return _result
    End Function

    Public Shared Function LoadTrainsFromIsolatedStorage(tFilePath As String) As List(Of clsTrain)
        Dim _IsolatedStorage As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
        If _IsolatedStorage.FileExists(tFilePath) = False Then Return Nothing

        Dim _DateString As String = tFilePath.Substring(tFilePath.Length - 12)
        Dim _Year As String = _DateString.Substring(0, 4)
        Dim _Month As String = _DateString.Substring(4, 2)
        Dim _Day As String = _DateString.Substring(6, 2)

        Dim ResultTrain As New List(Of clsTrain)
        Dim FS As IsolatedStorageFileStream = _IsolatedStorage.OpenFile(tFilePath, FileMode.Open, FileAccess.Read)
        Dim Reader As XmlReader = XmlReader.Create(FS)

        Dim tempTrain As clsTrain = Nothing
        Dim _CarClass, _Cripple, _Dinning, _Line, _LineDir, _Note, _OverNightStn, _Package, _Route, _Train, _Type As String
        Dim tempTimeInfoList As ObservableCollection(Of clsTimeInfo) = Nothing

        While Reader.Read()
            If Reader.IsStartElement() Then
                If Reader.Name = "TrainInfo" Then
                    If tempTimeInfoList IsNot Nothing Then
                        tempTrain = New clsTrain(_Year, _Month, _Day, _CarClass, _Cripple, _Dinning, _Line, _LineDir, _Note, _OverNightStn, _Package, _Route, _Train, _Type, tempTimeInfoList)
                        If tempTrain.pTimeInfoList IsNot Nothing AndAlso tempTrain.pTimeInfoList.Count > 0 Then
                            tempTrain.pStartStation = tempTrain.pTimeInfoList.Item(0).pStation
                            tempTrain.pEndStation = tempTrain.pTimeInfoList.Item(tempTrain.pTimeInfoList.Count - 1).pStation
                        End If
                        ResultTrain.Add(tempTrain)
                        tempTrain = Nothing
                        tempTimeInfoList = Nothing
                    End If
                    _CarClass = Reader("CarClass")
                    _Cripple = Reader("Cripple")
                    _Dinning = Reader("Dinning")
                    _Line = Reader("Line")
                    _LineDir = Reader("LineDir")
                    _Note = Reader("Note")
                    _OverNightStn = Reader("OverNightStn")
                    _Package = Reader("Package")
                    _Route = Reader("Route")
                    _Train = Reader("Train")
                    _Type = Reader("Type")
                    tempTimeInfoList = New ObservableCollection(Of clsTimeInfo)
                ElseIf Reader.Name = "TimeInfo" Then
                    tempTimeInfoList.Add(New clsTimeInfo(_Year, _Month, _Day, Reader("ARRTime"), Reader("DEPTime"), Reader("Order"), Reader("Route"), Reader("Station")))
                End If
            End If
        End While

        Reader.Dispose()
        FS.Dispose()


        Return ResultTrain
    End Function

    ''return file path list
    Public Shared Function DeCompression(tZipFilePath As String, tSavedFolder As String) As List(Of String)
        If Not IsolatedStorageFile.GetUserStoreForApplication.FileExists(GlobalVariables.gZipTimeTableFilePath) Then Return Nothing

        Dim _ResultFilePathList As New List(Of String)

        Dim _FileStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
        Dim _InputStreamReader As StreamReader = New StreamReader(_FileStore.OpenFile(tZipFilePath, FileMode.Open, FileAccess.Read))
        Dim _ZipStream As ZipInputStream = New ZipInputStream(_InputStreamReader.BaseStream)
        Dim _Entry As ZipEntry = _ZipStream.GetNextEntry()
        Dim _tempDirectoryName As String = ""
        Dim _tempFileName As String = ""
        Dim _tempPath As String = ""
        Dim _BinaryWriter As BinaryWriter
        Dim _size As Integer = 2048
        Dim _data(_size) As Byte

        If Not IsolatedStorageFile.GetUserStoreForApplication.DirectoryExists(tSavedFolder) Then
            IsolatedStorageFile.GetUserStoreForApplication.CreateDirectory(tSavedFolder)
        End If

        Do While (_Entry IsNot Nothing)
            _tempDirectoryName = Path.GetDirectoryName(_Entry.Name)
            _tempFileName = Path.GetFileName(_Entry.Name)
            _tempPath = _tempDirectoryName + _tempFileName
            ''Create directory 
            If _tempDirectoryName.Length > 0 Then
                Directory.CreateDirectory(_tempDirectoryName)
            End If

            If _tempFileName.Length > 0 Then
                _ResultFilePathList.Add(_tempPath)
                _BinaryWriter = New BinaryWriter(New IsolatedStorageFileStream(tSavedFolder + _tempFileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write, _FileStore))
                Do
                    _size = _ZipStream.Read(_data, 0, _data.Length)
                    If _size > 0 Then
                        _BinaryWriter.Write(_data, 0, _size)
                    End If
                Loop While _size > 0
                _BinaryWriter.Close()
            End If

            _Entry = _ZipStream.GetNextEntry()
        Loop

        Return _ResultFilePathList
    End Function


    Public Shared Sub DownloadFile(tAddress As String, tSavePath As String, ByRef tCurrentRequest As BackgroundTransferRequest, tProgressChanged As EventHandler(Of BackgroundTransferEventArgs), tStatusChanged As EventHandler(Of BackgroundTransferEventArgs))
        If tCurrentRequest IsNot Nothing Then
            BackgroundTransferService.Remove(tCurrentRequest)
        End If
        DeleteFile(tSavePath)
        AddTransferRequest(tAddress, tSavePath, tCurrentRequest, tProgressChanged, tStatusChanged)
    End Sub

    Public Shared Sub CancelDownloadZipFile(ByRef tCurrentRequest As BackgroundTransferRequest)
        If tCurrentRequest IsNot Nothing Then
            If BackgroundTransferService.Find(tCurrentRequest.RequestId) IsNot Nothing Then
                BackgroundTransferService.Remove(tCurrentRequest)
                tCurrentRequest = Nothing
            End If
        End If
    End Sub

    Public Shared Sub DeleteFile(tFilePath As String)
        Dim _IsoStorage As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
        If _IsoStorage.FileExists(tFilePath) Then _IsoStorage.DeleteFile(tFilePath)
    End Sub


    Public Shared Sub AddTransferRequest(tAddress As String, tSavePath As String, ByRef tCurrentRequest As BackgroundTransferRequest, tProgressChanged As EventHandler(Of BackgroundTransferEventArgs), tStatusChanged As EventHandler(Of BackgroundTransferEventArgs))
        Dim _DownloadUri As New Uri(tAddress)
        Dim _SaveLocationUri As New Uri(tSavePath, UriKind.RelativeOrAbsolute)

        tCurrentRequest = New BackgroundTransferRequest(_DownloadUri, _SaveLocationUri)
        tCurrentRequest.TransferPreferences = TransferPreferences.AllowCellularAndBattery
        RemoveTheSameRequest(tAddress)

        BackgroundTransferService.Add(tCurrentRequest)
        AddHandler tCurrentRequest.TransferProgressChanged, tProgressChanged
        AddHandler tCurrentRequest.TransferStatusChanged, tStatusChanged
    End Sub

    Public Shared Sub RemoveTheSameRequest(tAddress As String)
        If BackgroundTransferService.Requests.Count < 1 Then Exit Sub
        For i As Integer = 0 To BackgroundTransferService.Requests.Count - 1
            If BackgroundTransferService.Requests.ElementAt(i).RequestUri.AbsoluteUri = tAddress Then
                Dim _TheSameRequest As BackgroundTransferRequest = BackgroundTransferService.Requests.ElementAt(i)
                BackgroundTransferService.Remove(_TheSameRequest)
            End If
        Next
    End Sub
    Public Sub LoadDefaultIni(tIniFilePath As String)

    End Sub
End Class
