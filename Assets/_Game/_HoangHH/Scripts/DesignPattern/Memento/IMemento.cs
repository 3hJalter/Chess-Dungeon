namespace HoangHH.DesignPattern.Memento
{
    public interface IMemento
    {
        public int Id
        {
            get;
        }
        public void Restore();
    }
}
