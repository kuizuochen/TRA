Imports System
Imports System.Threading
Imports System.Windows.Controls
Imports Microsoft.Phone.Controls
Imports Microsoft.Phone.Shell
Imports System.Windows.Data
Imports TRA_03.GlobalVariables
Imports Microsoft.Phone.Tasks
Imports System.IO.IsolatedStorage
Imports Microsoft.Phone.BackgroundTransfer
Imports System.ComponentModel
Imports System.Net.NetworkInformation
Imports Windows.Devices.Geolocation

Partial Public Class MainPage
    Inherits PhoneApplicationPage


    Public mInitializeState As Boolean = True

    ''Menu Button
    Private abtnSESearchRefresh As ApplicationBarIconButton
    Private abtnSESearchUpdateStatus As ApplicationBarIconButton
    Private abtnSESearchExchange As ApplicationBarIconButton
    Private abtnLocSchRefresh As ApplicationBarIconButton
    Private abtnLocSchUpdateStatus As ApplicationBarIconButton
    Private abtnMyFavoriteRefresh As ApplicationBarIconButton
    Private abtnMyFavoriteDel As ApplicationBarIconButton
    Private abtnGo2GP As ApplicationBarIconButton
    Private abtnSettingCheck As ApplicationBarIconButton

    Private SESearchTrainListUpdate_Pause As Boolean = False
    Private SESearchTrainListUpdate_SwitchPause As Boolean = False


    '' Location Search 
    Private LocSchTrainListUpdate_Pause As Boolean = False
    Private LocSchTrainListUpdate_Geo As Boolean = False


    ''for update
    Public mCurrentRequest As BackgroundTransferRequest = Nothing
    Private mBWorkerDecompression As BackgroundWorker = New BackgroundWorker

    ''No Nuke Page 
    Private mShowNoNukePop As Boolean = True
    ' Constructor
    Public Sub New()
        InitializeComponent()

        ' Set the data context of the listbox control to the sample data
        DataContext = App.ViewModel

        abtnSESearchRefresh = New ApplicationBarIconButton(New Uri("Assets/Icons/rotate.png", UriKind.Relative))
        abtnSESearchRefresh.Text = "更新"
        AddHandler abtnSESearchRefresh.Click, AddressOf abtnSESearchRefresh_Click
        abtnSESearchUpdateStatus = New ApplicationBarIconButton(New Uri("Assets/Icons/magnify_down.png", UriKind.Relative))
        abtnSESearchUpdateStatus.Text = "查誤點"
        AddHandler abtnSESearchUpdateStatus.Click, AddressOf abtnSESearchUpdateStatus_Click
        abtnSESearchExchange = New ApplicationBarIconButton(New Uri("Assets/Icons/exchange.png", UriKind.Relative))
        abtnSESearchExchange.Text = "起訖交換"
        AddHandler abtnSESearchExchange.Click, AddressOf abtnSESearchExchange_Click

        abtnLocSchRefresh = New ApplicationBarIconButton(New Uri("Assets/Icons/rotate.png", UriKind.Relative))
        abtnLocSchRefresh.Text = "更新"
        AddHandler abtnLocSchRefresh.Click, AddressOf abtnLocSchRefresh_Click
        abtnLocSchUpdateStatus = New ApplicationBarIconButton(New Uri("Assets/Icons/magnify_down.png", UriKind.Relative))
        abtnLocSchUpdateStatus.Text = "查誤點"
        AddHandler abtnLocSchUpdateStatus.Click, AddressOf abtnLocSchUpdateStatus_Click

        abtnMyFavoriteRefresh = New ApplicationBarIconButton(New Uri("Assets/Icons/rotate.png", UriKind.Relative))
        abtnMyFavoriteRefresh.Text = "更新"
        AddHandler abtnMyFavoriteRefresh.Click, AddressOf abtnMyFavoriteRefresh_Click
        abtnMyFavoriteDel = New ApplicationBarIconButton(New Uri("Assets/Icons/del_heart.png", UriKind.Relative))
        abtnMyFavoriteDel.Text = "刪除"
        AddHandler abtnMyFavoriteDel.Click, AddressOf abtnMyFavoriteDelItem_Click

        abtnGo2GP = New ApplicationBarIconButton(New Uri("Assets/Icons/leaf.png", UriKind.Relative))
        abtnGo2GP.Text = "了解綠電"
        AddHandler abtnGo2GP.Click, AddressOf OpenGPWebPage

        abtnSettingCheck = New ApplicationBarIconButton(New Uri("Assets/Icons/one_check.png", UriKind.Relative))
        abtnSettingCheck.Text = "確定"
        AddHandler abtnSettingCheck.Click, AddressOf abtnSettingCheck_Click

    End Sub

    ' Load data for the ViewModel Items 
    Protected Overrides Sub OnNavigatedTo(e As NavigationEventArgs)
        Try
            ''若是從 pgLoading進入 則刪除紀錄
            If e.NavigationMode = NavigationMode.New AndAlso "/pgLoading.xaml" = NavigationService.BackStack.Last.Source.OriginalString Then
                NavigationService.RemoveBackEntry()
            End If
            ''若是從 pgStSelection進入 則 設定車站資訊 + 刪除紀錄
            If e.NavigationMode = NavigationMode.Back AndAlso gUpdateSE = True Then
                SE_ChangeStTo()
                NavigationService.RemoveBackEntry()
            End If
            ''若是從 pgStSelectionSingle進入 則 設定車站資訊 + 刪除紀錄
            If e.NavigationMode = NavigationMode.Back AndAlso gUpdateLocSearchSt = True Then
                LocationSearch_ChangeStationTo()
                NavigationService.RemoveBackEntry()
            End If
        Catch ex As Exception

        End Try

        If Not App.ViewModel.IsDataLoaded Then
            App.ViewModel.LoadData_UIThread()
        End If 

        ''確定是否要關閉 綠電頁面
        If CType(DataContext, vmMain).pSettingVM.pShowgp = False Then
            Try
                pvMain.Items.Remove(pvGP)
            Catch ex As Exception
            End Try
        End If
    End Sub
    Protected Overrides Sub OnNavigatedFrom(e As NavigationEventArgs)
        CType(DataContext, vmMain).SaveToIsoSetting()
    End Sub

    Private Sub Pivot_Loaded(sender As Object, e As RoutedEventArgs)
        mInitializeState = False

        SESearch_ScrollToCloestTrain()
        LocSch_ScrollToCloestTrain()

    End Sub


    Public Shared Sub GetItemsRecursive(Of T As DependencyObject)(parents As DependencyObject, ByRef objectList As List(Of T))
        Dim childrenCount = VisualTreeHelper.GetChildrenCount(parents)

        For i As Integer = 0 To childrenCount - 1
            Dim child = VisualTreeHelper.GetChild(parents, i)

            If TypeOf child Is T Then
                objectList.Add(TryCast(child, T))
            End If

            GetItemsRecursive(Of T)(child, objectList)
        Next
    End Sub

#Region "Green Power"
    Public Sub OpenGPWebPage()
        Dim webBrowserTask As WebBrowserTask = New WebBrowserTask()

        webBrowserTask.Uri = New Uri("http://greenpower.ltc.tw/", UriKind.Absolute)

        webBrowserTask.Show()
    End Sub
#End Region

 
#Region "My Favorite"
    Private Sub abtnMyFavoriteDelItem_Click(sender As Object, e As EventArgs)
        Try
            CType(DataContext, vmMain).pMyFavoriteVM.RemoveSESearchFromMyFavorite(CType(llMyFavorite.SelectedItem, vmStartEndSearch))
            CType(DataContext, vmMain).SaveToIsoSetting()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub abtnMyFavoriteRefresh_Click(sender As Object, e As EventArgs)
        CType(DataContext, vmMain).pMyFavoriteVM.UpdateAllCloest3Trains()
    End Sub

    Private Sub btMfShowDetail_Click(sender As Object, e As RoutedEventArgs)
        GlobalVariables.gFavoriteToShow = CType(llMyFavorite.SelectedItem, vmStartEndSearch).Clone
        NavigationService.Navigate(New Uri("/pgFavorite.xaml", UriKind.RelativeOrAbsolute))
    End Sub

    Private Sub llMyFavorite_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        Dim userControlList As New List(Of UserControl)()
        GetItemsRecursive(Of UserControl)(llMyFavorite, userControlList)

        ' Selected.
        If e.AddedItems.Count > 0 AndAlso e.AddedItems(0) IsNot Nothing Then
            For Each userControl As Controls.UserControl In userControlList
                If e.AddedItems(0).Equals(userControl.DataContext) Then
                    VisualStateManager.GoToState(userControl, "Selected", True)
                End If
            Next
        End If
        ' Unselected.
        If e.RemovedItems.Count > 0 AndAlso e.RemovedItems(0) IsNot Nothing Then
            For Each userControl As Controls.UserControl In userControlList
                If e.RemovedItems(0).Equals(userControl.DataContext) Then
                    VisualStateManager.GoToState(userControl, "Normal", True)
                End If
            Next
        End If
    End Sub

#End Region

#Region "Location Search"
    Private Sub abtnLocSchRefresh_Click(sender As Object, e As EventArgs)
        CType(Me.DataContext, vmMain).pLocSchVM.UpdateTrainList()
        LocSch_ScrollToCloestTrain()
    End Sub
    Private Sub abtnLocSchUpdateStatus_Click(sender As Object, e As EventArgs)
        Dim _CloestTrainsFW As List(Of clsTrainTimeInfo) = clsTrainTimeInfo.GetCloestTrains(CType(DataContext, vmMain).pLocSchVM.pTrainListFW, 2, 5, DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)
        For i As Integer = 0 To _CloestTrainsFW.Count - 1
            _CloestTrainsFW.Item(i).UpdateStatusOnline()
        Next

        Dim _CloestTrainsBW As List(Of clsTrainTimeInfo) = clsTrainTimeInfo.GetCloestTrains(CType(DataContext, vmMain).pLocSchVM.pTrainListBW, 2, 5, DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)
        For i As Integer = 0 To _CloestTrainsBW.Count - 1
            _CloestTrainsBW.Item(i).UpdateStatusOnline()
        Next
    End Sub
    Private Sub tbTwoLine_0_MouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs)
        bdTwoLine_0.BorderBrush = New SolidColorBrush(Color.FromArgb(255, 0, 162, 255))
        bdTwoLine_1.BorderBrush = New SolidColorBrush(Color.FromArgb(255, 144, 144, 144))

        CType(DataContext, vmMain).pLocSchVM.pShownListLineID = 0
        CType(DataContext, vmMain).pLocSchVM.SortTrainByLineClass()

        LocSch_ScrollToCloestTrain()
    End Sub
    Private Sub tbTwoLine_1_MouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs)
        bdTwoLine_0.BorderBrush = New SolidColorBrush(Color.FromArgb(255, 144, 144, 144))
        bdTwoLine_1.BorderBrush = New SolidColorBrush(Color.FromArgb(255, 0, 162, 255))

        CType(DataContext, vmMain).pLocSchVM.pShownListLineID = 1
        CType(DataContext, vmMain).pLocSchVM.SortTrainByLineClass()

        LocSch_ScrollToCloestTrain()
    End Sub
    Private Sub tbThreeLine_0_MouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs)
        bdThreeLine_0.BorderBrush = New SolidColorBrush(Color.FromArgb(255, 0, 162, 255))
        bdThreeLine_1.BorderBrush = New SolidColorBrush(Color.FromArgb(255, 144, 144, 144))
        bdThreeLine_2.BorderBrush = New SolidColorBrush(Color.FromArgb(255, 144, 144, 144))

        CType(DataContext, vmMain).pLocSchVM.pShownListLineID = 0
        CType(DataContext, vmMain).pLocSchVM.SortTrainByLineClass()

        LocSch_ScrollToCloestTrain()
    End Sub
    Private Sub tbThreeLine_1_MouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs)
        bdThreeLine_0.BorderBrush = New SolidColorBrush(Color.FromArgb(255, 144, 144, 144))
        bdThreeLine_1.BorderBrush = New SolidColorBrush(Color.FromArgb(255, 0, 162, 255))
        bdThreeLine_2.BorderBrush = New SolidColorBrush(Color.FromArgb(255, 144, 144, 144))

        CType(DataContext, vmMain).pLocSchVM.pShownListLineID = 1
        CType(DataContext, vmMain).pLocSchVM.SortTrainByLineClass()

        LocSch_ScrollToCloestTrain()
    End Sub
    Private Sub tbThreeLine_2_MouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs)
        bdThreeLine_0.BorderBrush = New SolidColorBrush(Color.FromArgb(255, 144, 144, 144))
        bdThreeLine_1.BorderBrush = New SolidColorBrush(Color.FromArgb(255, 144, 144, 144))
        bdThreeLine_2.BorderBrush = New SolidColorBrush(Color.FromArgb(255, 0, 162, 255))

        CType(DataContext, vmMain).pLocSchVM.pShownListLineID = 2
        CType(DataContext, vmMain).pLocSchVM.SortTrainByLineClass()

        LocSch_ScrollToCloestTrain()
    End Sub
    'Private Sub lpLocationSearchSt_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
    '    If mInitializeState = True Then Exit Sub
    '    If LocSchTrainListUpdate_Pause = True Then Exit Sub
    '    If LocSchTrainListUpdate_Geo = True Then Exit Sub

    '    CType(Me.DataContext, vmMain).pLocSchVM.pStation = lpLocationSearchSt.SelectedItem

    '    CType(Me.DataContext, vmMain).pLocSchVM.UpdateTrainList()
    '    LocSch_ScrollToCloestTrain()
    'End Sub
    'Private Sub lpLocationSearchStGrp_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
    '    If mInitializeState = True Then Exit Sub
    '    If LocSchTrainListUpdate_Geo = True Then Exit Sub
    '    CType(Me.DataContext, vmMain).pLocSchVM.pStGrp = lpLocationSearchStGrp.SelectedItem

    '    LocSchTrainListUpdate_Pause = True
    '    CType(Me.DataContext, vmMain).pLocSchVM.pStList.Clear()
    '    For i As Integer = 0 To CType(Me.DataContext, vmMain).pLocSchVM.pStGrp.mStation.Count - 1
    '        CType(Me.DataContext, vmMain).pLocSchVM.pStList.Add(CType(Me.DataContext, vmMain).pLocSchVM.pStGrp.mStation.Item(i))
    '    Next

    '    ''跳至 最常使用車站
    '    For i As Integer = 0 To CType(Me.DataContext, vmMain).pLocSchVM.pStGrp.mStation.Count - 1
    '        If CType(Me.DataContext, vmMain).pLocSchVM.pStGrp.mFirstSelectedStName = CType(Me.DataContext, vmMain).pLocSchVM.pStGrp.mStation.Item(i).mChName Then
    '            Try
    '                lpLocationSearchSt.SelectedIndex = i
    '            Catch ex As Exception

    '            End Try
    '        End If
    '    Next
    '    LocSchTrainListUpdate_Pause = False

    '    lpLocationSearchSt_SelectionChanged(Nothing, Nothing)
    'End Sub

    Private Sub LocationSearch_ChangeStationTo()
        CType(Me.DataContext, vmMain).pLocSchVM.pStGrp = gLocSearchStGrp
        CType(Me.DataContext, vmMain).pLocSchVM.pStation = gLocSearchSt
        CType(Me.DataContext, vmMain).pLocSchVM.UpdateTrainList()
        LocSch_ScrollToCloestTrain()
    End Sub

    Private Sub tbLocationSearch_MouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs)
        GlobalVariables.gUpdateLocSearchSt = False
        NavigationService.Navigate(New Uri("/pgStSelectionSingle.xaml?StGrp=" + CType(Me.DataContext, vmMain).pLocSchVM.pStGrp.mChName + "&St=" + CType(Me.DataContext, vmMain).pLocSchVM.pStation.mChName, UriKind.RelativeOrAbsolute))
    End Sub
    Private Sub LocSch_FillterCarClass(sender As Object, e As RoutedEventArgs)
        If mInitializeState = True Then Exit Sub
        If CType(sender, CheckBox).IsPressed = False Then Exit Sub

        CType(Me.DataContext, vmMain).pLocSchVM.pVisibility_PNTZ = ckFillterPN_Loc.IsChecked
        CType(Me.DataContext, vmMain).pLocSchVM.pVisibility_TZ = ckFillterTZ_Loc.IsChecked
        CType(Me.DataContext, vmMain).pLocSchVM.pVisibility_CK = ckFillterCK_Loc.IsChecked
        CType(Me.DataContext, vmMain).pLocSchVM.pVisibility_Local = ckFillterLocal_Loc.IsChecked

        CType(Me.DataContext, vmMain).pLocSchVM.FillterTrainByDepTimeClass()

        ''update is Favorite  
        llsLocSchFW.SelectedItem = Nothing
        llsLocSchBW.SelectedItem = Nothing

        LocSch_ScrollToCloestTrain()
    End Sub

    Private Sub LocSch_ScrollToCloestTrain()
        Try
            Dim _ScrollTargetFW As clsTrainTimeInfo = CType(Me.DataContext, vmMain).pLocSchVM.GetCloestTrain_FW
            If _ScrollTargetFW IsNot Nothing Then llsLocSchFW.ScrollTo(_ScrollTargetFW)

            Dim _ScrollTargetBW As clsTrainTimeInfo = CType(Me.DataContext, vmMain).pLocSchVM.GetCloestTrain_BW
            If _ScrollTargetBW IsNot Nothing Then llsLocSchBW.ScrollTo(_ScrollTargetBW)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub llsLocSchFW_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        Dim userControlList As New List(Of UserControl)()
        GetItemsRecursive(Of UserControl)(llsLocSchFW, userControlList)

        ' Selected.
        If e.AddedItems.Count > 0 AndAlso e.AddedItems(0) IsNot Nothing Then
            For Each userControl As Controls.UserControl In userControlList
                If e.AddedItems(0).Equals(userControl.DataContext) Then
                    VisualStateManager.GoToState(userControl, "Selected", True)
                End If
            Next
        End If
        ' Unselected.
        If e.RemovedItems.Count > 0 AndAlso e.RemovedItems(0) IsNot Nothing Then
            For Each userControl As Controls.UserControl In userControlList
                If e.RemovedItems(0).Equals(userControl.DataContext) Then
                    VisualStateManager.GoToState(userControl, "Normal", True)
                End If
            Next
        End If
    End Sub

    Private Sub llsLocSchBW_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        Dim userControlList As New List(Of UserControl)()
        GetItemsRecursive(Of UserControl)(llsLocSchBW, userControlList)

        ' Selected.
        If e.AddedItems.Count > 0 AndAlso e.AddedItems(0) IsNot Nothing Then
            For Each userControl As Controls.UserControl In userControlList
                If e.AddedItems(0).Equals(userControl.DataContext) Then
                    VisualStateManager.GoToState(userControl, "Selected", True)
                End If
            Next
        End If
        ' Unselected.
        If e.RemovedItems.Count > 0 AndAlso e.RemovedItems(0) IsNot Nothing Then
            For Each userControl As Controls.UserControl In userControlList
                If e.RemovedItems(0).Equals(userControl.DataContext) Then
                    VisualStateManager.GoToState(userControl, "Normal", True)
                End If
            Next
        End If
    End Sub

    Private Sub btnGetCloestSt_Click(sender As Object, e As RoutedEventArgs)
        pbGetGeoLoc.IsIndeterminate = True
        GetCurrentLoc()
    End Sub

    ''取得 Geolocation
    Private Async Sub GetCurrentLoc()

        Try
            Dim _CloestSt As clsStation
            Dim _Finder As New Geolocator
            Dim _CurrentLoc As Geoposition
            _Finder.DesiredAccuracyInMeters = UInt32.Parse(200)
            _Finder.DesiredAccuracy = PositionAccuracy.Default

            _CurrentLoc = Await _Finder.GetGeopositionAsync(TimeSpan.FromMinutes(1), TimeSpan.FromSeconds(30))
            _CloestSt = clsStation.GetCloestSt(gStList, _CurrentLoc)


            LocSchTrainListUpdate_Geo = True

            CType(Me.DataContext, vmMain).pLocSchVM.pStGrp = clsStGrp.GetRegion(CType(Me.DataContext, vmMain).pLocSchVM.pStGrpList, _CloestSt.mID)
            ' lpLocationSearchStGrp.SelectedItem = CType(Me.DataContext, vmMain).pLocSchVM.pStGrp

            CType(Me.DataContext, vmMain).pLocSchVM.pStList.Clear()
            For i As Integer = 0 To CType(Me.DataContext, vmMain).pLocSchVM.pStGrp.mStation.Count - 1
                CType(Me.DataContext, vmMain).pLocSchVM.pStList.Add(CType(Me.DataContext, vmMain).pLocSchVM.pStGrp.mStation.Item(i))
            Next

            For i As Integer = 0 To CType(Me.DataContext, vmMain).pLocSchVM.pStList.Count - 1
                If _CloestSt.mChName = CType(Me.DataContext, vmMain).pLocSchVM.pStList.Item(i).mChName Then
                    Try
                        '  lpLocationSearchSt.SelectedIndex = i
                    Catch ex As Exception

                    End Try
                End If
            Next

            LocSchTrainListUpdate_Geo = False
            '    lpLocationSearchSt_SelectionChanged(Nothing, Nothing)

        Catch ex As Exception

        End Try

        LocSchTrainListUpdate_Geo = False
        pbGetGeoLoc.IsIndeterminate = False
    End Sub
#End Region

#Region "Start End Search"
    ''Appication Bar Button 
    Private Sub abtnSESearchExchange_Click(sender As Object, e As EventArgs)
        SESearchTrainListUpdate_SwitchPause = True

        Dim _tempStGrp As clsStGrp = CType(DataContext, vmMain).pSESchVM.pStGrp_S
        Dim _tempStName As clsStation = CType(DataContext, vmMain).pSESchVM.pStation_S

        CType(DataContext, vmMain).pSESchVM.pStGrp_S = CType(DataContext, vmMain).pSESchVM.pStGrp_E
        CType(DataContext, vmMain).pSESchVM.pStation_S = CType(DataContext, vmMain).pSESchVM.pStation_E
        CType(DataContext, vmMain).pSESchVM.pStGrp_E = _tempStGrp
        CType(DataContext, vmMain).pSESchVM.pStation_E = _tempStName

        CType(DataContext, vmMain).pSESchVM.UpdateTrainList()
        SESearch_ScrollToCloestTrain()

        SESearchTrainListUpdate_SwitchPause = False
    End Sub
    Private Sub abtnSESearchRefresh_Click(sender As Object, e As EventArgs)
        CType(Me.DataContext, vmMain).pSESchVM.UpdateTrainList()
        SESearch_ScrollToCloestTrain()
    End Sub
    Private Sub abtnSESearchUpdateStatus_Click(sender As Object, e As EventArgs)
        Dim _CloestTrains As List(Of clsTrainTimeInfo) = clsTrainTimeInfo.GetCloestTrains(CType(DataContext, vmMain).pSESchVM.pTrainList, 2, 5, DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)
        For i As Integer = 0 To _CloestTrains.Count - 1
            _CloestTrains.Item(i).UpdateStatusOnline()
        Next
    End Sub

    Private Sub lpSESearchTimeTable_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        If mInitializeState = True Then Exit Sub

        CType(Me.DataContext, vmMain).pSESchVM.pSelectedTimeTable = lpSESearchTimeTable.SelectedItem
        If CType(Me.DataContext, vmMain).pSESchVM.pSelectedTimeTable.mIsLoaded = False Then CType(Me.DataContext, vmMain).pSESchVM.pSelectedTimeTable.LoadTimeTable()

        CType(Me.DataContext, vmMain).pSESchVM.UpdateTrainList()
        SESearch_ScrollToCloestTrain()
    End Sub
    'Private Sub lpSESearchStGrp_S_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
    '    If mInitializeState = True Then Exit Sub

    '    CType(Me.DataContext, vmMain).pSESchVM.pStGrp_S = lpSESearchStGrp_S.SelectedItem

    '    SESearchTrainListUpdate_Pause = True
    '    CType(Me.DataContext, vmMain).pSESchVM.pStList_S.Clear()
    '    For i As Integer = 0 To CType(Me.DataContext, vmMain).pSESchVM.pStGrp_S.mStation.Count - 1
    '        CType(Me.DataContext, vmMain).pSESchVM.pStList_S.Add(CType(Me.DataContext, vmMain).pSESchVM.pStGrp_S.mStation.Item(i))
    '    Next


    '    If SESearchTrainListUpdate_SwitchPause = True Then
    '        SESearchTrainListUpdate_Pause = False
    '        Exit Sub
    '    End If
    '    ''跳至 最常使用車站
    '    For i As Integer = 0 To CType(Me.DataContext, vmMain).pSESchVM.pStGrp_S.mStation.Count - 1
    '        If CType(Me.DataContext, vmMain).pSESchVM.pStGrp_S.mFirstSelectedStName = CType(Me.DataContext, vmMain).pSESchVM.pStGrp_S.mStation.Item(i).mChName Then
    '            Try
    '                lpSESearchSt_S.SelectedIndex = i
    '            Catch ex As Exception

    '            End Try
    '        End If
    '    Next
    '    SESearchTrainListUpdate_Pause = False

    '    lpSESearchSt_S_SelectionChanged(Nothing, Nothing)
    'End Sub
    'Private Sub lpSESearchStGrp_E_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
    '    If mInitializeState = True Then Exit Sub


    '    CType(Me.DataContext, vmMain).pSESchVM.pStGrp_E = lpSESearchStGrp_E.SelectedItem

    '    SESearchTrainListUpdate_Pause = True
    '    CType(Me.DataContext, vmMain).pSESchVM.pStList_E.Clear()
    '    For i As Integer = 0 To CType(Me.DataContext, vmMain).pSESchVM.pStGrp_E.mStation.Count - 1
    '        CType(Me.DataContext, vmMain).pSESchVM.pStList_E.Add(CType(Me.DataContext, vmMain).pSESchVM.pStGrp_E.mStation.Item(i))
    '    Next


    '    If SESearchTrainListUpdate_SwitchPause = True Then
    '        SESearchTrainListUpdate_Pause = False
    '        Exit Sub
    '    End If

    '    ''跳至 最常使用車站
    '    For i As Integer = 0 To CType(Me.DataContext, vmMain).pSESchVM.pStGrp_E.mStation.Count - 1
    '        If CType(Me.DataContext, vmMain).pSESchVM.pStGrp_E.mFirstSelectedStName = CType(Me.DataContext, vmMain).pSESchVM.pStGrp_E.mStation.Item(i).mChName Then
    '            Try
    '                lpSESearchSt_E.SelectedIndex = i
    '            Catch ex As Exception

    '            End Try
    '        End If
    '    Next
    '    SESearchTrainListUpdate_Pause = False

    '    lpSESearchSt_E_SelectionChanged(Nothing, Nothing)
    'End Sub
    'Private Sub lpSESearchSt_S_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
    '    ''unselect all
    '    llsTickets.SelectedItem = Nothing
    '    If mInitializeState = True Then Exit Sub
    '    If SESearchTrainListUpdate_Pause = True Then Exit Sub


    '    '' lpSESearchSt_S.clear 時 SelectedItem is nothing 
    '    If lpSESearchSt_S.SelectedItem Is Nothing Then Exit Sub

    '    ''reset search variable
    '    CType(Me.DataContext, vmMain).pSESchVM.pStation_S = lpSESearchSt_S.SelectedItem

    '    ''update is Favorite
    '    CType(Me.DataContext, vmMain).pSESchVM.pIsMyFavorite = CType(Me.DataContext, vmMain).IsCurrentSESearchExistInMyFvList()

    '    ''update search result
    '    If SESearchTrainListUpdate_SwitchPause = True Then Exit Sub
    '    CType(Me.DataContext, vmMain).pSESchVM.UpdateTrainList()
    '    SESearch_ScrollToCloestTrain()
    'End Sub

    'Private Sub lpSESearchSt_E_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
    '    ''unselect all
    '    llsTickets.SelectedItem = Nothing
    '    If mInitializeState = True Then Exit Sub
    '    If SESearchTrainListUpdate_Pause = True Then Exit Sub

    '    If lpSESearchSt_E.SelectedItem Is Nothing Then Exit Sub

    '    ''reset search variable
    '    CType(Me.DataContext, vmMain).pSESchVM.pStation_E = lpSESearchSt_E.SelectedItem

    '    ''update is Favorite
    '    CType(Me.DataContext, vmMain).pSESchVM.pIsMyFavorite = CType(Me.DataContext, vmMain).IsCurrentSESearchExistInMyFvList()

    '    ''update search result
    '    If SESearchTrainListUpdate_SwitchPause = True Then Exit Sub
    '    CType(Me.DataContext, vmMain).pSESchVM.UpdateTrainList()
    '    SESearch_ScrollToCloestTrain()
    'End Sub

    Private Sub SE_ChangeStTo()
        CType(Me.DataContext, vmMain).pSESchVM.pStGrp_S = gSEStGrp_Start
        CType(Me.DataContext, vmMain).pSESchVM.pStation_S = gSESt_Start
        CType(Me.DataContext, vmMain).pSESchVM.pStGrp_E = gSEStGrp_End
        CType(Me.DataContext, vmMain).pSESchVM.pStation_E = gSESt_End
        CType(Me.DataContext, vmMain).pSESchVM.UpdateTrainList()
        SESearch_ScrollToCloestTrain()
    End Sub


    Private Sub tbSE_MouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs)
        GlobalVariables.gUpdateLocSearchSt = False
        NavigationService.Navigate(New Uri("/pgStSelection.xaml?StGrpStart=" + CType(Me.DataContext, vmMain).pSESchVM.pStGrp_S.mChName + "&StStart=" + CType(Me.DataContext, vmMain).pSESchVM.pStation_S.mChName _
                                           + "&StGrpEnd=" + CType(Me.DataContext, vmMain).pSESchVM.pStGrp_E.mChName + "&StEnd=" + CType(Me.DataContext, vmMain).pSESchVM.pStation_E.mChName, UriKind.RelativeOrAbsolute))

    End Sub

    Private Sub SESearch_ScrollToCloestTrain()
        Try
            CType(Me.DataContext, vmMain).pSESchVM.pIsMyFavorite = CType(Me.DataContext, vmMain).IsCurrentSESearchExistInMyFvList()
            Dim _ScrollTarget As clsTrainTimeInfo = CType(Me.DataContext, vmMain).pSESchVM.GetCloestTrain
            If _ScrollTarget IsNot Nothing Then llsTickets.ScrollTo(_ScrollTarget)
        Catch ex As Exception
        End Try
    End Sub

    ''分級顯示 自強 莒光 區間 
    Private Sub SESearch_FillterCarClass(sender As Object, e As RoutedEventArgs)
        If mInitializeState = True Then Exit Sub
        If CType(sender, CheckBox).IsPressed = False Then Exit Sub

        CType(Me.DataContext, vmMain).pSESchVM.pVisibility_PNTZ = ckFillterPN.IsChecked
        CType(Me.DataContext, vmMain).pSESchVM.pVisibility_TZ = ckFillterTZ.IsChecked
        CType(Me.DataContext, vmMain).pSESchVM.pVisibility_CK = ckFillterCK.IsChecked
        CType(Me.DataContext, vmMain).pSESchVM.pVisibility_Local = ckFillterLocal.IsChecked

        CType(Me.DataContext, vmMain).pSESchVM.FillterTrainByDepTimeClass()

        ''update is Favorite
        '  CType(Me.DataContext, vmMain).pSESchVM.pIsMyFavorite = CType(Me.DataContext, vmMain).IsCurrentSESearchExistInMyFvList()

        llsTickets.SelectedItem = Nothing
        SESearch_ScrollToCloestTrain()
    End Sub
    Private Sub SESearch_TimeChanged()
        If mInitializeState = True Then Exit Sub

        CType(Me.DataContext, vmMain).pSESchVM.UpdateTrainList()
        llsTickets.SelectedItem = Nothing
        SESearch_ScrollToCloestTrain()
    End Sub
    Private Sub tpSESearchStart_ValueChanged(sender As Object, e As DateTimeValueChangedEventArgs)
        If mInitializeState = True Then Exit Sub
        Dim _OneHour As TimeSpan = New TimeSpan(1, 0, 0)

        CType(Me.DataContext, vmMain).pSESchVM.pDepTime_S = CType(sender, TimePicker).Value
        If tpSESearchEnd.Value - tpSESearchStart.Value < _OneHour Then
            '   CType(Me.DataContext, vmMain).pSESchVM.pDepTime_E = CType(Me.DataContext, vmMain).pSESchVM.pDepTime_S.AddHours(1)
            tpSESearchEnd.Value = CType(Me.DataContext, vmMain).pSESchVM.pDepTime_S.AddHours(1)
        End If

        SESearch_TimeChanged()
    End Sub

    Private Sub tpSESearchEnd_ValueChanged(sender As Object, e As DateTimeValueChangedEventArgs)
        If mInitializeState = True Then Exit Sub
        Dim _OneHour As TimeSpan = New TimeSpan(1, 0, 0)

        CType(Me.DataContext, vmMain).pSESchVM.pDepTime_E = CType(sender, TimePicker).Value
        If tpSESearchEnd.Value - tpSESearchStart.Value < _OneHour Then
            ' CType(Me.DataContext, vmMain).pSESchVM.pDepTime_S = CType(Me.DataContext, vmMain).pSESchVM.pDepTime_E.AddHours(-1)
            tpSESearchStart.Value = CType(Me.DataContext, vmMain).pSESchVM.pDepTime_E.AddHours(-1)
        End If

        SESearch_TimeChanged()
    End Sub

    Private Sub cvFavorite_MouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs)
        If CType(Me.DataContext, vmMain).pSESchVM.pIsMyFavorite Then
            CType(Me.DataContext, vmMain).RemoveSESearchFromMyFavorite()
            CType(Me.DataContext, vmMain).pSESchVM.pIsMyFavorite = False
            CType(DataContext, vmMain).SaveToIsoSetting()
        Else
            CType(Me.DataContext, vmMain).AddCurrentSESearchIntoMyFavorite()
            CType(Me.DataContext, vmMain).pSESchVM.pIsMyFavorite = True
            CType(DataContext, vmMain).SaveToIsoSetting()
        End If
    End Sub
    Private Sub miShowTrain_Click(sender As Object, e As RoutedEventArgs)
        If sender.GetType.Equals(GetType(MenuItem)) Then
            gTrainToShow = CType(CType(sender, MenuItem).DataContext, clsTrain)
        End If

        If sender.GetType.Equals(GetType(Button)) Then
            gTrainToShow = CType(CType(sender, Button).DataContext, clsTrain)
        End If

        NavigationService.Navigate(New Uri("/pgTrainDetail.xaml", UriKind.RelativeOrAbsolute))
    End Sub
    Private Sub llsTickets_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        Dim userControlList As New List(Of UserControl)()
        GetItemsRecursive(Of UserControl)(llsTickets, userControlList)

        ' Selected.
        If e.AddedItems.Count > 0 AndAlso e.AddedItems(0) IsNot Nothing Then
            For Each userControl As Controls.UserControl In userControlList
                If e.AddedItems(0).Equals(userControl.DataContext) Then
                    VisualStateManager.GoToState(userControl, "Selected", True)
                End If
            Next
        End If
        ' Unselected.
        If e.RemovedItems.Count > 0 AndAlso e.RemovedItems(0) IsNot Nothing Then
            For Each userControl As Controls.UserControl In userControlList
                If e.RemovedItems(0).Equals(userControl.DataContext) Then
                    VisualStateManager.GoToState(userControl, "Normal", True)
                End If
            Next
        End If
    End Sub

#End Region

#Region "Main Pivot"
    ''切換頁面時 每一個 longlist selector 都切換成 無選擇狀態
    Private Sub SetSelectNothing_AllLongListSelector()
        Try
            llMyFavorite.SelectedItem = Nothing
            llsTickets.SelectedItem = Nothing
            llsLocSchFW.SelectedItem = Nothing
            llsLocSchBW.SelectedItem = Nothing
        Catch ex As Exception

        End Try
    End Sub
    Private Sub Pivot_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        ''   If mInitializeState = True Then Exit Sub 
        ''     If True Then Exit Sub
        Me.ApplicationBar.Buttons.Clear()

        Dim _ChangeColorToGP As Boolean = False

        If CType(sender, Pivot).SelectedItem.Equals(pvSESearch) Then
            Me.ApplicationBar.Buttons.Add(abtnSESearchUpdateStatus)
            Me.ApplicationBar.Buttons.Add(abtnSESearchRefresh)
            Me.ApplicationBar.Buttons.Add(abtnSESearchExchange)
            SESearch_ScrollToCloestTrain()
        ElseIf CType(sender, Pivot).SelectedItem.Equals(pvLocSch) Then
            Me.ApplicationBar.Buttons.Add(abtnLocSchUpdateStatus)
            Me.ApplicationBar.Buttons.Add(abtnLocSchRefresh)
            LocSch_ScrollToCloestTrain()
        ElseIf CType(sender, Pivot).SelectedItem.Equals(pvMyFavorite) Then
            Me.ApplicationBar.Buttons.Add(abtnMyFavoriteRefresh)
            Me.ApplicationBar.Buttons.Add(abtnMyFavoriteDel)
            CType(DataContext, vmMain).pMyFavoriteVM.UpdateAllCloest3Trains()
        ElseIf CType(sender, Pivot).SelectedItem.Equals(pvGP) Then
            Me.ApplicationBar.Buttons.Add(abtnGo2GP)
            _ChangeColorToGP = True
        ElseIf CType(sender, Pivot).SelectedItem.Equals(pvSetting) Then
            Me.ApplicationBar.Buttons.Add(abtnSettingCheck)
        End If

        ''反選 所有 longlistselector 
        SetSelectNothing_AllLongListSelector()

        Dim _PivotBgBrush As SolidColorBrush = Me.Resources("SceneBgColor")
        If _PivotBgBrush.Color = Me.Resources("LightGray") And _ChangeColorToGP = True Then
            _PivotBgBrush.Color = Me.Resources("GreenPowerGreen")
        ElseIf _PivotBgBrush.Color = Me.Resources("GreenPowerGreen") And _ChangeColorToGP = False Then
            _PivotBgBrush.Color = Me.Resources("LightGray")
        End If

    End Sub
#End Region

#Region "Setting"

    Private Sub tsGP_Click(sender As Object, e As RoutedEventArgs)

    End Sub
    Private Sub btnReopenOk_Click(sender As Object, e As RoutedEventArgs)
        popReopenOK.IsOpen = False
    End Sub
    Private Sub btnShowUpdatePop_Click(sender As Object, e As RoutedEventArgs)
        popDoUpdate.IsOpen = True
        btnUpdateCancel.IsEnabled = True
        pbDoingUpdate.IsIndeterminate = False
    End Sub
    Private Sub btnUpdateOk_Click(sender As Object, e As RoutedEventArgs)
        If NetworkInterface.GetIsNetworkAvailable() = True Then
            tbPopUpdateStatus.Text = "開始更新"
            pbDoingUpdate.IsIndeterminate = True
            UpdateProcedure()
        Else
            tbPopUpdateStatus.Text = "無法連線至網路"
        End If

    End Sub


    Private Sub btnUpdateCancel_Click(sender As Object, e As RoutedEventArgs)
        Try
            popDoUpdate.IsOpen = False
            clsDatabaseIO.CancelDownloadZipFile(mCurrentRequest)
        Catch ex As Exception

        End Try

    End Sub

    Private Sub rbPic_Checked(sender As Object, e As RoutedEventArgs)
        If mInitializeState = True Then Exit Sub

        Dim NewTile As New FlipTileData
        With NewTile
            If rbFirstPic_2.IsChecked = True Then
                .SmallBackgroundImage = New Uri("/Assets/Tiles/icon_159_2.png", UriKind.Relative)
            Else
                .SmallBackgroundImage = New Uri("/Assets/Tiles/icon_159.png", UriKind.Relative)
            End If

            If rbFirstPic_1.IsChecked = True Then
                .BackgroundImage = New Uri("/Assets/Tiles/icon336_2.png", UriKind.Relative)
            Else
                .BackgroundImage = New Uri("/Assets/Tiles/icon336.png", UriKind.Relative)
            End If

            If rbFirstPic.IsChecked = True Then
                .WideBackgroundImage = New Uri("/Assets/Tiles/NoNukeFlag_691_336.jpg", UriKind.Relative)
            Else
                .WideBackgroundImage = New Uri("/Assets/NoNuke/NoNukeYellow_691_336.jpg", UriKind.Relative)
            End If
        End With

        ShellTile.ActiveTiles.FirstOrDefault.Update(NewTile)
    End Sub

    Private Sub abtnSettingCheck_Click(sender As Object, e As EventArgs)
        If mInitializeState = True Then Exit Sub

        Dim NewTile As New FlipTileData
        With NewTile
            If rbFirstPic_2.IsChecked = True Then
                .SmallBackgroundImage = New Uri("/Assets/Tiles/icon_159_2.png", UriKind.Relative)
            Else
                .SmallBackgroundImage = New Uri("/Assets/Tiles/icon_159.png", UriKind.Relative)
            End If

            If rbFirstPic_1.IsChecked = True Then
                .BackgroundImage = New Uri("/Assets/Tiles/icon336_2.png", UriKind.Relative)
            Else
                .BackgroundImage = New Uri("/Assets/Tiles/icon336.png", UriKind.Relative)
            End If

            If rbFirstPic.IsChecked = True Then
                .WideBackgroundImage = New Uri("/Assets/Tiles/NoNukeFlag_691_336.jpg", UriKind.Relative)
            Else
                .WideBackgroundImage = New Uri("/Assets/NoNuke/NoNukeYellow_691_336.jpg", UriKind.Relative)
            End If
        End With

        ShellTile.ActiveTiles.FirstOrDefault.Update(NewTile)
    End Sub


    Private Sub imgFirst_MouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs)
        rbFirstPic.IsChecked = True
    End Sub

    Private Sub imgSecond_MouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs)
        rbSecondPic.IsChecked = True
    End Sub
    Private Sub imgFirst_1_MouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs)
        rbFirstPic_1.IsChecked = True
    End Sub

    Private Sub imgSecond_1_MouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs)
        rbSecondPic_1.IsChecked = True
    End Sub

    Private Sub imgFirst_2_MouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs)
        rbFirstPic_2.IsChecked = True
    End Sub

    Private Sub imgSecond_2_MouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs)
        rbSecondPic_2.IsChecked = True
    End Sub
#Region "Update"
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
    Private Sub DeleteZipTempFile()
        Dim _FileStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
        If _FileStore.FileExists(GlobalVariables.gZipTimeTableFileTempPath) Then
            _FileStore.DeleteFile(GlobalVariables.gZipTimeTableFileTempPath)
        End If
    End Sub
    Private Sub DownloadZipFile_Start()
        clsDatabaseIO.DownloadFile(GlobalVariables.gZipTimeTableAddress, GlobalVariables.gZipTimeTableFileTempPath, mCurrentRequest, AddressOf DownloadZipFile_PbUpdate, AddressOf DownloadZipFile_Completed)
    End Sub
    Public Sub DownloadZipFile_PbUpdate(sender As Object, e As BackgroundTransferEventArgs)
        If mCurrentRequest.TotalBytesToReceive.ToString = -1 Then
            clsDatabaseIO.CancelDownloadZipFile(mCurrentRequest)
            Exit Sub
        End If
        tbPopUpdateStatus.Text = "正在下載檔案 " + "(" + mCurrentRequest.BytesReceived.ToString + "/" + mCurrentRequest.TotalBytesToReceive.ToString + ")"
    End Sub
    Public Sub DownloadZipFile_Completed(sender As Object, e As BackgroundTransferEventArgs)
        If e.Request.TotalBytesToReceive < 0 Then
            tbPopUpdateStatus.Text = "無法連接網路"
            Exit Sub
        End If
        If e.Request.BytesReceived < e.Request.TotalBytesToReceive Or Not IsolatedStorageFile.GetUserStoreForApplication.FileExists(GlobalVariables.gZipTimeTableFileTempPath) Then
            tbPopUpdateStatus.Text = "下載中斷"
            Exit Sub
        End If

        ''將下載完成的zip file 移動至 正確位置
        If MoveZipFile() Then
            DeleteZipTempFile()
            ''啟動解壓縮
            DeCompressionBgWorker_Start()
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
    Private Sub DeCompressionBgWorker_Start()
        tbPopUpdateStatus.Text = "正在進行解壓縮"
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
        tbPopUpdateStatus.Text = "重新讀取列車資料"
        ReReadXML()
    End Sub
    Private Sub ReReadXML()
        CType(DataContext, vmMain).AddMoreXmlFromIsoStorage()
        popDoUpdate.IsOpen = False
    End Sub
#End Region
#End Region






End Class

Public Class BooleanVisiblityConv
    Implements IValueConverter

    Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As Globalization.CultureInfo) As Object Implements IValueConverter.Convert
        If CBool(value) = True Then
            Return Visibility.Visible
        Else
            Return Visibility.Collapsed
        End If
    End Function

    Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
        Throw New NotImplementedException()
    End Function
End Class
Public Class RvsBooleanVisiblityConv
    Implements IValueConverter

    Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As Globalization.CultureInfo) As Object Implements IValueConverter.Convert
        If CBool(value) = True Then
            Return Visibility.Collapsed
        Else
            Return Visibility.Visible
        End If
    End Function

    Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
        Throw New NotImplementedException()
    End Function
End Class
Public Class OneVisiblityConv
    Implements IValueConverter

    Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As Globalization.CultureInfo) As Object Implements IValueConverter.Convert
        If CInt(value) = 1 Or CInt(value) = 0 Then
            Return Visibility.Visible
        Else
            Return Visibility.Collapsed
        End If
    End Function

    Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
        Throw New NotImplementedException()
    End Function
End Class
Public Class TwoVisiblityConv
    Implements IValueConverter

    Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As Globalization.CultureInfo) As Object Implements IValueConverter.Convert
        If CInt(value) = 2 Then
            Return Visibility.Visible
        Else
            Return Visibility.Collapsed
        End If
    End Function

    Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
        Throw New NotImplementedException()
    End Function
End Class
Public Class ThreeVisiblityConv
    Implements IValueConverter

    Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As Globalization.CultureInfo) As Object Implements IValueConverter.Convert
        If CInt(value) = 3 Then
            Return Visibility.Visible
        Else
            Return Visibility.Collapsed
        End If
    End Function

    Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
        Throw New NotImplementedException()
    End Function
End Class