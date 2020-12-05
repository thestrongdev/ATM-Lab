using System;
using System.Collections.Generic;
using System.Text;

namespace ATMLabGoodson
{
    class ATM
    {
        public static List<Account> allAccounts = new List<Account>();
        public static Account inUse = null;
        public static int inUseIndex;
        public static Account tempAccount = new Account();
        public static void RegisterOrLogin()
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("GRAND CIRCUS ATM");
            Console.WriteLine("----------------");
            Console.WriteLine();

            do
            {
                Console.WriteLine("Would you like to log in (L) or create a new account (N)?");
                string userChoice = Console.ReadLine();

                if (userChoice.Equals("L", StringComparison.OrdinalIgnoreCase))
                {
                    LogIn();
                    
                }
                else if (userChoice.Equals("N", StringComparison.OrdinalIgnoreCase))
                {
                    Register();
                    
                }
                else
                {
                    Console.WriteLine("Invalid entry. Please enter L or N");
                }

            } while (true);
            
        }
            
            public static void Register()
            {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("\nACCOUNT REGISTRATION");
            Console.WriteLine("--------------------");
            Console.WriteLine();
            Console.WriteLine("Please enter a username for your new account");
            string userName = ReadLogin();
            Console.WriteLine();
            Console.WriteLine("Please enter a password for your new account");
            string password = ReadLogin();

            //TEST READ LOG IN 
            //Console.WriteLine(userName);
            //Console.WriteLine(password);

            Account account = new Account(userName, password);
            allAccounts.Add(account);

            Console.WriteLine("\nYour account has been registered!");
            Console.WriteLine("Please log in with your new user information");
            Console.WriteLine();

            LogIn();

            }

        public static void LogIn()
        {
            bool loggedIn = false;
            bool loop = true;
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("ACCOUNT LOG IN");
            Console.WriteLine("--------------");
            Console.WriteLine();

            do
            {
                Console.WriteLine("Please enter your username: ");
                string userName = ReadLogin();
                Console.WriteLine("Please enter your password: ");
                string password = ReadLogin();


                tempAccount = new Account(userName, password); //only making new instance for getAccount method

                //check if user logged in...
                if (inUse != null)
                {
                    Console.WriteLine("A user is already logged in. Please log out to access your account");
                    ShowMenu();
                }
                else
                {
                   

                    try
                    {
                        inUseIndex = tempAccount.GetAccount(userName, password, allAccounts);
                        inUse = allAccounts[inUseIndex];
                        AccountAction(inUse);
                        loggedIn = true;
                        break;

                    }
                    catch (ArgumentOutOfRangeException ex)
                    {
                        Console.WriteLine("It looks like you don't have an account with us yet");
                        loop = NoAccount();
                        continue;
                    }
                }
              

                //if (inUse != null) //we will call below methods (check balance, deposit, withdraw) here...
                //{
                //    AccountAction(inUse, userName);
                //    loggedIn = true;
                //    break;
                //}
                //else if (inUse == null)
                //{

                //}
                //else//check if user already logged
                //{
                //    if (loggedIn == true)
                //    {
                //        Console.WriteLine("A user is already logged in. Please log out to access your account");
                //        ShowMenu(userName);
                //        break;
                //    }
                //}

            } while (loop);

        }

        //these should only be called if there's a logged in account
        public static bool Logout() //after log out, ask user if they want to register new acct or log in
        {
            bool loggedIn = false;
            inUse = null;
            tempAccount = null;
            
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.WriteLine("You have been logged out of your account");
            Console.WriteLine();
            return loggedIn;
        }

        public static void CheckBalance()
        {
            Console.WriteLine($"Your balance is ${inUse.Balance}");
        }

        public static void Deposit()
        {
            Console.WriteLine("How much would you like to deposit?");

            do
            {
                string userDeposit = Console.ReadLine();
                bool checkDub = double.TryParse(userDeposit, out double addToBal);

                if (checkDub)
                {
                    inUse.Balance += addToBal;
                    Console.WriteLine($"Your new balance is ${inUse.Balance}");
                    break;
                }
                else
                {
                    Console.WriteLine("Please enter a number");
                }

            } while (true); 
        }

        public static void Withdraw()
        {
            Console.WriteLine("How much would you like to withdraw?");
            do
            {
                string userWithdrawal = Console.ReadLine();
                bool checkDub = double.TryParse(userWithdrawal, out double subBal);

                if (checkDub)
                {
                    if (subBal <= inUse.Balance)
                    {
                        allAccounts[inUseIndex].Balance -= subBal;
                        Console.WriteLine($"Your new balance is ${inUse.Balance}");
                    }
                    else
                    {
                        Console.WriteLine($"You only have ${inUse.Balance} in your account. You cannot withdraw ${subBal}");
                    }
                    break;
                }
                else
                {
                    Console.WriteLine("Please enter a number");
                }

            } while (true);
        }


        public static bool NoAccount()
        {
            Console.WriteLine("\nPlease re-enter your information (E) or register (R): ");
            string choice = Console.ReadLine();

            if (choice.Equals("E", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            else if (choice.Equals("R", StringComparison.OrdinalIgnoreCase))
            {
                Register();
                return false;
            }
            else
            {
                Console.WriteLine("Please enter E or R: ");
                return true;
            }
        }

        public static int ShowMenu()
        {
            Dictionary<int, string> atmMenu = new Dictionary<int, string>
            {
                {0, "Check Balance"},
                {1, "Deposit"},
                {2, "Withdraw"},
                {3, "Logout"}

            };

            do
            {
                Console.WriteLine($"Please select an action by number from the menu below: ");
                Console.WriteLine();
                foreach (var keyValuePair in atmMenu)
                {
                    Console.WriteLine($"[{keyValuePair.Key}] {keyValuePair.Value}");
                }

                Console.WriteLine();
                string userChoice = Console.ReadLine();
                bool checkValue = int.TryParse(userChoice, out int menuChoice);
                //validate this and return int

                if (checkValue && menuChoice<4 && menuChoice>=0)
                {
                    return menuChoice;
                }
                else
                {
                    Console.WriteLine("Please enter valid whole number");
                }

            } while (true);
        }

        public static bool UserActions(int menuChoice, Account account)
        {
            if(menuChoice == 0)
            {
                CheckBalance();
                return true;

            } else if (menuChoice == 1)
            {
                Deposit();
                return true;

            } else if (menuChoice == 2)
            {
                Withdraw();
                return true;
            }
            else
            {
                Logout();
                return false;
            }
        }

        public static void AccountAction(Account account)
        {
            bool keepGoing = true;

            do
            {
                int userChoice = ShowMenu();
                keepGoing = UserActions(userChoice, account);

            } while (keepGoing);
        }
    
        public static string ReadLogin() //to hide user input as they're typing
        {
            string password = "";
            ConsoleKeyInfo info = Console.ReadKey(true);
            while (info.Key != ConsoleKey.Enter)
            {
                if (info.Key != ConsoleKey.Backspace)
                {
                    Console.Write("*");
                    password += info.KeyChar;
                }
                else if (info.Key == ConsoleKey.Backspace)
                {
                    if (!string.IsNullOrEmpty(password))
                    {
                        // remove one character from the list of password characters
                        password = password.Substring(0, password.Length - 1);
                        // get the location of the cursor
                        int pos = Console.CursorLeft;
                        // move the cursor to the left by one character
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                        // replace it with space
                        Console.Write(" ");
                        // move the cursor to the left by one character again
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                    }
                }
                info = Console.ReadKey(true);
            }
            // add a new line because user pressed enter at the end of their password
            Console.WriteLine();
            return password;
        }



    }
}
