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
        private String mNameRegex, mPassRegex, mFileNameRegex;
        private string[] mPassRegexesEach;

        public RegexValidator()
        {
            //A regex that will validate the First and Last name
            mNameRegex = @"^[A-Za-z\\-]{1,50}$";

            //A regex that will validate the password
            mPassRegex = @"^[!-~]{12,128}$";

            mPassRegexesEach = new string[]
            {
                @"^[!-~]*[A-Z]{1}[!-~]*$",
                @"^[!-~]*[a-z]{1}[!-~]*$",
                @"^[!-~]*[0-9]{1}[!-~]*$",
                @"^[!-~]*[!-/:-@\\[-`\\{-~]{1}[!-~]*$"
            };

            //A regex that will validate the file names
            mFileNameRegex = @"^[A-Za-z0-9! #-\\)+-.;=@\\[]-`{}~]{1,255}[.]{1}[t]{1}[x]{1}[t]{1}$";

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

        //public method that will cehck the input for matches of regex for the file names
        public Boolean getFileNameRegex(String input)
        {
            return Matcher(input, mFileNameRegex);
        }

        //public method that will cehck the input for matches of regex for the password
        public Boolean getPasswordRegex(String input)
        {
            return Matcher(input, mPassRegex) && CheckIfContainsEach(input, mPassRegexesEach);
        }

        private Boolean CheckIfContainsEach(String input, String[] regexes)
        {
            if (input == null || regexes == null)
                return false;

            foreach (String regex in regexes)
            {
                var reg = new Regex(regex);
                if (!(reg.IsMatch(input)))
                    return false;
            }
            return true;
        }
    }
}
