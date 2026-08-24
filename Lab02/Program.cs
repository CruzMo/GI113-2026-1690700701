/*
 * Student ID : 1690700701
 * Name       : Thampapon Thuamboribun
 * Section    : 129A
 * No.        : 0
 * Course     : GI113 Computer Programming (GI)
 */

namespace Lab02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string bossName = "Kirin";
            char rank = 'S';
            int level = 7;
            int maxHp = 240;
            int currentHp = 175;
            float attackPower = 42.5f;
            double critMultiplier = 1.75;
            bool isBoss = true;

            Console.WriteLine("===== BOSS STATUS: INITIAL =====");
            Console.WriteLine($"Name: {bossName}");
            Console.WriteLine($"Rank: {rank}");
            Console.WriteLine($"Level:{level}");
            Console.WriteLine($"HP: {currentHp} / {maxHp}");
            Console.WriteLine();

            int hpPercet = currentHp * 100 / maxHp;
            Console.WriteLine($"HP Percent; {hpPercet}%");
            Console.WriteLine();
            Console.WriteLine("Kirin takes 60 damage!");
            Console.WriteLine();
            Console.WriteLine($"===== BOSS STATUS: AFTER DAMAGE =====");
            Console.WriteLine($"HP: {currentHp} / {maxHp}");
            Console.WriteLine($"{currentHp * 100 / maxHp}");

        }
    }
}
