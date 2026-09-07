/*
* Student ID : 1690700701
* Name       : Thampapon Thuamboribun
* Section    : 129A
* No.        : 30
* Course     : GI113 Computer Programming (GI)
*/
namespace Lab03
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int MaxLevel = 10;

            var bossName = "Kirin";   // ต้องประกาศด้วย var ห้ามเขียน string ตรงๆ
            var rank = 'S';            // ต้องประกาศด้วย var ห้ามเขียน char ตรงๆ
            int level = 7;
            int maxHp = 240;
            int currentHp = 115;       // ค่าตั้งต้นของ Lab นี้คือ HP "หลังโดนโจมตี" จาก Lab 2 แล้ว ไม่ใช่ 175
            float attackPower = 42.5f;
            double critMultiplier = 1.75;
            bool isBoss = true;

            //output แสดงค่าตัวแปร
            Console.WriteLine("===== KIRIN SAVE CONVERTER =====");
            Console.WriteLine($"Name:{bossName}\nRank:{rank}\nLevel:{level}/{MaxLevel}\nHP:{currentHp}/{maxHp}" +
                $"\nAttack Power:{attackPower}\nCrit Multipllier:{critMultiplier}\nIs Boss:{isBoss}");

            // ทำ implicit int -> double
            Console.WriteLine("\n----- Implicit Conversion: HP as double) -----");
            double currentHpDouble = currentHp; //Int -->Double แปลงแบบไม่ Cast ใส่ค่าได้เลย
            Console.WriteLine($"HP (double): {currentHpDouble}");

            //ทำ implicit หลังการคำนวนเปอร์เซ้นต์ ที่ผลลัพธ์มีทศนิยม
            Console.WriteLine("\n----- Exact HP Percent (no integer truncation) -----");
            double hpPercentExact = currentHpDouble * 100 / maxHp; //Int หาร int จากนั้นแปลงผลลลัพธ์การคำนวนเป็น double เพื่อเก็บทศนิยม
            Console.WriteLine($"HP Percent (exact): {hpPercentExact}%");

            //ทำ Explicit โดยการ Casting (float) AttackPower ->> Int
            Console.WriteLine("\n----- Explicit Cast: Attack Power -> Display Int -----");
            int attackDisplay = (int)attackPower; //เปลงข้อมูลโดยการ Cast Syntax: (ชนิดที่ต้องการแปลง)ชื่อตัวแปรที่อยากแปร 
            Console.WriteLine($"Attack Power (int cast): {attackDisplay}");

            //ส่วนสุดท้าย Cast + Convert
            Console.WriteLine("\n----- Cast vs Convert: Crit Multiplier -----");
            int critCast = (int)critMultiplier;
            int crirConvert = Convert.ToInt32(critMultiplier);

            Console.WriteLine($"Crit Multiplier (int cast): {critCast}");
            Console.WriteLine($"Crit Multiplier (Convert rouded): {crirConvert}");






        }
    }
}
