namespace SimonCLI.Domain.Models;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    public string Name => $"{FirstName} {LastName}";

    public User() { }

    public User(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}
