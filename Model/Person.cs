namespace BenchmarkOnDatabases.Model;

public class Person
{
    public Person(string firstname, string lastname)
    {
        Id = 0;
        Firstname = firstname;
        Lastname = lastname;
    }

    public int Id { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }

    public override string ToString() => $"[{Id}] {Firstname} {Lastname}";
}
