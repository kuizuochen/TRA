﻿#ExternalChecksum("C:\Users\kenny_000\Dropbox\WP_Project\TRA\TRA_031\TRA_03\pgLoading.xaml","{406ea660-64cf-4c82-b6f0-42d48172a799}","509B81D5C42F2E4E74986F490428CEAD")
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
Partial Public Class pgLoading
    Inherits Microsoft.Phone.Controls.PhoneApplicationPage
    
    Friend WithEvents LayoutRoot As System.Windows.Controls.Grid
    
    Friend WithEvents ContentPanel As System.Windows.Controls.Grid
    
    Friend WithEvents btShowDirectory As System.Windows.Controls.Button
    
    Friend WithEvents btnClearStorage As System.Windows.Controls.Button
    
    Friend WithEvents btnGetDefaultXML As System.Windows.Controls.Button
    
    Friend WithEvents btnLoadDBInXmlFolder As System.Windows.Controls.Button
    
    Friend WithEvents btnDownloadDB As System.Windows.Controls.Button
    
    Friend WithEvents btnDeCompress As System.Windows.Controls.Button
    
    Friend WithEvents btnReadDB As System.Windows.Controls.Button
    
    Friend WithEvents btnRefreshDB As System.Windows.Controls.Button
    
    Friend WithEvents tbStatus As System.Windows.Controls.TextBlock
    
    Friend WithEvents tbUpdateStatus As System.Windows.Controls.TextBlock
    
    Friend WithEvents popDoUpdate As System.Windows.Controls.Primitives.Popup
    
    Friend WithEvents tbPopMsg As System.Windows.Controls.TextBlock
    
    Friend WithEvents btnUpdateOK As System.Windows.Controls.Button
    
    Friend WithEvents btnUpdateCancel As System.Windows.Controls.Button
    
    Friend WithEvents tbDebug As System.Windows.Controls.TextBlock
    
    Friend WithEvents tbDebug2 As System.Windows.Controls.TextBlock
    
    Friend WithEvents tbDebug3 As System.Windows.Controls.TextBlock
    
    Friend WithEvents tbDebug4 As System.Windows.Controls.TextBlock
    
    Friend WithEvents btnCancel As System.Windows.Controls.Button
    
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
        System.Windows.Application.LoadComponent(Me, New System.Uri("/TRA_03;component/pgLoading.xaml", System.UriKind.Relative))
        Me.LayoutRoot = CType(Me.FindName("LayoutRoot"),System.Windows.Controls.Grid)
        Me.ContentPanel = CType(Me.FindName("ContentPanel"),System.Windows.Controls.Grid)
        Me.btShowDirectory = CType(Me.FindName("btShowDirectory"),System.Windows.Controls.Button)
        Me.btnClearStorage = CType(Me.FindName("btnClearStorage"),System.Windows.Controls.Button)
        Me.btnGetDefaultXML = CType(Me.FindName("btnGetDefaultXML"),System.Windows.Controls.Button)
        Me.btnLoadDBInXmlFolder = CType(Me.FindName("btnLoadDBInXmlFolder"),System.Windows.Controls.Button)
        Me.btnDownloadDB = CType(Me.FindName("btnDownloadDB"),System.Windows.Controls.Button)
        Me.btnDeCompress = CType(Me.FindName("btnDeCompress"),System.Windows.Controls.Button)
        Me.btnReadDB = CType(Me.FindName("btnReadDB"),System.Windows.Controls.Button)
        Me.btnRefreshDB = CType(Me.FindName("btnRefreshDB"),System.Windows.Controls.Button)
        Me.tbStatus = CType(Me.FindName("tbStatus"),System.Windows.Controls.TextBlock)
        Me.tbUpdateStatus = CType(Me.FindName("tbUpdateStatus"),System.Windows.Controls.TextBlock)
        Me.popDoUpdate = CType(Me.FindName("popDoUpdate"),System.Windows.Controls.Primitives.Popup)
        Me.tbPopMsg = CType(Me.FindName("tbPopMsg"),System.Windows.Controls.TextBlock)
        Me.btnUpdateOK = CType(Me.FindName("btnUpdateOK"),System.Windows.Controls.Button)
        Me.btnUpdateCancel = CType(Me.FindName("btnUpdateCancel"),System.Windows.Controls.Button)
        Me.tbDebug = CType(Me.FindName("tbDebug"),System.Windows.Controls.TextBlock)
        Me.tbDebug2 = CType(Me.FindName("tbDebug2"),System.Windows.Controls.TextBlock)
        Me.tbDebug3 = CType(Me.FindName("tbDebug3"),System.Windows.Controls.TextBlock)
        Me.tbDebug4 = CType(Me.FindName("tbDebug4"),System.Windows.Controls.TextBlock)
        Me.btnCancel = CType(Me.FindName("btnCancel"),System.Windows.Controls.Button)
    End Sub
End Class

