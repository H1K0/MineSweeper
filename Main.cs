namespace MineSweeper
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Game game;
            while (true)
            {
                Console.WriteLine("(C) Masahiko AMANO a.k.a. H1K0, 2022\n\n" +
                    "Hey! Let's play the MineSweeper game!\nType anything for help or just press Enter to start the game.");
                while (Console.ReadLine().Length != 0)
                    Console.WriteLine("\nThe game follows the classic rules.\n" +
                        "Target cell coordinates are represented this way: A1, F5, I8. Case insensitive.\n" +
                        "Type coordinates of the cell to open it or use prefixes: \"M\" to mark or \"U\" to unmark the cell.\n" +
                        "For example: \"B9\", \"M D7\".\n" +
                        "Type anything to see help again or just press Enter to start the game.\n");
                Console.Write("Okay, let's go!\nEnter field width, height and number of mines separated with a space: ");
                while (true)
                {
                    try
                    {
                        string[] input = Console.ReadLine().Split();
                        game = new Game(Convert.ToInt16(input[0]), Convert.ToInt16(input[1]), Convert.ToInt16(input[2]));
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex.Message + "\nTry again.\n\n" +
                            "Enter field width, height and number of mines separated with a space: ");
                    }
                }
                bool result = game.Run();
                if (result)
                    Console.WriteLine("You win! Congratulations!");
                else
                    Console.WriteLine("You lose!");
                Console.WriteLine("Press Enter to play again or Escape to exit.");
                ConsoleKey key;
                bool exit = false;
                while (true)
                {
                    key = Console.ReadKey().Key;
                    if (key == ConsoleKey.Enter)
                    {
                        Console.Clear();
                        break;
                    }
                    else if (key == ConsoleKey.Escape)
                    {
                        exit = true;
                        break;
                    }
                }
                if (exit)
                    break;
            }
        }
    }
}