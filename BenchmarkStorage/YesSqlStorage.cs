using LiteDB;
using BenchmarkOnDatabases.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata;
using YesSql;
using YesSql.Provider.Sqlite;

//https://github.com/sebastienros/yessql
namespace BenchmarkOnDatabases;

public class YesSqlStorage : IBenchmarkStorage, IDisposable
{
    public readonly string DatabasePath;
    private readonly string _connectionString;
    private readonly IStore _store;

    public YesSqlStorage()
    {
        var directory = @"Z:\BenchmarkOnDatabases";
        DatabasePath = System.IO.Path.Join(directory, "YesSql-Benchmark.db");
        _connectionString = $"Data Source={DatabasePath};Cache=Shared;";

        var configuration = new Configuration()
                .UseSqLite(_connectionString);

        _store = StoreFactory.CreateAndInitializeAsync(configuration).GetAwaiter().GetResult();

        //Warm up :)
        (this as IBenchmarkStorage).GetAll();
    }

    string IBenchmarkStorage.Information => $"YesSql Database ['{DatabasePath}']";

    void IBenchmarkStorage.Insert(Person person)
    {
        using var session = _store.CreateSession();
        session.Save(person);
        session.SaveChangesAsync().GetAwaiter().GetResult();
    }

    IEnumerable<Person> IBenchmarkStorage.GetAll()
    {
        using var session = _store.CreateSession();
        return session.Query<Person>().ListAsync().Result;
    }

    void IBenchmarkStorage.Update(Person person)
    {
        using var session = _store.CreateSession();
        session.Save(person);
        session.SaveChangesAsync().GetAwaiter().GetResult();
    }

    void IBenchmarkStorage.Remove(Person person)
    {
        using var session = _store.CreateSession();
        session.Delete(person);
        session.SaveChangesAsync().GetAwaiter().GetResult();
    }

    public void Dispose()
    {
    }
}
