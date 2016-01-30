Public Class ShiftManager
    Public firstShiftObservations As New ObservationCollection()
    Public secondShiftObservations As New ObservationCollection()
    Public thirdShiftObservations As New ObservationCollection()

    Private Const FIRST_SHIFT_START As DateTime = #8:00#
    Private Const FIRST_SHIFT_END As DateTime = #15:59#

    Private Const SECOND_SHIFT_START As DateTime = #16:00#
    Private Const SECOND_SHIFT_END As DateTime = #23:59#

    Private Const THIRD_SHIFT_START As DateTime = #00:00#
    Private Const THIRD_SHIFT_END As DateTime = #7:59#

    Public Sub Add(observation As Observation)
        Dim shiftForObservation As Shift = getShiftFromObservation(observation)

        Select Case shiftForObservation
            Case Shift.First
                firstShiftObservations.Add(observation)
            Case Shift.Second
                secondShiftObservations.Add(observation)
            Case Shift.Third
                thirdShiftObservations.Add(observation)
        End Select
    End Sub

    Private Function getShiftFromObservation(observation As Observation) As Shift
        ' .TimeOfDay is needed so that we only compare the times and not the dates
        If (observation.timeTaken.TimeOfDay >= FIRST_SHIFT_START.TimeOfDay) And (observation.timeTaken.TimeOfDay <= FIRST_SHIFT_END.TimeOfDay) Then
            Return Shift.First
        ElseIf (observation.timeTaken.TimeOfDay >= SECOND_SHIFT_START.TimeOfDay) And (observation.timeTaken.TimeOfDay <= SECOND_SHIFT_END.TimeOfDay) Then
            Return Shift.Second
        ElseIf (observation.timeTaken.TimeOfDay >= THIRD_SHIFT_START.TimeOfDay) And (observation.timeTaken.TimeOfDay <= THIRD_SHIFT_END.TimeOfDay) Then
            Return Shift.Third
        End If

        Return Nothing
    End Function

    Public Function getAllShiftMin() As Integer
        Dim tempMin As Integer = 0

        If firstShiftObservations.observations.Count > 0 Then
            tempMin = firstShiftObservations.minObservation
        End If

        If secondShiftObservations.observations.Count > 0 AndAlso secondShiftObservations.minObservation <= tempMin Then
            tempMin = secondShiftObservations.minObservation
        End If

        If thirdShiftObservations.observations.Count > 0 AndAlso thirdShiftObservations.minObservation <= tempMin Then
            tempMin = thirdShiftObservations.minObservation
        End If

        Return tempMin
    End Function

    Public Function getAllShiftMax() As Integer
        Dim tempMax As Integer = 0

        If firstShiftObservations.observations.Count > 0 Then
            tempMax = firstShiftObservations.maxObservation
        End If

        If secondShiftObservations.observations.Count > 0 AndAlso secondShiftObservations.maxObservation >= tempMax Then
            tempMax = secondShiftObservations.maxObservation
        End If

        If thirdShiftObservations.observations.Count > 0 AndAlso thirdShiftObservations.maxObservation >= tempMax Then
            tempMax = thirdShiftObservations.maxObservation
        End If

        Return tempMax
    End Function

    Public Function getAllObservations() As List(Of Observation)
        Dim allObservations As List(Of Observation) = firstShiftObservations.observations
        allObservations.AddRange(secondShiftObservations.observations)
        allObservations.AddRange(thirdShiftObservations.observations)

        Return allObservations
    End Function

    Public Function getAllObservationsSortedByValue() As List(Of Observation)
        Return getAllObservationsSortedByValue(getAllObservations())
    End Function

    Public Function getAllObservationsSortedByValue(allObservations As List(Of Observation)) As List(Of Observation)
        allObservations.Sort(Function(x, y) x.value.CompareTo(y.value))  ' sort in ascending order
        Return allObservations
    End Function

    Public Function getAllObservationsSortedByValueAndBaseValue() As List(Of Observation)
        Dim allObservations As List(Of Observation) = getAllObservations()
        Dim hasBaseLineValue As Boolean = allObservations.Any(Function(o) o.value = 1000)

        If Not hasBaseLineValue Then
            allObservations.Add(New Observation("", 1000, "00:00"))
        End If

        Return getAllObservationsSortedByValue(allObservations)
    End Function

    Private Enum Shift
        First
        Second
        Third
    End Enum
End Class