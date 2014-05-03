Imports System.ComponentModel

Public Class vmSetting
    Implements INotifyPropertyChanged

    Public mTableFilesInIsoStorage As List(Of String)

    Public Property pIsFirstTilePicSelected As Boolean = True
    Public Property pIsSecondTilePicSelected As Boolean = False

    Public mDateOfLatestTable As DateTime = DateTime.Now
    Public Property pDateOfLatestTable As DateTime
        Get
            Return mDateOfLatestTable
        End Get
        Set(value As DateTime)
            mDateOfLatestTable = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("pDateOfLatestTable"))
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("pLatestTable"))
        End Set
    End Property
    Public ReadOnly Property pLatestTable As String
        Get
            Return mDateOfLatestTable.Year.ToString + "/" + mDateOfLatestTable.Month.ToString + "/" + mDateOfLatestTable.Day.ToString
        End Get
    End Property
    Public Property pIsAutoUpdate As Boolean = True
    Public Property mDayCntToAutoUpdate As Integer = 10
    Public Property pDayCntToAutoUpdate As Integer
        Get
            Return mDayCntToAutoUpdate
        End Get
        Set(value As Integer)
            mDayCntToAutoUpdate = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("pDayCntToAutoUpdate"))
        End Set
    End Property
    Public mShowNoNuke As Boolean = True
    Public Property pShowNoNuke As Boolean
        Get
            Return mShowNoNuke
        End Get
        Set(value As Boolean)
            mShowNoNuke = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("pShowNoNuke"))
        End Set
    End Property

    Public Function ToIsoSettingString() As String
        Dim _Result As String = ""

        If pIsAutoUpdate = True Then
            _Result += "1"
        Else
            _Result += "0"
        End If

        _Result += "_" + pDayCntToAutoUpdate.ToString

        If pShowNoNuke = True Then
            _Result += "_1"
        Else
            _Result += "_0"
        End If

        If pIsFirstTilePicSelected = True Then
            _Result += "_1"
        Else
            _Result += "_0"
        End If

        If pIsSecondTilePicSelected = True Then
            _Result += "_1"
        Else
            _Result += "_0"
        End If

        Return _Result
    End Function
    Public Sub LoadFromIsoSetting(ByRef tIsoSetting As System.IO.IsolatedStorage.IsolatedStorageSettings)
        ''讀取設定 
        Dim _ContentString As String = ""

        ''若IsoStorage中沒有儲存資料 則從 global data中啟動
        If tIsoSetting.TryGetValue("st", _ContentString) = False Then
            Exit Sub
        End If

        Dim _IsoSettingStrings() As String = _ContentString.Split("_")

        If _IsoSettingStrings(0) = "0" Then pIsAutoUpdate = False
        If _IsoSettingStrings(0) = "1" Then pIsAutoUpdate = True

        pDayCntToAutoUpdate = CInt(_IsoSettingStrings(1))

        If _IsoSettingStrings(2) = "0" Then pShowNoNuke = False
        If _IsoSettingStrings(2) = "1" Then pShowNoNuke = True

        If _IsoSettingStrings(3) = "0" Then pIsFirstTilePicSelected = False
        If _IsoSettingStrings(3) = "1" Then pIsFirstTilePicSelected = True

        If _IsoSettingStrings(4) = "0" Then pIsSecondTilePicSelected = False
        If _IsoSettingStrings(4) = "1" Then pIsSecondTilePicSelected = True

    End Sub

    Public Sub SaveToIsoSetting(ByRef tIsoSetting As System.IO.IsolatedStorage.IsolatedStorageSettings)
        RemoveNoNukeSettingFormIsoSetting(tIsoSetting)

        Dim _KeyString As String = "st"
        Dim _ContentString As String = ToIsoSettingString()

        tIsoSetting.Add(_KeyString, _ContentString)
    End Sub
    Private Sub RemoveNoNukeSettingFormIsoSetting(ByRef tIsoSetting As System.IO.IsolatedStorage.IsolatedStorageSettings)
        Dim _Keys As ICollection(Of String) = tIsoSetting.Keys
        Dim _KeyToRemove As New List(Of String)
        For i As Integer = 0 To _Keys.Count - 1
            If _Keys(i).Substring(0, 2) = "st" Then
                _KeyToRemove.Add(_Keys(i))
            End If
        Next

        For i As Integer = 0 To _KeyToRemove.Count - 1
            tIsoSetting.Remove(_KeyToRemove.Item(i))
        Next

    End Sub

    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged
End Class
