using System;
using System.Collections.Generic;
using System.Text;

namespace WEA.Common.Util.Extensions
{
    public static class NumberExtensions
    {
        public static string ConvertUnitToString(string number)
        {
            String name = "";
            if (number != null)
            {
                int _Number = int.Parse(number);

                switch (_Number)
                {

                    case 1:
                        name = "Bir";
                        break;
                    case 2:
                        name = "İki";
                        break;
                    case 3:
                        name = "Üç";
                        break;
                    case 4:
                        name = "Dörd";
                        break;
                    case 5:
                        name = "Beş";
                        break;
                    case 6:
                        name = "Altı";
                        break;
                    case 7:
                        name = "Yeddi";
                        break;
                    case 8:
                        name = "Səkkiz";
                        break;
                    case 9:
                        name = "Doqquz";
                        break;
                }

            }
            return name;

        }


        public static String ConvertDecimalToString(String number)
        {
            String name = "";
            if (number != null)
            {
                int _Number = int.Parse(number);

                switch (_Number)
                {
                    case 10:
                        name = "On";
                        break;
                    case 20:
                        name = "İyirmi";
                        break;
                    case 30:
                        name = "Otuz";
                        break;
                    case 40:
                        name = "Qırx";
                        break;
                    case 50:
                        name = "Əlli";
                        break;
                    case 60:
                        name = "Altmış";
                        break;
                    case 70:
                        name = "Yetmiş";
                        break;
                    case 80:
                        name = "Səksən";
                        break;
                    case 90:
                        name = "Doxsan";
                        break;
                    default:
                        if (_Number > 0)
                        {
                            name = ConvertDecimalToString(number.Substring(0, 1) + "0") + " " + ConvertUnitToString(number.Substring(1));
                        }
                        break;
                }
            }
            return name;
        }

        public static string ConvertWholeNumberToString(string number)
        {
            string word = "";
            try
            {
                bool beginsZero = false;
                bool isDone = false;
                double dblAmt = Convert.ToDouble(number);
                if (dblAmt > 0)
                {
                    beginsZero = number.StartsWith("0");

                    int numDigits = number.Length;
                    int pos = 0;
                    String place = "";
                    switch (numDigits)
                    {
                        case 1:
                            word = ConvertUnitToString(number);
                            isDone = true;
                            break;
                        case 2:
                            word = ConvertDecimalToString(number);
                            isDone = true;
                            break;
                        case 3:
                            pos = (numDigits % 3) + 1;
                            place = " Yüz ";
                            break;
                        case 4:
                        case 5:
                        case 6:
                            pos = (numDigits % 4) + 1;
                            place = " Min ";
                            break;
                        case 7:
                        case 8:
                        case 9:
                            pos = (numDigits % 7) + 1;
                            place = " Milyon ";
                            break;
                        case 10:
                        case 11:
                        case 12:

                            pos = (numDigits % 10) + 1;
                            place = " Milyard ";
                            break;
                        default:
                            isDone = true;
                            break;
                    }
                    if (!isDone)
                    {
                        if (number.Substring(0, pos) != "0" && number.Substring(pos) != "0")
                        {
                            try
                            {
                                word = ConvertWholeNumberToString(number.Substring(0, pos)) + place + ConvertWholeNumberToString(number.Substring(pos));
                            }
                            catch { }
                        }
                        else
                        {
                            word = ConvertWholeNumberToString(number.Substring(0, pos)) + ConvertWholeNumberToString(number.Substring(pos));
                        }
                    }
                    if (word.Trim().Equals(place.Trim())) word = "";
                }
            }
            catch { }
            return word.Trim();
        }

        public static string ConvertDecimalPointNumbersToString(this string numb)
        {
            String val = "", wholeNo = numb, points = "", andStr = "Manat";
            String endStr = "qəp.";
            try
            {
                int decimalPlace = numb.IndexOf(".");
                if (decimalPlace > 0)
                {
                    wholeNo = numb.Substring(0, decimalPlace);
                    points = numb.Substring(decimalPlace + 1);
                }

                if (points != "00")
                {
                    val = String.Format("{0} {1} {2} {3}", ConvertWholeNumberToString(wholeNo).Trim(), andStr, points, endStr);
                }
                else
                {
                    val = String.Format("{0} {1}", ConvertWholeNumberToString(wholeNo).Trim(), andStr);
                }

            }
            catch { }
            return val;
        }
    }
}
