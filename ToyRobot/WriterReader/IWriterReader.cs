namespace ToyRobot.WriterReader;

public interface IWriterReader
{
    /// <summary>
    /// Writes a line of string text to output
    /// </summary>
    /// <param name="s">String text to write</param>
    void WriteLine(string s);
    
    /// <summary>
    /// Reads a line of string from input
    /// </summary>
    /// <returns>Line of string text</returns>
    string? ReadLine();
}
