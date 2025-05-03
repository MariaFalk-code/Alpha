namespace WebApp.Helpers;

public class AvatarHelper
{
    public static string GenerateAvatarUrl(string fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            return "https://ui-avatars.com/api/?name=Unknown&rounded=true&background=random";

        return $"https://ui-avatars.com/api/?name={Uri.EscapeDataString(fullName)}&rounded=true&background=random";
    }

}
