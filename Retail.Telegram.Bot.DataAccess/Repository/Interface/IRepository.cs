using System;
using System.Collections.Generic;

namespace Retail.Telegram.Bot.DataAccess.Repository.Interface
{
    /// <summary>
    /// Basic interface for Repositories
    /// </summary>
    /// <typeparam name="T">Data object type</typeparam>
    public interface IRepository<T>
    {
        string ConnectionString { get; set; }

        T Get(Int32 id);
        T AddEdit(T entity);
        List<T> List();
        bool Delete(Int32 id);
    }
}
