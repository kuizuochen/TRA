Imports System.ComponentModel

Public Class vmTrain 
    Implements INotifyPropertyChanged

    Public Property mFromSt As clsStation
    Public Property mEndSt As clsStation
    Public Property mFromTimeInfo As clsTimeInfo
    Public Property mEndTimeInfo As clsTimeInfo
    Public Property mSpnTime As String
    Public Property mArrTime As String
    Public Property mEstTime As String

    Public Property mTrain As clsTrain
    Public Property mTrainTypeID As String
    Public Property mFrmStTimeEndStTimeString As String
    Public Property mSpanTimeString As String


    Public Sub New(tTrain As clsTrain, FromSt As clsStation)
        mTrain = tTrain

        mFromSt = FromSt

        For i As Integer = 0 To tTrain.pTimeInfoList.Count - 1
            If tTrain.pTimeInfoList.Item(i).pStation.mID = mFromSt.mID Then
                mFromTimeInfo = tTrain.pTimeInfoList.Item(i)
            End If
        Next

        Dim _EstTime As TimeSpan = mFromTimeInfo.mARRTime - DateTime.Now
        mEstTime = _EstTime.Hours.ToString + ":" + _EstTime.Minutes.ToString

        ''   mFrmStTimeEndStTimeString = mFromSt.DisplayName + mFromTimeInfo.mARRTime.Hour.ToString + ":" + mFromTimeInfo.mARRTime.Minute.ToString
        mFrmStTimeEndStTimeString = mFromSt.DisplayName + mFromTimeInfo.mARRTime.ToString("hh:mm")
    End Sub

    Public Sub New(tTrain As clsTrain, FromSt As clsStation, EndSt As clsStation)
        mTrain = tTrain

        mFromSt = FromSt
        mEndSt = EndSt

        For i As Integer = 0 To tTrain.pTimeInfoList.Count - 1
            If tTrain.pTimeInfoList.Item(i).pStation.mID = mFromSt.mID Then
                mFromTimeInfo = tTrain.pTimeInfoList.Item(i)
            End If
            If tTrain.pTimeInfoList.Item(i).pStation.mID = mEndSt.mID Then
                mEndTimeInfo = tTrain.pTimeInfoList.Item(i)
            End If
        Next

        Dim _SpnTime As TimeSpan = mEndTimeInfo.mARRTime - mFromTimeInfo.mDEPTime

        mFrmStTimeEndStTimeString = mFromSt.DisplayName + mFromTimeInfo.mARRTime.Hour.ToString + ":" + mFromTimeInfo.mARRTime.Minute.ToString + "->" + mEndSt.DisplayName + mEndTimeInfo.mARRTime.Hour.ToString + ":" + mEndTimeInfo.mARRTime.Minute.ToString
        mSpanTimeString = "行駛時間" + _SpnTime.ToString().Split(":")(0) + ":" + _SpnTime.ToString().Split(":")(1)

    End Sub


    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged
End Class