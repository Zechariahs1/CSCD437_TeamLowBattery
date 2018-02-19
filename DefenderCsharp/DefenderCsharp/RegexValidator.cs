using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DefenderCsharp
{
    class RegexValidator
    {
        private String mNameRegex;
        private String mPassRegex;

        public RegexValidator()
        {
            //A regex that will validate the First and Last name
            mNameRegex = @"^[A-Za-z\\-]{0,50}$";

            //A regex that will validate the password
            mPassRegex = @"^[A-Za-z0-9]{8,32}$"; //TODO update password regex
        }
        
        //private method to do the matching
        private Boolean Matcher(String input, String regex)
        {
            if (input != null)
            {
                var reg = new Regex(regex);
                return reg.IsMatch(input);

            }
            return false;
        }

        //public method that will cehck the input for matches of regex for the Names
        public Boolean getNameRegex(String input)
        {
            return Matcher(input, mNameRegex);
        }

        //public method that will cehck the input for matches of regex for the Names
        public Boolean getPasswordRegex(String input)
        {
            return Matcher(input, mPassRegex);
        }
    }
}
