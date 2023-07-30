namespace MarketApplication
{
    public enum Olala
    {
        Abr,
        foo,
        Assd,
        Asif,
    }

    internal class Program
    {
        static void Main()
        {
            Type enumType = typeof(Olala);

            Console.WriteLine(enumType.GetEnumNames().Length);

            for (int i = 0; i < enumType.GetEnumNames().Length; i++)
            {
                Console.WriteLine(enumType.GetEnumNames()[i]);
            }
        }
    }
} 