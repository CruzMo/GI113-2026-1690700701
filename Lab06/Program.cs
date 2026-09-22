/*
* Student ID : 1690700701
* Name       : Thampapon Thuamboribun
* Section    : 129A
* No.        : 30
* Course     : GI113 Computer Programming (GI)
*/

namespace Lab06
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //int lives = 0;
            //if (lives <= 0) // ด้านในวงเล็บ ต้องมีค่าเป็น Boolean
            //{
            //    Console.WriteLine("Game Over"); 
            //    // โด๊ดจะรันเมื่อ if เป็นจริงเท่านั้น
            //}
            //Console.WriteLine("Continue Running");

            ////2
            //int coins = 110;
            //int price = 100;
            //if (coins >= price)
            //{
            //    Console.WriteLine("Purchased"); // รันเมื่อเป็นจริง
            //}
            //else
            //{
            //    Console.WriteLine("Not enough coin."); // รันเมื่อเป็นเท็จ
            //}
            ////3
            //int score = 75;
            //if (score >=90) // ลำดับ 1 เช็ค = เท็จ
            //{
            //    Console.WriteLine("Rank S"); // โค๊ดไม่รัน ไม่อ่าน
            //}
            //else if (score >=60) // ลำดับ 2 เช็ค = จริง
            //{
            //    Console.WriteLine("Rank A"); // โค๊ดรัน
            //}
            //else // เป็นกรณีที่ไม่ตรงกับั้งสอง ลำดับด้านบน มีเป็นจริงแล้ว ส่วนนี้ไม่รัน
            //{
            //    Console.WriteLine("Rank B");
            //}

            ////4
            //bool hasKey = true; // ผู้เล่นมี key หรือไม่

            //Console.Write("Your level (1-99): ");
            //bool ok = int.TryParse(Console.ReadLine(), out int level);
            //if (!ok || level < 1 || level > 99) //  เช็คกรณีuser ใส่ผิด
            //{
            //    Console.WriteLine("Invalid Level, please try again");
            //}
            //else if (level >= 10 && hasKey) // ใส่เงื่อนไขกุญแจ
            //{
            //    Console.WriteLine("Boss floor unlocked.");
            //}
            //else if (level >= 5)
            //{
            //    Console.WriteLine("The door unlocked.");
            //}
            //else
            //{
            //    Console.WriteLine("The door stays shut.");
            //}
            
            int heroHp = 100;
            int monsterHp = 100;
            int attackPower = 35;
            int healAmount = 20;
            int counterDamage = 10;

            Console.WriteLine("=== SWAN BATTLE ===");
            Console.WriteLine("A: ATTACK");
            Console.WriteLine("B: DEFEND");
            Console.WriteLine("C: HEAL");

            Console.Write("Choose your action: ");
            bool inputOk = char.TryParse(Console.ReadLine(), out char choice);

            if (!inputOk ||
                (choice != 'a' && choice != 'A' &&
                 choice != 'b' && choice != 'B' &&
                 choice != 'c' && choice != 'C'))
            {
                Console.WriteLine("Invalid input. Please choose A, B or C.");
            }
            else if (choice == 'a' || choice == 'A')
            {
                monsterHp -= attackPower;

                Console.WriteLine($"Swan attacks the monster for {attackPower} damage.");
                Console.WriteLine($"Monster HP is now {monsterHp}.");
            }
            else if (choice == 'b' || choice == 'B')
            {
                heroHp -= counterDamage;

                Console.WriteLine($"Swan defends against the monster and takes {counterDamage} damage.");
                Console.WriteLine($"Swan HP is now {heroHp}.");
            }
            else if (choice == 'c' || choice == 'C')
            {
                heroHp += healAmount;

                Console.WriteLine($"Swan uses a potion and heals {healAmount} HP.");
                Console.WriteLine($"Swan HP is now {heroHp}.");
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }
            Console.ReadLine();
        }
    }
}