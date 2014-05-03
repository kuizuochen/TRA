Imports System.ComponentModel
Imports Microsoft.Phone.Tasks

Public Class vmNoNuke
    Implements INotifyPropertyChanged

    Public mPeopleCnt As Integer = 31797
    Public ReadOnly Property pPeopleCntString As Integer
        Get
            Return mPeopleCnt.ToString
        End Get
    End Property 
     

    Public Sub LoadFromIsoSetting(ByRef tIsoSetting As System.IO.IsolatedStorage.IsolatedStorageSettings)
        ''讀取設定 
        Dim _ContentString As String = ""

        ''若IsoStorage中沒有儲存資料 則從 global data中啟動
        If tIsoSetting.TryGetValue("nk", _ContentString) = False Then
            Exit Sub
        End If

        Dim _IsoSettingStrings() As String = _ContentString.Split("_")
        mPeopleCnt = CInt(_IsoSettingStrings(0)) 

    End Sub

    Public Sub SaveToIsoSetting(ByRef tIsoSetting As System.IO.IsolatedStorage.IsolatedStorageSettings)
        RemoveNoNukeSettingFormIsoSetting(tIsoSetting)

        Dim _KeyString As String = "nk"
        Dim _ContentString As String = ToIsoSettingString()

        tIsoSetting.Add(_KeyString, _ContentString)
    End Sub
    Private Sub RemoveNoNukeSettingFormIsoSetting(ByRef tIsoSetting As System.IO.IsolatedStorage.IsolatedStorageSettings)
        Dim _Keys As ICollection(Of String) = tIsoSetting.Keys
        Dim _KeyToRemove As New List(Of String)
        For i As Integer = 0 To _Keys.Count - 1
            If _Keys(i).Substring(0, 2) = "nk" Then
                _KeyToRemove.Add(_Keys(i))
            End If
        Next

        For i As Integer = 0 To _KeyToRemove.Count - 1
            tIsoSetting.Remove(_KeyToRemove.Item(i))
        Next

    End Sub

    Public Function ToIsoSettingString() As String 
            Return mPeopleCnt.ToString  
    End Function

    Public Sub UpdatePeopleCntOnline()
        Dim _AddressToSearch As String = "http://430.ngo.tw/?base=0"

        Dim _WebClient As New WebClient
        AddHandler _WebClient.DownloadStringCompleted, AddressOf UpdatePeopleCntUpdate_Completed
        _WebClient.DownloadStringAsync(New Uri(_AddressToSearch))

    End Sub
    Private Sub UpdatePeopleCntUpdate_Completed(sender As Object, e As DownloadStringCompletedEventArgs)
        Dim _IsGetStatus As Boolean = False

        Dim _WebContent As String
        _WebContent = e.Result

        Try
            _WebContent = _WebContent.Replace("name=""counter-value"" value=""", "$")
            _WebContent = _WebContent.Replace(""" /></span>", "$")
            Dim _String() As String = _WebContent.Split("$")
            mPeopleCnt = _String(1).ToString
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("pPeopleCntString"))
        Catch ex As Exception

        End Try
    End Sub

    'Public Async Function LaunchFB() As System.Threading.Tasks.Task
    '    Await Windows.System.Launcher.LaunchUriAsync(New System.Uri("fb:https://www.facebook.com/gcaa.org.tw?fref=ts"))
    'End Function

    Public Sub LaunchFB()
        Dim webBrowserTask As WebBrowserTask = New WebBrowserTask()

        webBrowserTask.Uri = New Uri("https://www.facebook.com/gcaa.org.tw?fref=ts", UriKind.Absolute)

        webBrowserTask.Show()
    End Sub

    Public Sub SendSMS()
        Dim smsComposeTask As SmsComposeTask = New SmsComposeTask

        smsComposeTask.To = "+886987571430"
        smsComposeTask.Body = "停止核四預算,拒絕危險核電!"
        smsComposeTask.Show()
    End Sub

    Public Sub OpenWebPage()
        Dim webBrowserTask As WebBrowserTask = New WebBrowserTask()

        webBrowserTask.Uri = New Uri("http://430.ngo.tw/?base=0", UriKind.Absolute)

        webBrowserTask.Show()
    End Sub
     
    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged
End Class
