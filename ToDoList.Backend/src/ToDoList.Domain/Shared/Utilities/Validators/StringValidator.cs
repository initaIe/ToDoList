namespace ToDoList.Domain.Shared.Utilities.Validators;

public static class StringValidator
{
    public static bool IsInRange(string input, int minLength, int maxLength)
    {
        return input.Length >= minLength && input.Length <= maxLength;
    }

    public static bool IsAlphabeticWithDigits(string input)
    {
        return input.All(char.IsLetterOrDigit);
    }
}