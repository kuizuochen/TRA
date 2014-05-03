Imports System.ComponentModel
Imports System.Collections.ObjectModel

Public Class vmMyFavorite
    Implements INotifyPropertyChanged

    Public Property pFavoriteItems As ObservableCollection(Of vmStartEndSearch)
#Region "Initialization"
    Public Sub New()
        pFavoriteItems = New ObservableCollection(Of vmStartEndSearch)
    End Sub 
#End Region

#Region "儲存/讀取 UI設定"
    ''mf ; 1059_1043_1_0_1_0
    ''key:mf_id
    ''Content: StartStID _ EndStID _ PNTZ _ TZ _ CK _ Local
    Public Sub SaveToIsoSetting(ByRef tIsoSetting As System.IO.IsolatedStorage.IsolatedStorageSettings)
        RemoveAllFavoriteFormIsoSetting(tIsoSetting)

        Dim _KeyString As String = ""
        Dim _ContentString As String = ""

        For i As Integer = 0 To pFavoriteItems.Count - 1
            _KeyString = "mf_" + i.ToString
            _ContentString = pFavoriteItems.Item(i).ToIsoSettingString_MyFavorite
            tIsoSetting.Add(_KeyString, _ContentString)
        Next
    End Sub

    Public Sub LoadFromIsoSetting(ByRef tIsoSetting As System.IO.IsolatedStorage.IsolatedStorageSettings)
        Dim _Keys As ICollection(Of String) = tIsoSetting.Keys
        Dim _ContentString As String = ""
        Dim _TempStartEndSearch As vmStartEndSearch

        pFavoriteItems.Clear()

        For i As Integer = 0 To _Keys.Count - 1
            If _Keys(i).Substring(0, 2) = "mf" Then
                If tIsoSetting.TryGetValue(_Keys(i), _ContentString) Then
                    _TempStartEndSearch = New vmStartEndSearch(_ContentString)
                    _TempStartEndSearch.LoadData_FavoriteMode()
                    pFavoriteItems.Add(_TempStartEndSearch)
                End If
            End If
        Next
         
    End Sub
    Private Sub RemoveAllFavoriteFormIsoSetting(ByRef tIsoSetting As System.IO.IsolatedStorage.IsolatedStorageSettings)
        Dim _Keys As ICollection(Of String) = tIsoSetting.Keys
        Dim _KeyToRemove As New List(Of String)
        For i As Integer = 0 To _Keys.Count - 1
            If _Keys(i).Substring(0, 2) = "mf" Then
                _KeyToRemove.Add(_Keys(i))
            End If
        Next

        For i As Integer = 0 To _KeyToRemove.Count - 1
            tIsoSetting.Remove(_KeyToRemove.Item(i))
        Next

    End Sub
#End Region

    Public Sub AddCurrentSESearchIntoMyFavorite(tCurrentSESearch As vmStartEndSearch)
        If pFavoriteItems Is Nothing Then Exit Sub
        Dim _NewSESearch As vmStartEndSearch = tCurrentSESearch.Clone
        '_NewSESearch.pDepTime_S = New DateTime(_NewSESearch.pDepTime_S.Year, _NewSESearch.pDepTime_S.Month, _NewSESearch.pDepTime_S.Day, 5, 0, 0)
        '_NewSESearch.pDepTime_E = New DateTime(_NewSESearch.pDepTime_E.Year, _NewSESearch.pDepTime_E.Month, _NewSESearch.pDepTime_E.Day, 23, 0, 0)
        _NewSESearch.LoadData_FavoriteMode()
        pFavoriteItems.Add(_NewSESearch)

        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("pFavoriteItems"))
    End Sub 

    Public Sub RemoveSESearchFromMyFavorite(tCurrentSESearch As vmStartEndSearch)
RecursivePnt:
        If pFavoriteItems.Count = 0 Then Exit Sub

        For i As Integer = 0 To pFavoriteItems.Count - 1
            If pFavoriteItems.Item(i).Equals(tCurrentSESearch) Then
                pFavoriteItems.RemoveAt(i)
                GoTo RecursivePnt
            End If
        Next

        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("pFavoriteItems"))
    End Sub

    Public Sub UpdateAllCloest3Trains()
        For i As Integer = 0 To pFavoriteItems.Count - 1
            pFavoriteItems.Item(i).SetCloestTrains_MyFavorite()
        Next
    End Sub
    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged
End Class
