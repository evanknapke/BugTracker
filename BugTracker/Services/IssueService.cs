using BugTracker.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System;

namespace BugTracker.Services
{
    public class IssueService
    {
        private readonly IMongoCollection<IssueItem> _issues;

        public IssueService(IBugTrackerDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _issues = database.GetCollection<IssueItem>(settings.IssuesCollectionName);        }

        public List<IssueItem> Get() =>
            _issues.Find(issue => true).ToList();

        public IssueItem Get(string id) =>
            _issues.Find<IssueItem>(issue => issue.Id == id).FirstOrDefault();

        public IssueItem Create(IssueItem issue)
        {
            _issues.InsertOne(issue);
            return issue;
        }

        public void Update(string id, IssueItem issueIn) =>
            _issues.ReplaceOne(issue => issue.Id == id, issueIn);

        public void Remove(IssueItem issueIn) =>
            _issues.DeleteOne(issue => issue.Id == issueIn.Id);

        public void Remove(string id) => 
            _issues.DeleteOne(issue => issue.Id == id);
    }
}