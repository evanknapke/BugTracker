// using MongoDB.Bson;  
// using MongoDB.Bson.Serialization.Attributes; 

namespace BugTracker.Models
{
    public class IssueItem
    {
        // [BsonRepresentation(BsonType.ObjectId)]  
        public long Id { get; set; } 
        public string CardName { get; set; }
        public string IssueText { get; set; }
    }
}