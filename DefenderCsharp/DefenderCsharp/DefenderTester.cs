using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 * Zechariah Speer
 * Fletcher Baker
 * Dillon Geier
 */
namespace DefenderCsharp
{
    class DefenderTester
    {

        static void Main(string[] args)
        {
            Defender defend = new Defender();
            defend.theDefender();
            Console.WriteLine("Press Any Key to Exit");
            Console.ReadKey();
        }
    }
}
