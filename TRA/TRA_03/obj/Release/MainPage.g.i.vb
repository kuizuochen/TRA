﻿#ExternalChecksum("C:\Users\kenny_000\Documents\GitHub\TRA\TRA\TRA_03\MainPage.xaml","{406ea660-64cf-4c82-b6f0-42d48172a799}","F0681207870276A63C5588AF4769E64B")
'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.34014
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On

Imports Microsoft.Phone.Controls
Imports System
Imports System.Windows
Imports System.Windows.Automation
Imports System.Windows.Automation.Peers
Imports System.Windows.Automation.Provider
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Ink
Imports System.Windows.Input
Imports System.Windows.Interop
Imports System.Windows.Markup
Imports System.Windows.Media
Imports System.Windows.Media.Animation
Imports System.Windows.Media.Imaging
Imports System.Windows.Resources
Imports System.Windows.Shapes
Imports System.Windows.Threading



<Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>  _
Partial Public Class MainPage
    Inherits Microsoft.Phone.Controls.PhoneApplicationPage
    
    Friend WithEvents LayoutRoot As System.Windows.Controls.Grid
    
    Friend WithEvents pvMain As Microsoft.Phone.Controls.Pivot
    
    Friend WithEvents pvSESearch As Microsoft.Phone.Controls.PivotItem
    
    Friend WithEvents tbSEGrp_Start As System.Windows.Controls.TextBlock
    
    Friend WithEvents tbSESt_Start As System.Windows.Controls.TextBlock
    
    Friend WithEvents tbSEGrp_End As System.Windows.Controls.TextBlock
    
    Friend WithEvents tbSESt_End As System.Windows.Controls.TextBlock
    
    Friend WithEvents ckFillterPN As System.Windows.Controls.CheckBox
    
    Friend WithEvents ckFillterTZ As System.Windows.Controls.CheckBox
    
    Friend WithEvents ckFillterCK As System.Windows.Controls.CheckBox
    
    Friend WithEvents ckFillterLocal As System.Windows.Controls.CheckBox
    
    Friend WithEvents cvFavorite As System.Windows.Controls.Canvas
    
    Friend WithEvents lpSESearchTimeTable As Microsoft.Phone.Controls.ListPicker
    
    Friend WithEvents tpSESearchStart As Microsoft.Phone.Controls.TimePicker
    
    Friend WithEvents tpSESearchEnd As Microsoft.Phone.Controls.TimePicker
    
    Friend WithEvents llsTickets As Microsoft.Phone.Controls.LongListSelector
    
    Friend WithEvents pvLocSch As Microsoft.Phone.Controls.PivotItem
    
    Friend WithEvents tbLocationSearchGrp As System.Windows.Controls.TextBlock
    
    Friend WithEvents tbLocationSearchSt As System.Windows.Controls.TextBlock
    
    Friend WithEvents btnGetCloestSt As System.Windows.Controls.Button
    
    Friend WithEvents imgGeoLoc As System.Windows.Controls.Image
    
    Friend WithEvents pbGetGeoLoc As System.Windows.Controls.ProgressBar
    
    Friend WithEvents ckFillterPN_Loc As System.Windows.Controls.CheckBox
    
    Friend WithEvents ckFillterTZ_Loc As System.Windows.Controls.CheckBox
    
    Friend WithEvents ckFillterCK_Loc As System.Windows.Controls.CheckBox
    
    Friend WithEvents ckFillterLocal_Loc As System.Windows.Controls.CheckBox
    
    Friend WithEvents gridThreeLine As System.Windows.Controls.Grid
    
    Friend WithEvents bdThreeLine_0 As System.Windows.Controls.Border
    
    Friend WithEvents tbThreeLine_0 As System.Windows.Controls.TextBlock
    
    Friend WithEvents bdThreeLine_1 As System.Windows.Controls.Border
    
    Friend WithEvents tbThreeLine_1 As System.Windows.Controls.TextBlock
    
    Friend WithEvents bdThreeLine_2 As System.Windows.Controls.Border
    
    Friend WithEvents tbThreeLine_2 As System.Windows.Controls.TextBlock
    
    Friend WithEvents gridTwoLine As System.Windows.Controls.Grid
    
    Friend WithEvents bdTwoLine_0 As System.Windows.Controls.Border
    
    Friend WithEvents tbTwoLine_0 As System.Windows.Controls.TextBlock
    
    Friend WithEvents bdTwoLine_1 As System.Windows.Controls.Border
    
    Friend WithEvents tbTwoLine_1 As System.Windows.Controls.TextBlock
    
    Friend WithEvents gridOneLine As System.Windows.Controls.Grid
    
    Friend WithEvents bdOneLine_0 As System.Windows.Controls.Border
    
    Friend WithEvents llsLocSchFW As Microsoft.Phone.Controls.LongListSelector
    
    Friend WithEvents llsLocSchBW As Microsoft.Phone.Controls.LongListSelector
    
    Friend WithEvents pvMyFavorite As Microsoft.Phone.Controls.PivotItem
    
    Friend WithEvents llMyFavorite As Microsoft.Phone.Controls.LongListSelector
    
    Friend WithEvents pvGP As Microsoft.Phone.Controls.PivotItem
    
    Friend WithEvents pvSetting As Microsoft.Phone.Controls.PivotItem
    
    Friend WithEvents tsAutoUpdate As Microsoft.Phone.Controls.ToggleSwitch
    
    Friend WithEvents sldDays As System.Windows.Controls.Slider
    
    Friend WithEvents btnShowUpdatePop As System.Windows.Controls.Button
    
    Friend WithEvents tsGP As Microsoft.Phone.Controls.ToggleSwitch
    
    Friend WithEvents rbFirstPic_1 As System.Windows.Controls.RadioButton
    
    Friend WithEvents imgFirst_1 As System.Windows.Controls.Image
    
    Friend WithEvents rbSecondPic_1 As System.Windows.Controls.RadioButton
    
    Friend WithEvents imgSecond_1 As System.Windows.Controls.Image
    
    Friend WithEvents rbFirstPic_2 As System.Windows.Controls.RadioButton
    
    Friend WithEvents imgFirst_2 As System.Windows.Controls.Image
    
    Friend WithEvents rbSecondPic_2 As System.Windows.Controls.RadioButton
    
    Friend WithEvents imgSecond_2 As System.Windows.Controls.Image
    
    Friend WithEvents rbFirstPic As System.Windows.Controls.RadioButton
    
    Friend WithEvents imgFirst As System.Windows.Controls.Image
    
    Friend WithEvents rbSecondPic As System.Windows.Controls.RadioButton
    
    Friend WithEvents imgSecond As System.Windows.Controls.Image
    
    Friend WithEvents popReopenOK As System.Windows.Controls.Primitives.Popup
    
    Friend WithEvents btnReopenOk As System.Windows.Controls.Button
    
    Friend WithEvents popDoUpdate As System.Windows.Controls.Primitives.Popup
    
    Friend WithEvents pbDoingUpdate As System.Windows.Controls.ProgressBar
    
    Friend WithEvents tbPopUpdateStatus As System.Windows.Controls.TextBlock
    
    Friend WithEvents btnUpdateOk As System.Windows.Controls.Button
    
    Friend WithEvents btnUpdateCancel As System.Windows.Controls.Button
    
    Private _contentLoaded As Boolean
    
    '''<summary>
    '''InitializeComponent
    '''</summary>
    <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
    Public Sub InitializeComponent()
        If _contentLoaded Then
            Return
        End If
        _contentLoaded = true
        System.Windows.Application.LoadComponent(Me, New System.Uri("/TRA_03;component/MainPage.xaml", System.UriKind.Relative))
        Me.LayoutRoot = CType(Me.FindName("LayoutRoot"),System.Windows.Controls.Grid)
        Me.pvMain = CType(Me.FindName("pvMain"),Microsoft.Phone.Controls.Pivot)
        Me.pvSESearch = CType(Me.FindName("pvSESearch"),Microsoft.Phone.Controls.PivotItem)
        Me.tbSEGrp_Start = CType(Me.FindName("tbSEGrp_Start"),System.Windows.Controls.TextBlock)
        Me.tbSESt_Start = CType(Me.FindName("tbSESt_Start"),System.Windows.Controls.TextBlock)
        Me.tbSEGrp_End = CType(Me.FindName("tbSEGrp_End"),System.Windows.Controls.TextBlock)
        Me.tbSESt_End = CType(Me.FindName("tbSESt_End"),System.Windows.Controls.TextBlock)
        Me.ckFillterPN = CType(Me.FindName("ckFillterPN"),System.Windows.Controls.CheckBox)
        Me.ckFillterTZ = CType(Me.FindName("ckFillterTZ"),System.Windows.Controls.CheckBox)
        Me.ckFillterCK = CType(Me.FindName("ckFillterCK"),System.Windows.Controls.CheckBox)
        Me.ckFillterLocal = CType(Me.FindName("ckFillterLocal"),System.Windows.Controls.CheckBox)
        Me.cvFavorite = CType(Me.FindName("cvFavorite"),System.Windows.Controls.Canvas)
        Me.lpSESearchTimeTable = CType(Me.FindName("lpSESearchTimeTable"),Microsoft.Phone.Controls.ListPicker)
        Me.tpSESearchStart = CType(Me.FindName("tpSESearchStart"),Microsoft.Phone.Controls.TimePicker)
        Me.tpSESearchEnd = CType(Me.FindName("tpSESearchEnd"),Microsoft.Phone.Controls.TimePicker)
        Me.llsTickets = CType(Me.FindName("llsTickets"),Microsoft.Phone.Controls.LongListSelector)
        Me.pvLocSch = CType(Me.FindName("pvLocSch"),Microsoft.Phone.Controls.PivotItem)
        Me.tbLocationSearchGrp = CType(Me.FindName("tbLocationSearchGrp"),System.Windows.Controls.TextBlock)
        Me.tbLocationSearchSt = CType(Me.FindName("tbLocationSearchSt"),System.Windows.Controls.TextBlock)
        Me.btnGetCloestSt = CType(Me.FindName("btnGetCloestSt"),System.Windows.Controls.Button)
        Me.imgGeoLoc = CType(Me.FindName("imgGeoLoc"),System.Windows.Controls.Image)
        Me.pbGetGeoLoc = CType(Me.FindName("pbGetGeoLoc"),System.Windows.Controls.ProgressBar)
        Me.ckFillterPN_Loc = CType(Me.FindName("ckFillterPN_Loc"),System.Windows.Controls.CheckBox)
        Me.ckFillterTZ_Loc = CType(Me.FindName("ckFillterTZ_Loc"),System.Windows.Controls.CheckBox)
        Me.ckFillterCK_Loc = CType(Me.FindName("ckFillterCK_Loc"),System.Windows.Controls.CheckBox)
        Me.ckFillterLocal_Loc = CType(Me.FindName("ckFillterLocal_Loc"),System.Windows.Controls.CheckBox)
        Me.gridThreeLine = CType(Me.FindName("gridThreeLine"),System.Windows.Controls.Grid)
        Me.bdThreeLine_0 = CType(Me.FindName("bdThreeLine_0"),System.Windows.Controls.Border)
        Me.tbThreeLine_0 = CType(Me.FindName("tbThreeLine_0"),System.Windows.Controls.TextBlock)
        Me.bdThreeLine_1 = CType(Me.FindName("bdThreeLine_1"),System.Windows.Controls.Border)
        Me.tbThreeLine_1 = CType(Me.FindName("tbThreeLine_1"),System.Windows.Controls.TextBlock)
        Me.bdThreeLine_2 = CType(Me.FindName("bdThreeLine_2"),System.Windows.Controls.Border)
        Me.tbThreeLine_2 = CType(Me.FindName("tbThreeLine_2"),System.Windows.Controls.TextBlock)
        Me.gridTwoLine = CType(Me.FindName("gridTwoLine"),System.Windows.Controls.Grid)
        Me.bdTwoLine_0 = CType(Me.FindName("bdTwoLine_0"),System.Windows.Controls.Border)
        Me.tbTwoLine_0 = CType(Me.FindName("tbTwoLine_0"),System.Windows.Controls.TextBlock)
        Me.bdTwoLine_1 = CType(Me.FindName("bdTwoLine_1"),System.Windows.Controls.Border)
        Me.tbTwoLine_1 = CType(Me.FindName("tbTwoLine_1"),System.Windows.Controls.TextBlock)
        Me.gridOneLine = CType(Me.FindName("gridOneLine"),System.Windows.Controls.Grid)
        Me.bdOneLine_0 = CType(Me.FindName("bdOneLine_0"),System.Windows.Controls.Border)
        Me.llsLocSchFW = CType(Me.FindName("llsLocSchFW"),Microsoft.Phone.Controls.LongListSelector)
        Me.llsLocSchBW = CType(Me.FindName("llsLocSchBW"),Microsoft.Phone.Controls.LongListSelector)
        Me.pvMyFavorite = CType(Me.FindName("pvMyFavorite"),Microsoft.Phone.Controls.PivotItem)
        Me.llMyFavorite = CType(Me.FindName("llMyFavorite"),Microsoft.Phone.Controls.LongListSelector)
        Me.pvGP = CType(Me.FindName("pvGP"),Microsoft.Phone.Controls.PivotItem)
        Me.pvSetting = CType(Me.FindName("pvSetting"),Microsoft.Phone.Controls.PivotItem)
        Me.tsAutoUpdate = CType(Me.FindName("tsAutoUpdate"),Microsoft.Phone.Controls.ToggleSwitch)
        Me.sldDays = CType(Me.FindName("sldDays"),System.Windows.Controls.Slider)
        Me.btnShowUpdatePop = CType(Me.FindName("btnShowUpdatePop"),System.Windows.Controls.Button)
        Me.tsGP = CType(Me.FindName("tsGP"),Microsoft.Phone.Controls.ToggleSwitch)
        Me.rbFirstPic_1 = CType(Me.FindName("rbFirstPic_1"),System.Windows.Controls.RadioButton)
        Me.imgFirst_1 = CType(Me.FindName("imgFirst_1"),System.Windows.Controls.Image)
        Me.rbSecondPic_1 = CType(Me.FindName("rbSecondPic_1"),System.Windows.Controls.RadioButton)
        Me.imgSecond_1 = CType(Me.FindName("imgSecond_1"),System.Windows.Controls.Image)
        Me.rbFirstPic_2 = CType(Me.FindName("rbFirstPic_2"),System.Windows.Controls.RadioButton)
        Me.imgFirst_2 = CType(Me.FindName("imgFirst_2"),System.Windows.Controls.Image)
        Me.rbSecondPic_2 = CType(Me.FindName("rbSecondPic_2"),System.Windows.Controls.RadioButton)
        Me.imgSecond_2 = CType(Me.FindName("imgSecond_2"),System.Windows.Controls.Image)
        Me.rbFirstPic = CType(Me.FindName("rbFirstPic"),System.Windows.Controls.RadioButton)
        Me.imgFirst = CType(Me.FindName("imgFirst"),System.Windows.Controls.Image)
        Me.rbSecondPic = CType(Me.FindName("rbSecondPic"),System.Windows.Controls.RadioButton)
        Me.imgSecond = CType(Me.FindName("imgSecond"),System.Windows.Controls.Image)
        Me.popReopenOK = CType(Me.FindName("popReopenOK"),System.Windows.Controls.Primitives.Popup)
        Me.btnReopenOk = CType(Me.FindName("btnReopenOk"),System.Windows.Controls.Button)
        Me.popDoUpdate = CType(Me.FindName("popDoUpdate"),System.Windows.Controls.Primitives.Popup)
        Me.pbDoingUpdate = CType(Me.FindName("pbDoingUpdate"),System.Windows.Controls.ProgressBar)
        Me.tbPopUpdateStatus = CType(Me.FindName("tbPopUpdateStatus"),System.Windows.Controls.TextBlock)
        Me.btnUpdateOk = CType(Me.FindName("btnUpdateOk"),System.Windows.Controls.Button)
        Me.btnUpdateCancel = CType(Me.FindName("btnUpdateCancel"),System.Windows.Controls.Button)
    End Sub
End Class

