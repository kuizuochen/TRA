Imports TRA_03.GlobalVariables
Imports System.ComponentModel
Imports System.IO.IsolatedStorage
Imports System.Collections.ObjectModel
Imports System.Windows.Data

Public Class vmMain
    Implements INotifyPropertyChanged

    Public mIsoSetting As System.IO.IsolatedStorage.IsolatedStorageSettings

    Public Property pLocSchVM As vmLocationSearch 
    Public Property pSESchVM As vmStartEndSearch
    Public Property pMyFavoriteVM As vmMyFavorite 
    Public Property pNoNukeVM As vmNoNuke
    Public Property pSettingVM As vmSetting


    Private mIsNewState As Boolean = False

    Public FileStorage As IsolatedStorageFile

    Public StationHLColor As String = "Red"
    Public StationColor As String = "White"
    Public StGrpHLColor As String = "Red"
    Public StGrpColor As String = "White"

    'Public Property mStartDate As String
    'Public Property mStartTime As String

    Public Property mFromSt As clsStation = Nothing
    Public Property mEndSt As clsStation = Nothing

    Private mBgWorker_LoadDB As BackgroundWorker

    Public Event LoadingComplete()

    Public Sub UpdateDatabase()

    End Sub

    Public Sub New()
        mIsNewState = True

        ''Initialize Start End Search
        pSESchVM = New vmStartEndSearch
        ''Initialize Location Search
        pLocSchVM = New vmLocationSearch
        ''Initialize Favorite Page
        pMyFavoriteVM = New vmMyFavorite
        ''Initialize NoNuke Page
        pNoNukeVM = New vmNoNuke
        ''Initialize NoNuke Page
        pSettingVM = New vmSetting

        mIsoSetting = System.IO.IsolatedStorage.IsolatedStorageSettings.ApplicationSettings

        gSchedule = New ObservableCollection(Of clsTrainsInOneDay)
    End Sub

    Public Sub ClearIsoSetting()
        Dim _Keys As ICollection(Of String) = mIsoSetting.Keys
        For i As Integer = 0 To _Keys.Count - 1
            mIsoSetting.Remove(_Keys(i))
        Next
    End Sub
    Public Sub AddMoreXmlFromIsoStorage()
        clsDatabaseIO.AddMoreXmlFromIsoStorage()
        pSESchVM.UpdateSchedule()
        clsDatabaseIO.GetTheLatestDayInXmlFileFolder(Me.pSettingVM.pDateOfLatestTable)
    End Sub
    Public Sub LoadData_MT()
        clsDatabaseIO.LoadXmlFromIsoStorage_BgWorker(mBgWorker_LoadDB, AddressOf LoadData_PbUpdate, AddressOf LoadData_Completed)
    End Sub
    Private Sub LoadData_PbUpdate(sender As Object, e As ComponentModel.ProgressChangedEventArgs) 
    End Sub 
    Private Sub LoadData_Completed(sender As Object, e As ComponentModel.AsyncCompletedEventArgs)

        ''啟動 SE search 
        pSESchVM.LoadData(mIsoSetting)
        ''啟動 Location Search 
        pLocSchVM.LoadData(mIsoSetting)
        ''啟動 My Favorite
        pMyFavoriteVM.LoadFromIsoSetting(mIsoSetting)
        ''啟動 NoNuke
        pNoNukeVM.LoadFromIsoSetting(mIsoSetting)
        ''啟動 Setting
        pSettingVM.LoadFromIsoSetting(mIsoSetting)

        ''check the selected serach parameter is Favorite or not
        Me.pSESchVM.pIsMyFavorite = Me.IsCurrentSESearchExistInMyFvList()
        clsDatabaseIO.GetTheLatestDayInXmlFileFolder(Me.pSettingVM.pDateOfLatestTable)


        Me.IsDataLoaded = True

        RaiseEvent LoadingComplete()
    End Sub 
    Public Sub LoadData_UIThread()

        ''讀取時刻表xml
        clsDatabaseIO.LoadXmlFromIsoStorage_SingleThread()

        ''啟動 SE search 
        pSESchVM.LoadData(mIsoSetting)
        ''啟動 Location Search 
        pLocSchVM.LoadData(mIsoSetting)
        ''啟動 My Favorite
        pMyFavoriteVM.LoadFromIsoSetting(mIsoSetting)
        ''啟動 NoNuke
        pNoNukeVM.LoadFromIsoSetting(mIsoSetting)
        ''啟動 Setting
        pSettingVM.LoadFromIsoSetting(mIsoSetting)

        ''check the selected serach parameter is Favorite or not
        Me.pSESchVM.pIsMyFavorite = Me.IsCurrentSESearchExistInMyFvList()
        clsDatabaseIO.GetTheLatestDayInXmlFileFolder(Me.pSettingVM.mDateOfLatestTable)


        Me.IsDataLoaded = True

        RaiseEvent LoadingComplete()
    End Sub

#Region "儲存/讀取 UI設定"
    Public Sub SaveToIsoSetting()
        pSESchVM.SaveToIsoSetting(mIsoSetting)
        pLocSchVM.SaveToIsoSetting(mIsoSetting)
        pMyFavoriteVM.SaveToIsoSetting(mIsoSetting)
        pNoNukeVM.SaveToIsoSetting(mIsoSetting)
        pSettingVM.SaveToIsoSetting(mIsoSetting)
    End Sub
#End Region

#Region "Edit My Favorite"
    Public Function IsCurrentSESearchExistInMyFvList() As Boolean
        If pMyFavoriteVM Is Nothing OrElse pMyFavoriteVM.pFavoriteItems Is Nothing OrElse pMyFavoriteVM.pFavoriteItems.Count = 0 Then Return False

        For i As Integer = 0 To pMyFavoriteVM.pFavoriteItems.Count - 1
            If pSESchVM.Equals(pMyFavoriteVM.pFavoriteItems.Item(i)) Then Return True
        Next

        Return False
    End Function

    Public Sub AddCurrentSESearchIntoMyFavorite()
        pMyFavoriteVM.AddCurrentSESearchIntoMyFavorite(pSESchVM)
    End Sub

    Public Sub RemoveSESearchFromMyFavorite()
        pMyFavoriteVM.RemoveSESearchFromMyFavorite(pSESchVM)
    End Sub

#End Region



    Public Property IsDataLoaded As Boolean

    'Public Sub UpdatemStartSelectedStList(tStGrpChID As String)
    '    mStartSelectedStList.Clear()
    '    For i As Integer = 0 To gStGrpList.Count - 1
    '        If gStGrpList.Item(i).mID = tStGrpChID Then
    '            For j As Integer = 0 To mStartStGrpList.Item(i).mStation.Count - 1
    '                mStartSelectedStList.Add(clsStation.GetStationByChName(mStartStGrpList.Item(i).mStation.Item(j).mChName))
    '            Next
    '        End If
    '    Next
    '    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("mStartSelectedStList"))
    'End Sub
    'Public Sub UpdatemEndSelectedStList(tStGrpChID As String)
    '    mEndSelectedStList.Clear()
    '    For i As Integer = 0 To gStGrpList.Count - 1
    '        If gStGrpList.Item(i).mID = tStGrpChID Then
    '            For j As Integer = 0 To mEndStGrpList.Item(i).mStation.Count - 1
    '                mEndSelectedStList.Add(clsStation.GetStationByChName(mEndStGrpList.Item(i).mStation.Item(j).mChName))
    '            Next
    '        End If
    '    Next
    '    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("mEndSelectedStList"))
    'End Sub
    'Public Sub UpdatemStartDate(tDateString As String)
    '    mStartDate = tDateString

    'End Sub

    'Public mScrollTo As vmTrain

    'Public Sub UpdateByFromSt(tFromSt As clsStation)
    '    If tFromSt Is Nothing Then Exit Sub
    '    mFromSt = tFromSt
    '    Dim _ResultTrain As List(Of clsTrain)
    '    If mEndSt IsNot Nothing Then
    '        _ResultTrain = clsTrain.GetTrainsByFromEnd(gSchedule.Item(0), mFromSt, mEndSt)
    '    Else
    '        _ResultTrain = clsTrain.GetTrainsByFromSt(gSchedule.Item(0), mFromSt)
    '    End If
    '    If _ResultTrain Is Nothing OrElse _ResultTrain.Count = 0 Then Exit Sub

    '    mSelectedTrainList.Clear()
    '    For i As Integer = 0 To _ResultTrain.Count - 1
    '        If mEndSt IsNot Nothing Then
    '            mSelectedTrainList.Add(New vmTrain(_ResultTrain.Item(i), mFromSt, mEndSt))
    '        Else
    '            mSelectedTrainList.Add(New vmTrain(_ResultTrain.Item(i), mFromSt))
    '        End If
    '    Next
    '    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("mSelectedTrainList"))

    '    mScrollTo = mSelectedTrainList.Item(4)
    'End Sub

    'Public Sub UpdateByEndSt(tEndSt As clsStation)
    '    If tEndSt Is Nothing Then Exit Sub
    '    mEndSt = tEndSt
    '    Dim _ResultTrain As List(Of clsTrain)
    '    If mFromSt Is Nothing Then
    '        Exit Sub
    '    Else
    '        _ResultTrain = clsTrain.GetTrainsByFromEnd(gSchedule.Item(0), mFromSt, mEndSt)
    '    End If
    '    If _ResultTrain Is Nothing OrElse _ResultTrain.Count = 0 Then Exit Sub

    '    mSelectedTrainList.Clear()
    '    For i As Integer = 0 To _ResultTrain.Count - 1
    '        mSelectedTrainList.Add(New vmTrain(_ResultTrain.Item(i), mFromSt, mEndSt))
    '    Next
    '    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("mSelectedTrainList"))

    '    mScrollTo = mSelectedTrainList.Item(4)
    'End Sub

    Public Sub UpdateSelectedTrainList()
        '      For i As Integer =  0 to 
    End Sub

    Public Sub test()
        Dim a As Integer = 1
    End Sub


    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    'Private Sub NotifyPropertyChanged(ByVal propertyName As String)
    '    Dim handler As PropertyChangedEventHandler = PropertyChangedEvent
    '    If Nothing IsNot handler Then
    '        handler(Me, New PropertyChangedEventArgs(propertyName))
    '    End If
    'End Sub
End Class



