Imports System.Windows.Data
Imports System.ComponentModel
Imports TRA_03.GlobalVariables

Partial Public Class pgTrainDetail
    Inherits PhoneApplicationPage 
      
    Public Property pTrain As clsTrain

    Public Sub New()
        InitializeComponent() 
    End Sub

    Protected Overrides Sub OnNavigatedTo(e As NavigationEventArgs) 
        pTrain = gTrainToShow
        pTrain.SetClosetSt()
        Me.DataContext = pTrain
        ScrollToCloestSt()
    End Sub 

    Private Sub abtnUpdateStatus_Click(sender As Object, e As EventArgs)
        pTrain.UpdateStatusOnline()
        ScrollToCloestSt()
    End Sub 

    Private Sub abtnUpdate_Click(sender As Object, e As EventArgs)
        pTrain.SetClosetSt()
        ScrollToCloestSt()
    End Sub

    Private Sub ScrollToCloestSt()
        Try
            For i As Integer = 0 To pTrain.pTimeInfoList.Count - 1
                If pTrain.pTimeInfoList.Item(i).pIsHighLight Then
                    llTimeInfoList.ScrollTo(pTrain.pTimeInfoList.Item(i))
                    Exit Sub
                End If
            Next
        Catch ex As Exception

        End Try 
    End Sub
End Class

Public Class dcStBg
    Implements IValueConverter

    Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As Globalization.CultureInfo) As Object Implements IValueConverter.Convert
        Dim _Value As Double = CDbl(value)
        If _Value Mod 2 = 0 Then
            Return "LightGray"
        Else
            Return "White"
        End If
        Return "White"
    End Function

    Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
        Return Nothing
    End Function
End Class

Public Class dcStForeground
    Implements IValueConverter

    Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As Globalization.CultureInfo) As Object Implements IValueConverter.Convert
        If CBool(value) Then
            Return "Red"
        End If

        Return "#323232"
    End Function

    Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
        Return Nothing
    End Function
End Class


