using Moq;
using ToyRobot.Interpreter;
using ToyRobot.Robot;
using ToyRobot.WriterReader;

namespace ToyRobotTest;

internal class RobotIntegrationTest
{
    private const int TableSize = 5;

    private Robot _robot;
    private RobotInterpreter _interpreter;
    private Mock<IWriterReader> _writerReaderMock;

    [SetUp]
    public void Init()
    {
        _robot = new Robot(TableSize, TableSize);
        _writerReaderMock = new Mock<IWriterReader>();
        _interpreter = new RobotInterpreter(_robot, _writerReaderMock.Object);
    }

    [Test]
    public void ReportsCorrectlyAfterSequenceOfMoves()
    {
        _interpreter.InterpretInstruction("PLACE 2,2,NORTH");
        _interpreter.InterpretInstruction("MOVE");
        _interpreter.InterpretInstruction("MOVE");
        _interpreter.InterpretInstruction("RIGHT");
        _interpreter.InterpretInstruction("REPORT");

        _writerReaderMock.Verify(writerReader => writerReader.WriteLine("X = 2, Y = 4, DIRECTION = EAST"));
    }

    [Test]
    public void ReportsNotPlacedAfterNotBeingPlacedProperly()
    {
        _interpreter.InterpretInstruction("PLACE 6,6,SOUTH");
        _interpreter.InterpretInstruction("MOVE");
        _interpreter.InterpretInstruction("MOVE");
        _interpreter.InterpretInstruction("RIGHT");
        _interpreter.InterpretInstruction("REPORT");

        _writerReaderMock.Verify(writerReader => writerReader.WriteLine("ROBOT HAS NOT BEEN PLACED ON THE TABLE PROPERLY YET"));
    }
}
