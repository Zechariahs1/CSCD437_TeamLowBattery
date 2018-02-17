using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefenderCsharp
{
    class Defender
    {
        public static void theDefender()
        {
            String first_name;
            String last_name;

            Boolean running = false;
            do
            {
                first_name = getUserInput("Enter your First Name: ");
                last_name = getUserInput("Enter your Last Name: ");
                running = NameValidator(first_name, last_name);

            } while (running);

        }

        private static Boolean NameValidator(String fistInput, String lastInput)
        {
            if (fistInput != null && lastInput != null)
            {

            }
            return false;
        }

        //A basic Method that Requests input from user and will return it when input is given
        private static String getUserInput(String typeofInput)
        {
            String input = null;

            Console.Write(typeofInput);
            input = Console.ReadLine();

            return input;
        }

    }
}
