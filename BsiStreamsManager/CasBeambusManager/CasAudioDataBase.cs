using MongoDB.Driver;
using SharedResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasBeambusManager
{
    public class CasAudioDataBase : IDataBase<TargetsAudio>
    {
        string m_ip;
        string m_dbName;
        string m_collectionName;
        IMongoCollection<TargetsAudio> m_collection;

        public CasAudioDataBase(string ip, string dbName, string collectionName)
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
            m_collection = db.GetCollection<TargetsAudio>(m_collectionName);
        }

        public void Disconnect()
        {
        }

        public void Save(TargetsAudio data)
        {
            m_collection.InsertOne(data);
        }
    }
}
