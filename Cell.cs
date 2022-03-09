namespace MineSweeper
{
    internal class Cell
    {
        public string Value = " ";
        public bool IsMine;
        public bool IsMarked;
        public Cell(bool mine)
        {
            if (mine)
            {
                IsMine = true;
                Value = "¤";
            }
        }
        public string Show()
        {
            if (IsMarked)
                return "X";
            else
                return Value;
        }
        public void SetMine() { IsMine = true; }
        public void Mark() { IsMarked = true; }
        public void Unmark() { IsMarked = false; }
        public void SetWrong()
        {
            IsMarked = false;
            Value = "!";
        }
        public bool IsEmpty() { return Value == " "; }
    }
}
