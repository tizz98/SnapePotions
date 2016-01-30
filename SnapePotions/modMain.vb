Imports Microsoft.VisualBasic.FileIO
Imports System.IO
Imports System.Text.RegularExpressions

Module modMain

    Sub Main()
        Const WINDOW_TITLE As String = "Snape Potion Manufacturing Quality Program"

        Dim readingsPath, reportPath As String
        Dim showReportFile As Boolean = False
        Dim myShiftManager As ShiftManager

        setupConsole(WINDOW_TITLE, bgColor:=ConsoleColor.Blue, fgColor:=ConsoleColor.White)

        readingsPath = promptUser("Please enter the path and name of the file containing the measurements:")
        validateReadingsPath(readingsPath)

        writeBlankLine()
        reportPath = promptUser("Please enter the path and name of the report file to generate:")
        validateReportPath(reportPath)

        myShiftManager = parseInputReadingsFile(readingsPath)

        ' Generate Report /// TODO
        writeBlankLine()
        Console.WriteLine("Report file has been generated!")
        showReportFile = promptUserYesNo("Would you like to see the report file?")

        playEasterEggIfActivated(reportPath)
        waitForUserPressEnter()
    End Sub

    Function parseInputReadingsFile(readingsPath As String) As ShiftManager
        Dim myShiftManager As New ShiftManager()

        Using myReader As New TextFieldParser(readingsPath)
            myReader.TextFieldType = FieldType.Delimited
            myReader.Delimiters = New String() {vbTab}

            Dim currentRow As String()

            While Not myReader.EndOfData
                Try
                    currentRow = myReader.ReadFields()

                    myShiftManager.Add(getObservationFromRow(currentRow))
                Catch ex As MalformedLineException
                    MsgBox("Line " & ex.Message & " is invalid. Skipping.")
                End Try
            End While
        End Using

        Return myShiftManager
    End Function

    Function getObservationFromRow(row As String()) As Observation
        Return New Observation(row(Observation.ID_INDEX),
                               row(Observation.VALUE_INDEX),
                               row(Observation.TIME_TAKEN_INDEX))
    End Function

    Sub waitForUserInput()
        Console.ReadLine()
    End Sub

    Sub writeBlankLine()
        Console.WriteLine("")
    End Sub

    Sub waitForUserPressEnter()
        Console.WriteLine("Press [enter] to exit...")
        waitForUserInput()
    End Sub

    Function promptUser(promptMessage As String) As String
        Console.WriteLine(promptMessage)
        Return Console.ReadLine()
    End Function

    Function promptUserYesNo(promptMessage As String) As Boolean
        Dim response = promptUser(promptMessage & " [Y/n]")
        Dim match As Match = Regex.Match(response, "n|no", RegexOptions.IgnoreCase)  ' Default to "Yes", "Y", "y" or anything else as being "True"

        Return (Not match.Success)  ' Returns True if "Yes" and False if "No"
    End Function

    Function validateFilePathExists(filePath As String) As Boolean
        Return File.Exists(filePath)
    End Function

    Function validateDirectoryContainingFilePathExists(filePath As String) As Boolean
        Dim directoryName As String

        ' Possible exceptions: https://msdn.microsoft.com/en-us/library/system.io.path.getdirectoryname%28v=vs.110%29.aspx#Anchor_1
        Try
            directoryName = Path.GetDirectoryName(filePath)
        Catch ex As Exception When TypeOf ex Is ArgumentException OrElse TypeOf ex Is PathTooLongException
            Return False
        End Try

        Return (Not IsNothing(directoryName)) AndAlso Directory.Exists(directoryName)
    End Function

    Sub validateReadingsPath(readingsPath As String)
        If Not validateFilePathExists(readingsPath) Then
            writeBlankLine()
            Console.WriteLine("The path: " & readingsPath & ", is not valid. Please try again.")
            waitForUserPressEnter()
            End
        End If
    End Sub

    Sub validateReportPath(reportPath As String)
        If Not validateDirectoryContainingFilePathExists(reportPath) Then
            writeBlankLine()
            Console.WriteLine("The path: " & reportPath & ", is not valid. Please try again.")
            waitForUserPressEnter()
            End
        End If
    End Sub

    Sub playEasterEggIfActivated(reportPath As String)
        Dim easterEggActivationString As String = "koopa_report"

        If reportPath.ToLower().Contains(easterEggActivationString) Then
            Music.Play()
            writeBlankLine()
            Console.WriteLine("Thank you Mario! But our princess is in another castle!")
        End If
    End Sub

    Sub setWindowTitle(newTitle As String)
        Console.Title = newTitle
    End Sub

    Sub setConsoleColors(Optional bgColor As ConsoleColor = ConsoleColor.Black,
                         Optional fgColor As ConsoleColor = ConsoleColor.Gray)
        Console.BackgroundColor = bgColor
        Console.ForegroundColor = fgColor
    End Sub

    Sub setupConsole(windowTitle As String, Optional bgColor As ConsoleColor = ConsoleColor.Black,
                     Optional fgColor As ConsoleColor = ConsoleColor.Gray)
        setWindowTitle(windowTitle)
        setConsoleColors(bgColor:=bgColor, fgColor:=fgColor)
        Console.Clear()  ' This will reset the complete console background
    End Sub

End Module
