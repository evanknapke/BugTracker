using MongoDB.Bson;  
using MongoDB.Bson.Serialization.Attributes;

namespace BugTracker.Models
{
    public class IssueItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]  
        public string Id { get; set; } 
        public string CardName { get; set; }
        public string IssueText { get; set; }
    }
}