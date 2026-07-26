namespace TrainReservationSystem.Services;

public class AuthService
{
    private readonly Dictionary<string, string> _users = new()
    {
        { "admin", "Password123" },
        { "staff", "Train@123" }
    };

    public bool ValidateUser(string username, string password)
    {
        return _users.TryGetValue(username.ToLower(), out var storedPassword)
               && storedPassword == password;
    }

    public string GetDisplayName(string username)
    {
        return username.ToLower() switch
        {
            "admin" => "Administrator",
            "staff" => "Staff",
            _ => username
        };
    }
}