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

namespace BenchmarkOnDatabases;

public class LiteDBStorage : IBenchmarkStorage, IDisposable
{
    public readonly string DatabasePath;
    private readonly string _connectionString;

    public LiteDBStorage()
    {
        var directory = @"Z:\BenchmarkOnDatabases";
        DatabasePath = System.IO.Path.Join(directory, "LiteDB-Benchmark.db");
        _connectionString = $"Filename={DatabasePath};connection=shared;";
        //Create
        using var _liteDatabase = new LiteDatabase(_connectionString);
        _liteDatabase.GetCollection<Person>().FindAll();
    }

    string IBenchmarkStorage.Information => $"LiteDB Database ['{DatabasePath}']";

    void IBenchmarkStorage.Insert(Person person)
    {
        using var _liteDatabase = new LiteDatabase(_connectionString);
        _liteDatabase.GetCollection<Person>().Insert(person);
    }

    IEnumerable<Person> IBenchmarkStorage.GetAll()
    {
        using var _liteDatabase = new LiteDatabase(_connectionString);
        return _liteDatabase.GetCollection<Person>().FindAll().ToList();
    }

    void IBenchmarkStorage.Update(Person person)
    {
        using var _liteDatabase = new LiteDatabase(_connectionString);
        _liteDatabase.GetCollection<Person>().Update(person);
    }

    void IBenchmarkStorage.Remove(Person person)
    {
        using var _liteDatabase = new LiteDatabase(_connectionString);
        _liteDatabase.GetCollection<Person>().Delete(person.Id);
    }

    public void Dispose()
    {
    }
}
