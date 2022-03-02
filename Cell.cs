namespace MineSweeper
{
    internal class Cell
    {
        public string value = " ";
        public bool isMine = false;
        public bool isOpened = false;
        private bool isMarked = false;

        public Cell(bool mine)
        {
            if (mine)
            {
                isMine = true;
                value = "¤";
            }
        }
        public string show()
        {
            if (isOpened)
                return value;
            else if (isMarked)
                return "X";
            else
                return "■";
        }
        public void setMine() { isMine = true; }
        public void open() { isOpened = true; }
        public void mark() { isMarked = true; }
        public void unmark() { isMarked = false; }
        public bool isEmpty() { return value == " "; }
    }
}
