using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using SharedResources;

namespace CasBeambusManager
{
    public class CasBeambusDataBase : IDataBase<StreamSegmentsRawData>
    {
        string m_ip;
        string m_dbName;
        string m_collectionName;
        IMongoCollection<StreamSegmentsRawData> m_collection;

        public CasBeambusDataBase(string ip, string dbName, string collectionName)
        {
            m_ip = ip;
            m_dbName = dbName;
            m_collectionName = collectionName;
        }

        public void Connect()
        {
            const int defaultMongoPort = 27017;
            string connectionString = $@"mongodb://{m_ip}:{defaultMongoPort}";
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase(m_dbName);
            m_collection = db.GetCollection<StreamSegmentsRawData>(m_collectionName);
        }

        public void Disconnect()
        {
        }

        public void Save(StreamSegmentsRawData data)
        {
            m_collection.InsertOne(data);
        }
    }
}
