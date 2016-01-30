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
        If observation.timeTaken >= FIRST_SHIFT_START And observation.timeTaken <= FIRST_SHIFT_END Then
            Return Shift.First
        ElseIf observation.timeTaken >= SECOND_SHIFT_START And observation.timeTaken <= SECOND_SHIFT_END Then
            Return Shift.Second
        ElseIf observation.timeTaken >= THIRD_SHIFT_START And observation.timeTaken <= THIRD_SHIFT_END Then
            Return Shift.Third
        End If

        Return Nothing
    End Function

    Public Function getAllShiftMin() As Integer
        Dim tempMin As Integer = 0

        If firstShiftObservations.minObservation <= tempMin Then
            tempMin = firstShiftObservations.minObservation
        End If

        If secondShiftObservations.minObservation <= tempMin Then
            tempMin = secondShiftObservations.minObservation
        End If

        If thirdShiftObservations.minObservation <= tempMin Then
            tempMin = thirdShiftObservations.minObservation
        End If

        Return tempMin
    End Function

    Public Function getAllShiftMax() As Integer
        Dim tempMax As Integer = 0

        If firstShiftObservations.maxObservation >= tempMax Then
            tempMax = firstShiftObservations.maxObservation
        End If

        If secondShiftObservations.maxObservation >= tempMax Then
            tempMax = secondShiftObservations.maxObservation
        End If

        If thirdShiftObservations.maxObservation >= tempMax Then
            tempMax = thirdShiftObservations.maxObservation
        End If

        Return tempMax
    End Function

    Public Function getAllObservationsSortedByValue() As List(Of Observation)
        Dim allObservations As List(Of Observation) = firstShiftObservations.observations
        allObservations.AddRange(secondShiftObservations.observations)
        allObservations.AddRange(thirdShiftObservations.observations)

        allObservations.Sort(Function(x, y) x.value.CompareTo(y.value))  ' sort in ascending order

        Return allObservations
    End Function

    Private Enum Shift
        First
        Second
        Third
    End Enum
End Class