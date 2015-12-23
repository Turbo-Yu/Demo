using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Demo.Datas.Interface
{
    /// <summary>
    /// 泛型类
    /// </summary>
    /// <typeparam name="T">Model</typeparam>
    public interface IRespository<T> where T : class
    {
        /// <summary>
        /// 查询符合条件的单个对象
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        T Get(Expression<Func<T, bool>> query);

        /// <summary>
        /// 查询符合条件的对象列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        List<T> Gets(Expression<Func<T, bool>> query);

        /// <summary>
        /// 查询符合条件的对象列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        List<T> Gets(IMongoQuery query);

        /// <summary>
        /// 查询符合条件对象某一属性的列表
        /// </summary>
        /// <param name="query"></param>
        /// <param name="select"></param>
        /// <returns></returns>
        List<object> GetOnePropertys(Expression<Func<T, bool>> query, Expression<Func<T, object>> selector);


        /// <summary>
        /// 查询符合条件的对象列表.分页
        /// </summary>
        /// <param name="query"></param>
        /// <param name="isDesc"></param>
        /// <param name="index"></param>
        /// <param name="pageSize"></param>
        /// <param name="count"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        List<T> GetsPaged(Expression<Func<T, bool>> query, Func<T, Object> sort, Boolean isDesc, int index, int pageSize, out int count);

        /// <summary>
        /// 插入对象
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        void Insert(T obj);

        /// <summary>
        /// 更新对象
        /// </summary>
        /// <param name="query"></param>
        /// <param name="update"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        void Update(IMongoQuery query, IMongoUpdate update, UpdateFlags flag);

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="query"></param>
        /// <param name="flag"></param>
        void Delete(IMongoQuery query, RemoveFlags flag);

        /// <summary>
        /// Aggregate聚合
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        IEnumerable<BsonDocument> Aggregate(AggregateArgs args);

        /// <summary>
        /// 获取数量
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        long Count(IMongoQuery query);

        /// <summary>
        /// Distinct方法
        /// </summary>
        IEnumerable<V> Distinct<V>(string key, IMongoQuery query);
    }
}
