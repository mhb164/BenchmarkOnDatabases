using BenchmarkOnDatabases.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkOnDatabases;

public class SQLiteDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public string DbPath { get; }

    public SQLiteDbContext()
    {
        var directory = @"Z:\BenchmarkOnDatabases";
        DbPath = System.IO.Path.Join(directory, "SQLite-Benchmark.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}

public class SQLiteStorage : IBenchmarkStorage
{
    public SQLiteStorage()
    {

    }

    SQLiteDbContext NewDbContext() => new SQLiteDbContext();
    string IBenchmarkStorage.Information => $"SQLite Database";

    void IBenchmarkStorage.Insert(Person person)
    {
        using var dbContext = NewDbContext();
        dbContext.Add(person);
        dbContext.SaveChanges();
    }

    IEnumerable<Person> IBenchmarkStorage.GetAll()
    {
        using var dbContext = NewDbContext();
        return dbContext.Persons.ToList();
    }

    void IBenchmarkStorage.Update(Person person)
    {
        using var dbContext = NewDbContext();
        dbContext.Persons.Update(person);
        dbContext.SaveChanges();
    }

    void IBenchmarkStorage.Remove(Person person)
    {
        using var dbContext = NewDbContext();
        dbContext.Remove(person);
        dbContext.SaveChanges();
    }
}
