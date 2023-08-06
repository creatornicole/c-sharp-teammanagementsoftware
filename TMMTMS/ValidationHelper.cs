using System;

public static class Class1
{
    private static bool IsStringValid(string str, int maxLength)
    {
        if (!string.IsNullOrWhiteSpace(str) && str.Length <= maxLength)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private static bool IsDateValid(DateTime date)
    {
        if (date <= DateTime.Now)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
