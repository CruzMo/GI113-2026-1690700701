/*
* Student ID : 1690700701
* Name       : Thampapon Thuamboribun
* Section    : 129A
* No.        : 30
* Course     : GI113 Computer Programming (GI)
*/

using System;

namespace Lan05
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("===> BATTLE SWAN <===");
            Console.WriteLine("Elegance VS Elegance");
            
            // User input of swan stats
            Console.Write("Swan HP: ");
            bool isSwanHp = int.TryParse(Console.ReadLine(), out int swanHp);
            Console.Write("Swan Attack: ");
            bool isSwanAttack = int.TryParse(Console.ReadLine(), out int swanAttack);
            Console.Write("Swan Defense: ");
            bool isSwanDefense = int.TryParse(Console.ReadLine(), out int swanDefense);
            
            // User input of monster stats
            Console.Write("Monster HP: ");
            bool isMonsterHp = int.TryParse(Console.ReadLine(), out int monsterHp);
            Console.Write("Monster Attack: ");
            bool isMonsterAttack = int.TryParse(Console.ReadLine(), out int monsterAttack);
            Console.Write("Monster Defense: ");
            bool isMonsterDefense = int.TryParse(Console.ReadLine(), out int monsterDefense);

            // Check if player inputs are valid
            bool allSwanValid = isSwanHp && isSwanAttack && isSwanDefense;
            bool allMonsterValid = isMonsterHp && isMonsterAttack && isMonsterDefense;
            Console.WriteLine($"Stats validation: SWAN: {allSwanValid}, MONSTER: {allMonsterValid}");
            Console.WriteLine($"[SWAN]    HP: {swanHp}, ATK: {swanAttack}, DEF: {swanDefense}");
            Console.WriteLine($"[MONSTER] HP: {monsterHp}, ATK: {monsterAttack}, DEF: {monsterDefense}");

            // Before fighing: Swan drinks a potion (Compound Assignment)
            int potionHeal = 8;
            // 1.   1 = 1+2 swanHp = swanHp + potionHeal;
            // 2.   1 += 2 swanHp += potionHeal;
            swanHp += potionHeal; // แนะนำแบบนี้ คำนวณเหมือนกัน 1 = 1+2
            Console.WriteLine($"\nSwan drinks a potion, Healing {potionHeal} HP. Swan HP is now: {swanHp}");
            
            // คำนวณ famgeg normal attack (Arithmetic + Math)
            int normelDamege = Math.Max(0, swanAttack - monsterDefense);
            Console.WriteLine($"Nomal Attack deal: {normelDamege} DMG");

            // คำนวณ power attack (Perdence ลำดับการคำนวณ คูณ ก่อนที่จะ ลบ
            int powerDamege = Math.Max(0, swanAttack * 2 - monsterDefense); // เรียงลำดับ * มาก่อน - ไม่จำเป็นต้องมี ()
            Console.WriteLine($"Power Attack deal: {powerDamege} DMG");

            //คำนวน Monster Attack
            int counterDamge = Math.Max(0, monsterAttack - swanDefense);
            Console.WriteLine($"Monster Counter Attack deal: {counterDamge} DMG");

            // คำนวณ Cri Chance
            Random rng = new Random();
            int roll = rng.Next(1, 101);  // สุ่ม cri 1-100
            bool isCrit = roll <= 10; // 10%
            int criDamage = normelDamege + Convert.ToInt32(isCrit) * normelDamege; // โอกาศ 10% ติดคริ เลขได้ 1 ไม่ติด 0
            Console.WriteLine($"Critical hit roll: {roll} (critical: {isCrit})");
            Console.WriteLine($"Normal Attack would deal Critical: {criDamage} DMG");

            // Comparison + Logical Operators
            bool swanHitsHarder = swanAttack > monsterAttack;
            bool canOneShotWithNormal = normelDamege >= monsterHp;
            bool monsterCanOneShotSwan = counterDamge >= swanHp;
            bool safeTrade = normelDamege > counterDamge && !monsterCanOneShotSwan;
            bool luckyOrLethal = isCrit || canOneShotWithNormal;

            Console.WriteLine($"Swan hits harder: {swanHitsHarder}");
            Console.WriteLine($"Can one-shot with normal: {canOneShotWithNormal}");
            Console.WriteLine($"Monster can one-shot Swan: {monsterCanOneShotSwan}");
            Console.WriteLine($"Safe trade: {safeTrade}");
            Console.WriteLine($"Lucky or lethal: {luckyOrLethal}");

            // Swan attacks for real
            int monsterMaxHp = monsterHp;

            monsterHp -= normelDamege;

            Console.WriteLine($"Swan attacks! Monster HP: {monsterHp}/{monsterMaxHp}");

            bool monsterDefeated = monsterHp <= 0;
            int goldEarned = (monsterMaxHp - monsterHp) * 2;

            Console.WriteLine($"Monster defeated: {monsterDefeated}");
            Console.WriteLine($"Gold earned: {goldEarned}");

            Console.ReadKey();

        }
    }
}
