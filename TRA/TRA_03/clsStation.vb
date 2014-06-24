Imports System.ComponentModel
Imports System.Collections.ObjectModel
Imports System.Device.Location
Imports Windows.Devices.Geolocation


'主要幹線(對號快車)
'  西部幹線對號快車逆行	東部幹線對號快車逆行	南迴線逆行
'  西部幹線對號快車順行	東部幹線對號快車順行	南迴線順行
'西部區間列車(非對號快車)
'  基隆→新竹  	新竹→彰化	彰化→嘉義	嘉義→高雄	屏東線逆行
'  新竹→基隆	彰化→新竹	嘉義→彰化	高雄→嘉義	屏東線順行
'東部區間列車(非對號快車)
'  宜蘭線逆行	宜蘭線順行	北迴線	臺東線
'支線(非對號快車)
'  平溪線	內灣/六家線	 集集線	沙崙線

Public Enum Section As Integer
    MainLine = 0
    WestLocal = 1
    EastLocal = 2
    BranchLocal = 3
End Enum

Public Enum LineLocation As Integer
    WL_North         '1.1 縱貫線（北段）
    WL_Sea           '1.2 海岸線（海線）
    WL_Montain       '1.3 臺中線（山線）
    WL_CC            '1.4 成追線
    WL_South         '1.5 縱貫線（南段）
    WL_PD            '1.6 屏東線
    EL_EL            '3.1 宜蘭線
    EL_North         '3.2 北迴線
    EL_TD            '3.3 臺東線
    SR               '2 南迴線         
    BL_PC               '平溪線
    BL_CL               '深澳線
    BL_NWLG             '內灣 六家線 
    BL_NW               '內灣線 
    BL_LG               '六家線 
    BL_GG               '集集線
    BL_SL               '沙崙線 

    THSR ''高鐵

    None
End Enum

Public Enum Part As Integer
    '主要幹線(對號快車)
    '  西部幹線對號快車逆行	東部幹線對號快車逆行	南迴線逆行
    '  西部幹線對號快車順行	東部幹線對號快車順行	南迴線順行
    M_WR = 0
    M_ER = 1
    M_SR = 2
    M_WF = 3
    M_EF = 4
    M_SF = 5

    '西部區間列車(非對號快車)
    '  基隆→新竹  	新竹→彰化	彰化→嘉義	嘉義→高雄	屏東線逆行
    '  新竹→基隆	彰化→新竹	嘉義→彰化	高雄→嘉義	屏東線順行
    WL_KLHC = 6
    WL_HSCH = 7
    WL_CHCE = 8
    WL_CEKH = 9
    WL_PDR = 10
    WL_HCKL = 11
    WL_CHHC = 12
    WL_CECH = 13
    WL_KHCE = 14
    WL_PDF = 15

    '東部區間列車(非對號快車)
    '  宜蘭線逆行	宜蘭線順行	北迴線	臺東線
    EL_ELR = 16
    EL_ELF = 17
    EL_N = 18
    EL_TD = 19

    '支線(非對號快車)
    '  平溪線	內灣/六家線	 集集線	沙崙線
    BL_PC = 20
    BL_NW = 21
    BL_LG = 22
    BL_GG = 23
    BL_SL = 24
End Enum

Public Class clsStGrp_S
    Implements INotifyPropertyChanged

    Public mChName As String
    Public mEnName As String 

    Public mShowCh As Boolean = True
     
    Public Property pHLColor As String = "White"
    Public ReadOnly Property DisplayName As String
        Get
            If mShowCh Then
                Return mChName
            Else
                Return mEnName
            End If
        End Get
    End Property

    Public Sub New(tStGrp As clsStGrp)
        mChName = tStGrp.mChName
        mEnName = tStGrp.mEnName
        pHLColor = tStGrp.mHLColor
    End Sub

    Public Sub New(tChName As String, tEnName As String, tHLColor As String)
        mChName = tChName
        mEnName = tEnName
        pHLColor = tHLColor
    End Sub

    Public Function Clone() As clsStGrp_S
        Return New clsStGrp_S(Me.mChName, Me.mEnName, Me.pHLColor)
    End Function

    Public Shared Function GetStGrp(ByRef tStGrpList As ObservableCollection(Of clsStGrp_S), tStGrp As clsStGrp) As clsStGrp_S
        For i As Integer = 0 To tStGrpList.Count - 1
            If tStGrpList.Item(i).mChName = tStGrp.mChName Then
                Return tStGrpList.Item(i)
            End If
        Next

        Return Nothing
    End Function

    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged
End Class

Public Class clsStGrp
    Implements INotifyPropertyChanged

    Public mID As Integer
    Public mChName As String
    Public mEnName As String
    Public mShowCh As Boolean = True
    Public Property mHLColor As String
    Public mStation As ObservableCollection(Of clsStation)
    Public mFirstSelectedStName As String

    Public mStNameList As List(Of String)

    Public Function Clone() As clsStGrp
        Return New clsStGrp(Me.mID, Me.mChName, Me.mEnName, Me.mHLColor, Me.mStation, Me.mFirstSelectedStName)
    End Function

    Public ReadOnly Property DisplayName As String
        Get
            If mShowCh Then
                Return mChName
            Else
                Return mEnName
            End If
        End Get
    End Property
    Public Property pHLColor As String = "White"
    Public Sub New(ID As Integer, ChName As String, EnName As String, HLColor As String, StList As ObservableCollection(Of clsStation), tFirstSelectedStName As String)
        mID = ID
        mChName = ChName
        mEnName = EnName
        mStNameList = New List(Of String)
        mStation = New ObservableCollection(Of clsStation)
        For i As Integer = 0 To StList.Count - 1
            mStation.Add(StList.Item(i))
            mStNameList.Add(StList.Item(i).mChName)
        Next
        mHLColor = HLColor
        mFirstSelectedStName = tFirstSelectedStName
    End Sub

    Public Sub New(ID As Integer, ChName As String, EnName As String, HLColor As String, StList As List(Of String), tFirstSelectedStName As String)
        mID = ID
        mChName = ChName
        mEnName = EnName
        mStNameList = New List(Of String)
        mStation = New ObservableCollection(Of clsStation)
        For i As Integer = 0 To StList.Count - 1
            mStation.Add(clsStation.GetStationByChName(StList.Item(i)))
            mStNameList.Add(StList.Item(i))
        Next
        mHLColor = HLColor
        mFirstSelectedStName = tFirstSelectedStName
    End Sub

    Public Shared Function GetRegion(AllStationGrp As List(Of clsStGrp), tGrpChName As String) As clsStGrp
        For i As Integer = 0 To AllStationGrp.Count - 1
            If AllStationGrp.Item(i).mChName = tGrpChName Then
                Return AllStationGrp.Item(i)
            End If
        Next
        Return Nothing
    End Function

    Public Shared Function GetRegion(AllStationGrp As List(Of clsStGrp), theStation As clsStation, ByRef tGrpIndex As Integer, ByRef tStIndex As Integer) As clsStGrp
        For i As Integer = 0 To AllStationGrp.Count - 1
            For j As Integer = 0 To AllStationGrp.Item(i).mStation.Count - 1
                If theStation.mChName = AllStationGrp.Item(i).mStation.Item(j).mChName Then
                    tGrpIndex = i
                    tStIndex = j
                    Return AllStationGrp.Item(i)
                End If
            Next
        Next
        Return Nothing
    End Function

    Public Shared Function GetRegion(AllStationGrp As ObservableCollection(Of clsStGrp), theStationID As Integer) As clsStGrp
        For i As Integer = 0 To AllStationGrp.Count - 1
            For j As Integer = 0 To AllStationGrp.Item(i).mStation.Count - 1
                If theStationID = AllStationGrp.Item(i).mStation.Item(j).mID Then
                    Return AllStationGrp.Item(i)
                End If
            Next
        Next
        Return Nothing
    End Function

    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged
End Class



Public Class clsStation
    Implements INotifyPropertyChanged


    Public mID As Integer
    Public mChName As String
    Public mEnName As String
    Public mShowCh As Boolean = True

    Public mLat As Double = 0
    Public mLng As Double = 0

    Public mIsHandShakeStop As Boolean = False

    Public mIDsInPart As New List(Of Integer)
    Public Property pHLColor As String = "Yellow"
    Public Property pDisplayLineName1 As String
    Public Property pDisplayLineName2 As String
    Public Property pDisplayLineName3 As String

    Public Property mDisplayLineName1Visible As Visibility = Visibility.Visible
    Public Property mDisplayLineName2Visible As Visibility = Visibility.Collapsed
    Public Property mDisplayLineName3Visible As Visibility = Visibility.Collapsed

    Public Property mDisplayLineNameStringSize As Integer = 40


    Public mCrossLineList As List(Of LineLocation)
    Public ReadOnly Property pCrossLineCnt As Integer
        Get
            Return mCrossLineList.Count
        End Get
    End Property

    Public Shared mLV0Color As String = "Pink"
    Public Shared mLV1Color As String = "White"
    Public Shared mLV2Color As String = "#999999"
    Public Shared mLV3Color As String = "#999999"

    ''特等 LV=0 一等 LV=1 二等 LV=2 三等 LV=3 招呼 LV=4 甲簡易 LV=5 乙簡易 LV=6 丙簡易 LV=7 號誌站 LV=8 廢站 LV=-1 
    Public mLevel As Integer = 3

    Public ReadOnly Property DisplayName As String
        Get
            If mShowCh Then
                Return mChName
            Else
                Return mEnName
            End If
        End Get
    End Property

    Public Sub New(ID As Integer, ChName As String, EnName As String, Optional tLevel As Integer = 3)
        mID = ID
        mChName = ChName
        mEnName = EnName

        mLevel = tLevel
        Select Case mLevel
            Case 0
                pHLColor = mLV0Color
            Case 1
                pHLColor = mLV1Color
            Case 2
                pHLColor = mLV2Color
            Case 3
                pHLColor = mLV3Color
            Case Else
                pHLColor = mLV3Color
        End Select

        mCrossLineList = GetLineLocationList(Me)

        Select Case mCrossLineList.Count
            Case 1
                mDisplayLineName1Visible = Visibility.Visible
                mDisplayLineName2Visible = Visibility.Collapsed
                mDisplayLineName3Visible = Visibility.Collapsed

                pDisplayLineName1 = GetLineLocationChName(mCrossLineList.Item(0))

                mDisplayLineNameStringSize = 40
            Case 2
                mDisplayLineName1Visible = Visibility.Visible
                mDisplayLineName2Visible = Visibility.Visible
                mDisplayLineName3Visible = Visibility.Collapsed

                pDisplayLineName1 = GetLineLocationChName(mCrossLineList.Item(0))
                pDisplayLineName2 = GetLineLocationChName(mCrossLineList.Item(1))

                mDisplayLineNameStringSize = 30
            Case 3
                mDisplayLineName1Visible = Visibility.Visible
                mDisplayLineName2Visible = Visibility.Visible
                mDisplayLineName3Visible = Visibility.Visible

                pDisplayLineName1 = GetLineLocationChName(mCrossLineList.Item(0))
                pDisplayLineName2 = GetLineLocationChName(mCrossLineList.Item(1))
                pDisplayLineName3 = GetLineLocationChName(mCrossLineList.Item(2))

                mDisplayLineNameStringSize = 20
        End Select
    End Sub

    Public Sub New(ID As Integer, ChName As String, EnName As String, tLevel As Integer, tLat As Double, tLng As Double)
        Me.New(ID, ChName, EnName, tLevel)
        mLat = tLat
        mLng = tLng
    End Sub

    Public Function Clone() As clsStation
        Dim aStation As New clsStation(mID, mChName, mEnName, mLevel)
        Return aStation
    End Function

    Public Shared Function GetStationByChName(ChName As String) As clsStation

        For i As Integer = 0 To GlobalVariables.gStList.Count - 1
            If GlobalVariables.gStList.Item(i).mChName = ChName Then
                Return GlobalVariables.gStList.Item(i).Clone
            End If
        Next

        Return New clsStation(-1, "", "")
    End Function

    Public Shared Function GetCloneStationByIDFromGlobalVariable(theID As String) As clsStation
        For i As Integer = 0 To GlobalVariables.gStList.Count - 1
            If GlobalVariables.gStList.Item(i).mID = theID Then
                Return GlobalVariables.gStList.Item(i).Clone
            End If
        Next
        Return New clsStation(-1, "", "")
    End Function

    Public Shared Function GetStationByID(tGrpList As ObservableCollection(Of clsStGrp), tID As String) As clsStation
        For i As Integer = 0 To tGrpList.Count - 1
            For j As Integer = 0 To tGrpList.Item(i).mStation.Count - 1
                If tGrpList.Item(i).mStation.Item(j).mID = tID Then
                    Return tGrpList.Item(i).mStation.Item(j)
                End If
            Next
        Next
        Return New clsStation(-1, "", "")
    End Function

    Public Shared Function GetStationByID(tStList As List(Of clsStation), tID As String) As clsStation
        For i As Integer = 0 To tStList.Count - 1
            If tStList.Item(i).mID = tID Then
                Return tStList.Item(i)
            End If
        Next
        Return New clsStation(-1, "", "")
    End Function

    ''根據車站位置 判斷 北下 南上 是 順行 還是逆行

    ''判斷 車站有哪些線路經過
    Public Shared Function GetLineLocationList(tStation As clsStation) As List(Of LineLocation)
        Dim _Result As New List(Of LineLocation)
        Dim _tempLine As LineLocation = LineLocation.WL_North

        If tStation.mID < 1100 Then
            _Result.Add(LineLocation.WL_North)
        ElseIf tStation.mID > 1100 AndAlso tStation.mID < 1200 Then
            _Result.Add(LineLocation.WL_Sea)
        ElseIf tStation.mID > 1200 AndAlso tStation.mID < 1300 Then
            _Result.Add(LineLocation.WL_South)
        ElseIf tStation.mID > 1300 AndAlso tStation.mID < 1400 Then
            _Result.Add(LineLocation.WL_Montain)
        ElseIf tStation.mID > 1400 AndAlso tStation.mID < 1500 Then
            _Result.Add(LineLocation.WL_PD)
        ElseIf tStation.mID > 1500 AndAlso tStation.mID < 1600 Then
            _Result.Add(LineLocation.SR)
        ElseIf tStation.mID > 1600 AndAlso tStation.mID < 1700 Then
            _Result.Add(LineLocation.EL_TD)
        ElseIf tStation.mID > 1700 AndAlso tStation.mID < 1800 Then
            _Result.Add(LineLocation.EL_North)
        ElseIf tStation.mID > 1800 AndAlso tStation.mID < 1900 Then
            _Result.Add(LineLocation.EL_EL)
        ElseIf tStation.mID > 1900 AndAlso tStation.mID < 2000 Then
            _Result.Add(LineLocation.BL_PC)
        ElseIf tStation.mID > 2000 AndAlso tStation.mID < 2100 Then
            _Result.Add(LineLocation.BL_PC)
            'ElseIf tStation.mID > 2200 AndAlso tStation.mID < 2300 Then
            '    _Result.Add(LineLocation.BL_NW)
        ElseIf tStation.mID > 2700 AndAlso tStation.mID < 2800 Then
            _Result.Add(LineLocation.BL_GG)
        ElseIf tStation.mID > 5100 AndAlso tStation.mID < 5200 Then
            _Result.Add(LineLocation.BL_SL)
        End If



        ''設定轉運站
        If tStation.mID = 1806 Then
            _Result.Add(LineLocation.BL_PC) '' New clsStation(1806, "三貂嶺", "Sandiaoling"),
        End If

        If tStation.mID = 1025 Then _Result.Add(LineLocation.BL_NWLG) ''  New clsStation(1025, "新竹", "Hsinchu", 1),
        If tStation.mID = 2203 Then _Result.Add(LineLocation.BL_LG) ''   New clsStation(2203, "竹中", "Jhujhong"),
        If tStation.mID = 2203 Then _Result.Add(LineLocation.BL_NW) ''   New clsStation(2203, "竹中", "Jhujhong"),
        If tStation.mID = 1230 Then _Result.Add(LineLocation.BL_SL) ''  New clsStation(1230, "中洲", "Jhongjhou", 2),

        ''設定六家內灣線 
        If tStation.mID = 2212 Then _Result.Add(LineLocation.BL_NWLG) '', "千甲", "Qianjia"),
        If tStation.mID = 2213 Then _Result.Add(LineLocation.BL_NWLG) '', "新莊", "Xinzhuang"),
        If tStation.mID = 2203 Then _Result.Add(LineLocation.BL_NWLG) '', "竹中", "Jhujhong"),

        If tStation.mID = 2214 Then _Result.Add(LineLocation.BL_LG) '', "六家", "Liujia"),

        If tStation.mID = 2204 Then _Result.Add(LineLocation.BL_NW) ''  , "上員", "Shangyuan", 4),
        If tStation.mID = 2205 Then _Result.Add(LineLocation.BL_NW) ''  , "竹東", "Jhudong", 2),
        If tStation.mID = 2206 Then _Result.Add(LineLocation.BL_NW) ''  , "橫山", "Hengshan", 4),
        If tStation.mID = 2207 Then _Result.Add(LineLocation.BL_NW) ''  , "九讚頭", "jiouzantou"),
        If tStation.mID = 2208 Then _Result.Add(LineLocation.BL_NW) ''  , "合興", "Hesing", 4),
        If tStation.mID = 2209 Then _Result.Add(LineLocation.BL_NW) ''  , "富貴", "Fuguei", 4),
        If tStation.mID = 2210 Then _Result.Add(LineLocation.BL_NW) ''  , "內灣", "Neiwan"),
        If tStation.mID = 2211 Then _Result.Add(LineLocation.BL_NW) ''  , "榮華", "Ronghua", 4),

        Return _Result
    End Function

    Public Function GetLineLocationChName(tLine As LineLocation) As String
        Select Case tLine
            Case LineLocation.WL_North
                Return "縱貫線（北）"
                'Return "北縱貫"
            Case LineLocation.WL_Sea
                Return "海岸線"
            Case LineLocation.WL_Montain
                ' Return "臺中線(山線)"
                Return "臺中線"
            Case LineLocation.WL_CC
                Return "成追線"
            Case LineLocation.WL_South
                Return "縱貫線（南）"
                ' Return "南縱貫"
            Case LineLocation.WL_PD
                Return "屏東線"
            Case LineLocation.EL_EL
                Return "宜蘭線"
            Case LineLocation.EL_North
                Return "北迴線"
            Case LineLocation.EL_TD
                Return "臺東線"
            Case LineLocation.SR
                Return "南迴線"
            Case LineLocation.BL_PC
                Return "平溪線"
            Case LineLocation.BL_CL
                Return "深澳線"
            Case LineLocation.BL_NWLG
                Return "內灣 六家"
            Case LineLocation.BL_NW
                Return "內灣線"
            Case LineLocation.BL_LG
                Return "六家線"
            Case LineLocation.BL_GG
                Return "集集線"
            Case LineLocation.BL_SL
                Return "沙崙線"
        End Select

        Return ""
    End Function

    Public Shared Function GetCloestSt(StList As List(Of clsStation), tCurrentGeoLoc As GeoPosition) As clsStation
        Dim _TempDist As Double
        Dim _ShortDist As Double = 10000000
        Dim _CloestSt As clsStation = Nothing
        Dim _CurrentLoc As New System.Device.Location.GeoCoordinate(tCurrentGeoLoc.Coordinate.Latitude, tCurrentGeoLoc.Coordinate.Longitude)
        Dim _StLoc As System.Device.Location.GeoCoordinate

        For i As Integer = 0 To StList.Count - 1
            _StLoc = New System.Device.Location.GeoCoordinate(StList.Item(i).mLat, StList.Item(i).mLng)
            _TempDist = _CurrentLoc.GetDistanceTo(_StLoc)
            If _TempDist < _ShortDist Then
                _ShortDist = _TempDist
                _CloestSt = StList.Item(i)
            End If
        Next
        Return _CloestSt
    End Function
    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged
End Class