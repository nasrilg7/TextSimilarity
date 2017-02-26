using MongoDB.Driver;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace TextDuplication
{
    class Database
    {
        protected static IMongoClient _client = new MongoClient();
        protected static IMongoDatabase _database = _client.GetDatabase("sentencedb");

        BsonDocument document = new BsonDocument
        {
            {
               "student cheat exam", "The Student Cheated in his Exam" 
            },
            {
               "student assignment","The Student did very well in the Assignment"
            }
        };



        IMongoCollection<BsonDocument> collection = _database.GetCollection<BsonDocument>("sentences");
        
       
    }
}
