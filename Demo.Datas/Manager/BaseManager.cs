using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Linq.Expressions;
using Demo.Datas.Contexts;
using Demo.Datas.Interface;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Demo.Datas.Manager
{
    /// <summary>
    /// 基础类,继承自Respository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseManager<T> : IRespository<T> where T : class
    {
        protected readonly MongoCollection<T> Collection;
        private MongoDatabase _db = DataContext.DB;

        public BaseManager()
        {
            var tableName = GetTableName(typeof(T));
            Collection = _db.GetCollection<T>(!String.IsNullOrEmpty(tableName) ? tableName : typeof(T).Name);
        }

        public BaseManager(string tableName)
        {
            Collection = _db.GetCollection<T>(tableName);
        }

        private string GetTableName(Type type)
        {
            var attribute = (TableAttribute)Attribute.GetCustomAttribute(type, typeof(TableAttribute));
            return attribute != null ? attribute.Name : string.Empty;
        }

        T IRespository<T>.Get(Expression<Func<T, bool>> query)
        {
            return Collection.AsQueryable().FirstOrDefault(query);
        }

        List<T> IRespository<T>.Gets(Expression<Func<T, bool>> query)
        {
            return Collection.AsQueryable().Where(query).ToList();
        }

        List<T> IRespository<T>.Gets(IMongoQuery query)
        {
            return Collection.Find(query).ToList();
        }

        List<object> IRespository<T>.GetOnePropertys(Expression<Func<T, bool>> query, Expression<Func<T, object>> selector)
        {
            return Collection.AsQueryable().Where(query).Select(selector).ToList();
        }

        List<T> IRespository<T>.GetsPaged(Expression<Func<T, bool>> query, Func<T, object> sort, bool isDesc, int index, int pageSize, out int count)
        {
            var q = Collection.AsQueryable();
            count = q.Count(query);

            var sortable = isDesc ? q.Where(query).OrderByDescending(sort) : q.Where(query).OrderBy(sort);

            return sortable.Skip((index - 1) * pageSize).Take(pageSize).ToList();
        }

        void IRespository<T>.Insert(T obj)
        {
            Collection.Insert(obj);
        }

        void IRespository<T>.Update(IMongoQuery query, IMongoUpdate update, UpdateFlags flag)
        {
            Collection.Update(query, update, flag);
        }

        void IRespository<T>.Delete(IMongoQuery query, RemoveFlags flag)
        {
            Collection.Remove(query, flag);
        }

        IEnumerable<BsonDocument> IRespository<T>.Aggregate(AggregateArgs args)
        {
            return Collection.Aggregate(args);
        }

        long IRespository<T>.Count(IMongoQuery query)
        {
            return Collection.Count(query);
        }

        IEnumerable<V> IRespository<T>.Distinct<V>(string key, IMongoQuery query)
        {
            return Collection.Distinct<V>(key, query).ToList();
        }
    }
}
