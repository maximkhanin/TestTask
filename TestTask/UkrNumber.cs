using System;
using System.Linq;
using System.Text;
using static System.String;

namespace TestTask
{
    public enum Currency
    {
        Hryvna,
        Coins
    }

    public class UkrNumber
    {
        private static string _str;
        private static double _number;
        private static int _leftPart;
        private static int _rightPart;

        private readonly string[] Hundreds =
        {
            "", "сто", "двісті", "триста", "чотириста",
            "п'ятсот", "шістсот", "сімсот", "вісімсот", "дев'ятсот"
        };

        private readonly string[] Tens =
        {
            "", "десять", "двадцять", "тридцять", "сорок", "пятьдесят",
            "шістдесят", "сімдесят", "вісімдесят", "дев'яносто"
        };

        private string Str(int val, bool male, string one, string two, string five)
        {
            string[] valuesFrom1To20 =
            {
                "", "один", "два", "три", "чотири", "п'ять", "шість",
                "сім", "вісім", "дев'ять", "десять", "одинадцять",
                "дванадцять", "тринадцять", "чотирнадцять", "п'ятнадцять",
                "шістнадцять", "сімнадцять", "вісімнадцять", "дев'ятнадцять"
            };

            var num = val % 1000;

            if (num == 0)
            {
                return "";
            }

            if (!male)
            {
                valuesFrom1To20[1] = "одна";
                valuesFrom1To20[2] = "дві";
            }

            var r = new StringBuilder();
            if (!valuesFrom1To20[num / 100].Equals(""))
            {
                r.Append(Hundreds[num / 100] + " ");
            }

            if (num % 100 < 20)
            {
                r.Append(valuesFrom1To20[num % 100] + " ");
            }
            else
            {
                r.Append(Tens[num % 100 / 10] + " ");
                r.Append(valuesFrom1To20[num % 10] + " ");
            }

            r.Append(Case(num, one, two, five));

         
            return r.ToString();
        }

        private string Case(int val, string one, string two, string five)
        {
            var t = (val % 100 > 20) ? val % 10 : val % 20;

            switch (t)
            {
                case 1:
                    return one;
                case 2:
                case 3:
                case 4:
                    return two;
                default:
                    return five;
            }
        }

        private string Str(int val, Currency currency)
        {
            var n = val;

            var r = new StringBuilder();

            if (n == 0)
            {
                return r.ToString();
            }

            if (n % 1000 != 0)
            {
                r.Append(Str(n, false, "", "", ""));
            }

            r.Append(currency == Currency.Hryvna
                ? Case(n, "гривня", "гривні", "гривень")
                : Case(n, "копійка", "копійки", "копійок"));

            n /= 1000;

            r.Insert(0, Str(n, false, "тисяча ", "тисячі ", "тисяч "));
            n /= 1000;

            r.Insert(0, Str(n, true, "мільйон ", "мільйони ", "мільйонів "));
            n /= 1000;

            r.Insert(0, Str(n, true, "мільярд ", "мільярди ", "мільярдів "));

            return r.ToString();
        }

        public void UkrInput()
        {
            Console.WriteLine("Введіть число");

            _str = Console.ReadLine();

            if (IsNullOrEmpty(_str) || _str.Contains('.'))
            {
                Console.WriteLine("Invalid data");
                return;
            }

            bool resultParseNumber;

            if (_str.Contains(','))
            {
                resultParseNumber = double.TryParse(_str.Replace(',', '.'), out _number);

                if (resultParseNumber && _number <= 2147483647 && _number >= 0)
                {
                    var number = Convert.ToString(_number).Split('.');

                    _leftPart = int.Parse(number[0]);

                    if (number.Length == 2)
                    {
                        _rightPart = int.Parse(number[1]);

                        if (_rightPart <= 99 && number[1].Length <= 2)
                        {
                            Console.WriteLine(Str(_leftPart, Currency.Hryvna) + " " +
                                              Str(_rightPart, Currency.Coins));
                            return;
                        }

                        Console.WriteLine("Invalid data");
                        return;

                    }

                    if (_number <= 2147483647 && _number >= 0)
                    {
                        Console.WriteLine(Str(_leftPart, Currency.Hryvna));
                        return;
                    }

                    Console.WriteLine("Invalid data");

                    return;
                }

                Console.WriteLine("Invalid data");
                return;
            }

            resultParseNumber = double.TryParse(_str, out _number);

            if (resultParseNumber && _number <= 2147483647 && _number >= 0)
            {
                if (_number == 0)
                {
                    Console.WriteLine("нуль гривень");
                    return;
                }

                if (_number <= 2147483647 && _number >= 0)
                {
                    Console.WriteLine(Str(Convert.ToInt32(_number), Currency.Hryvna));
                    return;
                }

                Console.WriteLine("Invalid data");
                return;
            }

            Console.WriteLine("Invalid data");
        }
    }
}

