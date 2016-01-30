'------------------------------------------------------------
'-                File Name: Observation.vb                 -
'-                 Part of Project: Assign3                 -
'------------------------------------------------------------
'-                Written By: Elijah Wilson                 -
'-                  Written On: 01/30/2016                  -
'------------------------------------------------------------
'- File Purpose:                                            -
'-                                                          -
'- Contains the Observation class                           -
'------------------------------------------------------------
Public Class Observation
    Public id As String
    Public value As Integer
    Public timeTaken As DateTime

    Public Const ID_INDEX As Integer = 0
    Public Const VALUE_INDEX As Integer = 1
    Public Const TIME_TAKEN_INDEX As Integer = 2

    Public Const DESIRED_READING_LEVEL As Integer = 1000

    '------------------------------------------------------------
    '-                   Subprogram Name: Sub                   -
    '------------------------------------------------------------
    '-                Written By: Elijah Wilson                 -
    '-                  Written On: 01/30/2016                  -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-                                                          -
    '- Creates a new Observation object based on the input      -
    '- parameters.                                              -
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- id - The id of the observation                           -
    '- value - The value of the observation                     -
    '- timeTaken - The time the observation was taken           -
    '------------------------------------------------------------
    '- Local Variable Dictionary (alphabetically):              -
    '- (None)                                                   -
    '------------------------------------------------------------
    Public Sub New(id As String, value As Integer, timeTaken As String)
        Me.id = id
        Me.value = value
        Me.timeTaken = Convert.ToDateTime(timeTaken)
    End Sub

    '------------------------------------------------------------
    '-        Function Name: isUnderDesiredReadingLevel         -
    '------------------------------------------------------------
    '-                Written By: Elijah Wilson                 -
    '-                  Written On: 01/30/2016                  -
    '------------------------------------------------------------
    '- Function Purpose:                                        -
    '-                                                          -
    '- Determines whether or not the observation is under the   -
    '- desired reading level.                                   -
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- (None)                                                   -
    '------------------------------------------------------------
    '- Local Variable Dictionary (alphabetically):              -
    '- (None)                                                   -
    '------------------------------------------------------------
    '- Returns:                                                 -
    '- Boolean - Whether or not the observation value is lower  -
    '-           than the desired reading level.                -
    '------------------------------------------------------------
    Public Function isUnderDesiredReadingLevel() As Boolean
        Return value < DESIRED_READING_LEVEL
    End Function

    '------------------------------------------------------------
    '-         Function Name: isOverDesiredReadingLevel         -
    '------------------------------------------------------------
    '-                Written By: Elijah Wilson                 -
    '-                  Written On: 01/30/2016                  -
    '------------------------------------------------------------
    '- Function Purpose:                                        -
    '-                                                          -
    '- Determines whether or not the observation is above the   -
    '- desired reading level.                                   -
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- (None)                                                   -
    '------------------------------------------------------------
    '- Local Variable Dictionary (alphabetically):              -
    '- (None)                                                   -
    '------------------------------------------------------------
    '- Returns:                                                 -
    '- Boolean - Whether or not the observation value is higher -
    '-           than the desired reading level.                -
    '------------------------------------------------------------
    Public Function isOverDesiredReadingLevel() As Boolean
        Return value > DESIRED_READING_LEVEL
    End Function

    '------------------------------------------------------------
    '-          Function Name: isOnDesiredReadingLevel          -
    '------------------------------------------------------------
    '-                Written By: Elijah Wilson                 -
    '-                  Written On: 01/30/2016                  -
    '------------------------------------------------------------
    '- Function Purpose:                                        -
    '-                                                          -
    '- Determines whether or not the observation is at the      -
    '- desired reading level.                                   -
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- (None)                                                   -
    '------------------------------------------------------------
    '- Local Variable Dictionary (alphabetically):              -
    '- (None)                                                   -
    '------------------------------------------------------------
    '- Returns:                                                 -
    '- Boolean - Whether or not the observation value is at the -
    '-           desired reading level.                         -
    '------------------------------------------------------------
    Public Function isOnDesiredReadingLevel() As Boolean
        Return value = DESIRED_READING_LEVEL
    End Function
End Class