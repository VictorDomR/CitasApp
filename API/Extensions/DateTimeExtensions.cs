namespace API.Extensions;

public static class DateTimeExtensions
{
    public static int CalculateAge(this DateOnly dob)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var age = today.Year - dob.Year;
        // Today 2023-10-23
        // dob 1992-12-26
        // age = 30

        if (dob > today.AddYears(-age)) age--;
        // 1992-12-26 > 2023-10-23
        // age = 30

        return age;
    }
}
