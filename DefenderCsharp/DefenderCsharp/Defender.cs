using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
            String first_name, last_name, input_file, output_file;
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

            //TODO input file
            //prompts for reads the name of an input file from the user

            //TODO output file
            //prompts for reads the name of an output file from the user

            //prompts the user to enter a password, store the password, then ask the user to re-enter the password and verify that it is correct
            EnterPassword();
            bool valid = CheckPassword();

            //TODO get input file
            //opens the output file and writes the user's name along with the result of adding the two integer values and the result of multiplying the two integer values, followed by the contents of the input file

        }//end of theDefender

        /* This method prompts the user to enter a password, makes sure it is valid, and stores the password
         * Credit:
         * https://stackoverflow.com/questions/4181198/how-to-hash-a-password/10402129#10402129
         */
        private void EnterPassword()
        {
            String input;
            Boolean running = false;
            //Making sure if the user Inputs an invalid form it will reprompt
            do
            {
                input = getUserInput("Your password must be 12 to 128 characters long, and must contain at least 1 of each of the following: uppercase letter, lowercase letter, number, and symbol." +
                                        "\nEnter a new Password: ");
                running = PassValidator(input);
            } while (!running);

            //step 1 - Create the salt value with a cryptographic PRNG:
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            //step 2 - Create the Rfc2898DeriveBytes and get the hash value:
            var pbkdf2 = new Rfc2898DeriveBytes(input, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            //step 3 - Combine the salt and password bytes for later use:
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            //step 4 - Turn the combined salt+hash into a string for storage
            string savedHash = Convert.ToBase64String(hashBytes);
            System.IO.File.WriteAllText("./sh.ush", savedHash);

        }

        /* This method asks the user to re-enter the password, and verifies that it is correct
         * Credit:
         * https://stackoverflow.com/questions/4181198/how-to-hash-a-password/10402129#10402129
         */
        private bool CheckPassword()
        {
            const string shush = "./sh.ush";
            if (System.IO.File.Exists(shush))
            { 
                String input = getUserInput("Enter your Password: ");
                // Fetch the stored value
                string savedPasswordHash = System.IO.File.ReadAllText(shush);
                // Extract the bytes
                byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);
                // Get the salt
                byte[] salt = new byte[16];
                Array.Copy(hashBytes, 0, salt, 0, 16);
                // Compute the hash on the password the user entered
                var pbkdf2 = new Rfc2898DeriveBytes(input, salt, 10000);
                byte[] hash = pbkdf2.GetBytes(20);
                // Compare the results
                for (int i = 0; i < 20; i++)
                    if (hashBytes[i + 16] != hash[i])
                    {
                        Console.WriteLine("Password was invalid.");
                        return false;
                    }
                return true;
            }
            else
            {
                Console.WriteLine("Error. No password is currently stored.");
                return false;
            }
        }

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

        /* This method will validate the inputs for the Password
         */
        private Boolean PassValidator(String input)
        {
            Boolean isMatch = false;
            if (input != null)
            {
                isMatch = regexValidator.getPasswordRegex(input);
                //if the regex match is false it will display a message that it is.
                if (isMatch == false)
                {
                    Console.WriteLine("Password does not meet the minimum criteria. Please enter it again.");
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
