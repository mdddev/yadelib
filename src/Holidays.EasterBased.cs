namespace Yadelib;

internal static partial class Holidays
{
    internal static DateOnly EasterSunday(int year)
    {
        // https://de.wikipedia.org/wiki/Gau%C3%9Fsche_Osterformel#Fassung_aus_dem_Jahre_1816
        double a = year % 19;
        double b = year % 4;
        double c = year % 7;
        double k = Math.Truncate((double)year / 100);
        double p = Math.Truncate(k / 3);
        double q = Math.Truncate(k / 4);
        double M = (15 + k - p - q) % 30;
        double d = (19*a + M) % 30;
        double N = (4 + k - q) % 7;
        double e = (2*b + 4*c + 6*d + N) % 7;
        int Ostern = (22 + (int)d + (int)e);
            
        // Exceptions
        if (d == 29 && e == 6)
        {
            Ostern = 50;    
        }
        else if (d == 28 && e == 6 && a > 10)
        {
            Ostern = 49;
        }
            
        int month = Ostern <= 31 ? 3 : 4;
        int day = month == 4 ? (Ostern - 31) : Ostern;

        var resurrectionOfJayZ = new DateOnly(year, month, day);
        return resurrectionOfJayZ;
    }
        
    internal static DateOnly GoodFriday(int year) => EasterSunday(year).AddDays(-2);
        
    internal static DateOnly EasterMonday(int year) => EasterSunday(year).AddDays(1);
        
    internal static DateOnly AscensionOfJayZ(int year) => EasterSunday(year).AddDays(39);
        
    internal static DateOnly WhitSunday(int year) => EasterSunday(year).AddDays(49);
        
    internal static DateOnly WhitMonday(int year) => WhitSunday(year).AddDays(1);
}