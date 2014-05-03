Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports TRA_03.GlobalVariables

Public Class vmStartEndSearch
    Implements INotifyPropertyChanged

    Public ReadOnly Property pSchedule As ObservableCollection(Of clsTrainsInOneDay)
        Get
            Return gSchedule
        End Get
    End Property
    Public Sub UpdateSchedule()
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("pSchedule"))
    End Sub
    Public Property pSelectedTimeTable As clsTrainsInOneDay

    Private mDepTime_S As DateTime
    Public Property pDepTime_S As DateTime
        Get
            Return mDepTime_S
        End Get
        Set(value As DateTime)
            mDepTime_S = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("pDepTime_S"))
        End Set
    End Property
    Private mDepTime_E As DateTime
    Public Property pDepTime_E As DateTime
        Get
            Return mDepTime_E
        End Get
        Set(value As DateTime)
            mDepTime_E = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("pDepTime_E"))
        End Set
    End Property

    Public Property pColor_PNTZ As String = "Pink"
    Public Property pColor_TZ As String = "Red"
    Public Property pColor_CK As String = "Orange"
    Public Property pColor_Local As String = "RoyalBlue"

    Public Property pVisibility_PNTZ As Boolean = True
    Public Property pVisibility_TZ As Boolean = True
    Public Property pVisibility_CK As Boolean = True
    Public Property pVisibility_Local As Boolean = True
    Public mIsMyFavorite As Boolean = False

    Public Property pCloestTrain_0 As clsTrainTimeInfo
    Public Property pCloestTrain_1 As clsTrainTimeInfo
    Public Property pCloestTrain_2 As clsTrainTimeInfo

    Public mPriceTZ As Double
    Public mPriceCK As Double
    Public mPriceLocal As Double

    Public ReadOnly Property pPriceTZ As String
        Get
            Return mPriceTZ.ToString
        End Get
    End Property
    Public ReadOnly Property pPriceCK As String
        Get
            Return mPriceCK.ToString
        End Get
    End Property
    Public ReadOnly Property pPriceLocal As String
        Get
            Return mPriceLocal.ToString
        End Get
    End Property

    Public ReadOnly Property pClassSortString As String
        Get
            Dim _PNTZ As String = "0"
            Dim _TZ As String = "0"
            Dim _CK As String = "0"
            Dim _Local As String = "0"

            If pVisibility_PNTZ = True Then _PNTZ = "1"
            If pVisibility_TZ = True Then _TZ = "1"
            If pVisibility_CK = True Then _CK = "1"
            If pVisibility_Local = True Then _Local = "1"

            Return _PNTZ + "_" + _TZ + "_" + _CK + "_" + _Local
        End Get
    End Property

    Public Property pIsMyFavorite As Boolean
        Get
            Return mIsMyFavorite
        End Get
        Set(value As Boolean)
            mIsMyFavorite = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("pIsMyFavorite"))
        End Set
    End Property

    Public Property pStGrpList As ObservableCollection(Of clsStGrp)
    Public mStGrp_S As clsStGrp
    Public Property pStGrp_S As clsStGrp
        Get
            Return mStGrp_S
        End Get
        Set(value As clsStGrp)
            mStGrp_S = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("pStGrp_S"))
        End Set
    End Property
    Public mStGrp_E As clsStGrp
    Public Property pStGrp_E As clsStGrp
        Get
            Return mStGrp_E
        End Get
        Set(value As clsStGrp)
            mStGrp_E = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("pStGrp_E"))
        End Set
    End Property

    Public Property pStList_S As ObservableCollection(Of clsStation)
    Public mStation_S As clsStation
    Public Property pStation_S As clsStation
        Get
            Return mStation_S
        End Get
        Set(value As clsStation)
            mStation_S = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("pStation_S"))
        End Set
    End Property

    Public Property pStList_E As ObservableCollection(Of clsStation)
    Public mStation_E As clsStation
    Public Property pStation_E As clsStation
        Get
            Return mStation_E
        End Get
        Set(value As clsStation)
            mStation_E = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("pStation_E"))
        End Set
    End Property

    Public mTrainListFitSETime As ObservableCollection(Of clsTrainTimeInfo)
    Public Property pTrainList As ObservableCollection(Of clsTrainTimeInfo)

#Region "Initialization"
    Public Sub New()
        ''各站列車
        pStGrpList = New ObservableCollection(Of clsStGrp)
        pStList_S = New ObservableCollection(Of clsStation)
        pStList_E = New ObservableCollection(Of clsStation)
        mTrainListFitSETime = New ObservableCollection(Of clsTrainTimeInfo)
        pTrainList = New ObservableCollection(Of clsTrainTimeInfo)
    End Sub

    Public Function ToIsoSettingString_MyFavorite() As String
        Return Me.pStation_S.mID.ToString + "_" + Me.pStation_E.mID.ToString + "_" + Me.pClassSortString
    End Function
    Public Sub New(tIsoSettingString As String)
        Dim _IsoSettingStrings() As String = tIsoSettingString.Split("_")

        pStation_S = clsStation.GetStationByID(GlobalVariables.gStList, _IsoSettingStrings(0))
        pStation_E = clsStation.GetStationByID(GlobalVariables.gStList, _IsoSettingStrings(1))

        If _IsoSettingStrings(2) = "0" Then pVisibility_PNTZ = False
        If _IsoSettingStrings(2) = "1" Then pVisibility_PNTZ = True
        If _IsoSettingStrings(3) = "0" Then pVisibility_TZ = False
        If _IsoSettingStrings(3) = "1" Then pVisibility_TZ = True
        If _IsoSettingStrings(4) = "0" Then pVisibility_CK = False
        If _IsoSettingStrings(4) = "1" Then pVisibility_CK = True
        If _IsoSettingStrings(5) = "0" Then pVisibility_Local = False
        If _IsoSettingStrings(5) = "1" Then pVisibility_Local = True

        pStGrpList = New ObservableCollection(Of clsStGrp)
        pStList_S = New ObservableCollection(Of clsStation)
        pStList_E = New ObservableCollection(Of clsStation)
        mTrainListFitSETime = New ObservableCollection(Of clsTrainTimeInfo)
        pTrainList = New ObservableCollection(Of clsTrainTimeInfo)
    End Sub

    Public Sub LoadData_FavoriteMode()
        '' initialize 日期
        InitializeTimeTable()
        '' initialize 搜尋時間
        InitializeSETime()
    End Sub

    Public Sub LoadData()
        '' initialize 日期
        InitializeTimeTable()

        '' initialize 搜尋時間
        InitializeSETime()

        '' initialize 各站列車
        For i As Integer = 0 To gStGrpList.Count - 1
            pStGrpList.Add(gStGrpList.Item(i).Clone)
        Next
        pStGrp_S = clsStGrp.GetRegion(pStGrpList, GlobalVariables.gSESearchIniStID_S)
        pStGrp_E = clsStGrp.GetRegion(pStGrpList, GlobalVariables.gSESearchIniStID_E)

        For i As Integer = 0 To pStGrp_S.mStation.Count - 1
            pStList_S.Add(pStGrp_S.mStation.Item(i))
        Next
        pStation_S = clsStation.GetStationByID(pStGrpList, GlobalVariables.gSESearchIniStID_S)

        For i As Integer = 0 To pStGrp_E.mStation.Count - 1
            pStList_E.Add(pStGrp_E.mStation.Item(i))
        Next
        pStation_E = clsStation.GetStationByID(pStGrpList, GlobalVariables.gSESearchIniStID_E)

        UpdateTrainList()
    End Sub

    Public Sub LoadData(ByRef tIsoSetting As System.IO.IsolatedStorage.IsolatedStorageSettings)

        ''讀取設定 
        Dim _ContentString As String = ""

        ''若IsoStorage中沒有儲存資料 則從 global data中啟動
        If tIsoSetting.TryGetValue("se", _ContentString) = False Then
            LoadData()
            Exit Sub
        End If

        Dim _IsoSettingStrings() As String = _ContentString.Split("_")

        If _IsoSettingStrings(2) = "0" Then pVisibility_PNTZ = False
        If _IsoSettingStrings(2) = "1" Then pVisibility_PNTZ = True
        If _IsoSettingStrings(3) = "0" Then pVisibility_TZ = False
        If _IsoSettingStrings(3) = "1" Then pVisibility_TZ = True
        If _IsoSettingStrings(4) = "0" Then pVisibility_CK = False
        If _IsoSettingStrings(4) = "1" Then pVisibility_CK = True
        If _IsoSettingStrings(5) = "0" Then pVisibility_Local = False
        If _IsoSettingStrings(5) = "1" Then pVisibility_Local = True

        Dim SESearchIniStID_S As String = _IsoSettingStrings(0)
        Dim SESearchIniStID_E As String = _IsoSettingStrings(1)

        '' initialize 日期
        InitializeTimeTable()

        '' initialize 搜尋時間
        InitializeSETime(_IsoSettingStrings(6), _IsoSettingStrings(7))

        '' initialize 各站列車
        For i As Integer = 0 To gStGrpList.Count - 1
            pStGrpList.Add(gStGrpList.Item(i).Clone)
        Next
        pStGrp_S = clsStGrp.GetRegion(pStGrpList, SESearchIniStID_S)
        pStGrp_E = clsStGrp.GetRegion(pStGrpList, SESearchIniStID_E)

        For i As Integer = 0 To pStGrp_S.mStation.Count - 1
            pStList_S.Add(pStGrp_S.mStation.Item(i))
        Next
        pStation_S = clsStation.GetStationByID(pStGrpList, SESearchIniStID_S)

        For i As Integer = 0 To pStGrp_E.mStation.Count - 1
            pStList_E.Add(pStGrp_E.mStation.Item(i))
        Next
        pStation_E = clsStation.GetStationByID(pStGrpList, SESearchIniStID_E)

        UpdateTrainList()
    End Sub
    Public Sub InitializeTimeTable()
        pSelectedTimeTable = clsTrainsInOneDay.GetTrainsByDate(GlobalVariables.gSchedule, DateTime.Now)
    End Sub
    Public Sub InitializeSETime()
        pDepTime_S = New DateTime(pSelectedTimeTable.mDate.Year, pSelectedTimeTable.mDate.Month, pSelectedTimeTable.mDate.Day, DateTime.Now.Hour, 0, 0)

        If pDepTime_S.Hour = 23 Then
            pDepTime_E = New DateTime(pSelectedTimeTable.mDate.Year, pSelectedTimeTable.mDate.Month, pSelectedTimeTable.mDate.Day, 23, 59, 0)
            Exit Sub
        End If

        Dim _AddedHour As Integer = 8
MinusOneHour:
        If DateTime.Now.Hour + _AddedHour > 23 Then
            _AddedHour -= 1
            GoTo MinusOneHour
        Else
            pDepTime_E = New DateTime(pSelectedTimeTable.mDate.Year, pSelectedTimeTable.mDate.Month, pSelectedTimeTable.mDate.Day, DateTime.Now.Hour + _AddedHour, 0, 0)
        End If
    End Sub

    Public Sub InitializeSETime(tStartTimeString As String, tEndTimeString As String)
        ''判斷 現在時間是否 在  指定的起始時間 之內
        ''No => 現在時間<=>現在時間+8hr.
        ''Yes => 讀取
        Dim _StartTime As DateTime = New DateTime(pSelectedTimeTable.mDate.Year, pSelectedTimeTable.mDate.Month, pSelectedTimeTable.mDate.Day, tStartTimeString.Split(":")(0), tStartTimeString.Split(":")(1), 0)
        Dim _EndTime As DateTime = New DateTime(pSelectedTimeTable.mDate.Year, pSelectedTimeTable.mDate.Month, pSelectedTimeTable.mDate.Day, tEndTimeString.Split(":")(0), tEndTimeString.Split(":")(1), 0)
        Dim _ZeroSpan As TimeSpan = New TimeSpan(0)
        If DateTime.Now - _StartTime < _ZeroSpan Or DateTime.Now - _EndTime > _ZeroSpan Then
            InitializeSETime()
            Exit Sub
        End If

        Try
            pDepTime_S = _StartTime
            If CInt(tEndTimeString.Split(":")(0)) < CInt(tStartTimeString.Split(":")(0)) Then
                pDepTime_E = New DateTime(pSelectedTimeTable.mDate.Year, pSelectedTimeTable.mDate.Month, pSelectedTimeTable.mDate.Day, 23, 59, 0)
            ElseIf CInt(tStartTimeString.Split(":")(0)) = 23 Then
                pDepTime_E = New DateTime(pSelectedTimeTable.mDate.Year, pSelectedTimeTable.mDate.Month, pSelectedTimeTable.mDate.Day, 23, 59, 0)
            Else
                pDepTime_E = _EndTime
            End If
        Catch ex As Exception
            InitializeSETime()
        End Try
    End Sub

    Public Function ToIsoSettingString() As String
        Return Me.pStation_S.mID.ToString + "_" + Me.pStation_E.mID.ToString + "_" + Me.pClassSortString + "_" + pDepTime_S.Hour.ToString + ":" + pDepTime_S.Minute.ToString + "_" + pDepTime_E.Hour.ToString + ":" + pDepTime_E.Minute.ToString
    End Function
#End Region


#Region "儲存/讀取 UI設定"
    ''se ; 1059_1043_1_0_1_0_05:30_18:00
    ''key:se
    ''Content: StartStID _ EndStID _ PNTZ _ TZ _ CK _ Local _ DepStart _ DepEnd
    Public Sub SaveToIsoSetting(ByRef tIsoSetting As System.IO.IsolatedStorage.IsolatedStorageSettings)
        RemoveIsoSetting(tIsoSetting)

        Dim _KeyString As String = "se"
        Dim _ContentString As String = ToIsoSettingString()

        tIsoSetting.Add(_KeyString, _ContentString)
    End Sub

    Private Sub RemoveIsoSetting(ByRef tIsoSetting As System.IO.IsolatedStorage.IsolatedStorageSettings)
        Dim _Keys As ICollection(Of String) = tIsoSetting.Keys
        Dim _KeyToRemove As New List(Of String)
        For i As Integer = 0 To _Keys.Count - 1
            If _Keys(i).Substring(0, 2) = "se" Then
                _KeyToRemove.Add(_Keys(i))
            End If
        Next

        For i As Integer = 0 To _KeyToRemove.Count - 1
            tIsoSetting.Remove(_KeyToRemove.Item(i))
        Next

    End Sub
#End Region

    Public Function Clone() As vmStartEndSearch
        Dim _Result As New vmStartEndSearch

        _Result.pVisibility_PNTZ = pVisibility_PNTZ
        _Result.pVisibility_TZ = pVisibility_TZ
        _Result.pVisibility_CK = pVisibility_CK
        _Result.pVisibility_Local = pVisibility_Local

        _Result.mStation_S = mStation_S
        _Result.mStation_E = mStation_E

        Return _Result
    End Function

    Public Overrides Function Equals(tSESearch2 As Object) As Boolean
        Try
            If tSESearch2 Is Nothing Then Return False

            If Not pVisibility_PNTZ = CType(tSESearch2, vmStartEndSearch).pVisibility_PNTZ Then Return False
            If Not pVisibility_TZ = CType(tSESearch2, vmStartEndSearch).pVisibility_TZ Then Return False
            If Not pVisibility_CK = CType(tSESearch2, vmStartEndSearch).pVisibility_CK Then Return False
            If Not pVisibility_Local = CType(tSESearch2, vmStartEndSearch).pVisibility_Local Then Return False

            If Not mStation_S.mID = CType(tSESearch2, vmStartEndSearch).mStation_S.mID Then Return False
            If Not mStation_E.mID = CType(tSESearch2, vmStartEndSearch).mStation_E.mID Then Return False

            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Sub SetTicketPrice()
        mPriceTZ = clsPrice.GetPrice(pStation_S, pStation_E, CarClass.Tze_ChiangLimitedExpress1)
        mPriceCK = clsPrice.GetPrice(pStation_S, pStation_E, CarClass.Chu_KuangExpress)
        mPriceLocal = clsPrice.GetPrice(pStation_S, pStation_E, CarClass.LocalTrain)
    End Sub

    Public Sub SetTrainInfoPrice()
        SetTicketPrice()
        For i As Integer = 0 To pTrainList.Count - 1
            Select Case clsTrain.GetCarClass(pTrainList.Item(i).mCarClass)
                Case CarClass.FastLocalTrain, CarClass.LocalTrain, CarClass.Fu_HsingSemiExpress
                    pTrainList.Item(i).mPrice = CInt(pPriceLocal)
                Case CarClass.Chu_KuangExpress
                    pTrainList.Item(i).mPrice = CInt(pPriceCK)
                Case CarClass.Tze_ChiangLimitedExpress1, CarClass.Tze_ChiangLimitedExpress2, CarClass.Tze_ChiangLimitedExpressNew, CarClass.Tze_ChiangLimitedExpressTL
                    pTrainList.Item(i).mPrice = CInt(pPriceTZ)
            End Select
        Next
    End Sub

    Public Sub UpdateTrainList()
        Dim tempTrainList As ObservableCollection(Of clsTrainTimeInfo) = clsTrain.GetTrainTimeInfosByStartEndSt(pSelectedTimeTable, pStation_S, pStation_E)
        mTrainListFitSETime = clsTrain.FillterTrainTimeInfoByDepTime(tempTrainList, pDepTime_S, pDepTime_E, True)
        FillterTrainByDepTimeClass()
    End Sub

    Public Sub FillterTrainByDepTimeClass()
        pTrainList = clsTrain.GetTrainByDepTimeCarClass(mTrainListFitSETime, pDepTime_S, pDepTime_E, pVisibility_PNTZ, pVisibility_TZ, pVisibility_CK, pVisibility_Local, True)
        SetTrainInfoPrice()
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("pTrainList"))
    End Sub

    Public Function GetCloestTrain() As clsTrainTimeInfo
        Return clsTrainTimeInfo.GetCloestTrain(pTrainList, DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)
    End Function

    Public Sub SetCloestTrains_MyFavorite()
        mTrainListFitSETime = clsTrain.GetTrainTimeInfosByStartEndSt(pSelectedTimeTable, pStation_S, pStation_E)
        pTrainList = clsTrain.GetTrainByDepTimeCarClass(mTrainListFitSETime, pDepTime_S, pDepTime_E, pVisibility_PNTZ, pVisibility_TZ, pVisibility_CK, pVisibility_Local, True)

        If pTrainList Is Nothing Or pTrainList.Count = 0 Then Exit Sub

        Dim _ThreeCloestTrains As List(Of clsTrainTimeInfo) = clsTrainTimeInfo.GetCloestTrains(pTrainList, 3, DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)
        If _ThreeCloestTrains Is Nothing Then Exit Sub

        If _ThreeCloestTrains.Count > 0 Then
            pCloestTrain_0 = _ThreeCloestTrains.Item(0)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("pCloestTrain_0"))
        End If

        If _ThreeCloestTrains.Count > 1 Then
            pCloestTrain_1 = _ThreeCloestTrains.Item(1)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("pCloestTrain_1"))
        End If
        If _ThreeCloestTrains.Count > 2 Then
            pCloestTrain_2 = _ThreeCloestTrains.Item(2)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("pCloestTrain_2"))
        End If



    End Sub

    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged
End Class
