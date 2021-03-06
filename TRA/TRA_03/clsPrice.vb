﻿Public Class clsLoc_St
    Public mChName As String
    Public mLoc As Double

    Public Sub New(tChName As String, tLoc As Double)
        mChName = tChName
        mLoc = tLoc
    End Sub
End Class

Public Class clsLoc_Line
    Public mChName As String 
    Public mLocStList As List(Of clsLoc_St)

    Public Sub New(tChName As String, tLocStList As List(Of clsLoc_St))
        mChName = tChName
        mLocStList = tLocStList
    End Sub
End Class

Public Class clsPrice

    Public Shared Sub GetLoc(tSt As clsStation, ByRef tLocList As List(Of Double), ByRef tLineList As List(Of clsLoc_Line))
        tLocList = New List(Of Double)
        tLineList = New List(Of clsLoc_Line)

        For i As Integer = 0 To GlobalVariables.gLocLineList.Count - 1
            For k As Integer = 0 To GlobalVariables.gLocLineList.Item(i).mLocStList.Count - 1
                If GlobalVariables.gLocLineList.Item(i).mLocStList.Item(k).mChName = tSt.mChName Then
                    tLocList.Add(GlobalVariables.gLocLineList.Item(i).mLocStList.Item(k).mLoc)
                    tLineList.Add(GlobalVariables.gLocLineList.Item(i))
                    GoTo NextLine
                End If
            Next
NextLine:
        Next

    End Sub

    Public Shared Function GetLoc(tSt As clsStation, tLine As clsLoc_Line) As Double
        For i As Integer = 0 To tLine.mLocStList.Count - 1
            If tSt.mChName = tLine.mLocStList.Item(i).mChName Then
                Return tLine.mLocStList.Item(i).mLoc
            End If
        Next
        Return -1
    End Function

    Public Shared Function GetPrice(tStartSt As clsStation, tEndSt As clsStation, tClass As CarClass) As Integer

        Dim _StartStLocListList As New List(Of Double)
        Dim _StartStLineList As New List(Of clsLoc_Line)

        Dim _StartStAtLine As clsLoc_Line = Nothing
        Dim _EndStAtLine As clsLoc_Line = Nothing
        Dim _StartStLoc As Double
        Dim _EndStLoc As Double

        Dim _TotalDistance As Double = 0

        GetLoc(tStartSt, _StartStLocListList, _StartStLineList)

        For i As Integer = 0 To _StartStLineList.Count - 1
            For j As Integer = 0 To _StartStLineList.Item(i).mLocStList.Count - 1
                If tEndSt.mChName = _StartStLineList.Item(i).mLocStList.Item(j).mChName Then
                    _StartStAtLine = _StartStLineList.Item(i)
                    _EndStAtLine = _StartStLineList.Item(i)
                    _StartStLoc = _StartStLocListList(i)
                    _EndStLoc = _StartStLineList.Item(i).mLocStList.Item(j).mLoc
                End If
            Next
        Next

        ''正常情況
        If _StartStAtLine IsNot Nothing AndAlso _EndStAtLine IsNot Nothing AndAlso _StartStAtLine.mChName = _EndStAtLine.mChName Then
            _TotalDistance = Math.Abs(_StartStLoc - _EndStLoc)
            Return GetPrice(_TotalDistance, tClass)
        End If



        '山海線計價方式: 
        'A.	 由竹南以北各站至彰化以南各站或由彰化以南各站至竹南以北各站之票價，一律按經由山線之里程計算。
        'B.	 由竹南以北或彰化以南各站至山、海線各站或由山、海線各站至竹南以北或彰化以南各站之票價。一律 按最短里程計算。
        'C.	 由山線各站至海線各站，或由海線各站至山線各站之票價，一律按列車行駛方向實際里程計算。 
        '' 竹南 125.4 山線
        '' 彰化 210.9 山線
        '' 彰化 90.2 海線
        Dim _EndStLocListList As New List(Of Double)
        Dim _EndStLineList As New List(Of clsLoc_Line)
        GetLoc(tEndSt, _EndStLocListList, _EndStLineList)
 

        ''開始站 和 終點在 分別在 山線 與海線
        If _StartStLineList.Count = 1 And _EndStLineList.Count = 1 Then
            ''S站 在海線 M站 在山線
            Dim _StLocS As Double = 0
            Dim _StLocM As Double = 0
            If _StartStLineList.Item(0).mChName = "西部幹線海" And _EndStLineList.Item(0).mChName = "西部幹線" Then
                _StLocS = _StartStLocListList.Item(0)
                _StLocM = _EndStLocListList.Item(0)
            ElseIf _StartStLineList.Item(0).mChName = "西部幹線" And _EndStLineList.Item(0).mChName = "西部幹線海" Then
                _StLocS = _EndStLocListList.Item(0)
                _StLocM = _StartStLocListList.Item(0)
            Else
                GoTo OverlapLine
            End If
             
            If _StLocM < 125.4 Then
                _TotalDistance = 125.4 - _StLocM
                _TotalDistance += _StLocS
                Return GetPrice(_TotalDistance, tClass)
            ElseIf _StLocM > 125.4 And _StLocM < 210.9 Then
                Dim _DistBypassGN As Double = 0
                Dim _DistBypassGH As Double = 0
                _DistBypassGN = _StLocM - 125.4
                _DistBypassGN += _StLocS

                _DistBypassGH = 210.9 - _StLocM
                _DistBypassGH += 90.2 - _StLocS

                _TotalDistance = Math.Min(_DistBypassGH, _DistBypassGN)
                Return GetPrice(_TotalDistance, tClass)
            ElseIf _StLocM > 210.9 Then
                _TotalDistance = _StLocM - 210.9
                _TotalDistance += 90.2 - _StLocS

                Return GetPrice(_TotalDistance, tClass)
            End If  
        End If

        ''跨線情況
OverlapLine:
        Try
            _TotalDistance = GetDistance_OverlapLine(tStartSt, tEndSt)
            Return GetPrice(_TotalDistance, tClass)
        Catch ex As Exception

        End Try


        Return 0
    End Function


    Private Shared Function GetDistance_OverlapLine(tStartSt As clsStation, tEndSt As clsStation) As Double
        Dim _StartLocInSeaLine As Double = -1
        Dim _EndLocInSeaLine As Double = -1
        Dim _StartLocInMountainLine As Double = -1
        Dim _EndLocInMountainLine As Double = -1

        Dim _Dist1 As Double = 0
        Dim _Dist2 As Double = 0

        For i As Integer = 0 To GlobalVariables.gLoc_CirMountain.mLocStList.Count - 1
            With GlobalVariables.gLoc_CirMountain.mLocStList
                If .Item(i).mChName = tStartSt.mChName Then
                    _StartLocInMountainLine = .Item(i).mLoc
                End If
                If .Item(i).mChName = tEndSt.mChName Then
                    _EndLocInMountainLine = .Item(i).mLoc
                End If
                If _EndLocInMountainLine > -1 And _StartLocInMountainLine > -1 Then
                    Exit For
                End If
            End With
        Next

        If _StartLocInMountainLine > -1 And _EndLocInMountainLine > -1 Then
            _Dist1 = Math.Abs(_EndLocInMountainLine - _StartLocInMountainLine)
            _Dist2 = 876.9 - Math.Max(_StartLocInMountainLine, _EndLocInMountainLine) + Math.Min(_StartLocInMountainLine, _EndLocInMountainLine)
            Return Math.Min(_Dist1, _Dist2)
        End If

        For i As Integer = 0 To GlobalVariables.gLoc_CirSea.mLocStList.Count - 1
            With GlobalVariables.gLoc_CirSea.mLocStList
                If .Item(i).mChName = tStartSt.mChName Then
                    _StartLocInSeaLine = .Item(i).mLoc
                End If
                If .Item(i).mChName = tEndSt.mChName Then
                    _EndLocInSeaLine = .Item(i).mLoc
                End If
                If _StartLocInSeaLine > -1 And _EndLocInSeaLine > -1 Then
                    Exit For
                End If
            End With
        Next

        If _StartLocInSeaLine > -1 And _EndLocInSeaLine > -1 Then
            _Dist1 = Math.Abs(_EndLocInSeaLine - _StartLocInSeaLine)
            _Dist2 = 881.6 - Math.Max(_StartLocInSeaLine, _EndLocInSeaLine) + Math.Min(_StartLocInSeaLine, _EndLocInSeaLine)
            Return Math.Min(_Dist1, _Dist2)
        End If

        Return 0
    End Function

    Private Shared Function GetPrice(tLength As Double, tClass As CarClass) As Integer
        '各車種現行起碼里程為10公里計價，不滿10公里，以10公里計價。


        '(1)	 普通（快）車：每人每公里1.06元。	
        '(2)	 復興號/區間車：每人每公里1.46元。
        '(3)	 莒光號：每人每公里1.75元。
        '(4)	 自強號：每人每公里2.27元。
        Dim _PricePerKM As Double
        Select Case tClass
            Case CarClass.Ordinarytrain
                _PricePerKM = 1.06
            Case CarClass.FastLocalTrain, CarClass.LocalTrain, CarClass.Fu_HsingSemiExpress
                _PricePerKM = 1.46
            Case CarClass.Chu_KuangExpress
                _PricePerKM = 1.75
            Case CarClass.Tze_ChiangLimitedExpress1, CarClass.Tze_ChiangLimitedExpress2, CarClass.Tze_ChiangLimitedExpressNew, CarClass.Tze_ChiangLimitedExpressTL
                _PricePerKM = 2.27
        End Select

        If tLength < 10 Then Return tLength = 10

        Return _PricePerKM * tLength

    End Function

    Public Shared Function StationPriceTesting() As Boolean

        For i As Integer = 0 To GlobalVariables.gStGrpList.Count - 1
            For j As Integer = 0 To GlobalVariables.gStGrpList.Item(i).mStation.Count - 1
                Dim a As List(Of Double)
                Dim b As List(Of clsLoc_Line)
                clsPrice.GetLoc(GlobalVariables.gStGrpList.Item(i).mStation.Item(j), a, b)

                If b.Count = 0 Then
                    Dim gg As String = GlobalVariables.gStGrpList.Item(i).mStation.Item(j).mChName
                    Dim c As Integer = 1
                    Return False
                End If
            Next
        Next

        For i As Integer = 0 To GlobalVariables.gLocLineList.Count - 1
            For j As Integer = 0 To GlobalVariables.gLocLineList.Item(i).mLocStList.Count - 1
                Dim tempS As clsStation = clsStation.GetStationByChName(GlobalVariables.gLocLineList.Item(i).mLocStList.Item(j).mChName)
                If tempS.mChName = "" Then
                    Dim k As Integer = 10
                    Return False
                End If
            Next
        Next

        Return True
    End Function
End Class
