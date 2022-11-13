using BenchmarkDotNet.Attributes;
using BenchmarkOnDatabases.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkOnDatabases;


[MemoryDiagnoser(true)]
public class BenchmarkDatabases
{
    private IBenchmarkStorage _SQLiteStorage;
    private Person _SQLiteStoragePerson;
    private IBenchmarkStorage _SQLLocalDBStorage;
    private Person _SQLLocalDBStoragePerson;
    private IBenchmarkStorage _LiteDBStorage;
    private Person _LiteDBStoragePerson;

    [GlobalSetup]
    public void GlobalSetup()
    {
        _SQLiteStorage = new SQLiteStorage();
        _SQLiteStoragePerson = new Person("John", "Constantine");
        _SQLiteStorage.Insert(_SQLiteStoragePerson);

        _SQLLocalDBStorage = new SQLLocalDBStorage();
        _SQLLocalDBStoragePerson = new Person("John", "Constantine");
        _SQLLocalDBStorage.Insert(_SQLLocalDBStoragePerson);

        _LiteDBStorage = new LiteDBStorage();
        _LiteDBStoragePerson = new Person("John", "Constantine");
        _LiteDBStorage.Insert(_LiteDBStoragePerson);
    }

    [Benchmark]
    public void SQLiteInsert()
    {
        _SQLiteStorage.Insert(new Person("John", "Constantine"));
    }

    [Benchmark]
    public void SQLLocalDBInsert()
    {
        _SQLLocalDBStorage.Insert(new Person("John", "Constantine"));
    }

    [Benchmark]
    public void LiteDBInsert()
    {
        _LiteDBStorage.Insert(new Person("John", "Constantine"));
    }

    [Benchmark]
    public void SQLiteUpdate()
    {
        _SQLiteStorage.Update(_SQLiteStoragePerson);
    }

    [Benchmark]
    public void SQLLocalDBUpdate()
    {
        _SQLLocalDBStorage.Update(_SQLLocalDBStoragePerson);
    }

    [Benchmark]
    public void LiteDBUpdate()
    {
        _LiteDBStorage.Update(_LiteDBStoragePerson);
    }
}
