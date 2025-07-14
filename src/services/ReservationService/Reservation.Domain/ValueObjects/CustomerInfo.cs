namespace Reservation.Domain.ValueObjects;

public record CustomerInfo
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }

    public CustomerInfo(string firstName, string lastName, string email)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name is required", nameof(firstName));
        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name is required", nameof(lastName));
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required", nameof(email));

        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }
    
    public string FullName => $"{FirstName} {LastName}";

}
