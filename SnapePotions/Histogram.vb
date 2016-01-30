'------------------------------------------------------------
'-                 File Name: Histogram.vb                  -
'-                 Part of Project: Assign3                 -
'------------------------------------------------------------
'-                Written By: Elijah Wilson                 -
'-                  Written On: 01/30/2016                  -
'------------------------------------------------------------
'- File Purpose:                                            -
'-                                                          -
'- This file contains the Histogram class, which is         -
'- responsible for creating Histograms.                     -
'------------------------------------------------------------
Public Class Histogram
    Private biggestNumber As Integer
    Private smallestNumber As Integer
    Private lineLength As Integer

    Private Const DEFAULT_DISPLAY_CHARACTER As String = "*"
    Private Const DEFAULT_SPECIAL_DISPLAY_CHARACTER As String = "="
    Public Const DEFAULT_LINE_LENGTH As Integer = 65

    Private _scale As Integer = 1
    Private _scaleIsSet As Boolean = False

    '------------------------------------------------------------
    '-                   Subprogram Name: New                   -
    '------------------------------------------------------------
    '-                Written By: Elijah Wilson                 -
    '-                  Written On: 01/30/2016                  -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-                                                          -
    '- This creates a new instance of the Histogram class. It   -
    '- also accounts for whether or not the biggest and         -
    '- smallest numbers being input are accurate based on the   -
    '- fact that a placeholder observation is artificially      -
    '- inserted if there aren't any of the desired level.       -
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- biggestNumber - The biggest number that will appear on   -
    '-                 the histogram                            -
    '- smallestNumber - The smallest number that will appear on -
    '-                  the histogram                           -
    '- lineLength - The max length the line should be           -
    '------------------------------------------------------------
    '- Local Variable Dictionary (alphabetically):              -
    '- (None)                                                   -
    '------------------------------------------------------------
    Public Sub New(biggestNumber As Integer, smallestNumber As Integer, Optional lineLength As Integer = DEFAULT_LINE_LENGTH)
        ' Make sure that we account for the artificially inserted desired reading level placeholder observation
        If biggestNumber < Observation.DESIRED_READING_LEVEL Then
            Me.biggestNumber = Observation.DESIRED_READING_LEVEL
        Else
            Me.biggestNumber = biggestNumber
        End If

        ' Make sure that we account for the artificially inserted desired reading level placeholder observation
        If smallestNumber > Observation.DESIRED_READING_LEVEL Then
            Me.smallestNumber = Observation.DESIRED_READING_LEVEL
        Else
            Me.smallestNumber = smallestNumber
        End If

        Me.lineLength = lineLength

        getScale()  ' populate "_scale"
    End Sub

    '------------------------------------------------------------
    '-                 Function Name: getScale                  -
    '------------------------------------------------------------
    '-                Written By: Elijah Wilson                 -
    '-                  Written On: 01/30/2016                  -
    '------------------------------------------------------------
    '- Function Purpose:                                        -
    '-                                                          -
    '- This function sets the scale if not already set and then -
    '- returns the scale no matter what.                        -
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- (None)                                                   -
    '------------------------------------------------------------
    '- Local Variable Dictionary (alphabetically):              -
    '- (None)                                                   -
    '------------------------------------------------------------
    '- Returns:                                                 -
    '- Integer - The scale that should be used for the          -
    '-           histogram                                      -
    '------------------------------------------------------------
    Public Function getScale() As Integer
        If Not _scaleIsSet Then
            _scale = biggestNumber - smallestNumber
        End If

        Return _scale
    End Function

    '------------------------------------------------------------
    '-             Function Name: getLineForReading             -
    '------------------------------------------------------------
    '-                Written By: Elijah Wilson                 -
    '-                  Written On: 01/30/2016                  -
    '------------------------------------------------------------
    '- Function Purpose:                                        -
    '-                                                          -
    '- This function returns a line of characters depending on  -
    '- the input reading value and the scale already determined -
    '- by the class. It will change the display character       -
    '- depending on whether or not the isSpecialLine parameter  -
    '- is True or False.                                        -
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- reading - The value of the reading                       -
    '- isSpecialLine - Whether or not the reading is special    -
    '------------------------------------------------------------
    '- Local Variable Dictionary (alphabetically):              -
    '- displayCharacter - Which character to use, to populate   -
    '-                    the line                              -
    '- returnString - The string that will be returned          -
    '------------------------------------------------------------
    '- Returns:                                                 -
    '- String - A line containing a certain number of the       -
    '-          determined character.                           -
    '------------------------------------------------------------
    Public Function getLineForReading(reading As Integer, Optional isSpecialLine As Boolean = False) As String
        Dim returnString As String = ""
        Dim displayCharacter As String = IIf(isSpecialLine,
                                             DEFAULT_SPECIAL_DISPLAY_CHARACTER,
                                             DEFAULT_DISPLAY_CHARACTER)
        Dim scale As Integer = getScale()
        Dim useReadingAsScale As Boolean = False

        If scale = 0 Then
            useReadingAsScale = True
        End If

        For i As Integer = 0 To ((reading - smallestNumber) / IIf(useReadingAsScale, reading, scale)) * lineLength
            returnString &= displayCharacter
        Next

        Return returnString
    End Function
End Class