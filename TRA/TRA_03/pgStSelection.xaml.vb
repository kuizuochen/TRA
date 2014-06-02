Imports Microsoft.Phone.Controls.Primitives

Partial Public Class pgStSelection
    Inherits PhoneApplicationPage


    Public mStringListStGrp As List(Of String) = New List(Of String) From {"臺北區", "桃園區", "新竹區", "苗栗區", "臺中區", "彰化區", "南投區", "雲林區", "嘉義區", "臺南區", "高雄區", "屏東區", "臺東區", "花蓮區", "宜蘭區", "平溪線", "內灣 六家", "集集區", "沙崙區", "深澳線"}
    Public Sub New()
        InitializeComponent()
         
        Dim _dsStGrpStart As New StringLoopingDataSourceBase(mStringListStGrp)
        AddHandler _dsStGrpStart.OnSelectionChanged, AddressOf StGrpStart_SelectionChanged
        _dsStGrpStart.SelectedItem = "臺北區"
        Me.lsStGrpStart.DataSource = _dsStGrpStart

        Dim _dsStGrpEnd As New StringLoopingDataSourceBase(mStringListStGrp)
        AddHandler _dsStGrpEnd.OnSelectionChanged, AddressOf StGrpEnd_SelectionChanged
        _dsStGrpEnd.SelectedItem = "新竹區"
        Me.lsStGrpEnd.DataSource = _dsStGrpEnd

    End Sub

    Private Sub StGrpStart_SelectionChanged(previousSelectedItem As Object, newSelectedItem As Object)

        Dim _NewSelectedStGrp As clsStGrp = GetStGrp(newSelectedItem)
        If _NewSelectedStGrp Is Nothing Then Exit Sub

        Dim _StringListStGrp As List(Of String) = _NewSelectedStGrp.mStNameList

        Dim _tempSD As New StringLoopingDataSourceBase(_StringListStGrp)
        _tempSD.SelectedItem = _NewSelectedStGrp.mFirstSelectedStName
        Me.lsStStart.DataSource = _tempSD
    End Sub

    Private Sub StGrpEnd_SelectionChanged(previousSelectedItem As Object, newSelectedItem As Object)

        Dim _NewSelectedStGrp As clsStGrp = GetStGrp(newSelectedItem)
        If _NewSelectedStGrp Is Nothing Then Exit Sub

        Dim _StringListStGrp As List(Of String) = _NewSelectedStGrp.mStNameList

        Dim _tempSD As New StringLoopingDataSourceBase(_StringListStGrp)
        _tempSD.SelectedItem = _NewSelectedStGrp.mFirstSelectedStName
        Me.lsStEnd.DataSource = _tempSD
    End Sub

    Private Function GetStGrp(tStGrpChName As String) As clsStGrp
        For i As Integer = 0 To GlobalVariables.gStGrpList.Count
            If tStGrpChName = GlobalVariables.gStGrpList.Item(i).mChName Then
                Return GlobalVariables.gStGrpList.Item(i)
            End If
        Next

        Return Nothing
    End Function
End Class

Public MustInherit Class LoopingDataSourceBase
    Implements ILoopingSelectorDataSource

    Public mSelectedItem As Object

    Event OnSelectionChanged(previousSelectedItem As Object, newSelectedItem As Object)

    Public MustOverride Function GetNext(relativeTo As Object) As Object Implements ILoopingSelectorDataSource.GetNext
    Public MustOverride Function GetPrevious(relativeTo As Object) As Object Implements ILoopingSelectorDataSource.GetPrevious


    Public Property SelectedItem As Object Implements ILoopingSelectorDataSource.SelectedItem
        Get
            Return mSelectedItem
        End Get
        Set(value As Object)
            If Object.Equals(Me.mSelectedItem, value) = False Then
                Dim previousSelectedItem As Object = mSelectedItem
                mSelectedItem = value
                RaiseEvent OnSelectionChanged(previousSelectedItem, mSelectedItem)
            End If

        End Set
    End Property

    Public Event SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Implements ILoopingSelectorDataSource.SelectionChanged
End Class
Public Class StringLoopingDataSourceBase
    Inherits LoopingDataSourceBase

    Private mStringList As List(Of String)

    Public Sub New(tStringLista As List(Of String))
        mStringList = tStringLista
    End Sub

    Private Function FindID(tTargetString As String) As Integer
        For i As Integer = 0 To mStringList.Count - 1
            If mStringList.Item(i) = tTargetString Then
                Return i
            End If
        Next

        Return -1
    End Function

    Public Overrides Function GetNext(relativeTo As Object) As Object
        Dim _ID As Integer = FindID(relativeTo)
        If _ID = -1 Then Return mStringList(0)
        If _ID = mStringList.Count - 1 Then
            Return mStringList(0)
        Else
            Return mStringList(_ID + 1)
        End If
    End Function

    Public Overrides Function GetPrevious(relativeTo As Object) As Object
        Dim _ID As Integer = FindID(relativeTo)
        If _ID = -1 Then Return mStringList(0)
        If _ID = 0 Then
            Return mStringList(mStringList.Count - 1)
        Else
            Return mStringList(_ID - 1)
        End If
    End Function
End Class

Public Class ListLoopingDataSource(Of T)
    Inherits LoopingDataSourceBase

    Private mLinkedList As LinkedList(Of T)
    Private mSortedList As List(Of LinkedListNode(Of T))
    Private mComparer As IComparer(Of T)
    Private mNodeComparer As NodeComparer

    Public Property pItems As IEnumerable(Of T)
        Get
            Return mLinkedList
        End Get
        Set(value As IEnumerable(Of T))
            SetItemCollection(value)
        End Set
    End Property
    Private Sub SetItemCollection(tCollection As IEnumerable(Of T))
        mLinkedList = New LinkedList(Of T)(tCollection)

        mSortedList = New List(Of LinkedListNode(Of T))(mLinkedList.Count)
        Dim _CurrentNode As LinkedListNode(Of T) = mLinkedList.First
        While _CurrentNode IsNot Nothing
            mSortedList.Add(_CurrentNode)
            _CurrentNode = _CurrentNode.Next
        End While

        Dim _Comparer As IComparer(Of T) = mComparer
        Dim _tempIComparable As IComparable(Of T) = Nothing
        If _Comparer Is Nothing Then
            '  If _tempIComparable.GetType.IsAssignableFrom(GetType(T)) Then
            _Comparer = Comparer(Of T).Default
            'Else
            '      Throw New InvalidOperationException("There is no default comparer for this type of item. You must set one.")
            '    End If
        End If

        mNodeComparer = New NodeComparer(_Comparer)
        mSortedList.Sort(mNodeComparer)
    End Sub

    Public Property pComparer As IComparer(Of T)
        Get
            Return mComparer
        End Get
        Set(value As IComparer(Of T))
            mComparer = value
        End Set
    End Property



    Public Overrides Function GetNext(relativeTo As Object) As Object
        Dim _LinkedListNode As LinkedListNode(Of T)
        _LinkedListNode = New LinkedListNode(Of T)(relativeTo)
        Dim _index As Integer = mSortedList.BinarySearch(_LinkedListNode, mNodeComparer)

        If _index < 0 Then
            Dim _tempDefault As T
            Return _tempDefault
        End If

        Dim _node As LinkedListNode(Of T) = mSortedList(_index).Next
        If _node Is Nothing Then
            _node = mLinkedList.First
        End If

        Return _node.Value
    End Function

    Public Overrides Function GetPrevious(relativeTo As Object) As Object
        Dim _LinkedListNode As LinkedListNode(Of T)
        _LinkedListNode = New LinkedListNode(Of T)(relativeTo)
        Dim _index As Integer = mSortedList.BinarySearch(_LinkedListNode, mNodeComparer)

        If _index < 0 Then
            Dim _tempDefault As T
            Return _tempDefault
        End If

        Dim _node As LinkedListNode(Of T) = mSortedList(_index).Previous
        If _node Is Nothing Then
            _node = mLinkedList.Last
        End If

        Return _node.Value
    End Function

    Private Class NodeComparer
        Implements IComparer(Of LinkedListNode(Of T))
        Private mComparer As IComparer(Of T)

        Public Sub New(tComparer As IComparer(Of T))
            mComparer = tComparer
        End Sub

        Public Function Compare(x As LinkedListNode(Of T), y As LinkedListNode(Of T)) As Integer Implements Collections.Generic.IComparer(Of LinkedListNode(Of T)).Compare
            Return Me.mComparer.Compare(x.Value, y.Value)
        End Function
    End Class


End Class

 

