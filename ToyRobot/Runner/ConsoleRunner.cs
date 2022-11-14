using ToyRobot.Interpreter;
using ToyRobot.WriterReader;

namespace ToyRobot.Runner;

public class LineRunner : IRunner
{
    private IRobotInterpreter _interpreter;
    private IWriterReader _writerReader;

    public LineRunner(IRobotInterpreter interpreter, IWriterReader writerReader)
    {
        _interpreter = interpreter;
        _writerReader = writerReader;
    }

    public void Run()
    {
        _writerReader.WriteLine("TOY ROBOT CHALLENGE");
        _writerReader.WriteLine("-------------------");

        string? line;
        while ((line = _writerReader.ReadLine()) != null)
        {
            var successfulCommand = _interpreter.InterpretInstruction(line);
            if (!successfulCommand)
            {
                _writerReader.WriteLine("COMMAND WAS INVALID");
            }
        }
    }
}
