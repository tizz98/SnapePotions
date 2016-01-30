'------------------------------------------------------------
'-                 File Name: ShiftManager                  -
'-                 Part of Project: Assign3                 -
'------------------------------------------------------------
'-                Written By: Elijah Wilson                 -
'-                  Written On: 01/30/2016                  -
'------------------------------------------------------------
'- File Purpose:                                            -
'-                                                          -
'- Contains the ShiftManager class. The ShiftManager class  -
'- manages all the shifts and where data should be stored.  -
'- It is also responsible for parsing a data file.          -
'------------------------------------------------------------
Imports Microsoft.VisualBasic.FileIO

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

    '------------------------------------------------------------
    '-                   Subprogram Name: Add                   -
    '------------------------------------------------------------
    '-                Written By: Elijah Wilson                 -
    '-                  Written On: 01/30/2016                  -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-                                                          -
    '- Adds an observation to the correct shift observation     -
    '- collection.                                              -
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- observation - The observation to be added                -
    '------------------------------------------------------------
    '- Local Variable Dictionary (alphabetically):              -
    '- shiftForObservation - The determined shift when the      -
    '-                       observation was taken              -
    '------------------------------------------------------------
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

    '------------------------------------------------------------
    '-         Subprogram Name: getShiftFromObservation         -
    '------------------------------------------------------------
    '-                Written By: Elijah Wilson                 -
    '-                  Written On: 01/30/2016                  -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-                                                          -
    '- This returns which shift the observation was taken in.   -
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- observation - The observation that needs to be           -
    '-               determined which shift it was taken        -
    '------------------------------------------------------------
    '- Local Variable Dictionary (alphabetically):              -
    '- (None)                                                   -
    '------------------------------------------------------------
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

    '------------------------------------------------------------
    '-              Function Name: getAllShiftMin               -
    '------------------------------------------------------------
    '-                Written By: Elijah Wilson                 -
    '-                  Written On: 01/30/2016                  -
    '------------------------------------------------------------
    '- Function Purpose:                                        -
    '-                                                          -
    '- This finds the minimum observation value from all of the -
    '- shifts.                                                  -
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- (None)                                                   -
    '------------------------------------------------------------
    '- Local Variable Dictionary (alphabetically):              -
    '- tempMin - The minimum value that is used to compare all  -
    '-           three shifts and is eventually returned        -
    '------------------------------------------------------------
    '- Returns:                                                 -
    '- Integer - The minimum observation value from all of the  -
    '-           shifts.                                        -
    '------------------------------------------------------------
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

    '------------------------------------------------------------
    '-              Function Name: getAllShiftMax               -
    '------------------------------------------------------------
    '-                Written By: Elijah Wilson                 -
    '-                  Written On: 01/30/2016                  -
    '------------------------------------------------------------
    '- Function Purpose:                                        -
    '-                                                          -
    '- This finds the maximum observation value from all of the -
    '- shifts.                                                  -
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- (None)                                                   -
    '------------------------------------------------------------
    '- Local Variable Dictionary (alphabetically):              -
    '- tempMax - The maximum value that is used to compare all  -
    '-           three shifts and is eventually returned        -
    '------------------------------------------------------------
    '- Returns:                                                 -
    '- Integer - The maximum observation value from all of the  -
    '-           shifts.                                        -
    '------------------------------------------------------------
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

    '------------------------------------------------------------
    '-            Function Name: getAllObservations             -
    '------------------------------------------------------------
    '-                Written By: Elijah Wilson                 -
    '-                  Written On: 01/30/2016                  -
    '------------------------------------------------------------
    '- Function Purpose:                                        -
    '-                                                          -
    '- Returns all of the observations from all shifts.         -
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- (None)                                                   -
    '------------------------------------------------------------
    '- Local Variable Dictionary (alphabetically):              -
    '- allObservations - An accumulation of all the shifts, it  -
    '-                   is eventually returned                 -
    '------------------------------------------------------------
    '- Returns:                                                 -
    '- List(Of Observation) - All of the shift observations     -
    '-                        combined                          -
    '------------------------------------------------------------
    Public Function getAllObservations() As List(Of Observation)
        Dim allObservations As New List(Of Observation)
        allObservations.AddRange(firstShiftObservations.observations)
        allObservations.AddRange(secondShiftObservations.observations)
        allObservations.AddRange(thirdShiftObservations.observations)

        Return allObservations
    End Function

    '------------------------------------------------------------
    '-      Function Name: getAllObservationsSortedByValue      -
    '------------------------------------------------------------
    '-                Written By: Elijah Wilson                 -
    '-                  Written On: 01/30/2016                  -
    '------------------------------------------------------------
    '- Function Purpose:                                        -
    '-                                                          -
    '- Returns all of the observations from all shifts sorted   -
    '- by their value                                           -
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- (None)                                                   -
    '------------------------------------------------------------
    '- Local Variable Dictionary (alphabetically):              -
    '- (None)                                                   -
    '------------------------------------------------------------
    '- Returns:                                                 -
    '- List(Of Observation) - All of the shift observations     -
    '-                        combined and sorted by their      -
    '-                        value                             -
    '------------------------------------------------------------
    Public Function getAllObservationsSortedByValue() As List(Of Observation)
        Return getAllObservationsSortedByValue(getAllObservations())
    End Function

    '------------------------------------------------------------
    '-      Function Name: getAllObservationsSortedByValue      -
    '------------------------------------------------------------
    '-                Written By: Elijah Wilson                 -
    '-                  Written On: 01/30/2016                  -
    '------------------------------------------------------------
    '- Function Purpose:                                        -
    '-                                                          -
    '- Returns all of the observations from all shifts sorted   -
    '- by their value                                           -
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- allObservations - All of the observations needed to be   -
    '-                   sorted                                 -
    '------------------------------------------------------------
    '- Local Variable Dictionary (alphabetically):              -
    '- (None)                                                   -
    '------------------------------------------------------------
    '- Returns:                                                 -
    '- List(Of Observation) - All of the shift observations     -
    '-                        combined and sorted by their      -
    '-                        value                             -
    '------------------------------------------------------------
    Public Function getAllObservationsSortedByValue(allObservations As List(Of Observation)) As List(Of Observation)
        allObservations.Sort(Function(x, y) x.value.CompareTo(y.value))  ' sort in ascending order
        Return allObservations
    End Function

    '------------------------------------------------------------
    '-                      Function Name:                      -
    '-       getAllObservationsSortedByValueAndBaseValue        -
    '------------------------------------------------------------
    '-                Written By: Elijah Wilson                 -
    '-                  Written On: 01/30/2016                  -
    '------------------------------------------------------------
    '- Function Purpose:                                        -
    '-                                                          -
    '- Returns all of the observations from all shifts sorted   -
    '- by their value and a placeholder observation if needed   -
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- (None)                                                   -
    '------------------------------------------------------------
    '- Local Variable Dictionary (alphabetically):              -
    '- allObservations - All of the observations from all       -
    '-                   shifts                                 -
    '- hasBaseLineValue - Whether or not all the observations   -
    '-                    have an observation with a value of   -
    '-                    the desired reading level             -
    '------------------------------------------------------------
    '- Returns:                                                 -
    '- List(Of Observation) - All of the shift observations     -
    '-                        combined and sorted by their      -
    '-                        value as well as a placeholder    -
    '-                        observation if needed             -
    '------------------------------------------------------------
    Public Function getAllObservationsSortedByValueAndBaseValue() As List(Of Observation)
        Dim allObservations As List(Of Observation) = getAllObservations()
        Dim hasBaseLineValue As Boolean = allObservations.Any(Function(o) o.value = Observation.DESIRED_READING_LEVEL)

        If Not hasBaseLineValue Then
            allObservations.Add(New Observation("", Observation.DESIRED_READING_LEVEL, "00:00"))
        End If

        Return getAllObservationsSortedByValue(allObservations)
    End Function

    '------------------------------------------------------------
    '-         Subprogram Name: parseInputReadingsFile          -
    '------------------------------------------------------------
    '-                Written By: Elijah Wilson                 -
    '-                  Written On: 01/30/2016                  -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-                                                          -
    '- Parses an input file and saves the data to the           -
    '- ShiftManager object.                                     -
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- readingsPath - The path of the file to be parsed         -
    '------------------------------------------------------------
    '- Local Variable Dictionary (alphabetically):              -
    '- myReader - A TextFieldParser that is used to parse the   -
    '-            input file                                    -
    '------------------------------------------------------------
    Public Sub parseInputReadingsFile(readingsPath As String)
        Using myReader As New TextFieldParser(readingsPath)
            myReader.TextFieldType = FieldType.Delimited
            myReader.Delimiters = New String() {vbTab}

            Dim currentRow As String()

            While Not myReader.EndOfData
                Try
                    currentRow = myReader.ReadFields()

                    Me.Add(getObservationFromRow(currentRow))
                Catch ex As MalformedLineException
                    MsgBox("Line " & ex.Message & " is invalid. Skipping.")
                End Try
            End While
        End Using
    End Sub

    '------------------------------------------------------------
    '-           Function Name: getObservationFromRow           -
    '------------------------------------------------------------
    '-                Written By: Elijah Wilson                 -
    '-                  Written On: 01/30/2016                  -
    '------------------------------------------------------------
    '- Function Purpose:                                        -
    '-                                                          -
    '- Returns a new Observation object from the input row      -
    '- array.                                                   -
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- row - Array of strings containing the observation data   -
    '------------------------------------------------------------
    '- Local Variable Dictionary (alphabetically):              -
    '- (None)                                                   -
    '------------------------------------------------------------
    '- Returns:                                                 -
    '- Observation - Observation based off of the input row.    -
    '------------------------------------------------------------
    Private Function getObservationFromRow(row As String()) As Observation
        Return New Observation(row(Observation.ID_INDEX),
                               row(Observation.VALUE_INDEX),
                               row(Observation.TIME_TAKEN_INDEX))
    End Function

    Private Enum Shift
        First
        Second
        Third
    End Enum
End Class