namespace ToyRobot.WriterReader;

public class ConsoleWriterReader : IWriterReader
{
    public string? ReadLine()
    {
        return Console.ReadLine();
    }

    public void WriteLine(string s)
    {
        Console.WriteLine(s);
    }
}
