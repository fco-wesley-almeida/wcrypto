using System;

namespace WCryptoApi.Testing.Utils;

public class NumberMockUtil
{

    private NumberMockUtil()
    {
        
    }
    
    public static int RandomInt()
    {
        Random random = new Random();
        int number = random.Next();
        return number;
    }
    
    public static int RandomIntGt0()
    {
        Random random = new Random();
        int    number = random.Next();
        while (number == 0)
        {
            number = random.Next();
        }
        return number;
    }
    
    public static int RandomIntLt0()
    {
        Random random = new Random();
        int    number = random.Next();
        while (number == 0)
        {
            number = random.Next();
        }
        return -number;
    }
}