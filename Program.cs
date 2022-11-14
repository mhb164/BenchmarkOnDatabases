
//dotnet ef migrations add SQLiteDbContext_InitialCreate --context SQLiteDbContext
//dotnet ef database update --context SQLiteDbContext
//dotnet ef migrations add SQLLocalDBDbContext_InitialCreate --context SQLLocalDBDbContext
//dotnet ef database update --context SQLLocalDBDbContext

using BenchmarkDotNet.Running;
using BenchmarkOnDatabases;
using BenchmarkOnDatabases.Model;
using System;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine(BenchmarkRunner.Run(typeof(BenchmarkDatabases)));


        //using var sQLiteStorage = new SQLiteStorage();
        //Test(sQLiteStorage);

        //using var sQLLocalDBStorage = new SQLLocalDBStorage();
        //Test(sQLLocalDBStorage);

        //using var liteDBStorage = new LiteDBStorage();
        //Test(liteDBStorage);

        //using var yesSqlDBStorage = new YesSqlDBStorage();
        //Test(yesSqlDBStorage);
    }

    private static void Test(IBenchmarkStorage storage)
    {
        Console.WriteLine($"Database: {storage.Information}.");
        //Console.WriteLine($"Press enter"); Console.ReadLine();

        // Create
        Console.WriteLine("Inserting a new person");
        var newPerson = new Person("John", "Constantine");
        storage.Insert(newPerson);

        Console.WriteLine($"New person created: {newPerson}");

        //Console.WriteLine($"Press enter"); Console.ReadLine();

        var firstPerson = storage.GetAll().First();
        Console.WriteLine($"First person: {firstPerson}");

        //Console.WriteLine($"Press enter"); Console.ReadLine();

        Console.WriteLine("Updating the first person name");
        firstPerson.Lastname = "Wick";
        storage.Update(firstPerson);

        Console.WriteLine($"Press enter"); Console.ReadLine();

        // Delete
        Console.WriteLine("Delete the first Person");
        storage.Remove(firstPerson);
    }

}