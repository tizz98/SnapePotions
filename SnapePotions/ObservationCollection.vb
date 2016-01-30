'------------------------------------------------------------
'-             File Name: ObservationCollection             -
'-                 Part of Project: Assign3                 -
'------------------------------------------------------------
'-                Written By: Elijah Wilson                 -
'-                  Written On: 01/30/2016                  -
'------------------------------------------------------------
'- File Purpose:                                            -
'-                                                          -
'- Contains the ObservationCollection class, which has      -
'- aggregate data of the Observations added to it.          -
'------------------------------------------------------------
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

    '------------------------------------------------------------
    '-                   Subprogram Name: Add                   -
    '------------------------------------------------------------
    '-                Written By: Elijah Wilson                 -
    '-                  Written On: 01/30/2016                  -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-                                                          -
    '- Add a new Observation to this collection. It also        -
    '- updates certain statistics based on the Observation      -
    '- itself.                                                  -
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- observation - The Observation being added to the         -
    '-               collection                                 -
    '------------------------------------------------------------
    '- Local Variable Dictionary (alphabetically):              -
    '- (None)                                                   -
    '------------------------------------------------------------
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

    '------------------------------------------------------------
    '-                Function Name: getAverage                 -
    '------------------------------------------------------------
    '-                Written By: Elijah Wilson                 -
    '-                  Written On: 01/30/2016                  -
    '------------------------------------------------------------
    '- Function Purpose:                                        -
    '-                                                          -
    '- This gets the average observation values based on the    -
    '- accumulated total and the total count. If there aren't   -
    '- any observations added yet, it returns 0.0               -
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- (None)                                                   -
    '------------------------------------------------------------
    '- Local Variable Dictionary (alphabetically):              -
    '- (None)                                                   -
    '------------------------------------------------------------
    '- Returns:                                                 -
    '- Decimal - The average observation value or 0.0 if there  -
    '-           aren't any observations added yet              -
    '------------------------------------------------------------
    Public Function getAverage() As Decimal
        Return IIf(totalCount = 0, 0.0, observationAccumulatedTotal / totalCount)
    End Function
End Class