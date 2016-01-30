Public Class Histogram
    Private biggestNumber As Integer
    Private smallestNumber As Integer
    Private lineLength As Integer

    Private Const DEFAULT_DISPLAY_CHARACTER As String = "*"
    Private Const DEFAULT_SPECIAL_DISPLAY_CHARACTER As String = "="
    Public Const DEFAULT_LINE_LENGTH As Integer = 65

    Private _scale As Integer = 1
    Private _scaleIsSet As Boolean = False

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

    Public Function getScale() As Integer
        If Not _scaleIsSet Then
            _scale = biggestNumber - smallestNumber
        End If

        Return _scale
    End Function

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