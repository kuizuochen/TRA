
Imports Microsoft.Phone.BackgroundTransfer
Imports Microsoft.Phone.Net.NetworkInformation
Imports System.IO.IsolatedStorage
Imports System.ComponentModel
Imports System.Threading

Partial Public Class pgLoading
    Inherits PhoneApplicationPage

    ''強迫啟動 GlobalVariables
    Private mDummy As Integer = GlobalVariables.gPreloadDays

    Public mCurrentRequest As BackgroundTransferRequest = Nothing

    ' Public Event LoadDataBase(tDayString As String)

    Private mBWorkerDecompression As BackgroundWorker = New BackgroundWorker
    Private mBWorkerReadXML As BackgroundWorker = New BackgroundWorker

    Private mXmlReadyList As List(Of String)
    Private mReadedXmlCnt As Integer = 0

    Private mIsReadyToReadXML As Boolean = False

    ''為了讀取前顯顯示開始頁面
    Private mInitializedDispatcherTimer As System.Windows.Threading.DispatcherTimer


#Region "系統自動程序"
    Public Sub New()
        InitializeComponent()
    End Sub

    Protected Overrides Sub OnNavigatedTo(e As NavigationEventArgs)
        MyBase.OnNavigatedTo(e)
    End Sub

    Protected Overrides Sub OnNavigatedFrom(e As NavigationEventArgs)
        MyBase.OnNavigatedFrom(e)
    End Sub

    Private Sub pgLoading_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        'GoToMainPage_MT("測試測試")
        '  Exit Sub

        ''根據xml folder中是否有xml file 決定 是否是第一次使用 
        ''若為第一次使用 => 複製default xml file 到 指定資料夾  
        Dim _LatestDayInXmlFolder As DateTime = DateTime.Now 
        Dim _LastestDayInAssets As DateTime = DateTime.Now
        Dim _OneDay As TimeSpan = New TimeSpan(24, 0, 0)

        If clsDatabaseIO.GetTheLatestDayInXmlFileFolder(_LatestDayInXmlFolder) = False Then
            clsDatabaseIO.CopyDefaultTimeTableToXMLFolder()
            clsDatabaseIO.GetTheLatestDayInXmlFileFolder(_LatestDayInXmlFolder)
        Else
            ''若Assets中的xml比isostorage 中來的新  => 取代isostorage中的資料
            If clsDatabaseIO.GetTheLatestDayInAssets(_LastestDayInAssets) = True Then
                If _LastestDayInAssets - _LatestDayInXmlFolder > _OneDay Then
                    clsDatabaseIO.CopyDefaultTimeTableToXMLFolder()
                    clsDatabaseIO.GetTheLatestDayInXmlFileFolder(_LatestDayInXmlFolder)
                End If
            End If
        End If


        ''檢查有無網路可以使用 
        ''若無 => 直接進入程序
        If NetworkInterface.GetIsNetworkAvailable() = False Then
            GoToMainPage_MT("讀取今日時刻表")
            Exit Sub
        End If

        ''讀取 設定
        Dim _IsAutoUpdate As Boolean = False
        Dim _IsAvailableDayCntNotEnough As Boolean = False
        Dim tempSettingVM As New vmSetting
        tempSettingVM.LoadFromIsoSetting(App.ViewModel.mIsoSetting)

        ''自動更新是否有被開啟     
        _IsAutoUpdate = tempSettingVM.pIsAutoUpdate

        ''檢查是否有效資料天數不足
        Dim _ValidDayCnt As Integer = _LatestDayInXmlFolder.DayOfYear - DateTime.Now.DayOfYear
        _ValidDayCnt = Math.Max(0, _ValidDayCnt)
        If _ValidDayCnt < tempSettingVM.pDayCntToAutoUpdate Then
            _IsAvailableDayCntNotEnough = True
        End If

        ''有效天數足夠 => 進入主頁
        If _IsAvailableDayCntNotEnough = False Then
            GoToMainPage_MT("讀取今日時刻表")
            Exit Sub
        End If

        ''有效天數不足 
        ''若自動跟新開啟 =>進入自動更新模式
        ''自動更新關閉  => 跳出對話框詢問是否進行更新 
        If _IsAutoUpdate = True Then
            If _IsAvailableDayCntNotEnough = True Then
                UpdateProcedure()
            End If
        Else
            popDoUpdate.IsOpen = True
            tbPopMsg.Text = "有效時刻表尚存" + _ValidDayCnt.ToString + "日"
        End If
    End Sub

    Private Sub UpdateProcedure()

        ''自動更新 程序
        ''1.刪除 暫存zip file
        ''2.下載 zip file (下載執行續) 到 暫存區
        ''2.5 
        ''3.確定下載完成後 刪除 xml folder
        ''4.解壓縮 zip file 
        ''5.進入主頁面

        DeleteZipTempFile()
        DownloadZipFile_Start()
    End Sub
    Private Sub GoToMainPage_MT(tStatusString As String)
        If Not App.ViewModel.IsDataLoaded Then
            tbUpdateStatus.Text = tStatusString
            AddHandler App.ViewModel.LoadingComplete, AddressOf GoToMainPage_MTCompleted
            App.ViewModel.LoadData_MT()
        Else
            GoToMainPage_MTCompleted()
        End If
    End Sub
    Private Sub GoToMainPage_MTCompleted()
        NavigationService.Navigate(New Uri("/MainPage.xaml", UriKind.RelativeOrAbsolute))
    End Sub

    Private Sub GoToMainPage_SingleThread(tStatusString As String)
        If Not App.ViewModel.IsDataLoaded Then
            tbUpdateStatus.Text = tStatusString
            App.ViewModel.LoadData_UIThread()
        End If

        NavigationService.Navigate(New Uri("/MainPage.xaml", UriKind.RelativeOrAbsolute))
    End Sub

    Private Sub btnUpdateOK_Click(sender As Object, e As RoutedEventArgs)
        popDoUpdate.IsOpen = False
        UpdateProcedure()
    End Sub

    Private Sub btnUpdateCancel_Click(sender As Object, e As RoutedEventArgs)
        popDoUpdate.IsOpen = False
        GoToMainPage_MT("讀取時刻表")
    End Sub
#End Region


#Region "讀取時刻表"

    Private Function IsNeedRefreshDB(ByVal tDays As Integer) As Boolean
        Dim _AllXmlFilePath As List(Of String) = clsDatabaseIO.GetAllXmlFilePath(GlobalVariables.gXmlFileFolderName)
        If _AllXmlFilePath Is Nothing OrElse _AllXmlFilePath.Count = 0 Then Return True

        Dim _LatestDayInStorage As DateTime = clsDatabaseIO.GetTheLatestDate(_AllXmlFilePath)
        Dim _PreloadDay As DateTime = DateTime.Now.AddDays(tDays)
        If _LatestDayInStorage > _PreloadDay Then
            Return False
        Else
            Return True
        End If
    End Function
    ''為避免系統中無資料可讀取 
    Private Sub ReadDB_Start()
        clsDatabaseIO.LoadXmlFromIsoStorage_SingleThread()
        'Dim _FileStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
        'If _FileStore.FileExists(GlobalVariables.gZipTimeTableFilePath) Then
        '    mBWorkerReadXML = New BackgroundWorker
        '    mBWorkerReadXML.WorkerReportsProgress = True
        '    mBWorkerReadXML.WorkerSupportsCancellation = True
        '    AddHandler mBWorkerReadXML.ProgressChanged, AddressOf ReadDB_PbUpdate
        '    AddHandler mBWorkerReadXML.RunWorkerCompleted, AddressOf ReadDB_Completed

        '    mXmlReadyList = GetXmlFilePathToRead()
        '    mReadedXmlCnt = 0
        '    If mXmlReadyList.Count <= 0 Then Exit Sub
        '    For i As Integer = 0 To mXmlReadyList.Count - 1
        '        AddHandler mBWorkerReadXML.DoWork, AddressOf ReadDB_Do
        '    Next
        '    tbStatus.Text = "讀取時刻表 0/" + mXmlReadyList.Count.ToString
        '    mBWorkerReadXML.RunWorkerAsync()
        'End If
    End Sub
    Private Sub ReadDB_Do(sender As Object, e As ComponentModel.DoWorkEventArgs)
        If CType(sender, BackgroundWorker).CancellationPending = False Then
            clsDatabaseIO.AddTimeTableFormIsolationStorageToDB(mXmlReadyList.Item(mReadedXmlCnt))
            mReadedXmlCnt += 1
            mBWorkerReadXML.ReportProgress(mReadedXmlCnt)
        Else
            e.Cancel = True
        End If
    End Sub
    Private Sub ReadDB_PbUpdate(sender As Object, e As ComponentModel.ProgressChangedEventArgs)
        tbStatus.Text = "讀取時刻表 " + e.ProgressPercentage.ToString + "/" + mXmlReadyList.Count.ToString
    End Sub
    Private Sub ReadDB_Completed(sender As Object, e As ComponentModel.AsyncCompletedEventArgs)
        mReadedXmlCnt = 0
        If e.Cancelled = True Then
            tbStatus.Text = "讀取中斷"
        Else
            tbStatus.Text = "時刻表讀取完成"

            NavigationService.Navigate(New Uri("/MainPage.xaml", UriKind.Relative))
        End If
    End Sub

    'Private Function GetXmlFilePathToRead() As List(Of String)
    '    Dim _Result As New List(Of String)

    '    Dim _FilePaths As List(Of String) = clsDatabaseIO.GetAllXmlFilePath(GlobalVariables.gXmlFileFolderName)
    '    Dim _Yesterday As DateTime = DateTime.Now.AddDays(-1)
    '    Dim _PreloadDay As DateTime = DateTime.Now.AddDays(GlobalVariables.gPreloadDays)
    '    Dim _FileDate As DateTime
    '    Dim _FileName As String

    '    For i As Integer = 0 To _FilePaths.Count - 1
    '        If _FilePaths.Item(i).Contains("xml") Then
    '            _FileName = _FilePaths.Item(i).Substring(_FilePaths.Item(i).Length - 12, 12)
    '            _FileDate = New DateTime(_FileName.Substring(0, 4), _FileName.Substring(4, 2), _FileName.Substring(6, 2))
    '            If _FileDate > _Yesterday And _FileDate < _PreloadDay Then
    '                _Result.Add(_FilePaths.Item(i))
    '            End If
    '        End If
    '    Next

    '    Return _Result
    'End Function


#End Region

#Region "下載壓縮檔"
    Private Sub DownloadZipFile_Start()
        clsDatabaseIO.DownloadFile(GlobalVariables.gZipTimeTableAddress, GlobalVariables.gZipTimeTableFileTempPath, mCurrentRequest, AddressOf DownloadZipFile_PbUpdate, AddressOf DownloadZipFile_Completed)
    End Sub
    Public Sub DownloadZipFile_PbUpdate(sender As Object, e As BackgroundTransferEventArgs)
        If mCurrentRequest.TotalBytesToReceive.ToString = -1 Then
            clsDatabaseIO.CancelDownloadZipFile(mCurrentRequest)
            Exit Sub
        End If
        tbUpdateStatus.Text = "正在下載檔案 " + "(" + mCurrentRequest.BytesReceived.ToString + "/" + mCurrentRequest.TotalBytesToReceive.ToString + ")"
    End Sub
    Public Sub DownloadZipFile_Completed(sender As Object, e As BackgroundTransferEventArgs)
        If e.Request.TotalBytesToReceive < 0 Then
            GoToMainPage_MT("無法連接網路")
            Exit Sub
        End If
        If e.Request.BytesReceived < e.Request.TotalBytesToReceive Or Not IsolatedStorageFile.GetUserStoreForApplication.FileExists(GlobalVariables.gZipTimeTableFileTempPath) Then
            GoToMainPage_MT("下載中斷")
            Exit Sub
        End If

        ''將下載完成的zip file 移動至 正確位置
        If MoveZipFile() Then
            DeleteZipTempFile()
            ''啟動解壓縮
            DeCompressionBgWorker_Start()
        End If


    End Sub

    Private Sub DownloadZipFile_Start_ForTest()
        clsDatabaseIO.DownloadFile(GlobalVariables.gZipTimeTableAddress, GlobalVariables.gZipTimeTableFileTempPath, mCurrentRequest, AddressOf DownloadZipFile_PbUpdate, AddressOf DownloadZipFile_Completed_ForTest)
    End Sub

    Public Sub DownloadZipFile_Completed_ForTest(sender As Object, e As BackgroundTransferEventArgs)
        If e.Request.TotalBytesToReceive < 0 Then
            tbStatus.Text = "無網路服務"

            Exit Sub
        End If
        If e.Request.BytesReceived < e.Request.TotalBytesToReceive Or Not IsolatedStorageFile.GetUserStoreForApplication.FileExists(GlobalVariables.gZipTimeTableFilePath) Then
            tbStatus.Text = "下載中斷"
            Exit Sub
        End If
    End Sub
#End Region

#Region "檔案管理"
    Private Sub ShowDirectory()
        Dim _FilePath As List(Of String) = clsDatabaseIO.GetAllXmlFilePath(GlobalVariables.gXmlFileFolderName)
        Dim _ZipFileCnt As Integer = 0
        Dim _XmlFileCnt As Integer = 0
        Dim _FileStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
        Dim _aa As String() = _FileStore.GetFileNames("shared/transfers/*")
        If _FileStore.FileExists(GlobalVariables.gZipTimeTableFilePath) Then
            tbDebug3.Text = "Zip file Exist"
        Else
            tbDebug3.Text = "Zip file not Exist"
        End If

        If _FileStore.FileExists(GlobalVariables.gZipTimeTableFileTempPath) Then
            tbDebug4.Text = "temp Zip file Exist"
        Else
            tbDebug4.Text = "temp Zip file not Exist"
        End If
        Try
            For i As Integer = 0 To _FilePath.Count - 1
                If _FilePath.Item(i).Contains("zip") Then
                    _ZipFileCnt += 1
                ElseIf _FilePath.Item(i).Contains("xml") Then
                    _XmlFileCnt += 1
                End If
            Next
        Catch ex As Exception

        End Try

        tbDebug.Text = _ZipFileCnt.ToString + " Zip files" + ";"
        tbDebug.Text += _XmlFileCnt.ToString + " Xml files"
    End Sub
    Private Sub DeleteZipTempFile()
        Dim _FileStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
        If _FileStore.FileExists(GlobalVariables.gZipTimeTableFileTempPath) Then
            _FileStore.DeleteFile(GlobalVariables.gZipTimeTableFileTempPath)
        End If
    End Sub
    Private Function MoveZipFile() As Boolean
        Dim _FileStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
        ''若是 暫存壓縮檔不存在
        If _FileStore.FileExists(GlobalVariables.gZipTimeTableFileTempPath) = False Then
            Return False
        End If
        ''刪除舊的壓縮檔
        If _FileStore.FileExists(GlobalVariables.gZipTimeTableFilePath) Then
            _FileStore.DeleteFile(GlobalVariables.gZipTimeTableFilePath)
        End If
        ''複製暫存壓縮檔至正確存放位置 
        _FileStore.CopyFile(GlobalVariables.gZipTimeTableFileTempPath, GlobalVariables.gZipTimeTableFilePath)
        Return True
    End Function
#End Region

#Region "解壓縮"
    Private Sub DeCompressionBgWorker_Start()
        btnCancel.IsEnabled = False
        tbUpdateStatus.Text = "正在進行解壓縮"
        mBWorkerDecompression = New BackgroundWorker
        AddHandler mBWorkerDecompression.DoWork, AddressOf DeCompressionBgWorker_Do
        AddHandler mBWorkerDecompression.RunWorkerCompleted, AddressOf DeCompressionBgWorker_Completed
        mBWorkerDecompression.WorkerSupportsCancellation = False
        mBWorkerDecompression.WorkerReportsProgress = False
        mBWorkerDecompression.RunWorkerAsync()
    End Sub
    Private Sub DeCompressionBgWorker_Do()
        GlobalVariables.gTxtTimeTableFilePaths = clsDatabaseIO.DeCompression(GlobalVariables.gZipTimeTableFilePath, GlobalVariables.gXmlFileFolderName)
    End Sub
    Private Sub DeCompressionBgWorker_Completed(sender As Object, e As ComponentModel.AsyncCompletedEventArgs)
        btnCancel.IsEnabled = True
        GoToMainPage_MT("完成解壓縮")
    End Sub

    Private Sub DeCompressionBgWorker_Start_ForTest()

        btnCancel.IsEnabled = False
        tbStatus.Text = "解壓縮"
        mBWorkerDecompression = New BackgroundWorker
        AddHandler mBWorkerDecompression.DoWork, AddressOf DeCompressionBgWorker_Do
        AddHandler mBWorkerDecompression.RunWorkerCompleted, AddressOf DeCompressionBgWorker_Completed_ForTest
        mBWorkerDecompression.WorkerSupportsCancellation = False
        mBWorkerDecompression.WorkerReportsProgress = False
        mBWorkerDecompression.RunWorkerAsync()
    End Sub
    Private Sub DeCompressionBgWorker_Completed_ForTest(sender As Object, e As ComponentModel.AsyncCompletedEventArgs)
        btnCancel.IsEnabled = True
        tbStatus.Text = "火車時刻表下載完成"
    End Sub
#End Region

#Region "測試按鈕"
    Private Sub btnReadDB_Click(sender As Object, e As RoutedEventArgs)
        ReadDB_Start()
    End Sub
    Private Sub btnDeCompress_Click(sender As Object, e As RoutedEventArgs)
        mBWorkerDecompression = New BackgroundWorker
        AddHandler mBWorkerDecompression.DoWork, AddressOf DeCompressionBgWorker_Do
        AddHandler mBWorkerDecompression.RunWorkerCompleted, AddressOf DeCompressionBgWorker_Completed
        mBWorkerDecompression.WorkerSupportsCancellation = False
        mBWorkerDecompression.WorkerReportsProgress = False
        mBWorkerDecompression.RunWorkerAsync()
    End Sub


    Private Sub btnDownloadDB_Click(sender As Object, e As RoutedEventArgs)
        DownloadZipFile_Start()
    End Sub
    Private Sub btnClearStorage_Click(sender As Object, e As RoutedEventArgs)
        clsDatabaseIO.ClearStorage()
    End Sub
    Private Sub btnRefreshDB_Click(sender As Object, e As RoutedEventArgs)
        ReadDB_Start()
    End Sub
    Private Sub btShowDirectory_Click(sender As Object, e As RoutedEventArgs)
        ShowDirectory()
    End Sub
#End Region


    Private Sub btnCancel_Click(sender As Object, e As RoutedEventArgs)
        If mBWorkerReadXML IsNot Nothing AndAlso mBWorkerReadXML.IsBusy Then
            mBWorkerReadXML.CancelAsync()
        End If
        clsDatabaseIO.CancelDownloadZipFile(mCurrentRequest)
    End Sub

    Private Sub btnGetDefaultXML_Click(sender As Object, e As RoutedEventArgs)
        clsDatabaseIO.CopyDefaultTimeTableToXMLFolder()
    End Sub


    Private Sub btnLoadDBInXmlFolder_Click(sender As Object, e As RoutedEventArgs)
        clsDatabaseIO.LoadXmlFromIsoStorage_SingleThread()
    End Sub


End Class



