/*Name: Sansayan Pratheepan
 * Title: Personal Budget Tracker Application
 * Date: 4/25/2024
 * purpose: Create a personal budget tracker, that asks the user for their income, and their expenses, then display their balance, along with what they entered for income and expenses, and give them an option to exit the program
 */

using System;

namespace PersonalBudgetTrackerSP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BudgetingApplication();
        }

        public static void BudgetingApplication() 
        {
            int[] income = new int[0]; //Creates two new arrays, and sets the size of them both equal to zero. 
            int[] expenses = new int[0]; //one array is for the income, and the other is for expenses. 

            while (true) //a semi-infinite loop that displays a menu where the user can enter and index, and do the corresponding task assigned to the index. 
            {
                Console.WriteLine("\n----------Menu----------");
                Console.WriteLine("1. Add Income");
                Console.WriteLine("2. Add Expenses");
                Console.WriteLine("3. Display Budget Summary/Balance");
                Console.WriteLine("4. Reset All Values");
                Console.WriteLine("5. Exit");
                string strChoice = Console.ReadLine();

                // Check if input is empty
                if (string.IsNullOrEmpty(strChoice)) //if their choice is empty the program gives an error message, then allows them to continue on with the program. 
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                int Choice;
                // Validate input to be a number
                if (!int.TryParse(strChoice, out Choice)) //if the user enters a character which isn't a number, and error message occurs, then lets them continue with the program. 
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                switch (Choice) //This checks what value the user entered if they entered one they go to the add income method
                    //if they entered 2, they go to the add expenses method
                    //if they entered 3, they go to the display summary method
                    //if they entered 4, they go to the reset values method
                    //if they entered 5, they exit the program
                    //if they enter any number other than 1,2,3,4,5 they get an error message. 
                {
                    case 1:
                        AddIncome(ref income);
                        break;
                    case 2:
                        AddExpenses(ref expenses);
                        break;
                    case 3:
                        DisplayBudgetSummary(income, expenses);
                        break;
                    case 4:
                        ResetValues(ref income, ref expenses);
                        break;
                    case 5:
                        Console.WriteLine("Thank You For Coming!");
                        return;
                    default:
                        Console.WriteLine("Invalid Choice, please try again");
                        break;
                }
            }
        }

        public static void AddIncome(ref int[] income) //method for adding the income
        {
            Console.Clear(); //clears the console so it becomes less cluttered. 
            Console.WriteLine("Enter Your Income for this month (In Dollars): "); //asks user to input their income 
            int intIncome;
            while (true)
            {
                string strIncome = Console.ReadLine();
                // Handle overflow case
                try
                {
                    intIncome = checked(int.Parse(strIncome));
                }
                catch (OverflowException)
                {
                    Console.WriteLine("The Number You Entered Is Too Big!! You need to reduce your value!.");
                    continue;
                }
                catch (FormatException) //if they enter something other than a number the error message below appears, then lets them continue with the program. 
                {
                    Console.WriteLine("Invalid choice. Please enter a valid number.");
                    continue;
                }

                if (intIncome < 0) //if they enter something under 0, an error message below appears, then lets them continue with the program.
                {
                    Console.WriteLine("Invalid choice. Please enter a positive number.");
                    continue;
                }
                break;
            }
            Array.Resize(ref income, income.Length + 1); //resizes the array so the user's inputs can be stored, and can keep adding other values on to the array
            income[income.Length - 1] = intIncome;
            Console.WriteLine("Income added successfully");
        }

        public static void AddExpenses(ref int[] expenses)
        {
            Console.Clear(); //clears the console to make it less clutered. 
            Console.WriteLine("How Many Expenses Do You Want To Add? You Can Enter -101 to exit this section");

            int intTotalExpenses;

            while (true)
            {
                string strTotalExpenses = Console.ReadLine();
                try
                {
                    intTotalExpenses = checked(int.Parse(strTotalExpenses));
                }
                catch (OverflowException)//if the number is too big the error message below appears, then lets them continue with the program.

                {
                    Console.WriteLine("You Number Is TOO BIG!!");
                    continue;
                }
                catch (FormatException) //if the user enters somethign other than a number the error message below appears, then lets them continue with the program.
                {
                    Console.WriteLine("Invalid choice. Please enter a valid number.");
                    continue;
                }

                if (intTotalExpenses <= 0 && intTotalExpenses != -101) //if the user enters a negative number the error message below appears, then lets them continue with the program.
                    //but the user can also enter -101 to exit the section of the program
                {
                    Console.WriteLine("Invalid choice. Please enter a positive number or -101 to stop.");
                    continue;
                }
                break;
            }

            if (intTotalExpenses == -101) // Check if sentinel value is entered
            {
                Console.WriteLine("Expense input stopped.");
                return;
            }

            int currentLength = expenses.Length; //resizes the array so the values the user inputs gets stored for future use, and allows the user to keep adding stuff to the array
            Array.Resize(ref expenses, expenses.Length + intTotalExpenses);
            for (int i = currentLength; i < expenses.Length; i++)
            {
                while (true)
                {
                    Console.Write($"Expense {i - currentLength + 1}: ");
                    string strExpense = Console.ReadLine();

                    if (strExpense == "-101") // Check for sentinel value to stop entering expenses
                    {
                        Console.WriteLine("Expense input stopped.");
                        Array.Resize(ref expenses, i); // Resize the expenses array to remove extra entries
                        return;
                    }

                    int intExpense;
                    try
                    {
                        intExpense = checked(int.Parse(strExpense));
                    }
                    catch (OverflowException)//if the number is too big the error message below appears, then lets them continue with the program.
                    {
                        Console.WriteLine("Your Number is TOO BIG!!!");
                        continue;
                    }
                    catch (FormatException)//if the user enters somethign other than a number the error message below appears, then lets them continue with the program.
                    {
                        Console.WriteLine("Invalid choice. Please enter a valid number.");
                        continue;
                    }

                    if (intExpense < 0)//if the user enters a negative number the error message below appears, then lets them continue with the program.
                    {
                        Console.WriteLine("Invalid choice. Please enter a positive number.");
                        continue;
                    }
                    expenses[i] = intExpense;
                    break;
                }
            }

            Console.WriteLine("Expenses added successfully!");
        }

        public static void DisplayBudgetSummary(int[] income, int[] expenses)
        {
            Console.Clear();
            Console.WriteLine("\n------ Budget Summary ------");

            int totalIncome = 0;
            for (int i = 0; i < income.Length; i++) //adds all the values in the income array to display for the user. 
            {
                totalIncome += income[i];
            }
            Console.WriteLine($"Total Income: {totalIncome}");

            int totalExpenses = 0;
            for (int i = 0; i < expenses.Length; i++)
            {
                totalExpenses += expenses[i]; //adds all the values in the expenses array to display for the user. 
            }
            Console.WriteLine($"Total Expenses: {totalExpenses}");

            int NetIncome = totalIncome - totalExpenses; //finds the balance by subtracting the total from both arrays ( income total - expenses total)
            if (NetIncome >= 0)
            {
                Console.WriteLine($"You Have: ${NetIncome} Dollars Left to Spend"); //if the leftover is positive display this message along with how much money they have left to spend. 
            }
            else if (NetIncome < 0)
            {
                Console.WriteLine($"You Are in Debt!, You Owe: ${Math.Abs(NetIncome)}"); //if the leftover is negative display this message along with how much money they need to pay back. 
            }
        }

        public static void ResetValues(ref int[] income, ref int[] expenses)
        {
            if (income.Length == 0 && expenses.Length == 0) //if the user tries to reset the values when the values in the array are already zero in both, the message below appears. 
            {
                Console.WriteLine("Arrays already have no values.");
            }
            else
            {
                income = new int[0]; //if not the user can reset the values, this code sets the values in both arrays equal to zero. 
                expenses = new int[0];
                Console.WriteLine("All values reset successfully!");
            }
        }
    }
}