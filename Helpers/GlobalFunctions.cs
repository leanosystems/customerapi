namespace CustomerApi.Helpers;

public class GlobalFunctions
{
    public static int GenerateAge(DateTime birthdate)
    {
        return DateTime.Now.Year - birthdate.Year;
    }

    public static string GenerateFullName(string firstname, string lastname, string middlename)
    {
        return lastname + ", " + firstname + " " + middlename[0];
    }
}
