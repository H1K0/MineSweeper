namespace MineSweeper
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Game game;
            Database database = new();
            int PlayerId;
            string PlayerName = "";
            Console.Title = "MineSweeper by H1K0";
            Console.WriteLine("(C) Masahiko AMANO a.k.a. H1K0, 2022\n\n" +
                    "Hey! Let's play the MineSweeper game!\nPress any button for help or Enter to log in.");
            while (Console.ReadKey(true).Key != ConsoleKey.Enter)
                Console.WriteLine("\nThe game follows the classic rules.\n" +
                    "Target cell coordinates are represented this way: A1, F5, I8. Case insensitive.\n" +
                    "Type coordinates of the cell to open it or use prefixes: \"M\" to mark or \"U\" to unmark the cell.\n" +
                    "For example: \"B9\", \"M D7\".\n" +
                    "Press any button to see help again or just press Enter to start the game.");
            while (true)
            {
                Console.Write("\nYour name: ");
                PlayerName = Console.ReadLine();
                PlayerId = database.FindPlayer(PlayerName);
                if (PlayerId == -1 || PlayerName == "")
                {
                    Console.WriteLine("Could not find a player with the name \"" + PlayerName + "\". Press Enter to add new or any other button to re-enter.");
                    if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                    {
                        database.AddPlayer(PlayerName);
                        PlayerId = database.PlayersCount() - 1;
                        break;
                    }
                }
                else
                    break;
            }
            Console.WriteLine("You logged in as \"" + PlayerName + "\".\nYour stats:");
            Console.WriteLine($"Wins: {database.Stats(PlayerId)[0]}\nLoses: {database.Stats(PlayerId)[1]}");
            while (true)
            {
                Console.Write("\nOkay, let's play!\nEnter field width, height and number of mines separated with a space: ");
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
                database.AddGame(PlayerId, result);
                database.Update();
                Console.WriteLine("Press Enter to play again or Escape to exit.");
                ConsoleKey key;
                bool exit = false;
                while (true)
                {
                    key = Console.ReadKey(true).Key;
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