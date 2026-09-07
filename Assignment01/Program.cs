/*
 * Student ID : 1690700701
 * Name       : Thampapon Thuamboribun
 * Section    : 129A
 * No.        : 30
 * Course     : GI113 Computer Programming (GI)
 */
namespace Assignment01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string GameTitle = "VALORANT";

            var agentName = "Jett";
            var agentRole = 'A';
            int agentLevel = 27;
            float movementSpeed = 12.8f;
            double combatRating = 93.6;
            bool isUnlocked = true;

            Console.WriteLine("+==========================================+");
            Console.WriteLine($"|            {GameTitle} AGENT CARD             |");
            Console.WriteLine("+==========================================+");
            Console.WriteLine($"| Agent Name     : {agentName}");
            Console.WriteLine($"| Role Code      : {agentRole}");
            Console.WriteLine($"| Agent Level    : {agentLevel}");
            Console.WriteLine($"| Move Speed     : {movementSpeed}");
            Console.WriteLine($"| Combat Rating  : {combatRating}");
            Console.WriteLine($"| Unlocked       : {isUnlocked}");
            Console.WriteLine("+==========================================+");
            Console.WriteLine();

            double agentLevelDouble = agentLevel;

            Console.WriteLine("+------------------------------------------+");
            Console.WriteLine("|           IMPLICIT CONVERSION             |");
            Console.WriteLine("+------------------------------------------+");
            Console.WriteLine($"| Level as double : {agentLevelDouble}");
            Console.WriteLine("+------------------------------------------+");
            Console.WriteLine();

            int combatRatingCast = (int)combatRating;
            int combatRatingConvert = Convert.ToInt32(combatRating);

            Console.WriteLine("+------------------------------------------+");
            Console.WriteLine("|             CAST VS CONVERT               |");
            Console.WriteLine("+------------------------------------------+");
            Console.WriteLine($"| Original Rating : {combatRating}");
            Console.WriteLine($"| Explicit Cast   : {combatRatingCast}");
            Console.WriteLine($"| Convert Rounded : {combatRatingConvert}");
            Console.WriteLine("+------------------------------------------+");
        }
    }
}
