Public Class Observation
    Public id As String
    Public value As Integer
    Public timeTaken As DateTime

    Public Const ID_INDEX As Integer = 0
    Public Const VALUE_INDEX As Integer = 1
    Public Const TIME_TAKEN_INDEX As Integer = 2

    Private Const DESIRED_READING_LEVEL As Integer = 1000

    Public Sub New(id As String, value As Integer, timeTaken As String)
        Me.id = id
        Me.value = value
        Me.timeTaken = Convert.ToDateTime(timeTaken)
    End Sub

    Public Function isUnderDesiredReadingLevel() As Boolean
        Return value < DESIRED_READING_LEVEL
    End Function

    Public Function isOverDesiredReadingLevel() As Boolean
        Return value > DESIRED_READING_LEVEL
    End Function

    Public Function isOnDesiredReadingLevel() As Boolean
        Return value = DESIRED_READING_LEVEL
    End Function
End Class