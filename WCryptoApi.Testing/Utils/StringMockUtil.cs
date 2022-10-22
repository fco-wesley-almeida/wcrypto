using System;

namespace WCryptoApi.Testing.Utils;

public class StringMockUtil
{
    private StringMockUtil() {}
    public static string RandomString(int stringLength)
    {
        Random rd = new Random();
        const string allowedChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz0123456789!@$?_-";
        char[]       chars        = new char[stringLength];
        for (int i = 0; i < stringLength; i++)
        {
            chars[i] = allowedChars[rd.Next(minValue: 0, allowedChars.Length)];
        }
        return new string(chars);
    }
}