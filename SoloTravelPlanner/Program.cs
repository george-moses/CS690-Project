using System;
using System.Collections.Generic;
using System.Linq;

// doing this all in one file for iteration 1 since modularity doesnt matter yet
namespace SoloTravelPlanner
{
    class Program
    {
        // global lists to hold our stuff in memory
        static List<string> destinations = new List<string>();
        static List<Expense> expenses = new List<Expense>();
        static decimal totalBudget = 2500.00m; // hardcoded budget for mvp

        static void Main(string[] args)
        {
            bool running = true;

            // main loop to keep the app alive until they exit
            while (running)
            {
                decimal totalSpent = expenses.Sum(e => e.Amount);
                decimal remaining = totalBudget - totalSpent;

                Console.Clear();
                Console.WriteLine("===================================================");
                Console.WriteLine("           SOLO TRAVEL PLANNER v1.0");
                Console.WriteLine("===================================================");
                Console.WriteLine($" Current Trip: European Backpacking");
                Console.WriteLine($" Total Budget: ${totalBudget:F2}");
                Console.WriteLine($" Remaining:    ${remaining:F2}");
                Console.WriteLine("===================================================\n");
                
                Console.WriteLine(" Main Menu:");
                Console.WriteLine(" 1. Manage Itinerary (Add Destination)");
                Console.WriteLine(" 2. Manage Expenses (Add Cost)");
                Console.WriteLine(" 3. View Financial Report");
                Console.WriteLine(" 4. Exit\n");
                
                Console.Write(" Select an option: ");
                string choice = Console.ReadLine();

                // switch statement for menu navigation
                switch (choice)
                {
                    case "1":
                        ManageItinerary();
                        break;
                    case "2":
                        ManageExpenses();
                        break;
                    case "3":
                        ViewReport();
                        break;
                    case "4":
                        running = false;
                        break;
                    default:
                        Console.WriteLine(" bad input. press enter to try again");
                        Console.ReadLine();
                        break;
                }
            }
        }

        // UC1
        static void ManageItinerary()
        {
            Console.Clear();
            Console.WriteLine("===================================================");
            Console.WriteLine("               ADD DESTINATION");
            Console.WriteLine("===================================================");
            
            Console.Write(" Enter City: ");
            string city = Console.ReadLine();
            
            Console.Write(" Enter Dates (e.g. Oct 1 - Oct 5): ");
            string dates = Console.ReadLine();

            destinations.Add($"{city} ({dates})");

            Console.WriteLine("\n [SUCCESS] Destination added!");
            Console.Write(" Press enter to go back... ");
            Console.ReadLine();
        }

        // UC2
        static void ManageExpenses()
        {
            Console.Clear();
            Console.WriteLine("===================================================");
            Console.WriteLine("               ADD A NEW EXPENSE");
            Console.WriteLine("===================================================");
            
            Console.Write(" Enter Expense Amount: $");
            
            // making sure they actually typed a number and not letters
            if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                Console.WriteLine(" invalid number. press enter to go back.");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("\n Select Category:");
            Console.WriteLine(" 1. Flight\n 2. Hotel\n 3. Food\n 4. Activity\n 5. Other");
            Console.Write(" Category Selection (1-5): ");
            string catChoice = Console.ReadLine();
            
            string category = catChoice switch
            {
                "1" => "Flight",
                "2" => "Hotel",
                "3" => "Food",
                "4" => "Activity",
                _ => "Other"
            };

            Console.Write("\n Enter Description: ");
            string desc = Console.ReadLine();

            expenses.Add(new Expense { Amount = amount, Category = category, Description = desc });

            decimal newRemaining = totalBudget - expenses.Sum(e => e.Amount);

            Console.WriteLine("\n [SUCCESS] Expense added!");
            Console.WriteLine($" Current Remaining Balance is now ${newRemaining:F2}.");
            Console.Write("\n Press enter to return to the Main Menu... ");
            Console.ReadLine();
        }

        // UC4
        static void ViewReport()
        {
            Console.Clear();
            decimal totalSpent = expenses.Sum(e => e.Amount);
            decimal remaining = totalBudget - totalSpent;

            Console.WriteLine("===================================================");
            Console.WriteLine("               FINANCIAL REPORT");
            Console.WriteLine("===================================================");
            Console.WriteLine($" Total Budget:       ${totalBudget:F2}");
            Console.WriteLine($" Total Spent:        ${totalSpent:F2}");
            Console.WriteLine(" --------------------------------------------------");
            Console.WriteLine($" REMAINING BALANCE:  ${remaining:F2}\n");

            Console.WriteLine(" Expenses by Category:");
            
            // grouping expenses by category so we can sum them up automatically
            var grouped = expenses.GroupBy(e => e.Category);
            foreach (var group in grouped)
            {
                Console.WriteLine($" - {group.Key}: ${group.Sum(e => e.Amount):F2}");
            }

            if (!expenses.Any())
            {
                Console.WriteLine(" no expenses yet.");
            }

            Console.Write("\n Press enter to return to the Main Menu... ");
            Console.ReadLine();
        }
    }

    // simple class to just hold the expense data
    class Expense
    {
        public decimal Amount { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
    }
}