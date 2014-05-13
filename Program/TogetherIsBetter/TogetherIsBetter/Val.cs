using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TogetherIsBetter
{
    class Val
    {
        public static bool isNumber(String input)
        {
            int number = -1;
            Int32.TryParse(input, out number);
            return number == 0 ? false : true;
        }

        public static bool isEmpty(String input)
        {
            return input.Equals("") ? true : false;   
        }

        public static bool isLongerThan(String input, int length)
        {
            return input.Length > length ? true : false;
        }

        public static bool isEmail(String input)
        {
            Match match = Regex.Match(input, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            return match.Success ? true : false;
            
        }
    }
}
