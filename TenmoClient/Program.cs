using System;
using System.Collections.Generic;
using TenmoClient.Data;

namespace TenmoClient
{
    class Program
    {
        private static readonly ConsoleService consoleService = new ConsoleService();
        private static readonly AuthService authService = new AuthService();
        private static readonly AccountService aService = new AccountService();

        static void Main(string[] args)
        {
            Run();
        }
        private static void Run()
        {
            int loginRegister = -1;
            while (loginRegister != 1 && loginRegister != 2)
            {
                Console.WriteLine("Welcome to TEnmo!");
                Console.WriteLine("1: Login");
                Console.WriteLine("2: Register");
                Console.Write("Please choose an option: ");

                if (!int.TryParse(Console.ReadLine(), out loginRegister))
                {
                    Console.WriteLine("Invalid input. Please enter only a number.");
                }
                else if (loginRegister == 1)
                {
                    while (!UserService.IsLoggedIn()) //will keep looping until user is logged in
                    {
                        LoginUser loginUser = consoleService.PromptForLogin();
                        API_User user = authService.Login(loginUser);
                        if (user != null)
                        {
                            UserService.SetLogin(user);
                        }
                    }
                }
                else if (loginRegister == 2)
                {
                    bool isRegistered = false;
                    while (!isRegistered) //will keep looping until user is registered
                    {
                        LoginUser registerUser = consoleService.PromptForLogin();
                        isRegistered = authService.Register(registerUser);
                        if (isRegistered)
                        {
                            Console.WriteLine("");
                            Console.WriteLine("Registration successful. You can now log in.");
                            loginRegister = -1; //reset outer loop to allow choice for login
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Invalid selection.");
                }
            }

            MenuSelection();
        }

        private static void MenuSelection()
        {
            int menuSelection = -1;
            while (menuSelection != 0)
            {
                Console.WriteLine("");
                Console.WriteLine("Welcome to TEnmo! Please make a selection: ");
                Console.WriteLine("1: View your current balance");
                Console.WriteLine("2: View your past transfers");
                Console.WriteLine("3: View your pending requests");
                Console.WriteLine("4: Send TE bucks");
                Console.WriteLine("5: Request TE bucks");
                Console.WriteLine("6: Log in as different user");
                Console.WriteLine("0: Exit");
                Console.WriteLine("---------");
                Console.Write("Please choose an option: ");

                if (!int.TryParse(Console.ReadLine(), out menuSelection))
                {
                    Console.WriteLine("Invalid input. Please enter only a number.");
                }
                else if (menuSelection == 1)
                {
                    decimal balance = aService.GetCurrentBalance();
                    Console.WriteLine($"User balance is {balance}.");
                }
                else if (menuSelection == 2)
                {

                }
                else if (menuSelection == 3)
                {

                }
                else if (menuSelection == 4)
                {

                    List<User> users = UserService.GetUsers();
                    Dictionary<int, string> idKeyUserValue = new Dictionary<int, string>();
                    decimal balance = aService.GetCurrentBalance();
                    Console.WriteLine($"The max amount you can send is ${balance}.");
                    foreach (User user in users)
                    {
                        idKeyUserValue.Add(user.UserId, user.Username);
                        Console.WriteLine($"{user.Username}, ID {user.UserId}");
                    }
                    int id = -1;
                    while (!idKeyUserValue.ContainsKey(id))
                    {
                        Console.WriteLine("Enter Id that you want to send money to or enter 0 to exit: ");
                        id = Convert.ToInt32(Console.ReadLine());
                        if (id == 0)
                        {
                            break;
                        }
                    }
                    if (id == 0)
                    {
                        break;
                    }
                    decimal amountToSend = -1;

                    while (amountToSend > balance || amountToSend < 0)
                    {
                        Console.WriteLine($"Please enter the amount you wish to send to {idKeyUserValue[id]}: ");
                        amountToSend = Convert.ToDecimal(Console.ReadLine());
                        if (amountToSend > balance)
                        {
                            Console.WriteLine($"Please enter an amount less than or equal to your balance of ${balance}");
                            amountToSend = -1;
                        }
                    }
                    
                    
                }
                else if (menuSelection == 5)
                {

                }
                else if (menuSelection == 6)
                {
                    Console.WriteLine("");
                    UserService.SetLogin(new API_User()); //wipe out previous login info
                    Run(); //return to entry point
                }
                else
                {
                    Console.WriteLine("Goodbye!");
                    Environment.Exit(0);
                }
            }
        }
    }
}
