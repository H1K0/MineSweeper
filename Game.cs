namespace MineSweeper
{
    internal class Game
    {
        private readonly Field field;
        public Game(int field_width, int field_height, int nmines)
        {
            field = new Field(field_width, field_height, nmines);
        }
        public bool Run()
        {
            string[] command;
            field.Draw();
            while (true)
            {
                Console.Clear();
                field.Draw();
                if (field.Check())
                {
                    return true;
                }
                Console.Write("Enter your command: ");
                try
                {
                    command = Console.ReadLine().ToUpper().Split();
                    if (command.Length == 1)
                    {
                        int x = Convert.ToInt16(command[0][1..]) - 1;
                        int y = command[0].First() - 'A';
                        if (field.Open(y, x))
                        {
                            this.Finish();
                            return false;
                        }
                    }
                    else if (command.Length > 1)
                    {
                        if (command[0] != "M" && command[0] != "U")
                            throw new Exception("Invalid command.");
                        for (int i = 1; i < command.Length; i++)
                        {
                            int x = Convert.ToInt16(command[i][1..]) - 1;
                            int y = command[i].First() - 'A';
                            if (command[0] == "M")
                                field.Mark(y, x);
                            else
                                field.Unmark(y, x);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + "\nTry again.");
                    Thread.Sleep(1500);
                }
            }
        }
        private void Finish()
        {
            field.OpenAll();
            Console.Clear();
            field.Draw();
        }
    }
}
