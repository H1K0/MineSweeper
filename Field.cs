namespace MineSweeper
{
    internal class Field
    {
        private readonly int width = 0;
        private readonly int height = 0;
        private readonly int size = 0;
        private int nmines = 0;
        private readonly List<Cell> cells = new();
        private readonly List<Cell> opened = new();
        public Field(int input_width, int input_height, int input_nmines)
        {
            if (input_width < 0 || input_height < 0)
                throw new ArgumentException("Field dimensions must be natural numbers.");
            if (input_width < 2 || input_height < 2)
                throw new ArgumentException("Too small field.");
            if (input_width > 10 || input_height > 26)
                throw new ArgumentException("Too large field.");
            if (nmines < 0)
                throw new ArgumentException("The number of mines can not be negative.");
            if (nmines > input_width * input_height)
                throw new ArgumentException("The number of mines can not be greater than the number of all cells.");
            (width, height, nmines) = (input_width, input_height, input_nmines);
            size = width * height;
            cells = new List<Cell>();
            for (int i = 0; i < size; i++)
                cells.Add(new Cell(input_nmines-- > 0));
            Random rnd = new();
            for (int i = 0; i < size; i++)
            {
                int dest = rnd.Next(size);
                (cells[i], cells[dest]) = (cells[dest], cells[i]);
            }
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (cells[y * width + x].IsMine)
                    {
                        for (int y1 = y - 1; y1 <= y + 1; y1++)
                        {
                            if (y1 < 0 || y1 >= height)
                                continue;
                            for (int x1 = x - 1; x1 <= x + 1; x1++)
                            {
                                if (x1 < 0 || x1 >= width)
                                    continue;
                                Cell current = cells[y1 * width + x1];
                                if (current.IsMine)
                                    continue;
                                if (current.Value == " ")
                                    current.Value = "1";
                                else
                                    current.Value = Convert.ToString(Convert.ToInt16(current.Value) + 1);
                            }
                        }
                    }
                }
            }
        }
        public void Draw()
        {
            Console.Write("   ");
            for (int i = 0; i < width;)
            {
                Console.Write(" " + ++i);
            }
            Console.Write("\n  ╔");
            for (int i = 0; i < 2 * width + 1; i++)
            {
                Console.Write("═");
            }
            Console.WriteLine("╗");
            for (int y = 0; y < height; y++)
            {
                Console.Write((char)('A' + y) + " ║");
                for (int x = 0; x < width; x++)
                {
                    Console.Write(" " + ((opened.Contains(cells[y * width + x]) || cells[y * width + x].IsMarked) ? cells[y * width + x].Show() : "■"));
                }
                Console.WriteLine(" ║");
            }
            Console.Write("  ╚");
            for (int i = 0; i < 2 * width + 1; i++)
            {
                Console.Write("═");
            }
            Console.WriteLine("╝");
            Console.WriteLine($"Remaining mines: {nmines}.");
        }
        public bool Open(int y, int x)
        {
            if (y < 0 || y >= height || x < 0 || x >= width)
                throw new Exception("Coordinates out of the field!");
            if (cells[y * width + x].IsMarked)
                return false;
            if (cells[y * width + x].IsMine)
                return true;
            if (!opened.Contains(cells[y * width + x]))
            {
                opened.Add(cells[y * width + x]);
                if (cells[y * width + x].IsEmpty())
                {
                    if (x < width - 1)
                        this.Open(y, x + 1);
                    if (x > 0)
                        this.Open(y, x - 1);
                    if (y < height - 1)
                        this.Open(y + 1, x);
                    if (y > 0)
                        this.Open(y - 1, x);
                    if (x < width - 1 && y < height - 1)
                        this.Open(y + 1, x + 1);
                    if (x > 0 && y < height - 1)
                        this.Open(y + 1, x - 1);
                    if (x < width - 1 && y > 0)
                        this.Open(y - 1, x + 1);
                    if (x > 0 && y > 0)
                        this.Open(y - 1, x - 1);
                }
            }
            else if (!cells[y * width + x].IsEmpty())
            {
                int marked = 0;
                for (int y1 = y - 1; y1 < y + 2; y1++)
                {
                    if (y1 < 0 || y1 >= height)
                        continue;
                    for (int x1 = x - 1; x1 < x + 2; x1++)
                    {
                        if (x1 < 0 || x1 >= width || y1 == y && x1 == x)
                            continue;
                        if (cells[y1 * width + x1].IsMarked)
                            marked++;
                    }
                }
                if (marked == Convert.ToInt16(cells[y * width + x].Value))
                    for (int y1 = y - 1; y1 < y + 2; y1++)
                    {
                        if (y1 < 0 || y1 >= height)
                            continue;
                        for (int x1 = x - 1; x1 < x + 2; x1++)
                        {
                            if (x1 < 0 || x1 >= width || y1 == y && x1 == x)
                                continue;
                            if (!cells[y1 * width + x1].IsMarked && !opened.Contains(cells[y1 * width + x1]))
                                if (this.Open(y1, x1))
                                    return true;
                        }
                    }
            }
            return false;
        }
        public void OpenAll()
        {
            for (int i = 0; i < size; i++)
            {
                if (!opened.Contains(cells[i]))
                    opened.Add(cells[i]);
                if (cells[i].IsMarked && !cells[i].IsMine)
                    cells[i].SetWrong();
            }
        }
        public void Mark(int y, int x)
        {
            if (!opened.Contains(cells[y * width + x]))
            {
                cells[y * width + x].Mark();
                nmines--;
            }
        }
        public void Unmark(int y, int x)
        {
            cells[y * width + x].Unmark();
            nmines++;
        }
        public bool Check()
        {
            for (int i = 0; i < size; i++)
                if (!cells[i].IsMine && !opened.Contains(cells[i]))
                    return false;
            return true;
        }
    }
}
