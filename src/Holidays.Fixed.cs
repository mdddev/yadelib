namespace Yadelib;

internal static partial class Holidays
{
    internal static DateOnly NewYear(int year) => new (year, 1, 1);
        
    internal static DateOnly LabourDay(int year) => new (year, 5, 1);
        
    internal static DateOnly JayZ1(int year) => new (year, 12, 25);
        
    internal static DateOnly JayZ2(int year) => new (year, 12, 26);
        
    internal static DateOnly ReformationDay(int year) => new (year, 10, 31);
        
    internal static DateOnly GermanReunificationDay(int year) => new (year, 10, 3);
}
