using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefenderCsharp
{
    class Defender
    {
        private RegexValidator regexValidator;

        public Defender()
        {
            regexValidator = new RegexValidator();
        }//end of Default CtorS

        public void theDefender()
        {
            String first_name;
            String last_name;
            int numOne;
            int numTwo;

            Boolean running = false;
            //Making sure if the user Inputs an invalid form of name it will reprompt
            do
            {
                first_name = getUserInput("Enter your First Name: ");
                running = NameValidator(first_name,"First Name");
           
                if (running)
                {
                    last_name = getUserInput("Enter your Last Name: ");
                    running = NameValidator(last_name,"Last Name");
                }//end of if
            } while (!running);

            //Making sure if the user Inputs an invalid input for the int values it will reprompt
            //doing it this way the user won't have to reinput everything so once its section is valid it can't be messed with again
            do
            {
                // This way will try to see if it is able to be parsed to int and is in the range on int
                String num_one = getUserInput("Enter First Number: ");
                running = int.TryParse(num_one, out numOne); 

                if (running)
                {
                    String num_two = getUserInput("Enter Second Number: ");
                    running = int.TryParse(num_two, out numTwo);
                }//end of if
            } while (!running);


        }//end of theDefender

        /* This method will validate the inputs for the Name
         * parm @ firstInput = this is the user input of first name
         * parm @ lastInput = this is the user input of last name
         */
        private Boolean NameValidator(String input, String whichName)
        {
            Boolean isMatch = false;
            if (input != null)
            {
                isMatch = regexValidator.getNameRegex(input);
                //if the regex match is false it will display a message that it is.
                if(isMatch == false)
                {
                    Console.WriteLine("Incorrect Input for {0}, Please enter it again.",whichName);
                }
            }

            return isMatch;
        }

        /* A basic Method that Requests input from user and will return it when input is given
        *  parm @ typeOfInput = is a String that contains a sentence to Declare the type of input users may enter
        */
        private String getUserInput(String typeofInput)
        {
            String input = null;

            Console.Write(typeofInput);
            input = Console.ReadLine();

            return input;
        }

    }
}
