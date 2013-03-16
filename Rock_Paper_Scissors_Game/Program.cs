/*
	Program Purpose: This is a console Rock, Paper, Scissors game
*
* 
*  ROCK PAPER SCISSORS 
*  
* 
    BY Rafael Mathieu
*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Principal;

namespace Rock_Paper_Scissors_Game
{
    class Program
    {
        static ConsoleKeyInfo cki;

        // Declaring globally available variables
        protected static int origRow = 0;
        protected static int origCol = 0;
        static int intSelection = 0, intRandomNumber, intCounter = 0, intStatPage = 1;
        static string strChoice = null, strName, strCPUSelection = null, strUserSelection = null, msg;
        static double dblmsg, dblGamePlayed, dblUsed;

        // MENU
        // Assign the items of the main menu to an array
        static string[] strTopMenuItems = {"Play","Stats","Options","Exit" };
        static string[] strPlaySubMenu = {"Single", "Multiplayer", "Back" };
        static string[] strPlayAgainMenu = { "Play Again", "Back" };
        static string[] strStatsSubMenu = { "General", "User", "Computer","Back" };
        static int lcol, lrow = 8, intMenuSelection, intMenuCounter, numItems, intX;

        static int intUserScore = 0, intCPUScore = 0;
        // Will be used to check the width and height of the console
        static int intAppWidth, intAppHeight;

        // The Main Method
        static void Main()
        {
            // Prevent example from ending if CTL+C is pressed.
            Console.TreatControlCAsInput = true;
            Console.ResetColor();
            

            // Set the window Size to the following dimensions
            Console.SetWindowSize(72, 27);
            // Set the buffer size to help remove the side arrows
            Console.BufferHeight = 27;
            Console.BufferWidth = 72;

            // the lenght of menu to windowwidth - 3
            lcol = Console.WindowWidth - 4;

            // Set Cursor to invisible
            Console.CursorVisible = false;
            // Assign the width and the height of the app to the following variables
            intAppWidth = Console.WindowWidth;
            intAppHeight = Console.WindowHeight;

            // Calls the header function
            Header();
                      
            // Launch the menu drawing
            Menu(strTopMenuItems);           
           
        }

        // Main Menu
        static void Menu(string [] strSelectedMenu)
        {
            // draw the menu and assign selection
            intMenuSelection = MenuChooseItem(strSelectedMenu, 2, 7, ConsoleColor.Black, ConsoleColor.White);

            // reset color
            Console.ResetColor();
            Console.Clear();
            // This will determine the menu selection
            switch (intMenuSelection)
            {
                case 1:
                    Game();
                    break;
                case 2:
                    Stats();
                    break;
                case 3:
                    Options();
                    break;
                case 4:
                    // Makes the program exit
                    Environment.Exit(1);
                    break;
                case 5:
                    Main();
                    break;
                case 6:
                    Stats();
                    break;
            }       
        }

        // This method allows the input of text anywhere on the console (as seen on the msdn site)
        protected static void WriteAt(string s, int x, int y)
        {
            try
            {
                Console.SetCursorPosition(origCol + x, origRow + y);
                Console.Write(s);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }

        // This method takes care of drawing the hand gestures
        private static void DrawingHand(string strCount)
        {
            int intCount;

            // if intSelection == 1, then the CPU is making the choice
            if (intSelection == 1)
                // If the CPU is selecting, the x axis coordinate is set a 5/8th of the width
                intCount = ((intAppWidth / 8)*5);
            // if not the User gets the nod
            else
                // If the user is selecting, the x axis coordinate is set a 1/8th of the width
                intCount = intAppWidth / 8;
            
            // The switch statement will go through the "strCount" and determine if it is one of the options.
            switch (strCount)
            {
                // if the rock is selected, then the following will be drawn at the assigned location (based on if this is the user or CPU choice).
                case "Rock":
                    WriteAt("    _______  ", intCount, 12);
                    WriteAt("---'   ____) ", intCount, 13);
                    WriteAt("      (_____)", intCount, 14);
                    WriteAt(" ROCK (_____)", intCount, 15);
                    WriteAt("      (____) ", intCount, 16);
                    WriteAt("---.__(___)  ", intCount, 17);
                    break;

                // if paper is selected, then the following will be drawn.
                case "Paper":
                    WriteAt("    _______       ", intCount, 12);
                    WriteAt("---'   ____)____  ", intCount, 13);
                    WriteAt("          ______) ", intCount, 14);
                    WriteAt("  PAPER   _______)", intCount, 15);
                    WriteAt("         _______) ", intCount, 16);
                    WriteAt("---.__________)   ", intCount, 17);
                    break;

                // if a Scissors is selected, then the following will be drawn.
                case "Scissors":
                    WriteAt("    _______       ", intCount, 12);
                    WriteAt("---'   ____)____  ", intCount, 13);
                    WriteAt(" SCISSOR  ______) ", intCount, 14);
                    WriteAt("       __________)", intCount, 15);
                    WriteAt("      (____)      ", intCount, 16);
                    WriteAt("---.__(___)       ", intCount, 17);
                    break;
            }
        }

        // This method help updating the scores in the console
        private static void Scores()
        {
            // This will use the WriteAt method to write the scoreboard at the location assigned
            // It will put the first score at 1/4th of the width, and the 2nd at 3/4th
            // the first number as a -1 because it doesn't align perfectly
            WriteAt(Convert.ToString(intUserScore), (intAppWidth/4)-1, 4);
            WriteAt(Convert.ToString(intCPUScore), (intAppWidth / 4)*3, 4);
        }

        // This is the main game logic
        private static void Game()
        {
            Console.Clear();
            Header();

            String strLastCharString;
            bool boolRightKey = false;

            msg = "Select your element:";
            Console.WriteLine("{0," + ((Console.WindowWidth / 2) + msg.Length / 2) + "}", msg);
            Console.Write("\n");
            msg = "Rock = r | Paper = p | Scissors = s";
            Console.WriteLine("{0," + ((Console.WindowWidth / 2) + msg.Length / 2) + "}", msg);

            // This will run as long as boolRightKey == false
            do
            {
                // This will check if the r, p or s keys were pressed
                switch (Console.ReadKey(true).Key)
                {
                    // if the r key is pressed Then rock will be the user selection
                    case ConsoleKey.R:
                        // Game logic
                        strChoice = "r";
                        // User Choice Statistics
                        Properties.Settings.Default.UserRock += 1;
                        // Drawing Logic
                        strUserSelection = "Rock";
                        boolRightKey = true;
                        break;
                    // if the p key is pressed Then paper will be the user selection
                    case ConsoleKey.P:
                        // Game logic
                        strChoice = "p";
                        // User Choice Statistics
                        Properties.Settings.Default.UserPaper += 1;
                        // Drawing Logic
                        strUserSelection = "Paper";
                        boolRightKey = true;
                        break;
                    // if the s key is pressed Then scissors will be the user selection
                    case ConsoleKey.S:
                        // Game logic
                        strChoice = "s";
                        // User Choice Statistics
                        Properties.Settings.Default.UserScissors += 1;
                        // Drawing Logic
                        strUserSelection = "Scissors";
                        boolRightKey = true;
                        break;
                }
            } while (boolRightKey == false);




            // Create a random number
            Random ranNumber = new Random();

            // the random number will be between 0 and 1000 (for better random generation)
            intRandomNumber = ranNumber.Next(0, 1000);
            // transform "intRandomNumber" to string
            strLastCharString = Convert.ToString(intRandomNumber);
            // Get the last charachter of the string
            strLastCharString = strLastCharString.Substring(strLastCharString.Length - 1, 1);
            // Convert it back to integer
            intRandomNumber = Convert.ToInt16(strLastCharString);

            // if the number is 0, 3 or 6 then its a Rock
            if ((intRandomNumber == 0) || (intRandomNumber == 3) || (intRandomNumber == 6))
                intRandomNumber = 1;
            // if the number is 0, 3 or 6 then its a Scissors
            else if ((intRandomNumber == 1) || (intRandomNumber == 4) || (intRandomNumber == 7))
                intRandomNumber = 3;
            // Because of 9 would be the odd number out, it is assign randomly to either paper, orck or scissors
            else if (intRandomNumber == 9)
                intRandomNumber = ranNumber.Next(1, 3);
            // Else it is Paper
            else
                intRandomNumber = 2;

            // This switch determines the winner of the game. 0 = Tied, 1 = Win, 2 = Lose
            switch (intRandomNumber)
            {
                // Sets the CPU selection to Rock
                case 1:
                    strCPUSelection = "Rock";
                    Properties.Settings.Default.CPURock += 1;
                    // Compare the CPU to User and determine the winner
                    switch (strChoice)
                    {
                        // Tie
                        case "r":
                            intCounter = 0;
                            break;
                        // Win
                        case "p":
                            intCounter = 1;
                            break;
                        // Lose
                        case "s":
                            intCounter = 2;
                            break;
                    }
                    break;
                // Sets the CPU selection to Paper
                case 2:
                    strCPUSelection = "Paper";
                    Properties.Settings.Default.CPUPaper += 1;
                    // Compare the CPU to User and determine the winner
                    switch (strChoice)
                    {
                        // Lose
                        case "r":
                            intCounter = 2;
                            break;
                        // Tie
                        case "p":
                            intCounter = 0;
                            break;
                        // Win
                        case "s":
                            intCounter = 1;
                            break;
                    }
                    break;
                // Sets the CPU selection to Scissors
                case 3:
                    strCPUSelection = "Scissors";
                    Properties.Settings.Default.CPUScissors += 1;
                    // Compare the CPU to User and determine the winner
                    switch (strChoice)
                    {
                        // Win
                        case "r":
                            intCounter = 1;
                            break;
                        // Lose
                        case "p":
                            intCounter = 2;
                            break;
                        // Tie
                        case "s":
                            intCounter = 0;
                            break;
                    }
                    break;
            }

            Console.Clear();

            Header();

            // Set the text color to white
            Console.ForegroundColor = ConsoleColor.White;
            // Write the username at 3/16th of the width
            WriteAt(strName, (intAppWidth / 16) * 3, 14);
            // Write the CPU name at the 3/4th of the width
            WriteAt("CPU", (intAppWidth / 16) * 13, 14);
            // Set the text color back to gray
            Console.ForegroundColor = ConsoleColor.Gray;

            // This will draw the user and CPU selection and set the "intSelection" to 1
            DrawingHand(strUserSelection);
            intSelection = 1;
            DrawingHand(strCPUSelection);

            // Set the cursor on line 19
            Console.SetCursorPosition(0, 19);

            // Based on the "intCounter" will select the appropriate text
            switch (intCounter)
            {
                //Tied
                case 0:
                    msg = "You Tied";
                    Properties.Settings.Default.GameTied += 1;  
                    Console.WriteLine("{0," + ((Console.WindowWidth / 2) + msg.Length / 2) + "}", msg);
                    break;
                // Won
                case 1:
                    //Set the text color to green
                    Console.ForegroundColor = ConsoleColor.Green;
                    msg = "You Won";
                    Properties.Settings.Default.GameWon += 1;
                    Console.WriteLine("{0," + ((Console.WindowWidth / 2) + msg.Length / 2) + "}", msg);
                    // Will add one point to the CPU
                    intUserScore += 1;
                    break;
                // Lost
                case 2:
                    // Set the text color to red
                    Console.ForegroundColor = ConsoleColor.Red;
                    msg = "You Lost";
                    Properties.Settings.Default.GameLost += 1;
                    Console.WriteLine("{0," + ((Console.WindowWidth / 2) + msg.Length / 2) + "}", msg);
                    // Will add one point to the CPU
                    intCPUScore += 1;
                    break;
            }
            // setback the text color to gray
            Console.ForegroundColor = ConsoleColor.Gray;
            // This will call the dash function that will create a line on the screen
            Dash();
            // Updates the scoreboard
            Scores();
            // Set the cursor position to the following location
            Console.SetCursorPosition(0, 23);

            // This will add one game played to the statistics
            Properties.Settings.Default.GamePlayed += 1;
            Properties.Settings.Default.Save();

            // Ask the user if he wants to play again
            Console.Write("Press ENTER to play again.\n\nPress ESCAPE to go to the main menu.");
            int intKeyCount = 0;

            Menu(strPlayAgainMenu);

            // Check for key pressed
            do
            {
                switch (Console.ReadKey(true).Key)
                {
                    // if the escape key is go back to the main menu
                    case ConsoleKey.Escape:
                        // Makes the program go back to the main menu
                        Console.Clear();
                        Reset();
                        Main();
                        break;
                    // if the Enter key is pressed, the while loop will end and another game will start
                    case ConsoleKey.Enter:
                        intKeyCount = 1;
                        Reset();
                        Console.Clear();
                        Game();
                        break;
                }
            } while (intKeyCount == 0);

        }

        // Shows the statistics of all the games played
        private static void Stats()
        {
            Reset();
            Header();
               
            msg = "Statistics";
            // Centers the text to the middle of the console
            Console.WriteLine("{0," + ((Console.WindowWidth / 2) + msg.Length / 2) + "}", msg);

            dblGamePlayed = Properties.Settings.Default.GamePlayed;

            Console.WriteLine("\n");

            Console.SetCursorPosition(0, 10);

            // This will help select the right stat page
            switch (intStatPage)
            {
                // Page 1
                case 1:
                    msg = "------ General ------\n";
                    Console.WriteLine("{0," + ((Console.WindowWidth / 2) + msg.Length / 2) + "}\n", msg);
                    Console.WriteLine("  Game played: {0}\n", Properties.Settings.Default.GamePlayed);
                    Console.WriteLine("  Game won: {0}\n", Properties.Settings.Default.GameWon);
                    Console.WriteLine("  Game lost: {0}\n", Properties.Settings.Default.GameLost);
                    Console.WriteLine("  Game tied: {0}", Properties.Settings.Default.GameTied);
                    break;
                // Page 2
                case 2:
                    msg = "------ User ------\n";
                    Console.WriteLine("{0," + ((Console.WindowWidth / 2) + msg.Length / 2) + "}\n", msg);
                    dblUsed = Properties.Settings.Default.UserRock;
                    dblmsg = (dblUsed / dblGamePlayed);
                    Console.WriteLine("  Percentage of Rock: {0:P1}", dblmsg);
                    dblUsed = Properties.Settings.Default.UserPaper;
                    dblmsg = (dblUsed / dblGamePlayed);
                    Console.WriteLine("\n  Percentage of Paper: {0:P1}", dblmsg);
                    dblUsed = Properties.Settings.Default.UserScissors;
                    dblmsg = (dblUsed / dblGamePlayed);
                    Console.WriteLine("\n  Percentage of Scissors: {0:P1}", dblmsg);
                    break;
                // Page 3
                case 3:
                    msg = "------ Computer ------\n";
                    Console.WriteLine("{0," + ((Console.WindowWidth / 2) + msg.Length / 2) + "}\n", msg);
                    dblUsed = Properties.Settings.Default.CPURock;
                    dblmsg = (dblUsed / dblGamePlayed);
                    Console.WriteLine("  Percentage of Rock: {0:P1}", dblmsg);
                    dblUsed = Properties.Settings.Default.CPUPaper;
                    dblmsg = (dblUsed / dblGamePlayed);
                    Console.WriteLine("\n  Percentage of Paper: {0:P1}", dblmsg);
                    dblUsed = Properties.Settings.Default.CPUScissors;
                    dblmsg = (dblUsed / dblGamePlayed);
                    Console.WriteLine("\n  Percentage of Scissors: {0:P1}", dblmsg);
                    break;
            }

            Menu(strStatsSubMenu);
        }

        // This will draw the header
        private static void Header()
        {
            msg = "The Game of Rock, Paper, Scissors";
            // Intro Text
            Console.WriteLine("");
            Console.WriteLine("{0," + ((Console.WindowWidth / 2) + msg.Length / 2) + "}", msg);

            // This assigns the username of the current computer to "strName". 
            // It gets the username and computer name and split it to only leave the username. 
            strName = WindowsIdentity.GetCurrent().Name.Split('\\')[1];

            // This will call the dash function that will create a line on the screen
            Dash();
            msg = strName+" Vs. CPU";
            Console.WriteLine("{0," + ((Console.WindowWidth / 2) + msg.Length / 2) + "}", msg);
            // Put the scoreboard on the console
            Scores();
            // This will call the dash function that will create a line on the screen
            Dash();
        }

        // Reset all the Stats to zero
        private static void ResetStats()
        {
            int intKeyPressed = 0;
            Console.Clear();
            Console.SetCursorPosition(0, intAppHeight / 2);
            Console.ForegroundColor = ConsoleColor.Red;
            msg = "Are you sure you want to Reset all the statistics ?";
            Console.WriteLine("{0," + ((Console.WindowWidth / 2) + msg.Length / 2) + "}", msg);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(0, (intAppHeight / 2)+5);
            msg = "Press ENTER to reset, ESCAPE to cancel";
            Console.WriteLine("{0," + ((Console.WindowWidth / 2) + msg.Length / 2) + "}", msg);

            // This will check for the key input
            do
            {
                // This will check if the escape key, right or left arrow keys was pressed
                switch (Console.ReadKey(true).Key)
                {
                    // if the escape key is pressed intKey will be set to 1
                    case ConsoleKey.Escape:
                        intKeyPressed = 1;
                        break;
                    // if the R key is pressed, then the Statistics will be reset
                    case ConsoleKey.Enter:
                        intKeyPressed = 2;
                        break;
                }
            } while (intKeyPressed == 0);

            // If the user pressed "Enter" the stat will reset
            if (intKeyPressed == 2)
            {
                Properties.Settings.Default.CPUPaper = 0;
                Properties.Settings.Default.CPURock = 0;
                Properties.Settings.Default.CPUScissors = 0;
                Properties.Settings.Default.GameLost = 0;
                Properties.Settings.Default.GamePlayed = 0;
                Properties.Settings.Default.GameTied = 0;
                Properties.Settings.Default.GameWon = 0;
                Properties.Settings.Default.UserPaper = 0;
                Properties.Settings.Default.UserRock = 0;
                Properties.Settings.Default.UserScissors = 0;
                Properties.Settings.Default.Save();
                intUserScore = 0;
                intCPUScore = 0;
            }
            Console.Clear();
            Stats();
        }

        // Create the dash line
        private static void Dash()
        {
            // This will check for the width of the app and based on that will display the right number of
            // dashes
            for (int i = 1; i <= intAppWidth; i++)
            {   
                // If this is the first pass, this will create an empty line
                if (i == 1)
                    Console.Write("\n");
                Console.Write("-");
            }
        }

        // Options
        private static void Options()
        {
            int intKeyPressed = 0;
            Console.Clear();
            Console.SetCursorPosition(0, intAppHeight / 2);
            Console.ForegroundColor = ConsoleColor.Red;
            msg = "The Options Menu has not been implemented yet";
            Console.WriteLine("{0," + ((Console.WindowWidth / 2) + msg.Length / 2) + "}", msg);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(0, (intAppHeight / 2) + 5);
            msg = "Press ENTER to continue, and return to the main menu";
            Console.WriteLine("{0," + ((Console.WindowWidth / 2) + msg.Length / 2) + "}", msg);

            // This will check for the key input
            do
            {
                // This will check if the Enter Key is pressed
                switch (Console.ReadKey(true).Key)
                {
                   // if the Enter key is pressed, then return to the main menu
                    case ConsoleKey.Enter:
                        intKeyPressed = 1;
                        break;
                }
            } while (intKeyPressed == 0);

            // Clear the console
            Console.Clear();
            // Return to the main menu
            Main();
        }

        // Multiplayer Place Holder
        private static void GameMulti()
        {
            int intKeyPressed = 0;
            Console.Clear();
            Console.SetCursorPosition(0, intAppHeight / 2);
            Console.ForegroundColor = ConsoleColor.Red;
            msg = "The Multiplayer has not been implemented yet";
            Console.WriteLine("{0," + ((Console.WindowWidth / 2) + msg.Length / 2) + "}", msg);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(0, (intAppHeight / 2) + 5);
            msg = "Press ENTER to continue, and return to the main menu";
            Console.WriteLine("{0," + ((Console.WindowWidth / 2) + msg.Length / 2) + "}", msg);

            // This will check for the key input
            do
            {
                // This will check if the Enter Key is pressed
                switch (Console.ReadKey(true).Key)
                {
                    // if the Enter key is pressed, then return to the main menu
                    case ConsoleKey.Enter:
                        intKeyPressed = 1;
                        break;
                }
            } while (intKeyPressed == 0);

            // Clear the console
            Console.Clear();
            // Return to the main menu
            Main();
        }

        // The menu logic
        public static int MenuChooseItem(string[] items, int ucol, int urow, ConsoleColor back, ConsoleColor fore)
        {
            // check the number of items and assign it to numItems
            numItems = items.Length;
            // Set the max lenght to the lenght of the first item. (will be changed after)
            int maxLength = items[0].Length;

            // Set the lenght based of the longest item in the array
            for (int i = 1; i < numItems; i++)
            {
                if (items[i].Length > maxLength)
                {
                    maxLength = items[i].Length;
                }
            }


            int[] rightSpaces = new int[numItems];
            for (int i = 0; i < numItems; i++)
            {
                rightSpaces[i] = maxLength - items[i].Length + 1;
            }

            // Sets the distance on the x axis at which the option will be written
            int intSpacing = (Console.WindowWidth - 4) / (5);

            // Will change the starting place based on number of menu options
            switch (numItems)
            {
                // if the menu has two items, then the following dimensions will apply
                case 2:
                    intSpacing = 20;
                    urow = 7;
                    ucol = 17;
                    lcol = 53;
                    lrow = 8;
                    intX = 17;
                    break;
                // if the menu has three items, then the following dimensions will apply
                case 3:
                    intSpacing = 13;
                    break;
                // [Default Values] if the menu has Four items, then the following dimensions will apply
                case 4:
                    intSpacing = 13;
                    intX = 2;
                    break;
            }

            // Will draw the box
            DrawMenu(ucol, urow, lcol, lrow, back, fore, true);

            // Sets the first highlighted option
            MenuColorString(" " + items[0] + new string(' ', rightSpaces[0]), intSpacing , urow, fore, back);

            // Draw the original non-selescted menu items
            for (int i = 2; i <= numItems; i++)
            {
                MenuColorString(" " + items[i - 1] + new string(' ', rightSpaces[i - 1]), (i * intSpacing), urow, back, fore);

            }

            char key;
            int choice = 1;

            // Check for keys behing pressed
            while (true)
            {
                cki = Console.ReadKey(true);
                key = cki.KeyChar;
                // if enter is pressed, return the value of item selected
                if (key == '\r') // enter 
                {
                    // determine which menu was selected
                    switch (items[choice -1])
                    {
                        case "Play":
                        case "Play Again":
                            intSelection = 0;
                            intMenuCounter = 1;
                            break;
                        case "Stats":
                            intMenuCounter = 2;
                            break;
                        case "Options":
                            intMenuCounter = 3;
                            break;
                        case "Exit":
                            intMenuCounter = 4;
                            break;
                        case "Back":
                            intMenuCounter = 5;
                            break;
                        case "General":
                            intMenuCounter = 6;
                            intStatPage = 1;
                            break;
                        case "User":
                            intMenuCounter = 6;
                            intStatPage = 2;
                            break;
                        case "Computer":
                            intMenuCounter = 6;
                            intStatPage = 3;
                            break;
                    }
                    // return the value of the selected menu
                    return intMenuCounter;
                }
                // If right arrow is pressed the following logic will apply to change the color of the selected item
                // and assign the right positioning
                else if (cki.Key == ConsoleKey.RightArrow)
                {
                    MenuColorString(" " + items[choice - 1] + new string(' ', rightSpaces[choice - 1]), (choice * intSpacing), urow, back, fore);
                    if (choice < numItems)
                    {
                        choice++;
                    }
                    else
                    {
                        choice = 1;
                    }
                    MenuColorString(" " + items[choice - 1] + new string(' ', rightSpaces[choice - 1]), (choice * intSpacing), urow, fore, back);

                }
                // If left arrow is pressed the following logic will apply to change the color of the selected item
                // and assign the right positioning
                else if (cki.Key == ConsoleKey.LeftArrow)
                {
                    MenuColorString(" " + items[choice - 1] + new string(' ', rightSpaces[choice - 1]), (choice * intSpacing), urow, back, fore);
                    if (choice > 1)
                    {
                        choice--;
                    }
                    else
                    {
                        choice = numItems;
                    }
                    MenuColorString(" " + items[choice - 1] + new string(' ', rightSpaces[choice - 1]), (choice * intSpacing), urow, fore, back);
                }
            }
        }

        // Draw the menu
        public static void DrawMenu(int ucol, int urow, int lcol, int lrow, ConsoleColor back, ConsoleColor fore, bool fill)
        {
            // This created the lines around the menu
            const char HORIZONTAL = '\u2500';
            const char VERTICAL = '\u2502';
            const char UPPERLEFT = '\u250c';
            const char UPPERRIGHT = '\u2510';
            const char LOWERLEFT = '\u2514';
            const char LOWERRIGHT = '\u2518';

            // This assign the following blank to the fillLine variable. (spaces determined based off width of window)
            string fillLine = fill ? new string(' ', Console.WindowWidth - 5) : " ";
            // Set new colors
            MenuSetColors(back, fore);

            // draw top left edge 
            Console.SetCursorPosition(intX, 6);
            Console.Write(UPPERLEFT);

            // Draw the top line of the menu
            for (int i = ucol; i < lcol; i++)
            {
                Console.Write(HORIZONTAL);
            }
            // Draw the top right edge
            Console.Write(UPPERRIGHT);


            // draw sides 
            for (int i = urow; i < lrow; i++)
            {
                Console.SetCursorPosition(ucol, i);
                // Draw left side
                Console.Write(VERTICAL);
                // fill the middle with blank
                if (fill) Console.Write(fillLine);
                Console.SetCursorPosition(lcol + 1, i);
                // Draw right side
                Console.Write(VERTICAL);
            }
            // draw bottom left edge 
            Console.SetCursorPosition(ucol, lrow);
            Console.Write(LOWERLEFT);
            // Draw bottom menu line
            for (int i = ucol + 1; i < lcol + 1; i++)
            {
                Console.Write(HORIZONTAL);
            }
            // draw bottom right edge
            Console.Write(LOWERRIGHT);
        }

        // Select the color of the text based on the original input
        public static void MenuColorString(string s, int col, int row, ConsoleColor back, ConsoleColor fore)
        {
            MenuSetColors(back, fore);
            // write string 
            Console.SetCursorPosition(col, row);
            Console.Write(s);
        }

        // change the color of the background and foreground
        public static void MenuSetColors(ConsoleColor back, ConsoleColor fore)
        {
            // Set the background color
            Console.BackgroundColor = back;
            // Set the foreground color
            Console.ForegroundColor = fore;
        }

        // This method is in charge of reseting the console and some variables
        public static void Reset()
        {
            Console.ResetColor();
            Console.Clear();
            intSelection = 0;
        }
    }
}
