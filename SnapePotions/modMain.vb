'------------------------------------------------------------
'-                  File Name: modMain.vb                   -
'-                 Part of Project: Assign3                 -
'------------------------------------------------------------
'-                Written By: Elijah Wilson                 -
'-                  Written On: 01/30/2016                  -
'------------------------------------------------------------
'- File Purpose:                                            -
'-                                                          -
'- This file contains the main module and subprogram. This  -
'- is where the program starts and ends.                    -
'------------------------------------------------------------
'- Program Purpose:                                         -
'-                                                          -
'- This program takes a input file from a user and parses   -
'- it. Then displays a histogram of the data as well as     -
'- some statistics about the data. It also writes the       -
'- report to a user specified file.                         -
'------------------------------------------------------------
'- Global Variable Dictionary (alphabetically):             -
'- (None)                                                   -
'------------------------------------------------------------
Imports System.IO
Imports System.Text.RegularExpressions

Module modMain

    '------------------------------------------------------------
    '-                  Subprogram Name: Main                   -
    '------------------------------------------------------------
    '-                Written By: Elijah Wilson                 -
    '-                  Written On: 01/30/2016                  -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-                                                          -
    '- This is where the program starts and ends. It contains   -
    '- all the interaction with the user.                       -
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- (None)                                                   -
    '------------------------------------------------------------
    '- Local Variable Dictionary (alphabetically):              -
    '- CORP_NAME - The name of the corporation using this       -
    '-             program                                      -
    '- WINDOW_TITLE - The title to be displayed for the console -
    '- myShiftManager - The ShiftManager that does the bulk of  -
    '-                  the work, including parsing the input   -
    '-                  data file                               -
    '- readingsPath - The path the user inputs where the file   -
    '-                to be parsed is                           -
    '- report - A ShiftQualityReport object that handles        -
    '-          generating the report as well as writing it to  -
    '-          file                                            -
    '- reportPath - The path the user inputs where the report   -
    '-              that is generated, should be saved          -
    '- showReportFile - Whether or not to show the report file  -
    '-                  on the screen                           -
    '------------------------------------------------------------
    Sub Main()
        Const CORP_NAME As String = "Snape Potion Manufacturing"
        Const WINDOW_TITLE As String = CORP_NAME & " Quality Program"

        Dim readingsPath, reportPath As String
        Dim showReportFile As Boolean = False
        Dim myShiftManager As New ShiftManager()
        Dim report As ShiftQualityReport

        setupConsole(WINDOW_TITLE, bgColor:=ConsoleColor.Blue, fgColor:=ConsoleColor.White)

        readingsPath = promptUser("Please enter the path and name of the file containing the measurements:")
        validateReadingsPath(readingsPath)

        writeBlankLine()
        reportPath = promptUser("Please enter the path and name of the report file to generate:")
        validateReportPath(reportPath)

        myShiftManager.parseInputReadingsFile(readingsPath)
        report = New ShiftQualityReport(myShiftManager, CORP_NAME)

        writeBlankLine()
        Console.WriteLine("Report file has been generated!")
        showReportFile = promptUserYesNo("Would you like to see the report file?")

        report.writeReportToFile(reportPath)

        If showReportFile Then
            Console.Clear()
            Console.Write(report.generateReport())
        End If

        playEasterEggIfActivated(reportPath)
        waitForUserPressEnter()
    End Sub

    '------------------------------------------------------------
    '-            Subprogram Name: waitForUserInput             -
    '------------------------------------------------------------
    '-                Written By: Elijah Wilson                 -
    '-                  Written On: 01/30/2016                  -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-                                                          -
    '- This is a more verbose named wrapper for                 -
    '- Console.ReadLine()                                       -
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- (None)                                                   -
    '------------------------------------------------------------
    '- Local Variable Dictionary (alphabetically):              -
    '- (None)                                                   -
    '------------------------------------------------------------
    Sub waitForUserInput()
        Console.ReadLine()
    End Sub

    '------------------------------------------------------------
    '-             Subprogram Name: writeBlankLine              -
    '------------------------------------------------------------
    '-                Written By: Elijah Wilson                 -
    '-                  Written On: 01/30/2016                  -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-                                                          -
    '- This is a more verbose named wrapper for                 -
    '- Console.WriteLine("")                                    -
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- (None)                                                   -
    '------------------------------------------------------------
    '- Local Variable Dictionary (alphabetically):              -
    '- (None)                                                   -
    '------------------------------------------------------------
    Sub writeBlankLine()
        Console.WriteLine("")
    End Sub

    '------------------------------------------------------------
    '-          Subprogram Name: waitForUserPressEnter          -
    '------------------------------------------------------------
    '-                Written By: Elijah Wilson                 -
    '-                  Written On: 01/30/2016                  -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-                                                          -
    '- This writes a message to the console telling the user to -
    '- press enter to exit and waits for them to do so.         -
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- (None)                                                   -
    '------------------------------------------------------------
    '- Local Variable Dictionary (alphabetically):              -
    '- (None)                                                   -
    '------------------------------------------------------------
    Sub waitForUserPressEnter()
        Console.WriteLine("Press [enter] to exit...")
        waitForUserInput()
    End Sub

    '------------------------------------------------------------
    '-                Function Name: promptUser                 -
    '------------------------------------------------------------
    '-                Written By: Elijah Wilson                 -
    '-                  Written On: 01/30/2016                  -
    '------------------------------------------------------------
    '- Function Purpose:                                        -
    '-                                                          -
    '- Prompts the user with a message and returns the result   -
    '- (what they type).                                        -
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- promptMessage - The message to be displayed as the       -
    '-                 prompt                                   -
    '------------------------------------------------------------
    '- Local Variable Dictionary (alphabetically):              -
    '- (None)                                                   -
    '------------------------------------------------------------
    '- Returns:                                                 -
    '- String - What the user responded with                    -
    '------------------------------------------------------------
    Function promptUser(promptMessage As String) As String
        Console.WriteLine(promptMessage)
        Return Console.ReadLine()
    End Function

    '------------------------------------------------------------
    '-              Function Name: promptUserYesNo              -
    '------------------------------------------------------------
    '-                Written By: Elijah Wilson                 -
    '-                  Written On: 01/30/2016                  -
    '------------------------------------------------------------
    '- Function Purpose:                                        -
    '-                                                          -
    '- Prompts the user with a message and returns true or      -
    '- false based on whether they said yes or no. Yes = True;  -
    '- No = False.                                              -
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- promptMessage - The message to prompt the user with      -
    '------------------------------------------------------------
    '- Local Variable Dictionary (alphabetically):              -
    '- (None)                                                   -
    '------------------------------------------------------------
    '- Returns:                                                 -
    '- Boolean - Whether or not the user responded Yes or No    -
    '------------------------------------------------------------
    Function promptUserYesNo(promptMessage As String) As Boolean
        Dim response = promptUser(promptMessage & " [Y/n]")
        Dim match As Match = Regex.Match(response, "n|no", RegexOptions.IgnoreCase)  ' Default to "Yes", "Y", "y" or anything else as being "True"

        Return (Not match.Success)  ' Returns True if "Yes" and False if "No"
    End Function

    '------------------------------------------------------------
    '-          Function Name: validateFilePathExists           -
    '------------------------------------------------------------
    '-                Written By: Elijah Wilson                 -
    '-                  Written On: 01/30/2016                  -
    '------------------------------------------------------------
    '- Function Purpose:                                        -
    '-                                                          -
    '- A verbose named wrapper for File.Exists().               -
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- filePath - File path in question                         -
    '------------------------------------------------------------
    '- Local Variable Dictionary (alphabetically):              -
    '- (None)                                                   -
    '------------------------------------------------------------
    '- Returns:                                                 -
    '- Boolean - Whether or not the file path exists            -
    '------------------------------------------------------------
    Function validateFilePathExists(filePath As String) As Boolean
        Return File.Exists(filePath)
    End Function

    '------------------------------------------------------------
    '- Function Name: validateDirectoryContainingFilePathExists -
    '------------------------------------------------------------
    '-                Written By: Elijah Wilson                 -
    '-                  Written On: 01/30/2016                  -
    '------------------------------------------------------------
    '- Function Purpose:                                        -
    '-                                                          -
    '- This tests to see whether or not the directory           -
    '- containing the input file path actually exists. Useful   -
    '- for checking whether or not a file could be created in a -
    '- given directory.                                         -
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- filePath - The file path in question                     -
    '------------------------------------------------------------
    '- Local Variable Dictionary (alphabetically):              -
    '- directoryName - The name of the directory that would     -
    '-                 contain the filePath                     -
    '------------------------------------------------------------
    '- Returns:                                                 -
    '- Boolean - Whether or not the directory containing the    -
    '-           file path exists                               -
    '------------------------------------------------------------
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

    '------------------------------------------------------------
    '-          Subprogram Name: validateReadingsPath           -
    '------------------------------------------------------------
    '-                Written By: Elijah Wilson                 -
    '-                  Written On: 01/30/2016                  -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-                                                          -
    '- Checks to see if the input path is valid, if not display -
    '- a message and wait for the user to press enter to exit.  -
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- readingsPath - The path in question of validity          -
    '------------------------------------------------------------
    '- Local Variable Dictionary (alphabetically):              -
    '- (None)                                                   -
    '------------------------------------------------------------
    Sub validateReadingsPath(readingsPath As String)
        If Not validateFilePathExists(readingsPath) Then
            writeBlankLine()
            Console.WriteLine("The path: " & readingsPath & ", is not valid. Please try again.")
            waitForUserPressEnter()
            End
        End If
    End Sub

    '------------------------------------------------------------
    '-           Subprogram Name: validateReportPath            -
    '------------------------------------------------------------
    '-                Written By: Elijah Wilson                 -
    '-                  Written On: 01/30/2016                  -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-                                                          -
    '- This checks to see if the report output path would be    -
    '- valid to write to. If it isn't then it displays an error -
    '- message and waits for the user to press enter to exit.   -
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- reportPath - The path in question of validity            -
    '------------------------------------------------------------
    '- Local Variable Dictionary (alphabetically):              -
    '- (None)                                                   -
    '------------------------------------------------------------
    Sub validateReportPath(reportPath As String)
        If Not validateDirectoryContainingFilePathExists(reportPath) Then
            writeBlankLine()
            Console.WriteLine("The path: " & reportPath & ", is not valid. Please try again.")
            waitForUserPressEnter()
            End
        End If
    End Sub

    '------------------------------------------------------------
    '-        Subprogram Name: playEasterEggIfActivated         -
    '------------------------------------------------------------
    '-                Written By: Elijah Wilson                 -
    '-                  Written On: 01/30/2016                  -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-                                                          -
    '- This checks to see if the report path contains a certain -
    '- string and if so, plays a short easter egg.              -
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- reportPath - The path of the report file generated       -
    '------------------------------------------------------------
    '- Local Variable Dictionary (alphabetically):              -
    '- easterEggActivationString - The string that will         -
    '-                             activate the easter egg      -
    '------------------------------------------------------------
    Sub playEasterEggIfActivated(reportPath As String)
        Dim easterEggActivationString As String = "koopa"

        If reportPath.ToLower().Contains(easterEggActivationString) Then
            Music.Play()
            writeBlankLine()
            Console.WriteLine("Thank you Mario! But our princess is in another castle!")
        End If
    End Sub

    '------------------------------------------------------------
    '-             Subprogram Name: setWindowTitle              -
    '------------------------------------------------------------
    '-                Written By: Elijah Wilson                 -
    '-                  Written On: 01/30/2016                  -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-                                                          -
    '- This sets the title of the console.                      -
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- newTitle - String to set the title of the console        -
    '------------------------------------------------------------
    '- Local Variable Dictionary (alphabetically):              -
    '- (None)                                                   -
    '------------------------------------------------------------
    Sub setWindowTitle(newTitle As String)
        Console.Title = newTitle
    End Sub

    '------------------------------------------------------------
    '-            Subprogram Name: setConsoleColors             -
    '------------------------------------------------------------
    '-                Written By: Elijah Wilson                 -
    '-                  Written On: 01/30/2016                  -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-                                                          -
    '- This sets the background and foreground colors of the    -
    '- console window. Both parameters are optional and default -
    '- to a black background with a white foreground.           -
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- bgColor - The background color to set                    -
    '- fgColor - The foreground color to set                    -
    '------------------------------------------------------------
    '- Local Variable Dictionary (alphabetically):              -
    '- (None)                                                   -
    '------------------------------------------------------------
    Sub setConsoleColors(Optional bgColor As ConsoleColor = ConsoleColor.Black,
                         Optional fgColor As ConsoleColor = ConsoleColor.Gray)
        Console.BackgroundColor = bgColor
        Console.ForegroundColor = fgColor
    End Sub

    '------------------------------------------------------------
    '-              Subprogram Name: setupConsole               -
    '------------------------------------------------------------
    '-                Written By: Elijah Wilson                 -
    '-                  Written On: 01/30/2016                  -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-                                                          -
    '- This sets the title of the console as well as the        -
    '- background and foreground colors.                        -
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- windowTitle - Title to set the console to                -
    '- bgColor - Background color to set the console to         -
    '- fgColor - Foreground color to set the console to         -
    '------------------------------------------------------------
    '- Local Variable Dictionary (alphabetically):              -
    '- (None)                                                   -
    '------------------------------------------------------------
    Sub setupConsole(windowTitle As String, Optional bgColor As ConsoleColor = ConsoleColor.Black,
                     Optional fgColor As ConsoleColor = ConsoleColor.Gray)
        setWindowTitle(windowTitle)
        setConsoleColors(bgColor:=bgColor, fgColor:=fgColor)
        Console.Clear()  ' This will reset the complete console background
    End Sub

End Module
