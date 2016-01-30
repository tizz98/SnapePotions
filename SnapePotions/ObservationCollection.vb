Public Class ObservationCollection
    Public totalCount As Integer = 0
    Public underCount As Integer = 0
    Public overCount As Integer = 0
    Public onCount As Integer = 0
    Public minObservation As Integer = 0
    Public maxObservation As Integer = 0
    Public observationAccumulatedTotal As Integer = 0

    Public observations As New List(Of Observation)

    Private minObservationSet As Boolean = False
    Private maxObservationSet As Boolean = False

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

        If Not maxObservationSet Then
            maxObservation = observation.value
            maxObservationSet = True
        ElseIf (observation.value >= maxObservation) Then
            maxObservation = observation.value
        End If

        If Not minObservationSet Then
            minObservation = observation.value
            minObservationSet = True
        ElseIf (observation.value <= minObservation) Then
            minObservation = observation.value
        End If
    End Sub

    Public Function getAverage() As Decimal
        Return IIf(totalCount = 0, 0.0, observationAccumulatedTotal / totalCount)
    End Function
End Class