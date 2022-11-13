using BenchmarkOnDatabases.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkOnDatabases;

public interface IBenchmarkStorage
{
    string Information { get; }

    void Insert(Person person);
    IEnumerable<Person> GetAll();
    void Update(Person person);
    void Remove(Person person);
}
