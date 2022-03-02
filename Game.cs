namespace MineSweeper
{
    internal class Game
    {
        private Field field;
        public Game(int field_width, int field_height, int nmines)
        {
            field = new Field(field_width, field_height, nmines);
        }
        public bool run()
        {
            string[] command;
            field.draw();
            while (true)
            {
                Console.Clear();
                field.draw();
                if (field.check())
                {
                    return true;
                }
                Console.Write("Enter your command: ");
                try
                {
                    command = Console.ReadLine().ToUpper().Split();
                    if (command.Length == 1)
                    {
                        int x = Convert.ToInt16(command[0].Substring(1)) - 1;
                        int y = command[0].First() - 'A';
                        if (field.open(y, x))
                        {
                            this.finish();
                            return false;
                        }
                    }
                    else if (command.Length == 2)
                    {
                        if (command[0] != "M" && command[0] != "U")
                            throw new Exception("Invalid command.");
                        int x = Convert.ToInt16(command[1].Substring(1)) - 1;
                        int y = command[1].First() - 'A';
                        if (command[0] == "M")
                            field.mark(y, x);
                        else
                            field.unmark(y, x);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + "\nTry again.");
                    Thread.Sleep(1500);
                }
            }
        }
        private void finish()
        {
            field.openall();
            Console.Clear();
            field.draw();
        }
    }
}
