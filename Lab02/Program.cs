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
            Console.WriteLine($"Attack Power: {attackPower}");
            Console.WriteLine($"Critical Multiplier: {critMultiplier}");
            Console.WriteLine($"Is Boss: {isBoss}");
            Console.WriteLine();

            int hpPercent = currentHp * 100 / maxHp;
            Console.WriteLine($"HP Percent: {hpPercent}%");
            Console.WriteLine();
            Console.WriteLine("Kirin takes 60 damage!");
            currentHp -= 60;
            Console.WriteLine();
            Console.WriteLine($"===== BOSS STATUS: AFTER DAMAGE =====");
            Console.WriteLine($"HP: {currentHp} / {maxHp}");
            hpPercent = currentHp * 100 / maxHp;
            Console.WriteLine($"HP Percent: {hpPercent}%");
            
            Console.WriteLine();
            Console.WriteLine($"===== PART B : MY RPG CHARACTERS =====");
            Console.WriteLine();

            // ===== Knight =====
            string knightName = "George";
            int knightLevel = 12;
            int kinghtHp = 320;
            float knightAttack = 48.5f;
            float knightDefense = 96.9f;
            bool knightIsTank = true;

            Console.WriteLine($"KNIGHT");
            Console.WriteLine($"Name: {knightName}");
            Console.WriteLine($"Level: {knightLevel}");
            Console.WriteLine($"HP: {kinghtHp}");
            Console.WriteLine($"Attack Power: {knightAttack}");
            Console.WriteLine($"Defense: {knightDefense}");
            Console.WriteLine($"Is Tank:{knightIsTank}");
            Console.WriteLine();

            // ===== Mage =====
            string mageName = "Nox";
            int mageLevel = 14;
            int mageHp = 200;
            float mageMagic = 92.0f;
            double mageCrit = 2.15;
            bool mageIsBoss = false;

            Console.WriteLine($"MAGE");
            Console.WriteLine($"Name: {mageName}");
            Console.WriteLine($"Level: {mageLevel}");
            Console.WriteLine($"HP: {mageHp}");
            Console.WriteLine($"Magic Power: {mageMagic}");
            Console.WriteLine($"Critical Multiplier: {mageCrit}");
            Console.WriteLine($"Is Boss: {mageIsBoss}");
            Console.WriteLine();

            // ===== Archer =====
            string archerName = "Sylvan";
            int archerLevel = 11;
            int archerHp = 210;
            float archerSpeed = 38.7f;
            char archerRank = 'A';
            bool archerIsHasPet = true;

            Console.WriteLine($"ARCHER");
            Console.WriteLine($"Name: {archerName}");
            Console.WriteLine($"Level: {archerLevel}");
            Console.WriteLine($"HP: {archerHp}");
            Console.WriteLine($"Speed: {archerSpeed}");
            Console.WriteLine($"Rank: {archerRank}");
            Console.WriteLine($"Has Pet: {archerIsHasPet}");
            Console.WriteLine();

            // ===== Assassin =====
            String assassinName = "Ronan";
            int assassinLevel = 15;
            int assassinHp = 180;
            float assassinAttack = 55.3f;
            double assassinCrit = 2.45;
            bool assassinIsStealth = true;

            Console.WriteLine($"ASSASSIN");
            Console.WriteLine($"Name: {assassinName}");
            Console.WriteLine($"Level: {assassinLevel}");
            Console.WriteLine($"HP: {assassinHp}");
            Console.WriteLine($"Attack Power: {assassinAttack}");
            Console.WriteLine($"Critical Multiplier: {assassinCrit}");
            Console.WriteLine($"Is Stealth: {assassinIsStealth}");
            Console.WriteLine();
            Console.ReadLine();
        }





    }
    }

