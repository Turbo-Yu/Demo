using System;
using Demo.Core;
using MongoDB.Driver;

namespace Demo.Datas.Contexts
{
    internal sealed class DataContext
    {
        private static readonly Object _contextLock = new object();
        private static String _serverIp = ConfigUtil.GetString("Mongodb.Server", "l20.0.0.0");//"121.40.213.248";
        private static Int32 _serverPort = ConfigUtil.GetInt("Mongodb.ServerPort", 27017);//12138;
        private static String _dataBase = ConfigUtil.GetString("Mongodb.Database", "test");//"TruckNo1_Test150601";

        static MongoClient _client;
        static MongoServer _server;
        static MongoDatabase _db;

        public static String ServerIP
        {
            set { _serverIp = value; }
            get { return _serverIp; }
        }

        public static Int32 ServerPort
        {
            set { _serverPort = value; }
            get { return _serverPort; }
        }

        public static String DataBase
        {
            set { _dataBase = value; }
            get { return _dataBase; }
        }

        public static MongoDatabase DB
        {
            get
            {
                if (_db == null) return _db;
                lock (_contextLock)
                {
                    if (_db == null)
                    {
                        InitContext();
                    }
                }
                return _db;
            }
        }

        static void InitContext()
        {
            lock (_contextLock)
            {
                var settings = new MongoClientSettings
                {
                    Server = new MongoServerAddress(_serverIp, _serverPort)
                };

                _client = new MongoClient(settings);
                _server = _client.GetServer();
                _db = _server.GetDatabase(_dataBase);
            }
        }
    }
}
