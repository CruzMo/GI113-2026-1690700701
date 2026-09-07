namespace Lab04
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("+_+_+ NWE ADVENTURE +_+_+"); // Print ธรรมดา Title
            //Console.Write("Name your hero: "); // WriteLine พิมไ์บรรทัดใหม่, Wrie พิมพ์ต่อเต่อบรรทัดเดิม

            //string playerName = Console.ReadLine(); // เมื่อผู้เล่นกรอกชื่อ จากนั้นชื่อจะเข้าไปเก็บใน playerName

            //Console.WriteLine($"\nWelcome, {playerName}. Your journey begins...");

            //Console.WriteLine("\n+++++++++++++++" +
            //                    "\n+ Difficulty Selecion +" +
            //                    "\n++++++++++++++++++++++++++");

            //Console.Write("Choose Difficulty (1-3): ");
            ////string choice = Console.ReadLine(); // รับ Input และเก็บใส่ choice

            //int difficulty = Convert.ToInt32(Console.ReadLine);
            //Console.WriteLine($"Difficulty set to {difficulty}");

            //Console.WriteLine("\\\\\\ ITEM SHOP //////");
            //Console.WriteLine("How many potions?: ");
            //bool isValid = int.TryParse(Console.ReadLine(), out int quantity);
            //Console.WriteLine($">> Valid input:{isValid}");
            //Console.WriteLine($">> Quantity: {quantity}");
            
            Console.WriteLine("Welcome to the Adventure Game!");
            Console.WriteLine("+-----------------------------+ "+
                              "\n    Character Creation      | " +
                              "\n+-----------------------------+ ");
            Console.Write("Name your character: ");
            string charName = Console.ReadLine();
            Console.Write("Choose a class (1-3): ");
            bool classValid = int.TryParse(Console.ReadLine(), out int classNum);
            Console.Write("Staring luck (0.0-10.0): ");
            bool luckValid = double.TryParse(Console.ReadLine(), out double luckNum);
            Console.WriteLine($"\n{charName} the Class-{classNum} adventurer enters the dungeon. Luck: {luckNum}");


        }
    }
}
