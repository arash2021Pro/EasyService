namespace EasyApiService.StartupServices.MainServices;

public class MainService:IMainService
{
    
    public static bool Gmail_protocols_checker(string gmail)
    {
        bool hasdigit = false;
        bool hasupper = false;
        bool haslower = false;
        int digitcout = 0;
        for (int loc = 0; loc < gmail.Length; loc++)
        {
            if (Char.IsDigit(gmail[loc]))
            {
                hasdigit = true;
                digitcout++;
            }
            else if (Char.IsUpper(gmail[loc]))
                hasupper = true;
            else if (Char.IsLower(gmail[loc]))
                haslower = true;
        }
        if ((hasupper && haslower == true) && (hasdigit == true && digitcout > 1))
            return true;
        return false;
    }
    
    public string GenerateGmail(int charcount)
    {
        string StrTemp = " ";
        char Char;
        for (int start = 0; start < 128; start++)
        {
            Char = (char)start;
            if (char.IsLetter(Char) || char.IsDigit(Char))
            {
                StrTemp += Char.ToString();
            }
        }
        Random randome = new Random();
        char[] CharLetter = new char[charcount];
        for (int start = 0; start < charcount; start++)
        {
            int index = randome.Next(StrTemp.Length);
            CharLetter[start] = StrTemp[index];
        }

        string Gmail = " ";
        for (int index = 0; index < charcount; index++)
        {
            Gmail += CharLetter[index];
        }
        if (Gmail_protocols_checker(Gmail))
            return Gmail + "@gmail.com";
        return "not a safe gmail";
    }

    public string GeneratePassword(int len)
    {
        string StrTemp = " ";
        char Char;
        for (int start = 0; start < 128; start++)
        {
            Char = (char)start;
            if (char.IsDigit(Char) || char.IsLetter(Char) || char.IsPunctuation(Char))
            {
                StrTemp += Char.ToString();
            }
        }

        Random randome = new Random();
        char[] CharLetter = new char[len];
        for (int start = 0; start < len; start++)
        {
            int index = randome.Next(StrTemp.Length);
            CharLetter[start] = StrTemp[index];
        }

        string password = "";
        for (int index = 0; index < len; index++)
        {
            password += CharLetter[index];
        }
        return password;
    }

    public string GeneratePhoneNumber(int len)
    {
        string StrTemp = " ";
        char Char;
        for (int start = 0; start < 128; start++)
        {
            if(start != 32)
            {
                Char = (char)start;
                if (char.IsDigit(Char))
                {
                    StrTemp += Char.ToString();
                }
            }
                
        }
        Random randome = new Random();
        char[] CharLetter = new char[len];
        for (int start = 0; start < len; start++)
        {
            int index = randome.Next(StrTemp.Length);
            CharLetter[start] = StrTemp[index];
        }
        var preCode = "";
        string[] PreCodes = new string[] {null,"0913","0912","0933","0936","0912","0918","0916","0915","0999"};
        for(int sl = 1;sl<=1;sl++)
        {
            var index = randome.Next(1, PreCodes.Length);
            preCode += PreCodes[index];
        }
        string phone = "";
        for (int index = 0; index < len; index++)
        {
            phone += CharLetter[index];
        }
        return preCode + phone;
    }
}