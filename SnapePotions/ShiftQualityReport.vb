'------------------------------------------------------------
'-             File Name: ShiftQualityReport.vb             -
'-                 Part of Project: Assign3                 -
'------------------------------------------------------------
'-                Written By: Elijah Wilson                 -
'-                  Written On: 01/30/2016                  -
'------------------------------------------------------------
'- File Purpose:                                            -
'-                                                          -
'- Contains the ShiftQualityReport class, which manages     -
'- generating the report as well as saving it to a file.    -
'------------------------------------------------------------
Imports System.IO

Public Class ShiftQualityReport
    Private histogram As Histogram
    Private shiftManager As ShiftManager
    Private headerCorpTitle As String

    Private Const FULL_LINE_LENGTH As Integer = 80
    Private Const HEADER_FOOTER_DIVIDER_CHAR As String = "="
    Private Const HEADER_REPORT_TITLE As String = "Manufacturing Shift Quality Report"
    Private Const SHIFT_INFO_HEADER_TITLE As String = "Shift Information"
    Private Const OBSERVATION_HEADER_LINE As String = "(ID  ): Value"

    Private Const SHIFT_OBSERVATION_FMT_STR As String = "{0,-35}{1,-15}{2,-15}{3,-15}"
    Private Const SHIFT_STATS_FMT_STR As String = "{0,-35}{1,-15:N2}{2,-15:N2}{3,-15:N2}"

    Private OBSERVATION_LINE_FORMAT As String
    Private HEADER_FOOTER_DIVIDER_LINE As String = StrDup(FULL_LINE_LENGTH, HEADER_FOOTER_DIVIDER_CHAR)

    '------------------------------------------------------------
    '-                   Subprogram Name: New                   -
    '------------------------------------------------------------
    '-                Written By: Elijah Wilson                 -
    '-                  Written On: 01/30/2016                  -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-                                                          -
    '- Creates a new ShiftQualityReport object. It also creates -
    '- a new Histogram object to be used when generating the    -
    '- report.                                                  -
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- shiftManager - The ShiftManager object to be used for    -
    '-                generating the report                     -
    '- headerCorpTitle - The name of the corporation to be used -
    '-                   in the heading                         -
    '- histogramLineLength - Optional: The max line length to   -
    '-                       be used when generating lines      -
    '------------------------------------------------------------
    '- Local Variable Dictionary (alphabetically):              -
    '- (None)                                                   -
    '------------------------------------------------------------
    Public Sub New(shiftManager As ShiftManager, headerCorpTitle As String,
                   Optional histogramLineLength As Integer = Histogram.DEFAULT_LINE_LENGTH)
        Me.shiftManager = shiftManager
        Me.histogram = New Histogram(Me.shiftManager.getAllShiftMax(), Me.shiftManager.getAllShiftMin(), histogramLineLength)
        Me.headerCorpTitle = headerCorpTitle
        Me.OBSERVATION_LINE_FORMAT = "({0,-4}) {1,4} {2, -" & histogramLineLength & "}"
    End Sub

    '------------------------------------------------------------
    '-              Function Name: generateReport               -
    '------------------------------------------------------------
    '-                Written By: Elijah Wilson                 -
    '-                  Written On: 01/30/2016                  -
    '------------------------------------------------------------
    '- Function Purpose:                                        -
    '-                                                          -
    '- Generate a report based on the shiftManager data.        -
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- (None)                                                   -
    '------------------------------------------------------------
    '- Local Variable Dictionary (alphabetically):              -
    '- returnString - The string to be returned, accumulated    -
    '-                over time with additional report data     -
    '------------------------------------------------------------
    '- Returns:                                                 -
    '- String - A string containing the report                  -
    '------------------------------------------------------------
    Public Function generateReport() As String
        Dim returnString As String = getReportHeading() & getObservationHistogram()
        returnString &= getShiftInfoHeading() & getShiftInformation()

        Return returnString
    End Function

    '------------------------------------------------------------
    '-             Function Name: getReportHeading              -
    '------------------------------------------------------------
    '-                Written By: Elijah Wilson                 -
    '-                  Written On: 01/30/2016                  -
    '------------------------------------------------------------
    '- Function Purpose:                                        -
    '-                                                          -
    '- Generates a heading for the report.                      -
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- (None)                                                   -
    '------------------------------------------------------------
    '- Local Variable Dictionary (alphabetically):              -
    '- returnString - The string to be returned, accumulated    -
    '-                over time with more data for this         -
    '-                function                                  -
    '------------------------------------------------------------
    '- Returns:                                                 -
    '- String - A string containing the report heading          -
    '------------------------------------------------------------
    Private Function getReportHeading() As String
        Dim returnString As String = HEADER_FOOTER_DIVIDER_LINE & vbCrLf

        returnString &= centerString(headerCorpTitle) & vbCrLf
        returnString &= centerString(HEADER_REPORT_TITLE) & vbCrLf
        returnString &= HEADER_FOOTER_DIVIDER_LINE & vbCrLf & vbCrLf

        Return returnString
    End Function

    '------------------------------------------------------------
    '-            Function Name: getShiftInfoHeading            -
    '------------------------------------------------------------
    '-                Written By: Elijah Wilson                 -
    '-                  Written On: 01/30/2016                  -
    '------------------------------------------------------------
    '- Function Purpose:                                        -
    '-                                                          -
    '- Generates a shift information heading for the report.    -
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- (None)                                                   -
    '------------------------------------------------------------
    '- Local Variable Dictionary (alphabetically):              -
    '- returnString - The string to be returned, accumulated    -
    '-                over time with more data for this         -
    '-                function                                  -
    '------------------------------------------------------------
    '- Returns:                                                 -
    '- String - A string containing the shift information       -
    '-          heading                                         -
    '------------------------------------------------------------
    Private Function getShiftInfoHeading() As String
        Dim returnString As String = HEADER_FOOTER_DIVIDER_LINE & vbCrLf
        returnString &= centerString(SHIFT_INFO_HEADER_TITLE) & vbCrLf
        returnString &= HEADER_FOOTER_DIVIDER_LINE & vbCrLf

        Return returnString
    End Function

    '------------------------------------------------------------
    '-            Function Name: getShiftInformation            -
    '------------------------------------------------------------
    '-                Written By: Elijah Wilson                 -
    '-                  Written On: 01/30/2016                  -
    '------------------------------------------------------------
    '- Function Purpose:                                        -
    '-                                                          -
    '- Generates shift information for the report. Including:   -
    '- the observation information for each shift and the       -
    '- statistics for each shift.                               -
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- (None)                                                   -
    '------------------------------------------------------------
    '- Local Variable Dictionary (alphabetically):              -
    '- returnString - The string to be returned, accumulated    -
    '-                over time with more data for this         -
    '-                function                                  -
    '------------------------------------------------------------
    '- Returns:                                                 -
    '- String - A string containing the shift information       -
    '------------------------------------------------------------
    Private Function getShiftInformation() As String
        Dim returnString As String = getObservationShiftInformation() & vbCrLf
        returnString &= getShiftStatisticsInformation() & vbCrLf
        returnString &= HEADER_FOOTER_DIVIDER_LINE & vbCrLf

        Return returnString
    End Function

    '------------------------------------------------------------
    '-      Function Name: getObservationShiftInformation       -
    '------------------------------------------------------------
    '-                Written By: Elijah Wilson                 -
    '-                  Written On: 01/30/2016                  -
    '------------------------------------------------------------
    '- Function Purpose:                                        -
    '-                                                          -
    '- Generates observation information for each shift. This   -
    '- includes: number of observations, number under target,   -
    '- number on target and number over target.                 -
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- (None)                                                   -
    '------------------------------------------------------------
    '- Local Variable Dictionary (alphabetically):              -
    '- firstStr - Contains the word to be used to describe the  -
    '-            first shift                                   -
    '- returnString - The string to be returned, accumulated    -
    '-                over time with more data for this         -
    '-                function                                  -
    '- secondStr - Contains the word to be used to describe the -
    '-             second shift                                 -
    '- thirdStr - Contains the word to be used to describe the  -
    '-            third shift                                   -
    '------------------------------------------------------------
    '- Returns:                                                 -
    '- String - A string containing the shift information for   -
    '-          each shift                                      -
    '------------------------------------------------------------
    Private Function getObservationShiftInformation() As String
        Dim firstStr As String = "First"
        Dim secondStr As String = "Second"
        Dim thirdStr As String = "Third"
        Dim returnString As String = String.Format(SHIFT_OBSERVATION_FMT_STR, "", firstStr, secondStr, thirdStr) & vbCrLf
        returnString &= String.Format(SHIFT_OBSERVATION_FMT_STR, "", StrDup(firstStr.Length, "-"),
                                      StrDup(secondStr.Length, "-"), StrDup(thirdStr.Length, "-")) & vbCrLf

        returnString &= String.Format(SHIFT_OBSERVATION_FMT_STR, "Observations:",
                                      shiftManager.firstShiftObservations.totalCount,
                                      shiftManager.secondShiftObservations.totalCount,
                                      shiftManager.thirdShiftObservations.totalCount) & vbCrLf
        returnString &= String.Format(SHIFT_OBSERVATION_FMT_STR, "Number Under Target:",
                                      shiftManager.firstShiftObservations.underCount,
                                      shiftManager.secondShiftObservations.underCount,
                                      shiftManager.thirdShiftObservations.underCount) & vbCrLf
        returnString &= String.Format(SHIFT_OBSERVATION_FMT_STR, "Number On Target:",
                                      shiftManager.firstShiftObservations.onCount,
                                      shiftManager.secondShiftObservations.onCount,
                                      shiftManager.thirdShiftObservations.onCount) & vbCrLf
        returnString &= String.Format(SHIFT_OBSERVATION_FMT_STR, "Number Over Target",
                                      shiftManager.firstShiftObservations.overCount,
                                      shiftManager.secondShiftObservations.overCount,
                                      shiftManager.thirdShiftObservations.overCount) & vbCrLf

        Return returnString
    End Function

    '------------------------------------------------------------
    '-       Function Name: getShiftStatisticsInformation       -
    '------------------------------------------------------------
    '-                Written By: Elijah Wilson                 -
    '-                  Written On: 01/30/2016                  -
    '------------------------------------------------------------
    '- Function Purpose:                                        -
    '-                                                          -
    '- Generates observation statistics for each shift. This    -
    '- includes: minimum observed, average observed and maximum -
    '- observed.                                                -
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- (None)                                                   -
    '------------------------------------------------------------
    '- Local Variable Dictionary (alphabetically):              -
    '- returnString - The string to be returned, accumulated    -
    '-                over time with more data for this         -
    '-                function                                  -
    '------------------------------------------------------------
    '- Returns:                                                 -
    '- String - A string containing the shift statistics for    -
    '-          each shift                                      -
    '------------------------------------------------------------
    Private Function getShiftStatisticsInformation() As String
        Dim returnString As String = String.Format(SHIFT_STATS_FMT_STR, "Statistics:", "", "", "") & vbCrLf
        returnString &= String.Format(SHIFT_STATS_FMT_STR, "Minimum Observed:",
                                      shiftManager.firstShiftObservations.minObservation,
                                      shiftManager.secondShiftObservations.minObservation,
                                      shiftManager.thirdShiftObservations.minObservation) & vbCrLf
        returnString &= String.Format(SHIFT_STATS_FMT_STR, "Average Observed:",
                                      shiftManager.firstShiftObservations.getAverage(),
                                      shiftManager.secondShiftObservations.getAverage(),
                                      shiftManager.thirdShiftObservations.getAverage()) & vbCrLf
        returnString &= String.Format(SHIFT_STATS_FMT_STR, "Maximum Observed:",
                                      shiftManager.firstShiftObservations.maxObservation,
                                      shiftManager.secondShiftObservations.maxObservation,
                                      shiftManager.thirdShiftObservations.maxObservation) & vbCrLf

        Return returnString
    End Function

    '------------------------------------------------------------
    '-          Function Name: getObservationHistogram          -
    '------------------------------------------------------------
    '-                Written By: Elijah Wilson                 -
    '-                  Written On: 01/30/2016                  -
    '------------------------------------------------------------
    '- Function Purpose:                                        -
    '-                                                          -
    '- Generates a histogram based on the observations. These   -
    '- are sorted in ascending order.                           -
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- (None)                                                   -
    '------------------------------------------------------------
    '- Local Variable Dictionary (alphabetically):              -
    '- returnString - The string to be returned, accumulated    -
    '-                over time with more data for this         -
    '-                function                                  -
    '------------------------------------------------------------
    '- Returns:                                                 -
    '- String - A string containing the histogram for all of    -
    '-          the observations                                -
    '------------------------------------------------------------
    Private Function getObservationHistogram() As String
        Dim returnString As String = OBSERVATION_HEADER_LINE & vbCrLf

        For Each observation As Observation In shiftManager.getAllObservationsSortedByValueAndBaseValue()
            returnString &= String.Format(OBSERVATION_LINE_FORMAT, observation.id, observation.value,
                                          histogram.getLineForReading(observation.value, observation.isOnDesiredReadingLevel()))
            returnString &= vbCrLf
        Next

        Return returnString
    End Function

    '------------------------------------------------------------
    '-               Function Name: centerString                -
    '------------------------------------------------------------
    '-                Written By: Elijah Wilson                 -
    '-                  Written On: 01/30/2016                  -
    '------------------------------------------------------------
    '- Function Purpose:                                        -
    '-                                                          -
    '- Centers a specified string in white space.               -
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- stringToCenter - The string that should be centered      -
    '- lineLength - Optional: The length of the line that is    -
    '-              being centered.                             -
    '------------------------------------------------------------
    '- Local Variable Dictionary (alphabetically):              -
    '- (None)                                                   -
    '------------------------------------------------------------
    '- Returns:                                                 -
    '- String - A string containing the stringToCenter,         -
    '-          centered in spaces.                             -
    '------------------------------------------------------------
    Public Function centerString(stringToCenter As String, Optional lineLength As Integer = FULL_LINE_LENGTH) As String
        Return String.Format("{0,-" & CStr(lineLength) & "}",
                             String.Format("{0," & (Math.Ceiling((lineLength + stringToCenter.Length) / 2)).ToString() & "}", stringToCenter))
    End Function

    '------------------------------------------------------------
    '-            Subprogram Name: writeReportToFile            -
    '------------------------------------------------------------
    '-                Written By: Elijah Wilson                 -
    '-                  Written On: 01/30/2016                  -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-                                                          -
    '- This generates the report and writes it to the specified -
    '- file path.                                               -
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- filePath - Where to write the report to                  -
    '------------------------------------------------------------
    '- Local Variable Dictionary (alphabetically):              -
    '- file - The StreamWriter object of the input filePath     -
    '------------------------------------------------------------
    Public Sub writeReportToFile(filePath As String)
        Dim file As StreamWriter = My.Computer.FileSystem.OpenTextFileWriter(filePath, False)  ' Overwrite if the file exists
        file.Write(generateReport())
        file.Close()
    End Sub
End Class
