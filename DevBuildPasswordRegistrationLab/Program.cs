using System;
using System.Collections.Generic;

namespace DevBuildPasswordRegistrationLab
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize two lists: userNames passwords
            List<string> usernames = new List<string>();
            List<string> passwords = new List<string>();
            usernames.Add("admin");
            passwords.Add("admin");
            //initialize the Regular expressions I want to try

            //Create a while loop to keep asking if they want to try again
            bool keepGoing = true;

            while (keepGoing)
            {
                // Call method that asks user to choose if they want to create account or log in
                Menu(ref usernames, ref passwords);

                keepGoing = NewUser();
            }
            

            Console.WriteLine("Hello World!");
        }

        public static void Menu(ref List<string> usernames, ref List<string> passwords)
        {
            string userAnswer = GetUserInput("Would you like to (1) Create a new user (2) Log in:  ");

            switch (userAnswer.Trim())
            {
                case "1":
                    //Call method for user creation
                    CreateUser(ref usernames, ref passwords);
                    break;
                case "2":
                    //Call Method for checking user exists
                    UserExists(ref usernames, ref passwords);
                    break;
                default:
                    Console.WriteLine("Invalid choise. Please try again.");
                    Menu(ref usernames, ref passwords);
                    break;
            }
        }

        public static void CreateUser(ref List<string> user, ref List<string> pass)
        {

            // disply rules for Username Creation.
            // Ask them to enter a username
            string desiredUser = GetUserInput("Please enter a username that fulfills the following:" +
                "\n-Must have letters and numbers" +
                "\n-Must have at least 5 letters" +
                "\n-Must be a length of 7 character minimum" +
                "\n-Must be no longer than 12 characters" +
                "\n-Must not contain IronMan, DevBuild, or your password" +
                "\nUsername: ");

            // Display password creation rules
            // Ask to enter a password
            string desiredPass = GetUserInput("Please enter a password that fulfills the following:" +
                "\n-At least one lowercase letter" +
                "\n-At least one uppercase letter" +
                "\n-At least one number" +
                "\n-At least minimum 7 characters" +
                "\n-A maximum of 12 characters" +
                "\n"+ @"Any of the following special characters: ! @ # $ % ^ & *" +
                "\nPassword: ");

            // Check if password is valid.
            // Check user name is valid and available.
            //If username and passowrd is valid, enter into username and password list
            if ( ValidUser(user, desiredUser, desiredPass) 
                && UserAvailable(user, desiredUser)
                && ValidPassword(desiredPass))
            {
                user.Add(desiredUser);
                pass.Add(desiredPass);
                Console.WriteLine("You have succesfully registered!");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine();
                CreateUser(ref user, ref pass);
            }

        }

        public static bool ValidPassword(string desiredPass)
        {
            //check to make sure the password meets these criteria:
            //  At least one lowercase letter
            //  At least one uppercase letter
            //  At least one number
            //  At least minimum 7 characters(inclusive)-----
            //  At least maximum 12 characters(inclusive)------
            //  Any of the following special characters: ! @ # $ % ^ & * -----

            char[] symbols = { '!', '@', '#', '$', '%', '^', '&', '*' };
            bool hasSymbol = false;
            bool hasLowerLetter = false;
            bool hasUpperLetter = false;
            bool hasNumber = false;

            foreach (char symbol in symbols)
            {
                if (desiredPass.Contains(symbol))
                {
                    hasSymbol = true;
                    break;
                }
            }

            foreach(char letter in desiredPass)
            {
                if (char.IsUpper(letter)) { hasUpperLetter = true; }
                if (char.IsLower(letter)) { hasLowerLetter = true; }
                if (char.IsNumber(letter)) { hasNumber = true; }
            }

            Console.WriteLine("Upper "+ hasUpperLetter);
            Console.WriteLine("Upper " + hasLowerLetter);
            Console.WriteLine("Upper " + hasNumber);

            if (desiredPass.Length < 7)
            {
                Console.WriteLine("Your password must be at least 7 characters");
                return false;

            }
            else if (desiredPass.Length > 12)
            {
                Console.WriteLine("Your password must not be longer than 12 characters");
                return false;
            }
            else if (!hasSymbol)
            {
                Console.WriteLine("Your password must contain one of the following: ! @ # $ % ^ & * ");
                return false;
            }
            else if (!hasLowerLetter)
            {
                Console.WriteLine("Your password must contain a lowercase letter");
                return false;
            }
            else if (!hasUpperLetter)
            {
                Console.WriteLine("Your password must contain an uppercase letter");
                return false;
            }
            else if (!hasNumber)
            {
                Console.WriteLine("Your password must contain a number");
                return false;
            }




            return true;
        }

        public static bool ValidUser(List<string> usernames, string desiredUser, string desiredPass)
        {
            string[] forbidden = { "IronMan", "DevBuild", $"{desiredPass}" };
            int letterCount = 0;

            foreach(char letter in desiredUser)
            {
                if (char.IsLetter(letter))
                {
                    letterCount++;
                }
            }

            if (desiredUser.Length < 7)
            {
                Console.WriteLine("Your Username must be at least 7 characters");
                return false;

            }
            else  if(desiredUser.Length > 12)
            {
                Console.WriteLine("Your Username must not be longer than 12 characters");
                return false;
            }else if (letterCount < 5)
            {
                Console.WriteLine("Your username must contain at least 5 letters");
                return false;
            }


            foreach(string forbid in forbidden)
            {
                if (desiredUser.Contains(forbid))
                {
                    Console.WriteLine("Your Username contains a forbidden word. Please try again");
                    return false;
                }
            }

            return true;
        }

        public static bool UserAvailable(List<string> usernames, string desiredUser)
        {
            foreach(string user in usernames)
            {
                if (user == desiredUser)
                {
                    return false;
                }
            }

            return true;
        }

        public static void UserExists(ref List<string> usernames, ref List<string> passwords) //TODO
        {
            
            string userAttempt = GetUserInput("Username: ");
            string passAttempt = GetUserInput("Password: ");

            if (usernames.IndexOf(userAttempt) != -1)
            {
                if (passAttempt == passwords[usernames.IndexOf(userAttempt)] )
                {
                    Console.WriteLine("You have succesfully logged in!");
                }
                else
                {
                    Console.WriteLine("Your password is wrong");
                }
            }
            else
            {
                Console.WriteLine("The username you entered is invalid. please try again");
            }
            

        }

        public static string GetUserInput(string prompt)
        {
            Console.Write(prompt);
            string output = Console.ReadLine().Trim();


            return output;
        }

        public static bool NewUser()
        {
            string answer = GetUserInput("Would you like to try a new username and password? (y/n): ");

            if (answer.ToLower() == "y")
            {
                return true;
            }
            else if (answer.ToLower() == "n")
            {
                Console.WriteLine("Goodbye!");
                return false;
            }
            else
            {
                Console.WriteLine("Please enter either Y or N to continue.");
                return NewUser();
            }

        }
    }
}
