using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace QuickMongoApi.DAL
{
    /// <summary>
    /// Collection Properties
    /// </summary>
    public class Employee
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRequired]
        public string Firstname { get; set; }
        [BsonRequired]
        public string Email { get; set; }
    }
}