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

    Public Sub New(shiftManager As ShiftManager, headerCorpTitle As String,
                   Optional histogramLineLength As Integer = Histogram.DEFAULT_LINE_LENGTH)
        Me.shiftManager = shiftManager
        Me.histogram = New Histogram(Me.shiftManager.getAllShiftMax(), Me.shiftManager.getAllShiftMin(), histogramLineLength)
        Me.headerCorpTitle = headerCorpTitle
        Me.OBSERVATION_LINE_FORMAT = "({0,-4}) {1,4} {2, -" & histogramLineLength & "}"
    End Sub

    Public Function generateReport() As String
        Dim returnString As String = getReportHeading() & getObservationHistogram()
        returnString &= getShiftInfoHeading() & getShiftInformation()

        Return returnString
    End Function

    Private Function getReportHeading() As String
        Dim returnString As String = HEADER_FOOTER_DIVIDER_LINE & vbCrLf

        returnString &= centerString(headerCorpTitle) & vbCrLf
        returnString &= centerString(HEADER_REPORT_TITLE) & vbCrLf
        returnString &= HEADER_FOOTER_DIVIDER_LINE & vbCrLf & vbCrLf

        Return returnString
    End Function

    Private Function getShiftInfoHeading() As String
        Dim returnString As String = HEADER_FOOTER_DIVIDER_LINE & vbCrLf
        returnString &= centerString(SHIFT_INFO_HEADER_TITLE) & vbCrLf
        returnString &= HEADER_FOOTER_DIVIDER_LINE & vbCrLf

        Return returnString
    End Function

    Private Function getShiftInformation() As String
        Dim returnString As String = getObservationShiftInformation() & vbCrLf
        returnString &= getShiftStatisticsInformation() & vbCrLf
        returnString &= HEADER_FOOTER_DIVIDER_LINE & vbCrLf

        Return returnString
    End Function

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

    Private Function getObservationHistogram() As String
        Dim returnString As String = OBSERVATION_HEADER_LINE & vbCrLf

        For Each observation As Observation In shiftManager.getAllObservationsSortedByValueAndBaseValue()
            returnString &= String.Format(OBSERVATION_LINE_FORMAT, observation.id, observation.value,
                                          histogram.getLineForReading(observation.value, observation.isOnDesiredReadingLevel()))
            returnString &= vbCrLf
        Next

        Return returnString
    End Function

    Public Function centerString(stringToCenter As String, Optional lineLength As Integer = FULL_LINE_LENGTH) As String
        Return String.Format("{0,-" & CStr(lineLength) & "}",
                             String.Format("{0," & (Math.Ceiling((lineLength + stringToCenter.Length) / 2)).ToString() & "}", stringToCenter))
    End Function

    Public Sub writeReportToFile(filePath As String)
        Dim file As StreamWriter = My.Computer.FileSystem.OpenTextFileWriter(filePath, False)  ' Overwrite if the file exists
        file.Write(generateReport())
        file.Close()
    End Sub
End Class
