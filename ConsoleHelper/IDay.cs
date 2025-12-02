namespace ConsoleHelper
{
    public interface IDay
    {
        long Day { get; }
        string Title { get; }

        long GetFirstAnswer();

        long GetSecondAnswer();
    }
}
