Imports System.Windows.Data

Partial Public Class pgFavorite
    Inherits PhoneApplicationPage

    Public Property pSESearch As vmStartEndSearch

    Public mInitializeState As Boolean = True
    Public mLoaded As Boolean = False
      
    Public Sub New()
        InitializeComponent() 
    End Sub

    Protected Overrides Sub OnNavigatedTo(e As NavigationEventArgs)
        If mLoaded = True Then Exit Sub

        pSESearch = GlobalVariables.gFavoriteToShow

        pSESearch.InitializeTimeTable()
        pSESearch.InitializeSETime()  
        pSESearch.SetTicketPrice()

        Me.DataContext = pSESearch

        mLoaded = True
        CType(DataContext, vmStartEndSearch).UpdateTrainList()
        SESearch_ScrollToCloestTrain()
    End Sub
     


    Private Sub pgFavorite_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        mInitializeState = False
    End Sub

    Private Sub abtnLocSchExchange_Click(sender As Object, e As EventArgs)

    End Sub
    Private Sub btShowDetail_Click(sender As Object, e As RoutedEventArgs)
        If sender.GetType.Equals(GetType(MenuItem)) Then
            GlobalVariables.gTrainToShow = CType(CType(sender, MenuItem).DataContext, clsTrain)
        End If

        If sender.GetType.Equals(GetType(Button)) Then
            GlobalVariables.gTrainToShow = CType(CType(sender, Button).DataContext, clsTrain)
        End If

        NavigationService.Navigate(New Uri("/pgTrainDetail.xaml", UriKind.RelativeOrAbsolute))
    End Sub

    Private Sub SESearch_ScrollToCloestTrain()
        Dim _ScrollTarget As clsTrainTimeInfo = CType(DataContext, vmStartEndSearch).GetCloestTrain
        If _ScrollTarget Is Nothing Then Exit Sub
        Try
            llsTickets.ScrollTo(_ScrollTarget)
        Catch ex As Exception 
        End Try 
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

    Private Sub tpSESearchStart_ValueChanged(sender As Object, e As DateTimeValueChangedEventArgs)
        If mInitializeState = True Then Exit Sub
        Dim _OneHour As TimeSpan = New TimeSpan(1, 0, 0)

        pSESearch.pDepTime_S = CType(sender, TimePicker).Value
        If tpSESearchEnd.Value - tpSESearchStart.Value < _OneHour Then
            ' pSESearch.pDepTime_E = CType(Me.DataContext, vmMain).pSESchVM.pDepTime_S.AddHours(1)
            tpSESearchEnd.Value = pSESearch.pDepTime_S.AddHours(1)
        End If

        SESearch_TimeChanged()
        SESearch_ScrollToCloestTrain()
    End Sub

    Private Sub tpSESearchEnd_ValueChanged(sender As Object, e As DateTimeValueChangedEventArgs)
        If mInitializeState = True Then Exit Sub
        Dim _OneHour As TimeSpan = New TimeSpan(1, 0, 0)

        pSESearch.pDepTime_E = CType(sender, TimePicker).Value
        If tpSESearchEnd.Value - tpSESearchStart.Value < _OneHour Then
            ' pSESearch.pDepTime_S = CType(Me.DataContext, vmMain).pSESchVM.pDepTime_E.AddHours(-1) 
            tpSESearchStart.Value = pSESearch.pDepTime_E.AddHours(-1)
        End If

        SESearch_TimeChanged() 
        SESearch_ScrollToCloestTrain()
    End Sub

    Private Sub SESearch_TimeChanged() 
        pSESearch.UpdateTrainList()
        llsTickets.SelectedItem = Nothing
        SESearch_ScrollToCloestTrain()
    End Sub

    Private Sub abtnExhange_Click(sender As Object, e As EventArgs)
        Dim _tempSt As clsStation = CType(DataContext, vmStartEndSearch).mStation_S
        CType(DataContext, vmStartEndSearch).pStation_S = CType(DataContext, vmStartEndSearch).mStation_E
        CType(DataContext, vmStartEndSearch).pStation_E = _tempSt
        CType(DataContext, vmStartEndSearch).UpdateTrainList()
        SESearch_ScrollToCloestTrain()
    End Sub

    Private Sub lpSESearchTimeTable_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        If mInitializeState = True Then Exit Sub

        CType(DataContext, vmStartEndSearch).pSelectedTimeTable = lpSESearchTimeTable.SelectedItem
        If CType(DataContext, vmStartEndSearch).pSelectedTimeTable.mIsLoaded = False Then CType(DataContext, vmStartEndSearch).pSelectedTimeTable.LoadTimeTable()

        CType(DataContext, vmStartEndSearch).UpdateTrainList()
        SESearch_ScrollToCloestTrain()
    End Sub
End Class

 
