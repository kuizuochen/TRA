Imports System.ComponentModel
Imports System.Collections.ObjectModel
Imports TRA_03.GlobalVariables

Public Class vmLocationSearch
    Implements INotifyPropertyChanged
    ''各站列車 搜尋表
    Public pYear As Integer = 2014
    Public pMonth As Integer = 3
    Public pDate As Integer = 3
    Public pLineDir As String = 0

    Public Property pTodayTimeTable As clsTrainsInOneDay
    Public mTrainListFitSt As ObservableCollection(Of clsTrainTimeInfo) 

    Public Property pColor_PNTZ As String = "Pink"
    Public Property pColor_TZ As String = "Red"
    Public Property pColor_CK As String = "Orange"
    Public Property pColor_Local As String = "RoyalBlue"

    Public Property pVisibility_PNTZ As Boolean = True
    Public Property pVisibility_TZ As Boolean = True
    Public Property pVisibility_CK As Boolean = True 
    Public Property pVisibility_Local As Boolean = True

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
    Public Property pStGrpList As ObservableCollection(Of clsStGrp)
    Public mStGrp As clsStGrp
    Public Property pStGrp As clsStGrp
        Get
            Return mStGrp
        End Get
        Set(value As clsStGrp)
            mStGrp = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("pStGrp"))
        End Set
    End Property

    Public Property pStList As ObservableCollection(Of clsStation)
    Public mStation As clsStation
    Public Property pStation As clsStation
        Get
            Return mStation
        End Get
        Set(value As clsStation)
            mStation = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("pStation"))
        End Set
    End Property
    Public mTrainListFitLocFW As ObservableCollection(Of clsTrainTimeInfo)
    Public mTrainListFitLocBW As ObservableCollection(Of clsTrainTimeInfo)
    Public Property pTrainListFW As ObservableCollection(Of clsTrainTimeInfo)
    Public Property pTrainListBW As ObservableCollection(Of clsTrainTimeInfo)

    Public Property pShownListLineID As Integer = 0

    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged

    Public Sub New()
        ''各站列車
        pStGrpList = New ObservableCollection(Of clsStGrp)
        pStList = New ObservableCollection(Of clsStation)
        pTrainListFW = New ObservableCollection(Of clsTrainTimeInfo)
        pTrainListBW = New ObservableCollection(Of clsTrainTimeInfo)
        mTrainListFitLocFW = New ObservableCollection(Of clsTrainTimeInfo)
        mTrainListFitLocBW = New ObservableCollection(Of clsTrainTimeInfo)
    End Sub

    Public Sub LoadData()
        '' 將時刻表設定為今天
        pTodayTimeTable = clsTrainsInOneDay.GetTrainsByDate(GlobalVariables.gSchedule, DateTime.Now)

        '' initialize 各站列車
        For i As Integer = 0 To gStGrpList.Count - 1
            pStGrpList.Add(gStGrpList.Item(i).Clone)
        Next
        pStGrp = clsStGrp.GetRegion(pStGrpList, GlobalVariables.gLocationSearchIniStID)

        For i As Integer = 0 To pStGrp.mStation.Count - 1
            pStList.Add(pStGrp.mStation.Item(i))
        Next
        pStation = clsStation.GetStationByID(pStGrpList, GlobalVariables.gLocationSearchIniStID)

        UpdateTrainList()

    End Sub

    Public Sub LoadData(ByRef tIsoSetting As System.IO.IsolatedStorage.IsolatedStorageSettings)

        ''讀取設定 
        Dim _ContentString As String = ""

        ''若IsoStorage中沒有儲存資料 則從 global data中啟動
        If tIsoSetting.TryGetValue("ls", _ContentString) = False Then
            LoadData()
            Exit Sub
        End If

        Dim _IsoSettingStrings() As String = _ContentString.Split("_")

        If _IsoSettingStrings(1) = "0" Then pVisibility_PNTZ = False
        If _IsoSettingStrings(1) = "1" Then pVisibility_PNTZ = True
        If _IsoSettingStrings(2) = "0" Then pVisibility_TZ = False
        If _IsoSettingStrings(2) = "1" Then pVisibility_TZ = True
        If _IsoSettingStrings(3) = "0" Then pVisibility_CK = False
        If _IsoSettingStrings(3) = "1" Then pVisibility_CK = True
        If _IsoSettingStrings(4) = "0" Then pVisibility_Local = False
        If _IsoSettingStrings(4) = "1" Then pVisibility_Local = True

        Dim LocSchIniStID As String = _IsoSettingStrings(0)

        '' 將時刻表設定為今天
        pTodayTimeTable = clsTrainsInOneDay.GetTrainsByDate(GlobalVariables.gSchedule, DateTime.Now)

        For i As Integer = 0 To gStGrpList.Count - 1
            pStGrpList.Add(gStGrpList.Item(i).Clone)
        Next
        pStGrp = clsStGrp.GetRegion(pStGrpList, LocSchIniStID)

        For i As Integer = 0 To pStGrp.mStation.Count - 1
            pStList.Add(pStGrp.mStation.Item(i))
        Next
        pStation = clsStation.GetStationByID(pStGrpList, LocSchIniStID)

        UpdateTrainList() 
    End Sub

    Public Sub UpdateTrainList() 
        mTrainListFitSt = clsTrain.GetTrainTimeInfosByFromSt(pTodayTimeTable, pStation) 

        FillterTrainBySelectedLine() 
    End Sub

    Public Sub SortTrainByLineClass()
        FillterTrainBySelectedLine() 
    End Sub

    Private Sub FillterTrainBySelectedLine()
        If pStation.mCrossLineList.Count = 1 Then
            clsTrainTimeInfo.SortByTrainDir(mTrainListFitSt, mTrainListFitLocFW, mTrainListFitLocBW)
        Else
            Dim _TrainsFitLineID As New ObservableCollection(Of clsTrainTimeInfo)
            clsTrain.GetTrainTimeInfoByLineLoc(mTrainListFitSt, _TrainsFitLineID, pStation.mCrossLineList.Item(pShownListLineID))
            clsTrainTimeInfo.SortByTrainDir(_TrainsFitLineID, mTrainListFitLocFW, mTrainListFitLocBW)
        End If
        FillterTrainByDepTimeClass()
    End Sub

    Public Sub FillterTrainByDepTimeClass()
        pTrainListFW = clsTrain.GetTrainByCarClass(mTrainListFitLocFW, pVisibility_PNTZ, pVisibility_TZ, pVisibility_CK, pVisibility_Local)
        pTrainListBW = clsTrain.GetTrainByCarClass(mTrainListFitLocBW, pVisibility_PNTZ, pVisibility_TZ, pVisibility_CK, pVisibility_Local)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("pTrainListFW"))
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("pTrainListBW"))
    End Sub

    Public Function ToIsoSettingString() As String
        Return Me.pStation.mID.ToString + "_" + Me.pClassSortString
    End Function

    Public Function GetCloestTrain_FW() As clsTrainTimeInfo 
        Return clsTrainTimeInfo.GetCloestTrain(pTrainListFW, DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)
    End Function
    Public Function GetCloestTrain_BW() As clsTrainTimeInfo
        Return clsTrainTimeInfo.GetCloestTrain(pTrainListBW, DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)
    End Function



#Region "儲存/讀取 UI設定"
    ''ls ; 1059_1_0_1_0 
    ''key:se
    ''Content: StID _ PNTZ _ TZ _ CK _ Local
    Public Sub SaveToIsoSetting(ByRef tIsoSetting As System.IO.IsolatedStorage.IsolatedStorageSettings)
        RemoveIsoSetting(tIsoSetting)

        Dim _KeyString As String = "ls"
        Dim _ContentString As String = ToIsoSettingString()

        tIsoSetting.Add(_KeyString, _ContentString)
    End Sub

    Private Sub RemoveIsoSetting(ByRef tIsoSetting As System.IO.IsolatedStorage.IsolatedStorageSettings)
        Dim _Keys As ICollection(Of String) = tIsoSetting.Keys
        Dim _KeyToRemove As New List(Of String)
        For i As Integer = 0 To _Keys.Count - 1
            If _Keys(i).Substring(0, 2) = "ls" Then
                _KeyToRemove.Add(_Keys(i))
            End If
        Next

        For i As Integer = 0 To _KeyToRemove.Count - 1
            tIsoSetting.Remove(_KeyToRemove.Item(i))
        Next

    End Sub
#End Region
End Class
