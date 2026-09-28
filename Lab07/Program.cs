/*
* Student ID : 1690700701
* Name       : Thampapon Thuamboribun
* Section    : 129A
* No.        : 30
* Course     : GI113 Computer Programming (GI)
*/

namespace Lab07
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int MonsterHp = 10;

            Console.Write("Monster Defense: ");
            int.TryParse(Console.ReadLine(), out int monsterDefense);

            Console.WriteLine($"A Slime appears! HP {MonsterHp}, DEF {monsterDefense}");

            Console.WriteLine("=== BATTLE MENU ===");
            Console.WriteLine("1) Attack");
            Console.WriteLine("2) Fire Magic");
            Console.WriteLine("3) Defend");
            Console.WriteLine("4) Run");
            Console.WriteLine("5) Ice Spear");

            Console.Write("Choose (1-5): ");
            int.TryParse(Console.ReadLine(), out int command);

            // Step 2: switch statement
            switch (command)
            {
                case 1:
                    Console.WriteLine("Hero swings the sword!");
                    break;

                case 2:
                    Console.WriteLine("Hero casts Fire!");
                    break;

                case 3:
                    Console.WriteLine("Hero raises the shield.");
                    break;

                case 4:
                    Console.WriteLine("Hero looks for a way out...");
                    break;

                case 5:
                    Console.WriteLine("Hero casts Ice Spear!");
                    break;

                default:
                    Console.WriteLine("Hero hesitates. Invalid command!");
                    break;
            }

            // Step 3: switch expression
            int power = command switch
            {
                1 => 12,
                2 => 18,
                5 => 15,
                _ => 0
            };

            int damage = Math.Max(0, power - monsterDefense);
            Console.WriteLine($"Damage: {damage}");

            // Step 4: relational patterns
            string rating = damage switch
            {
                >= 12 => "Critical hit!",
                >= 5 => "Solid hit.",
                > 0 => "Scratch.",
                _ => "No damage."
            };

            Console.WriteLine($"Rating: {rating}");

            // Step 5: ternary
            string monsterStatus = damage >= MonsterHp
                ? "DEFEATED"
                : "still standing";

            Console.WriteLine($"Slime: {monsterStatus}");

            // Step 6: switch statement
            Console.Write("Really run away? (y/n): ");
            string answer = Console.ReadLine();

            switch (answer)
            {
                case "y":
                case "Y":
                    Console.WriteLine("You escaped!");
                    break;

                case "n":
                case "N":
                    Console.WriteLine("You stay and fight.");
                    break;

                default:
                    Console.WriteLine("Please type y or n.");
                    break;
            }

            Console.ReadLine();
        }
    }
}