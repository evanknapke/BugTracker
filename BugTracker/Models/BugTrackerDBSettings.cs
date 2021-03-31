namespace BugTracker.Models
{
    public class BugTrackerDBSettings : IBugTrackerDBSettings
    {
        public string UsersCollectionName { get; set; }
        public string IssuesCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IBugTrackerDBSettings
    {
        string UsersCollectionName { get; set; }
        string IssuesCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}