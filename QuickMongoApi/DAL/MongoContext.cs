using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using MongoDB.Driver;

namespace QuickMongoApi.DAL
{
    /// <summary>
    /// Mongo Context Base
    /// </summary>
    public class MongoContext
    {
        public MongoDatabaseBase Database;
        public MongoContext()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            Database = (MongoDatabaseBase)client.GetDatabase("QuickMongo");
        }
        /// <summary>
        /// Gets the dealers.
        /// Collection Definition
        /// </summary>
        /// <value>
        /// The dealers.
        /// </value>
        public IMongoCollection<Employee> Employees => Database.GetCollection<Employee>("Employees");

    }
}