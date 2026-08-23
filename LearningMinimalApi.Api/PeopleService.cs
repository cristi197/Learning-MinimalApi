namespace LearningMinimalApi.Api;

public record Person(string FullName);

public class PeopleService
{
    private readonly List<Person> _people = new()
    {
        new Person("Cristian Teodorescu"),
        new Person("John Doe"),
        new Person("Angel Goe")
    };

    public IEnumerable<Person> Search(string searchTerm)
    {
        return _people.Where(x => x.FullName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
    }
}
