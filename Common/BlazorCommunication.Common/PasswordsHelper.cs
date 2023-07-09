using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace BlazorCommunication.Common;

public static class PasswordsHelper
{
    public static bool IsMeetsRequirements(string password)
    {
        if (password == null)
        {
            return false;
        }

        if (password.Length < 8)
        {
            return false;
        }

        if (password.Length > 50)
        {
            return false;
        }

        if (!password.Any(x => char.IsUpper(x)))
        {
            return false;
        }

        if (!password.Any(x => char.IsLower(x)))
        {
            return false;
        }

        if (!password.Any(x => char.IsDigit(x)))
        {
            return false;
        }

        if (!IsSuitableCharacters(password))
        {
            return false;
        }

        return true;
    }

    private static bool IsSuitableCharacters(string password)
    {
        var regex = new Regex("^[A-Za-z0-9!@#$%^&*_-]*$");

        return regex.IsMatch(password);
    }
    
    public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512(passwordSalt))
        {
            var computedHash =
                hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }
    }
    
    public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac
                .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }
}