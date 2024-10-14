using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace category_api.Entities
{
    public class Category
    {
        [BsonId]
        [Column("_id")]
        public ObjectId Id { get; set; } 
        public string CategoryName {  get; set; } = string.Empty;
        public int CategoryId { get; set; }
    }
}
