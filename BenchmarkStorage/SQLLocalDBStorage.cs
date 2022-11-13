using BenchmarkOnDatabases.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkOnDatabases;

public class SQLLocalDBDbContext : DbContext
{
    public readonly string DatabasePath;

    public SQLLocalDBDbContext()
    {
        var directory = @"Z:\BenchmarkOnDatabases";
        DatabasePath = System.IO.Path.Join(directory, "SQLLocalDB-Benchmark.db");
    }

    public DbSet<Person> Persons { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer($"Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=BenchmarkPersons;Integrated Security=SSPI;AttachDBFilename='{DatabasePath}'");
}

public class SQLLocalDBStorage : IBenchmarkStorage
{
    public SQLLocalDBStorage()
    {

    }

    SQLLocalDBDbContext NewDbContext()=> new SQLLocalDBDbContext();
    string IBenchmarkStorage.Information => $"SQL-LocalDB Database";

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