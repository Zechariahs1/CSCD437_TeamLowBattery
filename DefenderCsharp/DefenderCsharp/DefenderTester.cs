using System;
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
