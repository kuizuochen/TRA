Imports System.Xml

Imports System.ComponentModel
Imports System.Collections.ObjectModel
Imports System.Runtime.Serialization
Imports System.IO
Imports System.Xml.Serialization

Public Enum TimeInfo
    ARRTime
    DEPTime
    Order
    Route
    Station
End Enum

Public Enum TrainInfo
    CarClass
    Cripple
    Dinning
    Line
    LineDir
    Note
    OverNightStn
    Package
    Route
    Train
    Type
End Enum

Public Enum CarClass As Integer
    Tze_ChiangLimitedExpress1    ''自強	  
    Tze_ChiangLimitedExpress2    ''自強	  
    Tze_ChiangLimitedExpressTL   ''自強    (太魯閣號)  
    Tze_ChiangLimitedExpressNew  ''新自強
    Chu_KuangExpress             ''莒光  
    Fu_HsingSemiExpress          ''復興  
    ElectricMultipleUnit         ''電車	  
    LocalTrain                   ''區間車
    FastLocalTrain               ''區間快
    Ordinarytrain                ''普快車
    DiselRailCar                 ''柴快車
    na                           ''柴油車
End Enum

Public Enum LineType As Integer
    No
    Mountain
    Sea
End Enum


Public Class clsTrainsInOneDay
    Public mTrainList As List(Of clsTrain)
    Public mDate As DateTime
    Public mFilePath As String
    Public mIsLoaded As Boolean = False
    Public mIsFromIsoStorage As Boolean = True
     
    Public Property DisplayName As String = ""
    Public Property DisplayNameShort As String = ""
    Public Property pHLColor As String = "White"
    Public Sub New(tDate As DateTime, tFilePath As String, tToLoad As Boolean, tIsFromIsoStorage As Boolean)
        mDate = tDate
        mFilePath = tFilePath
        mIsFromIsoStorage = tIsFromIsoStorage

        If tToLoad Then
            LoadTimeTable()
        Else
            mIsLoaded = False
        End If

        DisplayName = tDate.Month.ToString + "月" + tDate.Day.ToString + "日"
        DisplayNameShort = tDate.Month.ToString + "月" + tDate.Day.ToString + "日"
        Select Case tDate.DayOfWeek
            Case DayOfWeek.Monday
                DisplayName += "(一)"
            Case DayOfWeek.Tuesday
                DisplayName += "(二)"
            Case DayOfWeek.Wednesday
                DisplayName += "(三)"
            Case DayOfWeek.Thursday
                DisplayName += "(四)"
            Case DayOfWeek.Friday
                DisplayName += "(五)"
            Case DayOfWeek.Saturday
                DisplayName += "(六)"
            Case DayOfWeek.Sunday
                DisplayName += "(日)"
        End Select
        If tDate.Day = DateTime.Now.Day And tDate.Month = DateTime.Now.Month Then
            DisplayName = DisplayName + " <= 今天"
        End If

    End Sub

    Public Sub LoadTimeTable() 
            If mIsFromIsoStorage Then
                mTrainList = clsDatabaseIO.LoadTrainsFromIsolatedStorage(mFilePath)
            Else
                mTrainList = clsDatabaseIO.LoadAllTrains(mFilePath)
            End If
            mIsLoaded = True 
    End Sub

    ' Public Sub UnLoadTimeTable()
    Public Shared Function GetTrainsByDate(tTimeTableList As ObservableCollection(Of clsTrainsInOneDay), tDate As DateTime) As clsTrainsInOneDay
        For i As Integer = 0 To tTimeTableList.Count - 1
            If tTimeTableList.Item(i).mDate.Month = tDate.Month And tTimeTableList.Item(i).mDate.Day = tDate.Day Then
                Return tTimeTableList.Item(i)
            End If
        Next
        Return Nothing
    End Function

End Class

 
Public Class clsTrain
    Implements INotifyPropertyChanged

    Public Property pTimeInfoList As ObservableCollection(Of clsTimeInfo)

    Public Property pStartStation As clsStation
    Public Property pEndStation As clsStation

    Public mCarClass As String

    Public mLine As String
    Public mLineDir As String
    Public mNote As String
    Public mRoute As String
    Public mType As String

    Public mCripple As String
    Public mDinning As String
    Public mOverNightStn As String
    Public mPackage As String

    Public ReadOnly Property pEveryDayTrainVisibility As Visibility
        Get
            If mNote.Substring(0, 4) = "每日行駛" Then
                Return Visibility.Visible
            Else
                Return Visibility.Collapsed
            End If
        End Get
    End Property
    Public ReadOnly Property pNotDailyTrainNoteVisibility As Visibility
        Get
            If mNote.Substring(0, 4) = "每日行駛" Then
                Return Visibility.Collapsed
            Else
                Return Visibility.Visible
            End If
        End Get
    End Property 
    Public ReadOnly Property pNotDailyTrainNoteString As String
        Get
            Return "*" + mNote
        End Get
    End Property
    Public ReadOnly Property pIrregularTrainVisibility As Visibility
        Get
            If mType = "0" Then
                Return Visibility.Collapsed
            Else
                Return Visibility.Visible
            End If
        End Get
    End Property
    Public ReadOnly Property pIrregularTrainString As String
        Get
            Select Case mType
                Case "0"
                    Return "常態列車"
                Case "1"
                    Return "臨時"
                Case "2"
                    Return "團體列車"
                Case "3"
                    Return "節日加開車"
            End Select

            Return ""
        End Get
    End Property
    Public ReadOnly Property pOverNightVisibility As Visibility
        Get
            If mOverNightStn = "0" Then
                Return Visibility.Collapsed
            Else
                Return Visibility.Visible
            End If
        End Get
    End Property
    Public ReadOnly Property pMilkVisibility As Visibility
        Get
            Select Case GetCarClass(mCarClass)
                Case CarClass.Tze_ChiangLimitedExpress1, CarClass.Tze_ChiangLimitedExpress2, CarClass.Tze_ChiangLimitedExpressNew, CarClass.Tze_ChiangLimitedExpressTL
                    Return Visibility.Visible
                Case Else
                    Return Visibility.Collapsed
            End Select
        End Get
    End Property
    Public ReadOnly Property pPackageVisibility As Visibility
        Get
            If mPackage = "Y" Then
                Return Visibility.Visible
            Else
                Return Visibility.Collapsed
            End If
        End Get
    End Property
    Public ReadOnly Property pDinningVisibility As Visibility
        Get
            If mDinning = "Y" Then
                Return Visibility.Visible
            Else
                Return Visibility.Collapsed
            End If
        End Get
    End Property
    Public ReadOnly Property pCrippleVisibility As Visibility
        Get
            If mCripple = "Y" Then
                Return Visibility.Visible
            Else
                Return Visibility.Collapsed
            End If
        End Get
    End Property
    Public ReadOnly Property pBikeVisibility As Visibility
        Get
            If mCarClass = "1101" Then
                Return Visibility.Visible
            Else
                Return Visibility.Collapsed
            End If
        End Get
    End Property
    Public Property pTrainID As String
    Public Property mYear As String
    Public Property mMonth As String
    Public Property mDate As String
    Public Property pTrainColor1 As String
    Public Property pTrainColor2 As String
    Public Property pTrainColor3 As String 
    Public Property pTrainClassColor As String
    Public Property pSpTag As String
    Public Property pSpTag_Full As String
    Public Property pSpTagBgColor As String
    ''  Public Property mTrainTypeID As String

    Public mOnTimeFlag As Visibility = Visibility.Collapsed
    Public Property pOnTimeFlag As Visibility
        Get
            Return mOnTimeFlag
        End Get
        Set(value As Visibility)
            mOnTimeFlag = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("pOnTimeFlag"))
        End Set
    End Property
    Public mDelayFlag As Visibility = Visibility.Collapsed
    Public Property pDelayFlag As Visibility
        Get
            Return mDelayFlag
        End Get
        Set(value As Visibility)
            mDelayFlag = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("pDelayFlag"))
        End Set
    End Property

    Public mDelaySpan As String = "+0:00"
    Public Property pDelaySpan As String 
        Get
            Return mDelaySpan
        End Get
        Set(value As String)
            mDelaySpan = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("pDelaySpan"))
        End Set
    End Property

    Public mVisibility As Visibility = Visibility.Visible
    Public Property pVisibility As Visibility
        Get
            Return mVisibility
        End Get
        Set(value As Visibility)
            mVisibility = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("pVisibility"))
        End Set
    End Property
    Public ReadOnly Property pTrainClassCht As String
        Get
            Return GetTrainClassCht(GetCarClass(mCarClass))
        End Get
    End Property
    Public ReadOnly Property pTrainClassFullNameCht As String
        Get
            Return GetTrainClassFullNameCht(GetCarClass(mCarClass))
        End Get
    End Property
    Public ReadOnly Property pTrainClassDescriptionCht As String
        Get
            Return GetTrainClassDescriptionCht(GetCarClass(mCarClass))
        End Get
    End Property

    Public mTimeDisplayMode1Visibility As Visibility = Visibility.Visible
    Public Property pTimeDisplayMode1Visibility As Visibility 
        Get
            Return mTimeDisplayMode1Visibility
        End Get
        Set(value As Visibility)
            mTimeDisplayMode1Visibility = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("pTimeDisplayMode1Visibility"))
        End Set
    End Property
    Public mTimeDisplayMode2Visibility As Visibility = Visibility.Collapsed
    Public Property pTimeDisplayMode2Visibility As Visibility
        Get
            Return mTimeDisplayMode2Visibility
        End Get
        Set(value As Visibility)
            mTimeDisplayMode2Visibility = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("pTimeDisplayMode2Visibility"))
        End Set
    End Property
    Public mStatusString As String = "-"
    Public Event UpdateCompleted()

    Public mLineLoc As List(Of LineLocation)

    Dim request As HttpWebRequest
    Dim _stream As Stream

    Public Sub New(tYear As String, tMonth As String, tDate As String, _
               CarClass As String, Cripple As String, Dinning As String, Line As String, LineDir As String, Note As String, OverNightStn As String, Package As String, Route As String, Train As String, Type As String, _
               tTimeInfoList As ObservableCollection(Of clsTimeInfo))

        mYear = tYear
        mMonth = tMonth
        mDate = tDate
        mCarClass = CarClass
        mCripple = Cripple
        mDinning = Dinning
        mLine = Line
        mLineDir = LineDir
        mNote = Note
        mOverNightStn = OverNightStn
        mPackage = Package
        mRoute = Route
        pTrainID = Train
        mType = Type

        ''根據山/海 線  太魯閣 新自強(普悠瑪) 設定 特殊標記
        If mLine = 0 Then
            If GetCarClass(mCarClass) = TRA_03.CarClass.Tze_ChiangLimitedExpressNew Then
                pSpTag = "新"
                pSpTag_Full = "無站票"
                pSpTagBgColor = "Pink"
            ElseIf GetCarClass(mCarClass) = TRA_03.CarClass.Tze_ChiangLimitedExpressTL Then
                pSpTag = "太"
                pSpTag_Full = "無站票"
                pSpTagBgColor = "MediumSeaGreen"
            Else
                pSpTag = ""
                pSpTag_Full = ""
                pSpTagBgColor = "Transparent"
            End If
        ElseIf mLine = 1 Then
            pSpTag = "山"
            pSpTag_Full = "山線"
            pSpTagBgColor = "#07970D"
        ElseIf mLine = 2 Then
            pSpTag = "海"
            pSpTag_Full = "海線"
            pSpTagBgColor = "Blue"
        End If

        ''根據 自強 莒光 區間 設定顏色
        GetCarColor(GetCarClass(mCarClass), pTrainColor1, pTrainColor2, pTrainColor3, pTrainClassColor)

        pTimeInfoList = tTimeInfoList

        pStartStation = pTimeInfoList.Item(0).pStation
        pEndStation = pTimeInfoList.Item(pTimeInfoList.Count - 1).pStation
        SetTrainLineLoc()
    End Sub

    Public Sub SetClosetSt()
        If pTimeInfoList Is Nothing OrElse pTimeInfoList.Count = 0 Then Exit Sub

        Dim _tempList As New ObservableCollection(Of clsTimeInfo)
        Dim _Current As DateTime = New DateTime(mYear, mMonth, mDate, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)
        Dim _ZeroTime As TimeSpan = New TimeSpan(0)
        Dim _DiffTime1 As TimeSpan
        Dim _DiffTime2 As TimeSpan
        Dim _Token As Boolean = False

        For i As Integer = 0 To pTimeInfoList.Count - 2
            _DiffTime1 = pTimeInfoList.Item(i).mDEPTime - _Current
            _DiffTime2 = pTimeInfoList.Item(i + 1).mDEPTime - _Current
            If _DiffTime1 < _ZeroTime And _DiffTime2 > _ZeroTime Then
                pTimeInfoList.Item(i).pIsHighLight = True
            Else
                pTimeInfoList.Item(i).pIsHighLight = False
            End If
            _tempList.Add(pTimeInfoList.Item(i))
        Next

        _tempList.Add(pTimeInfoList.Item(pTimeInfoList.Count - 1))

        pTimeInfoList = _tempList

        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("pTimeInfoList"))
    End Sub
   
    Private Sub SetTrainLineLoc()  
        mLineLoc = New List(Of LineLocation) 

        For i As Integer = 0 To pTimeInfoList.Count - 1
            If pTimeInfoList.Item(i).pStation.mCrossLineList.Count = 1 AndAlso ExistInList(mLineLoc, pTimeInfoList.Item(i).pStation.mCrossLineList.Item(0)) = False Then
                mLineLoc.Add(pTimeInfoList.Item(i).pStation.mCrossLineList.Item(0))
            End If
        Next 
    End Sub

    Private Function ExistInList(ByRef tOriginalList As List(Of LineLocation), tLineLoc As LineLocation) As Boolean
        For i As Integer = 0 To tOriginalList.Count - 1
            If tLineLoc = tOriginalList.Item(i) Then Return True
        Next
        Return False
    End Function

    Public Sub UpdateStatusOnline()
        Dim _AddressToSearch As String = "http://twtraffic.tra.gov.tw/twrail/mobile/TrainDetail.aspx?searchdate="
        _AddressToSearch += mYear + "/" + mMonth + "/" + mDate + "&traincode=" + pTrainID

        Dim _WebClient As New WebClient
        AddHandler _WebClient.DownloadStringCompleted, AddressOf StatusUpdate_Completed
        _WebClient.DownloadStringAsync(New Uri(_AddressToSearch))

    End Sub
    Private Sub StatusUpdate_Completed(sender As Object, e As DownloadStringCompletedEventArgs)
        Dim _IsGetStatus As Boolean = False
        mStatusString = e.Result
        mStatusString = mStatusString.Replace("<script>", "$")
        mStatusString = mStatusString.Replace(";</script>", "$")

        System.Diagnostics.Debug.WriteLine(Me.pTrainID)
        System.Diagnostics.Debug.WriteLine(e.Result)

        Dim tempString() As String = mStatusString.Split("$")

        For i As Integer = 0 To tempString.Length - 1
            If tempString(i).ToLower.Contains("traindelaytime=") Then
                mStatusString = tempString(i).Substring(15)
                _IsGetStatus = True
            End If
        Next


        If _IsGetStatus = False Then Exit Sub
        If mStatusString = "" Then Exit Sub

        ''改變顯示狀態 
        If mStatusString = "0" Then
            pTimeDisplayMode1Visibility = Visibility.Visible
            pTimeDisplayMode2Visibility = Visibility.Collapsed
            pDelayFlag = Visibility.Collapsed
            pOnTimeFlag = Visibility.Visible
        Else
            pTimeDisplayMode1Visibility = Visibility.Collapsed
            pTimeDisplayMode2Visibility = Visibility.Visible
            pDelayFlag = Visibility.Visible
            pOnTimeFlag = Visibility.Collapsed

            pDelaySpan = "+" + mStatusString
        End If

        SetClosetSt()

        RaiseEvent UpdateCompleted()
    End Sub

    Public Shared Function GetCarClass(number As String) As CarClass
        Select Case number
            Case "1100"
                Return CType(0, CarClass)
            Case "1101"
                Return CType(1, CarClass)
            Case "1102"
                Return CType(2, CarClass)
            Case "1107"
                Return CType(3, CarClass)
            Case "1110"
                Return CType(4, CarClass)
            Case "1120"
                Return CType(5, CarClass)
            Case "1130"
                Return CType(6, CarClass)
            Case "1131"
                Return CType(7, CarClass)
            Case "1132"
                Return CType(8, CarClass)
            Case "1140"
                Return CType(9, CarClass)
            Case "1141"
                Return CType(10, CarClass)
            Case "1150"
                Return CType(11, CarClass)
        End Select

        Return Nothing
    End Function

    Public Shared Sub GetCarColor(tCarClass As CarClass, ByRef tTrainColor1 As String, ByRef tTrainColor2 As String, ByRef tTrainColor3 As String, ByRef tTrainClassColor As String)
        Select tCarClass
            Case CarClass.Tze_ChiangLimitedExpress1
                tTrainColor1 = "Silver"
                tTrainColor2 = "Orange"
                tTrainColor3 = "Gold"
                tTrainClassColor = "Red"  
            Case CarClass.Tze_ChiangLimitedExpress2
                tTrainColor1 = "Silver"
                tTrainColor2 = "Orange"
                tTrainColor3 = "Gold"
                tTrainClassColor = "Red"

            Case CarClass.Tze_ChiangLimitedExpressTL
                tTrainColor1 = "White"
                tTrainColor2 = "Orange"
                tTrainColor3 = "Gold"
                tTrainClassColor = "Green"

            Case CarClass.Tze_ChiangLimitedExpressNew
                tTrainColor1 = "White"
                tTrainColor2 = "Red"
                tTrainColor3 = "Gold"
                tTrainClassColor = "Pink"

            Case CarClass.Chu_KuangExpress
                tTrainColor1 = "Orange"
                tTrainColor2 = "Orange"
                tTrainColor3 = "Orange"
                tTrainClassColor = "Orange"

            Case CarClass.Fu_HsingSemiExpress
                tTrainColor1 = "Blue"
                tTrainColor2 = "White"
                tTrainColor3 = "Blue"
                tTrainClassColor = "RoyalBlue"

            Case Else
                tTrainColor1 = "Blue"
                tTrainColor2 = "Blue"
                tTrainColor3 = "Blue"
                tTrainClassColor = "RoyalBlue"
        End Select
    End Sub

    Public Shared Function GetLineType(number As String) As LineType
        Try
            Return CType(CInt(number), LineType)
        Catch ex As Exception
        End Try
        Return Nothing
    End Function

    Public Shared Function GetLineDirCht(number As String) As String
        If number = "0" Then
            Return "順行"
        ElseIf number = "1" Then
            Return "逆行"
        End If
        Return Nothing
    End Function

    Public Shared Function GetTrainTypeCht(number As String) As String
        If number = "0" Then
            Return "常態列車"
        ElseIf number = "1" Then
            Return "臨時"
        ElseIf number = "2" Then
            Return "團體列車"
        ElseIf number = "3" Then
            Return "春節加開車"
        End If
        Return Nothing
    End Function

    Public Shared Function GetTimeInfoByStation(tTrain As clsTrain, tSt As clsStation)
        If tTrain.pTimeInfoList Is Nothing OrElse tTrain.pTimeInfoList.Count = 0 Then Return Nothing

        For i As Integer = 0 To tTrain.pTimeInfoList.Count - 1
            If tTrain.pTimeInfoList.Item(i).pStation.mID = tSt.mID Then Return tTrain.pTimeInfoList.Item(i)
        Next

        Return Nothing
    End Function
    Public Shared Function GetTrainsByFromEnd(tTrainInOneDay As clsTrainsInOneDay, FromSt As clsStation, EndSt As clsStation) As List(Of clsTrain)
        Dim result As New List(Of clsTrain)

        For i As Integer = 0 To tTrainInOneDay.mTrainList.Count - 1
            With tTrainInOneDay.mTrainList.Item(i)
                For j As Integer = 0 To .pTimeInfoList.Count - 1
                    If .pTimeInfoList.Item(j).pStation.mID = FromSt.mID Then
                        For k As Integer = j + 1 To .pTimeInfoList.Count - 1
                            If .pTimeInfoList.Item(k).pStation.mID = EndSt.mID Then
                                result.Add(tTrainInOneDay.mTrainList.Item(i))
                                GoTo NextTrain
                            End If
                        Next
                        GoTo NextTrain
                    End If
                Next
            End With
NextTrain:
        Next

        Return result
    End Function

    Public Shared Function GetTrainsByFromSt(tTrainInOneDay As clsTrainsInOneDay, FromSt As clsStation) As List(Of clsTrain)
        Dim result As New List(Of clsTrain)

        For i As Integer = 0 To tTrainInOneDay.mTrainList.Count - 1
            With tTrainInOneDay.mTrainList.Item(i)
                For j As Integer = 0 To .pTimeInfoList.Count - 2
                    If .pTimeInfoList.Item(j).pStation.mID = FromSt.mID Then
                        result.Add(tTrainInOneDay.mTrainList.Item(i))
                        GoTo NextTrain
                    End If
                Next
            End With
NextTrain:
        Next

        Return result
    End Function

    Public Shared Function GetTrainTimeInfosByFromSt(tTrainInOneDay As clsTrainsInOneDay, FromSt As clsStation) As ObservableCollection(Of clsTrainTimeInfo)
        Dim result As New ObservableCollection(Of clsTrainTimeInfo)
        Dim tempList As New List(Of clsTrainTimeInfo)

        For i As Integer = 0 To tTrainInOneDay.mTrainList.Count - 1
            If tTrainInOneDay.mTrainList.Item(i).pTimeInfoList.Count < 1 Then
                GoTo NextTrain
            End If
            With tTrainInOneDay.mTrainList.Item(i)
                For j As Integer = 0 To .pTimeInfoList.Count - 2
                    If .pTimeInfoList.Item(j).pStation.mID = FromSt.mID Then
                        tempList.Add(New clsTrainTimeInfo(tTrainInOneDay.mTrainList.Item(i), .pTimeInfoList.Item(j)))
                        GoTo NextTrain
                    End If
                Next
            End With
NextTrain:
        Next

        tempList.Sort(New clsTrainTimeInfoComparer)

        For i As Integer = 0 To tempList.Count - 1
            result.Add(tempList.Item(i))
        Next
        Return result
    End Function

    Public Shared Function GetTrainTimeInfosByStartEndSt(tTrainInOneDay As clsTrainsInOneDay, FromSt As clsStation, EndSt As clsStation) As ObservableCollection(Of clsTrainTimeInfo)

        Dim result As New ObservableCollection(Of clsTrainTimeInfo)
        Dim tempList As New List(Of clsTrainTimeInfo)

        For i As Integer = 0 To tTrainInOneDay.mTrainList.Count - 1
            If tTrainInOneDay.mTrainList.Item(i).pTimeInfoList.Count < 1 Then
                GoTo NextTrain
            End If
            With tTrainInOneDay.mTrainList.Item(i)
                For j As Integer = 0 To .pTimeInfoList.Count - 2
                    If .pTimeInfoList.Item(j).pStation.mID = FromSt.mID Then
                        For k As Integer = j + 1 To .pTimeInfoList.Count - 1
                            If .pTimeInfoList.Item(k).pStation.mID = EndSt.mID Then
                                tempList.Add(New clsTrainTimeInfo(tTrainInOneDay.mTrainList.Item(i), .pTimeInfoList.Item(j), .pTimeInfoList.Item(k)))
                                GoTo NextTrain
                            End If
                        Next

                    End If
                Next
            End With
NextTrain:
        Next

        tempList.Sort(New clsTrainTimeInfoComparer)

        For i As Integer = 0 To tempList.Count - 1
            result.Add(tempList.Item(i))
        Next
        Return result
    End Function

    Public Shared Function GetTrainByDepTimeCarClass(tTrainInfos As ObservableCollection(Of clsTrainTimeInfo), _
                                                       tDepTime_S As DateTime, tDepTime_E As DateTime,
                                                       tClass_PNTZ As Boolean, tClass_TZ As Boolean, tClass_CK As Boolean, tClass_Local As Boolean, tIsIgnoreMonthDay As Boolean) As ObservableCollection(Of clsTrainTimeInfo)



        Dim _ZeroTime As TimeSpan = New TimeSpan(0)
        Dim result As New ObservableCollection(Of clsTrainTimeInfo)

        Dim _tempDepTime_S As DateTime
        Dim _tempDepTime_E As DateTime

        For i As Integer = 0 To tTrainInfos.Count - 1
            If tIsIgnoreMonthDay Then
                _tempDepTime_S = New DateTime(tTrainInfos.Item(i).pTimeInfo_Dep.mDEPTime.Year, tTrainInfos.Item(i).pTimeInfo_Dep.mDEPTime.Month, tTrainInfos.Item(i).pTimeInfo_Dep.mDEPTime.Day, _
                                            tDepTime_S.Hour, tDepTime_S.Minute, tDepTime_S.Minute)
                _tempDepTime_E = New DateTime(tTrainInfos.Item(i).pTimeInfo_Dep.mDEPTime.Year, tTrainInfos.Item(i).pTimeInfo_Dep.mDEPTime.Month, tTrainInfos.Item(i).pTimeInfo_Dep.mDEPTime.Day, _
                                              tDepTime_E.Hour, tDepTime_E.Minute, tDepTime_E.Minute)
            Else
                _tempDepTime_S = tDepTime_S
                _tempDepTime_E = tDepTime_E
            End If
            If tTrainInfos.Item(i).pTimeInfo_Dep.mDEPTime - _tempDepTime_S > _ZeroTime And tTrainInfos.Item(i).pTimeInfo_Dep.mDEPTime - _tempDepTime_E < _ZeroTime Then
                Select Case GetCarClass(tTrainInfos.Item(i).mCarClass)
                    Case CarClass.Tze_ChiangLimitedExpressNew, CarClass.Tze_ChiangLimitedExpressTL
                        If tClass_PNTZ = True Then result.Add(tTrainInfos.Item(i))
                    Case CarClass.Tze_ChiangLimitedExpress1, CarClass.Tze_ChiangLimitedExpress2
                        If tClass_TZ = True Then result.Add(tTrainInfos.Item(i))
                    Case CarClass.Chu_KuangExpress
                        If tClass_CK = True Then result.Add(tTrainInfos.Item(i))
                    Case Else
                        If tClass_Local = True Then result.Add(tTrainInfos.Item(i))
                End Select
            End If
        Next

        Return result
    End Function

    Public Shared Function GetTrainByCarClass(tTrainInfos As ObservableCollection(Of clsTrainTimeInfo), _
                                                       tClass_PNTZ As Boolean, tClass_TZ As Boolean, tClass_CK As Boolean, tClass_Local As Boolean) As ObservableCollection(Of clsTrainTimeInfo)

        Dim _ZeroTime As TimeSpan = New TimeSpan(0)
        Dim result As New ObservableCollection(Of clsTrainTimeInfo)

        For i As Integer = 0 To tTrainInfos.Count - 1
            Select Case GetCarClass(tTrainInfos.Item(i).mCarClass)
                Case CarClass.Tze_ChiangLimitedExpressNew, CarClass.Tze_ChiangLimitedExpressTL
                    If tClass_PNTZ = True Then result.Add(tTrainInfos.Item(i))
                Case CarClass.Tze_ChiangLimitedExpress1, CarClass.Tze_ChiangLimitedExpress2
                    If tClass_TZ = True Then result.Add(tTrainInfos.Item(i))
                Case CarClass.Chu_KuangExpress
                    If tClass_CK = True Then result.Add(tTrainInfos.Item(i))
                Case Else
                    If tClass_Local = True Then result.Add(tTrainInfos.Item(i))
            End Select
        Next

        Return result
    End Function
    Public Shared Sub ChangeVisibilityByDepTimeCarClass(tTrainInfos As ObservableCollection(Of clsTrainTimeInfo), _
                                                           tDepTime_S As DateTime, tDepTime_E As DateTime,
                                                           tClass_PNTZ As Visibility, tClass_TZ As Visibility, tClass_CK As Visibility, tClass_Local As Visibility)
        Dim _ZeroTime As TimeSpan = New TimeSpan(0)

        For i As Integer = 0 To tTrainInfos.Count - 1
            If tTrainInfos.Item(i).pTimeInfo_Dep.mDEPTime - tDepTime_S > _ZeroTime And tTrainInfos.Item(i).pTimeInfo_Dep.mDEPTime - tDepTime_E < _ZeroTime Then
                Select Case GetCarClass(tTrainInfos.Item(i).mCarClass)
                    Case CarClass.Tze_ChiangLimitedExpressNew, CarClass.Tze_ChiangLimitedExpressTL
                        tTrainInfos.Item(i).pVisibility = tClass_PNTZ
                    Case CarClass.Tze_ChiangLimitedExpress1, CarClass.Tze_ChiangLimitedExpress2
                        tTrainInfos.Item(i).pVisibility = tClass_TZ
                    Case CarClass.Chu_KuangExpress
                        tTrainInfos.Item(i).pVisibility = tClass_CK
                    Case Else
                        tTrainInfos.Item(i).pVisibility = tClass_Local
                End Select
            Else
                tTrainInfos.Item(i).pVisibility = Visibility.Collapsed
            End If
        Next

    End Sub

    Public Shared Function FillterTrainTimeInfoByDepTime(tTrainInfos As ObservableCollection(Of clsTrainTimeInfo), tDepTime_S As DateTime, tDepTime_E As DateTime, tIsIgnoreMonthDay As Boolean) As ObservableCollection(Of clsTrainTimeInfo)
        Dim _ZeroTime As TimeSpan = New TimeSpan(0)
        Dim result As New ObservableCollection(Of clsTrainTimeInfo)
        Dim _tempDepTime_S As DateTime
        Dim _tempDepTime_E As DateTime

        For i As Integer = 0 To tTrainInfos.Count - 1
            If tIsIgnoreMonthDay Then
                _tempDepTime_S = New DateTime(tTrainInfos.Item(i).pTimeInfo_Dep.mDEPTime.Year, tTrainInfos.Item(i).pTimeInfo_Dep.mDEPTime.Month, tTrainInfos.Item(i).pTimeInfo_Dep.mDEPTime.Day, _
                                            tDepTime_S.Hour, tDepTime_S.Minute, tDepTime_S.Minute)
                _tempDepTime_E = New DateTime(tTrainInfos.Item(i).pTimeInfo_Dep.mDEPTime.Year, tTrainInfos.Item(i).pTimeInfo_Dep.mDEPTime.Month, tTrainInfos.Item(i).pTimeInfo_Dep.mDEPTime.Day, _
                                              tDepTime_E.Hour, tDepTime_E.Minute, tDepTime_E.Minute)
            Else
                _tempDepTime_S = tDepTime_S
                _tempDepTime_E = tDepTime_E
            End If

            If tTrainInfos.Item(i).pTimeInfo_Dep.mDEPTime - _tempDepTime_S > _ZeroTime And tTrainInfos.Item(i).pTimeInfo_Dep.mDEPTime - _tempDepTime_E < _ZeroTime Then
                result.Add(tTrainInfos.Item(i))
            End If
            Next
            Return result
    End Function
    Public Shared Sub GetTrainTimeInfoByLineLoc(ByRef tTrainList As ObservableCollection(Of clsTrainTimeInfo), ByRef tResultTrainList As ObservableCollection(Of clsTrainTimeInfo), tLineLoc As LineLocation)
        If tResultTrainList Is Nothing Then
            tResultTrainList = New ObservableCollection(Of clsTrainTimeInfo)
        Else
            tResultTrainList.Clear()
        End If 

        For i As Integer = 0 To tTrainList.Count - 1
            For j As Integer = 0 To tTrainList.Item(i).mLineLoc.Count - 1
                If tLineLoc = tTrainList.Item(i).mLineLoc.Item(j) Then
                    tResultTrainList.Add(tTrainList.Item(i))
                    GoTo NextTrain
                End If
            Next
NextTrain:
        Next
    End Sub

    Public Shared Function GetTrainClassCht(input As CarClass) As String
        Select Case input
            Case CarClass.Tze_ChiangLimitedExpress1
                Return "自強"
            Case CarClass.Tze_ChiangLimitedExpress2
                Return "自強"
            Case CarClass.Tze_ChiangLimitedExpressTL
                '   Return "自強(太魯閣號)  "
                Return "自強" '  Return "自強(太)"
            Case CarClass.Tze_ChiangLimitedExpressNew
                Return "自強"  ' Return "新自強"
            Case CarClass.Chu_KuangExpress
                Return "莒光"
            Case CarClass.Fu_HsingSemiExpress
                Return "復興"
            Case CarClass.ElectricMultipleUnit
                Return "電車	"
            Case CarClass.LocalTrain
                ' Return "區間車"
                Return "區間"
            Case CarClass.FastLocalTrain
                '  Return "區間快"
                Return "區間"
            Case CarClass.Ordinarytrain
                ' Return "普快車"
            Case CarClass.DiselRailCar
                Return "柴快車"
            Case CarClass.na
                Return "柴油車"
        End Select
        Return Nothing
    End Function

    Public Shared Function GetTrainClassFullNameCht(input As CarClass) As String
        Select Case input
            Case CarClass.Tze_ChiangLimitedExpress1
                Return "自強號"
            Case CarClass.Tze_ChiangLimitedExpress2
                Return "自強號"
            Case CarClass.Tze_ChiangLimitedExpressTL
                Return "太魯閣"
            Case CarClass.Tze_ChiangLimitedExpressNew
                Return "普悠瑪"
            Case CarClass.Chu_KuangExpress
                Return "莒光號"
            Case CarClass.Fu_HsingSemiExpress
                Return "復興號"
            Case CarClass.ElectricMultipleUnit
                Return "電車	"
            Case CarClass.LocalTrain
                Return "區間車"
            Case CarClass.FastLocalTrain
                Return "區間快"
            Case CarClass.Ordinarytrain
                Return "普快車"
            Case CarClass.DiselRailCar
                Return "柴快車"
            Case CarClass.na
                Return "柴油車"
        End Select
        Return Nothing
    End Function

    Public Shared Function GetTrainClassDescriptionCht(input As CarClass) As String
        Select Case input
            Case CarClass.Tze_ChiangLimitedExpressTL
                Return "太魯閣"
            Case CarClass.Tze_ChiangLimitedExpressNew
                Return "普悠瑪"  ' Return "新自強"
            Case Else
                Return ""
        End Select
        Return Nothing
    End Function

    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged 
End Class

Public Class clsTrainTimeInfo
    Inherits clsTrain

    Public Property pTimeInfo_Dep As clsTimeInfo
    Public Property pTimeInfo_Arr As clsTimeInfo


    Public mPrice As Integer
    Public ReadOnly Property pPrice As String
        Get
            Return mPrice.ToString + "元"
        End Get
    End Property

    Public Property pFromEndStString As String = ""
    Public Property pEstTimeString As String = ""
    Public Property pEstTimeStringShort As String = ""
    Public mTimeSpan As TimeSpan
    Public ReadOnly Property pTimeSpan As String
        Get
            Return "行車時間 " + mTimeSpan.ToString("hh\:mm")
        End Get
    End Property

    Public Sub New(tTrain As clsTrain, tTimeInfo As clsTimeInfo)
        MyBase.New(tTrain.mYear, tTrain.mMonth, tTrain.mDate, tTrain.mCarClass, tTrain.mCripple, tTrain.mDinning, tTrain.mLine, tTrain.mLineDir, tTrain.mNote, tTrain.mOverNightStn, tTrain.mPackage, tTrain.mRoute, tTrain.pTrainID, tTrain.mType, tTrain.pTimeInfoList)
        pTimeInfo_Dep = tTimeInfo
    End Sub

    Public Sub New(tTrain As clsTrain, tTimeInfo_Dep As clsTimeInfo, tTimeInfo_Arr As clsTimeInfo)
        MyBase.New(tTrain.mYear, tTrain.mMonth, tTrain.mDate, tTrain.mCarClass, tTrain.mCripple, tTrain.mDinning, tTrain.mLine, tTrain.mLineDir, tTrain.mNote, tTrain.mOverNightStn, tTrain.mPackage, tTrain.mRoute, tTrain.pTrainID, tTrain.mType, tTrain.pTimeInfoList)
        pTimeInfo_Dep = tTimeInfo_Dep
        pTimeInfo_Arr = tTimeInfo_Arr

        mTimeSpan = pTimeInfo_Arr.mARRTime - pTimeInfo_Dep.mDEPTime
        Dim _EstTime As TimeSpan = pTimeInfo_Dep.mDEPTime - DateTime.Now
        Dim _ZeroTime As TimeSpan = New TimeSpan(0)
        Dim _OneHour As TimeSpan = New TimeSpan(1, 0, 0)
        If _EstTime < _ZeroTime Then
            pEstTimeString = ""
        Else
            If _EstTime > _OneHour Then
                '' pEstTimeString = _EstTime.ToString("hh") + "小時" + _EstTime.ToString("mm") + "分後出發"
                pEstTimeString = ""
                pEstTimeStringShort = ""
            Else
                pEstTimeString = _EstTime.ToString("mm") + "分後出發"
                pEstTimeStringShort = "(" + _EstTime.ToString("mm") + "分)"
            End If

        End If
        pFromEndStString = tTimeInfo_Dep.pStation.DisplayName + "=>" + tTimeInfo_Arr.pStation.DisplayName
    End Sub

    Public Shared Sub SortByTrainDir(ByRef tTrainInfoList As ObservableCollection(Of clsTrainTimeInfo), ByRef tListFW As ObservableCollection(Of clsTrainTimeInfo), ByRef tListBW As ObservableCollection(Of clsTrainTimeInfo))
        tListFW.Clear()
        tListBW.Clear()
        For i As Integer = 0 To tTrainInfoList.Count - 1
            Select Case tTrainInfoList.Item(i).mLineDir
                Case 0
                    tListFW.Add(tTrainInfoList.Item(i))
                Case 1
                    tListBW.Add(tTrainInfoList.Item(i))
            End Select
        Next
    End Sub


    Public Shared Function GetCloestTrain(tTrainInfoList As ObservableCollection(Of clsTrainTimeInfo), tYear As Integer, tMonth As Integer, tDate As Integer) As clsTrainTimeInfo
        If tTrainInfoList Is Nothing OrElse tTrainInfoList.Count = 0 Then Return Nothing

        Dim _Current As DateTime = New DateTime(tYear, tMonth, tDate, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)
        Dim _ZeroTime As TimeSpan = New TimeSpan(0)
        Dim _DiffTime As TimeSpan

        For i As Integer = 0 To tTrainInfoList.Count - 1
            _DiffTime = tTrainInfoList.Item(i).pTimeInfo_Dep.mDEPTime - _Current
            If _DiffTime > _ZeroTime Then
                Return tTrainInfoList.Item(i)
            End If
        Next

        Return tTrainInfoList.Item(0)
    End Function

    Public Shared Function GetCloestTrains(tTrainInfoList As ObservableCollection(Of clsTrainTimeInfo), tCnt As Integer, tYear As Integer, tMonth As Integer, tDate As Integer) As List(Of clsTrainTimeInfo)
        If tTrainInfoList Is Nothing OrElse tTrainInfoList.Count = 0 Then Return Nothing

        Dim _token As Integer = 0
        Dim ResultTrains As New List(Of clsTrainTimeInfo)


        Dim _Current As DateTime = New DateTime(tYear, tMonth, tDate, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)
        Dim _ZeroTime As TimeSpan = New TimeSpan(0)
        Dim _DiffTime As TimeSpan

        For i As Integer = 0 To tTrainInfoList.Count - 1
            _DiffTime = tTrainInfoList.Item(i).pTimeInfo_Dep.mDEPTime - _Current
            If tTrainInfoList.Item(i).pTimeInfo_Dep.mDEPTime - _Current > _ZeroTime Then
                ResultTrains.Add(tTrainInfoList.Item(i))
                _token += 1
                If _token >= tCnt Then Exit For
            End If
        Next 

        Return ResultTrains
    End Function
  
End Class

Class clsTrainTimeInfoComparer
    Implements IComparer(Of clsTrainTimeInfo)
    Public Function Compare(ByVal x As clsTrainTimeInfo, ByVal y As clsTrainTimeInfo) As Integer Implements System.Collections.Generic.IComparer(Of clsTrainTimeInfo).Compare
        Return x.pTimeInfo_Dep.mDEPTime.CompareTo(y.pTimeInfo_Dep.mDEPTime)
    End Function
End Class


Public Class clsTimeInfo
    Implements INotifyPropertyChanged
    Public mYearString As String
    Public mMonthString As String
    Public mDayString As String

    Public mARRTimeString As String
    Public mDEPTimeString As String
    Public mStationString As String

    Public Property pOrder As String
    Public Property mRoute As String

    Public mARRTime As DateTime
    Public mDEPTime As DateTime
    Public Property pStation As clsStation

    Public Property pIsHighLight As Boolean = False
    Public Property pDelayString As String = ""

    Public ReadOnly Property pDisplayDepTime As String
        Get
            Return mDEPTime.ToString("HH:mm")
        End Get
    End Property

    Public ReadOnly Property pDisplayArrTime As String
        Get
            Return mARRTime.ToString("HH:mm")
        End Get
    End Property


    Public Sub New(tYear As String, tMonth As String, tDay As String, ARRTime As String, DEPTime As String, Order As String, Route As String, Station As String)
        mYearString = tYear
        mMonthString = tMonth
        mDayString = tDay
        mARRTimeString = ARRTime
        mDEPTimeString = DEPTime
        mStationString = Station
        Dim _tempTimeStrings As String()
        _tempTimeStrings = ARRTime.Split(":")
        mARRTime = New DateTime(tYear, tMonth, tDay, _tempTimeStrings(0), _tempTimeStrings(1), _tempTimeStrings(2))
        _tempTimeStrings = DEPTime.Split(":")
        mDEPTime = New DateTime(tYear, tMonth, tDay, _tempTimeStrings(0), _tempTimeStrings(1), _tempTimeStrings(2))

        pOrder = Order
        mRoute = Route
        pStation = clsStation.GetCloneStationByIDFromGlobalVariable(Station)

    End Sub

    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged
End Class



'Public Class clsTimeInfo2
'    Implements INotifyPropertyChanged

'    <DataMember>
'    Public mYearString As String
'    <DataMember>
'    Public mMonthString As String
'    <DataMember>
'    Public mDayString As String

'    '<DataMember>
'    'Public mARRTimeString As String
'    '<DataMember>
'    'Public mDEPTimeString As String
'    '<DataMember>
'    'Public mStationString As String
'    '<DataMember>
'    'Public Property mOrder As String
'    '<DataMember>
'    'Public Property mRoute As String

'    'Public Property mARRTime As DateTime
'    'Public Property mDEPTime As DateTime
'    'Public Property mStation As clsStation


'    Public Sub New(tYear As String, tMonth As String, tDay As String, ARRTime As String, DEPTime As String, Order As String, Route As String, Station As String)
'        mYearString = tYear
'        mMonthString = tMonth
'        mDayString = tDay
'        'mARRTimeString = ARRTime
'        'mDEPTimeString = DEPTime
'        'mStationString = Station
'        'Dim _tempTimeStrings As String()
'        '_tempTimeStrings = ARRTime.Split(":")
'        ''mARRTime = New DateTime(tYear, tMonth, tDay, _tempTimeStrings(0), _tempTimeStrings(1), _tempTimeStrings(2))
'        '_tempTimeStrings = DEPTime.Split(":")
'        ''mDEPTime = New DateTime(tYear, tMonth, tDay, _tempTimeStrings(0), _tempTimeStrings(1), _tempTimeStrings(2))

'        'mOrder = Order
'        'mRoute = Route
'        'mStation = clsStation.GetStationByID(Station)

'    End Sub




'    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged
'End Class
