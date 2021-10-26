namespace Blueprint.Enums.Networking
{
    public enum ReportOutcome
    {
        NotReportedUnknown = 0,
        NotReportedNoAccount = 1,
        NotReportedNotFound = 2,
        NotReportedRateLimit = 3,
        Reported = 4
    }
}