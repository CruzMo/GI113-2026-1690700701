/*
* Student ID : 1690700701
* Name       : Thampapon Thuamboribun
* Section    : 129A
* No.        : 30
* Course     : GI113 Computer Programming (GI)
*/


namespace Assignment__2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string MaterialName = "Ruby";
            const double SmeltRate = 0.25;
            const double SalvageRate = 0.30;
            const double MaxBatch = 500;

            // =========================
            //        RUBY FORGE
            // =========================

            Console.WriteLine("========================================================");
            Console.WriteLine("||                                                    ||");
            Console.WriteLine("||                 R U B Y   F O R G E                ||");
            Console.WriteLine("||              THE FORGING CHAMBER                   ||");
            Console.WriteLine("||                                                    ||");
            Console.WriteLine("========================================================");

            Console.WriteLine("||                                                    ||");
            Console.WriteLine($"||  MATERIAL     : {MaterialName,-34}||");
            Console.WriteLine($"||  SMELT RATE   : {SmeltRate,-34}||");
            Console.WriteLine($"||  SALVAGE RATE : {SalvageRate,-34}||");
            Console.WriteLine("||                                                    ||");

            Console.WriteLine("========================================================");
            Console.WriteLine("||                  F O R G E   M E N U               ||");
            Console.WriteLine("||                                                    ||");
            Console.WriteLine("||  [S] Smelt      Ruby Ore   -> Ruby Ingot           ||");
            Console.WriteLine("||  [B] Breakdown  Ruby Ingot -> Ruby Ore             ||");
            Console.WriteLine("||                                                    ||");
            Console.WriteLine("========================================================");

            Console.WriteLine();

            // =========================
            //        INPUT MENU
            // =========================

            Console.Write("=> Choose Menu: ");
            bool menuOk = char.TryParse(
                Console.ReadLine(),
                out char menu
            );

            Console.Write("=> How much would you like: ");
            bool amountOk = double.TryParse(
                Console.ReadLine(),
                out double amount
            );

            Console.WriteLine();

            // =========================
            //      VALIDATE AMOUNT
            // =========================

            if (amountOk && amount > 0 && amount <= MaxBatch)
            {
                // =========================
                //          SMELT
                // =========================

                if (menuOk && (menu == 'S' || menu == 's'))
                {
                    double ingot = amount * SmeltRate;

                    Console.WriteLine("========================================================");
                    Console.WriteLine("||                  F O R G E   R E S U L T            ||");
                    Console.WriteLine("========================================================");
                    Console.WriteLine("||                                                    ||");
                    Console.WriteLine($"||  INPUT  : {amount:F2} {MaterialName} Ore");
                    Console.WriteLine($"||  OUTPUT : {ingot:F2} {MaterialName} Ingot");
                    Console.WriteLine("||                                                    ||");
                    Console.WriteLine("========================================================");
                }

                // =========================
                //        BREAKDOWN
                // =========================

                else if (menuOk && (menu == 'B' || menu == 'b'))
                {
                    double ore = amount / SalvageRate;

                    Console.WriteLine("========================================================");
                    Console.WriteLine("||                  F O R G E   R E S U L T            ||");
                    Console.WriteLine("========================================================");
                    Console.WriteLine("||                                                    ||");
                    Console.WriteLine($"||  INPUT  : {amount:F2} {MaterialName} Ingot");
                    Console.WriteLine($"||  OUTPUT : {ore:F2} {MaterialName} Ore");
                    Console.WriteLine("||                                                    ||");
                    Console.WriteLine("========================================================");
                }

                // =========================
                //        INVALID MENU
                // =========================

                else
                {
                    Console.WriteLine("========================================================");
                    Console.WriteLine("||                    ! E R R O R !                    ||");
                    Console.WriteLine("========================================================");
                    Console.WriteLine("||                                                    ||");
                    Console.WriteLine("||  Invalid menu. Please choose S or B.               ||");
                    Console.WriteLine("||                                                    ||");
                    Console.WriteLine("========================================================");
                }
            }

            // =========================
            //       INVALID AMOUNT
            // =========================

            else
            {
                Console.WriteLine("========================================================");
                Console.WriteLine("||                    ! E R R O R !                    ||");
                Console.WriteLine("========================================================");
                Console.WriteLine("||                                                    ||");
                Console.WriteLine("||  Invalid amount.                                   ||");
                Console.WriteLine("||  Enter a value from 0.01 to 500.                  ||");
                Console.WriteLine("||                                                    ||");
                Console.WriteLine("========================================================");
            }

            Console.ReadLine();
        }
    }
}
