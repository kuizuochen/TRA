Imports Microsoft.Phone.Controls.Primitives

Partial Public Class pgStSelectionSingle
    Inherits PhoneApplicationPage

    Public mSelectedStGrp As String
    Public mSelectedSt As String

    Private mLoading As Boolean = True

    Public mStringListStGrp As List(Of String) = New List(Of String) From {"臺北區", "桃園區", "新竹區", "苗栗區", "臺中區", "彰化區", "南投區", "雲林區", "嘉義區", "臺南區", "高雄區", "屏東區", "臺東區", "花蓮區", "宜蘭區", "平溪線", "內灣 六家", "集集區", "沙崙區", "深澳線"}
    Public Sub New()
        InitializeComponent()
    End Sub

    Protected Overrides Sub OnNavigatedTo(e As NavigationEventArgs)
        MyBase.OnNavigatedTo(e)


        Try
            mSelectedStGrp = NavigationContext.QueryString("StGrp")
            mSelectedSt = NavigationContext.QueryString("St")
        Catch ex As Exception
            mSelectedStGrp = "臺北區"
            mSelectedSt = "臺北"
        End Try



        mLoading = True

        Dim _dsStGrpStart As New StringLoopingDataSourceBase(mStringListStGrp)
        AddHandler _dsStGrpStart.OnSelectionChanged, AddressOf StGrpStart_SelectionChanged
        ' _dsStGrpStart.SelectedItem = "臺北區"
        _dsStGrpStart.SelectedItem = mSelectedStGrp
        Me.lsStGrp.DataSource = _dsStGrpStart

        mLoading = False
    End Sub


    Private Sub StGrpStart_SelectionChanged(previousSelectedItem As Object, newSelectedItem As Object)
        Dim _NewSelectedStGrp As clsStGrp = GetStGrp(newSelectedItem)
        If _NewSelectedStGrp Is Nothing Then Exit Sub

        Dim _StringListStGrp As List(Of String) = _NewSelectedStGrp.mStNameList

        Dim _tempSD As New StringLoopingDataSourceBase(_StringListStGrp)
        If mLoading Then
            Try
                _tempSD.SelectedItem = mSelectedSt
            Catch ex As Exception

            End Try 
        Else
            _tempSD.SelectedItem = _NewSelectedStGrp.mFirstSelectedStName
        End If

        Me.lsSt.DataSource = _tempSD
    End Sub 

    Private Function GetStGrp(tStGrpChName As String) As clsStGrp
        For i As Integer = 0 To GlobalVariables.gStGrpList.Count
            If tStGrpChName.Substring(0, 2) = GlobalVariables.gStGrpList.Item(i).mChName.Substring(0, 2) Then
                Return GlobalVariables.gStGrpList.Item(i)
            End If
        Next

        Return Nothing
    End Function
     

    Private Sub abtnCheck_Click(sender As Object, e As EventArgs) 
        GlobalVariables.gUpdateLocSearchSt = True
        GlobalVariables.gLocSearchStGrp = clsStGrp.GetRegion(GlobalVariables.gStGrpList, Me.lsStGrp.DataSource.SelectedItem)
        GlobalVariables.gLocSearchSt = clsStation.GetStationByChName(Me.lsSt.DataSource.SelectedItem)
        NavigationService.GoBack()
    End Sub


    Private Sub abtnClose_Click(sender As Object, e As EventArgs)
        GlobalVariables.gUpdateLocSearchSt = False
        NavigationService.GoBack()
    End Sub
End Class
