using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace Lab06
{
    internal class Program
    {
        static void Main(string[] args)
            
        {
            int lives = 0;
            if (lives <= 0) // ด้านในวงเล็บ ต้องมีค่าเป็น Boolean
            {
                Console.WriteLine("Game Over"); 
                // โด๊ดจะรันเมื่อ if เป็นจริงเท่านั้น
            }
            Console.WriteLine("Continue Running");

            //2
            int coins = 110;
            int price = 100;
            if (coins >= price)
            {
                Console.WriteLine("Purchased"); // รันเมื่อเป็นจริง
            }
            else
            {
                Console.WriteLine("Not enough coin."); // รันเมื่อเป็นเท็จ
            }
            //3
            int score = 75;
            if (score >=90) // ลำดับ 1 เช็ค = เท็จ
            {
                Console.WriteLine("Rank S"); // โค๊ดไม่รัน ไม่อ่าน
            }
            else if (score >=60) // ลำดับ 2 เช็ค = จริง
            {
                Console.WriteLine("Rank A"); // โค๊ดรัน
            }
            else // เป็นกรณีที่ไม่ตรงกับั้งสอง ลำดับด้านบน มีเป็นจริงแล้ว ส่วนนี้ไม่รัน
            {
                Console.WriteLine("Rank B");
            }

            //4
            bool hasKey = true; // ผู้เล่นมี key หรือไม่

            Console.Write("Your level (1-99): ");
            bool ok = int.TryParse(Console.ReadLine(), out int level);
            if (!ok || level < 1 || level > 99) //  เช็คกรณีuser ใส่ผิด
            {
                Console.WriteLine("Invalid Level, please try again");
            }
            else if (level >= 10 && hasKey) // ใส่เงื่อนไขกุญแจ
            {
                Console.WriteLine("Boss floor unlocked.");
            }
            else if (level >= 5)
            {
                Console.WriteLine("The door unlocked.");
            }
            else
            {
                Console.WriteLine("The door stays shut.");
            }
            //
            int heroHP = 100;
            int monHP = 100;
            int atk = 100;

            Console.WriteLine("Adventure of Brian");
            
            Console.WriteLine(">=== Monster Encounter ===<");
            Console.WriteLine("ACTION 1: ATTACK");
            Console.WriteLine("ACTION 2: FLEE\n");
            
            Console.Write("Choose your action:");
            bool imputOk = int.TryParse(Console.ReadLine(), out int choice);

            //เช็คว่าผู้เล่นพิมพ์มาถูกมั้ย 1. ตัวอกษร, A, a หรือ b, B
            if (!imputOk || (choice != 'a' && choice != 'A' && choice != 'b' && choice != 'B'))
            {
                Console.WriteLine("Invalid input, Please choose between a,b");
            }
            else if (choice == 'a' || choice == 'A')
            {
                //ให้ทำอะไรเมื่อเลือก 1(a/A)
                monHP -= atk;
                //ผู้เล่นโจมตีมอนต์ n หน่วย เลือดเหลือ ...
                if (monHP <= 0)
                {
                    Console.WriteLine($"Player attack monster with {atk} points, Monster defeated!!!"); // output เมื่อ HP เหลือ 0 
                }
               else
                {
                    Console.WriteLine($"Player attack monster with {atk} points, Monster HP has {monHP} HP left."); // output เมื่อ HP เหลือมากกว่า 0
                }
            }
            else if (choice == 'b' || choice == 'B')
            {
                //ให้ทำอะไรเมื่อเลือก 2(b/B)
                monHP -= atk;
                //ผู้เล่นวิ่งหนีมอนต์ โดนโจมตี n หน่วย เลือดผู้เล่นเหลือ ...
            }
            else
            {
                Console.WriteLine("You fled from the monster.");
            }
            










        }
    }
}
