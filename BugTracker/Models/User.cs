using MongoDB.Bson;  
using MongoDB.Bson.Serialization.Attributes;

namespace BugTracker.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]  
        public string Id { get; set; } 
        public string[] CardIds { get; set; }
    }
}