using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.String;

namespace TestTask
{

    class EngNumber
    {
        private static string _str;
        private static double _number;
        private static int _leftPart;
        private static int _rightPart;

        private readonly string[] Tens =
            {
            "", "", "twenty", "thirty", "forty", "fifty",
            "sixty", "seventy", "eighty", "ninety"
        };

        private string Str(int val, bool male, string one)
        {
            string[] valuesFrom1To20 =
            {
                "", "one", "two", "three", "four", "five", "six",
                "seven", "eight", "nine", "ten", "eleven",
                "twelve", "thirteen", "fourteen", "fifteen",
                "sixteen", "seventeen", "eighteen", "nineteen"
                };

            var num = val % 1000;

            if (num == 0)
            {
                return "";
            }

            var r = new StringBuilder();
            if (!valuesFrom1To20[num / 100].Equals(""))
            {
                r.Append(valuesFrom1To20[num / 100] + " hundred ");
            }

            if (num % 100 < 20)
            {
                r.Append(valuesFrom1To20[num % 100] + " ");
            }
            else
            {
                r.Append(Tens[num % 100 / 10] + '-' + valuesFrom1To20[num % 10] + " ");

            }

            r.Append(one);

            return r.ToString();
        }

        private string Str(int val)
        {
            var n = val;

            var r = new StringBuilder();

            if (n == 0)
            {
                return r.ToString();
            }

            if (n % 1000 != 0)
            {
                r.Append(Str(n, false, ""));
            }

            n /= 1000;

            r.Insert(0, Str(n, false, "thousand "));
            n /= 1000;

            r.Insert(0, Str(n, true, "million "));
            n /= 1000;

            r.Insert(0, Str(n, true, "billion "));

            return r.ToString();
        }

        public void EngInput()
        {
            Console.WriteLine("Input value");

            _str = Console.ReadLine();

            if (IsNullOrEmpty(_str) || _str.Contains(','))
            {
                Console.WriteLine("Invalid data");
                return;
            }

            bool resultParseNumber;

            if (_str.Contains('.'))
            {
                resultParseNumber = double.TryParse(_str, out _number);

                if (resultParseNumber && _number <= 2147483647 && _number >= 0)
                {
                    var number = Convert.ToString(_number).Split('.');

                    _leftPart = int.Parse(number[0]);

                    if (number.Length == 2)
                    {
                        _rightPart = int.Parse(number[1]);

                        if (_rightPart <= 99 && number[1].Length <= 2)
                        {
                            if (_leftPart == 1)
                            {
                                Console.Write(Str(_leftPart) + "dollar ");

                                if (_rightPart == 1)
                                {
                                    Console.WriteLine(Str(_rightPart) + "cent");
                                    return;
                                }
                                Console.WriteLine(Str(_rightPart) + "cents");
                                return;
                            }

                            if (_rightPart == 1 && _leftPart != 0)
                            {
                                Console.WriteLine(Str(_leftPart) + "dollars " +
                                                  Str(_rightPart) + "cent");
                                return;
                            }

                            if (_leftPart == 0)
                            {
                                if (_rightPart == 1)
                                {
                                    Console.WriteLine(Str(_rightPart) + "cent");
                                    return;
                                }
                                Console.WriteLine(Str(_rightPart) + "cents");
                                return;
                            }

                            Console.WriteLine(Str(_leftPart) + "dollars " +
                                              Str(_rightPart) + "cents");
                            return;
                        }
                    }

                    if (_number <= 2147483647 && _number > 0 && (_number % 1) == 0)
                    {
                        if (_number == 1)
                        {
                            Console.WriteLine(Str(_leftPart) + "dollar ");
                            return;
                        }

                        Console.WriteLine(Str(_leftPart) + "dollars ");
                        return;
                    }
                }
            }

            resultParseNumber = double.TryParse(_str, out _number);

            if (resultParseNumber && _number <= 2147483647 && _number >= 0 && (_number % 1) == 0)
            {
                if (_number == 0)
                {
                    Console.WriteLine("zero dollars");
                    return;
                }

                if (_number <= 2147483647 && _number > 0)
                {
                    Console.WriteLine(Str(Convert.ToInt32(_number)));
                    return;
                }
            }

            Console.WriteLine("Invalid data");
            

        }
    }
}
