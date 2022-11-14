using Moq;
using ToyRobot.Interpreter;
using ToyRobot.Robot;
using ToyRobot.Runner;
using ToyRobot.WriterReader;

namespace ToyRobotTest;

internal class RobotIntegrationTest
{
    private const int TableSize = 5;

    private LineRunner _runner;
    private Mock<IWriterReader> _writerReaderMock;

    [SetUp]
    public void Init()
    {
        var robot = new Robot(TableSize, TableSize);
        _writerReaderMock = new Mock<IWriterReader>();
        var interpreter = new RobotInterpreter(robot, _writerReaderMock.Object);
        _runner = new LineRunner(interpreter, _writerReaderMock.Object);
    }

    [Test]
    public void ReportsCorrectlyAfterSequenceOfMoves()
    {
        _writerReaderMock.SetupSequence(writerReader => writerReader.ReadLine())
            .Returns("PLACE 2,2,NORTH")
            .Returns("MOVE")
            .Returns("MOVE")
            .Returns("RIGHT")
            .Returns("REPORT")
            .Returns((string?) null);

        _runner.Run();

        _writerReaderMock.Verify(writerReader => writerReader.WriteLine("X = 2, Y = 4, DIRECTION = EAST"));
    }

    [Test]
    public void ReportsNotPlacedAfterNotBeingPlacedProperly()
    {
        _writerReaderMock.SetupSequence(writerReader => writerReader.ReadLine())
            .Returns("PLACE 6,6,SOUTH")
            .Returns("MOVE")
            .Returns("MOVE")
            .Returns("RIGHT")
            .Returns("REPORT")
            .Returns((string?)null);

        _runner.Run();

        _writerReaderMock.Verify(writerReader => writerReader.WriteLine("ROBOT HAS NOT BEEN PLACED ON THE TABLE PROPERLY YET"));
    }

    [Test]
    public void ReportsCommandInvalid()
    {
        _writerReaderMock.SetupSequence(writerReader => writerReader.ReadLine())
            .Returns("PLAC 6,6,SOUTH")
            .Returns("MOV")
            .Returns("MOV")
            .Returns("RIGHT")
            .Returns((string?)null);

        _runner.Run();

        _writerReaderMock.Verify(writerReader => writerReader.WriteLine("COMMAND WAS INVALID"), Times.Exactly(3));
    }
}
