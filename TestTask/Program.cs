using System;
using System.Text;

namespace TestTask
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Default;

            Console.WriteLine("Select language:\n1-Ukrainian\n2-English");
            try
            {
                var result = int.TryParse(Console.ReadLine(), out var language);

                if (result)
                {
                    switch (language)
                    {
                        case 1:
                            var ukrNumber = new UkrNumber();
                            ukrNumber.UkrInput();
                            break;
                        case 2:
                            var engNumber = new EngNumber();
                            engNumber.EngInput();
                            break;
                        default:
                            Console.WriteLine("Invalid data");
                            return;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid data");
                }

                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Invalid data");
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
