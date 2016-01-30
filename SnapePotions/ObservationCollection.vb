Public Class ObservationCollection
    Public totalCount As Integer = 0
    Public underCount As Integer = 0
    Public overCount As Integer = 0
    Public onCount As Integer = 0
    Public minObservation As Integer = 0
    Public maxObservation As Integer = 0
    Public observationAccumulatedTotal As Integer = 0

    Public observations As New List(Of Observation)

    Public Sub Add(observation As Observation)
        observations.Add(observation)

        totalCount += 1
        observationAccumulatedTotal += observation.value

        If observation.isUnderDesiredReadingLevel() Then
            underCount += 1
        ElseIf observation.isOverDesiredReadingLevel() Then
            overCount += 1
        ElseIf observation.isOnDesiredReadingLevel() Then
            onCount += 1
        End If

        If observation.value >= maxObservation Then
            maxObservation = observation.value
        End If

        If observation.value <= minObservation Then
            minObservation = observation.value
        End If
    End Sub

    Public Function getAverage() As Decimal
        Return observationAccumulatedTotal / totalCount
    End Function
End Class