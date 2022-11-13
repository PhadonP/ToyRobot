using Moq;
using ToyRobot.Directions;
using ToyRobot.Interpreter;
using ToyRobot.Robot;
using ToyRobot.WriterReader;

namespace ToyRobotTest;

internal class RobotInterpreterTests
{
    private RobotInterpreter _interpreter;
    private Mock<IRobot> _robotMock;
    private Mock<IWriterReader> _writerReaderMock;

    [SetUp]
    public void Init()
    {
         _robotMock = new Mock<IRobot>();
        _writerReaderMock = new Mock<IWriterReader>();
        _interpreter = new RobotInterpreter(_robotMock.Object, _writerReaderMock.Object);
    }

    [Test]
    public void MoveCommandIssuedCorrectly()
    {
        _interpreter.InterpretInstruction("MOVE");
        _robotMock.Verify(robot => robot.Move(), Times.Once());

        _interpreter.InterpretInstruction("   MOVE   ");
        _robotMock.Verify(robot => robot.Move(), Times.Exactly(2));

        _interpreter.InterpretInstruction("move");
        _robotMock.Verify(robot => robot.Move(), Times.Exactly(3));
    }

    [Test]
    public void MoveCommandIssuedIncorrectly()
    {
        var failure = true;
        failure &= !_interpreter.InterpretInstruction("MOV");
        failure &= !_interpreter.InterpretInstruction("MOVa");
        failure &= !_interpreter.InterpretInstruction("MO VE");

        _robotMock.Verify(robot => robot.Move(), Times.Never());
        //All commands should be failed commands
        Assert.That(failure, Is.True);
    }

    [Test]
    public void MoveCommandIssuedMultipleTimes()
    {
        for (var i = 0; i < 10; i++)
        {
            _interpreter.InterpretInstruction("MOVE");
        }
     
        _robotMock.Verify(robot => robot.Move(), Times.Exactly(10));
    }

    [Test]
    public void LeftCommandIssuedCorrectly()
    {
        var success = _interpreter.InterpretInstruction("LEFT");

        _robotMock.Verify(robot => robot.Rotate(Rotation.Left), Times.Once());
        Assert.That(success, Is.True);
    }

    [Test]
    public void RightCommandIssuedCorrectly()
    {
        var success = _interpreter.InterpretInstruction("RIGHT");

        _robotMock.Verify(robot => robot.Rotate(Rotation.Right), Times.Once());
        Assert.That(success, Is.True);
    }

    [Test]
    public void ReportCommandWritesCorrectPosition()
    {
        _robotMock.Setup(robot => robot.Report()).Returns(new Position
        {
            X = 2,
            Y = 2,
            Direction = CompassDirection.East
        });

        _robotMock.Setup(robot => robot.IsPlaced).Returns(true);

        var success = _interpreter.InterpretInstruction("REPORT");

        _writerReaderMock.Verify(writerReader => writerReader.WriteLine("POSITION (X = 2, Y = 2, DIRECTION = EAST)"));
        Assert.That(success, Is.True);
    }

    [Test]
    public void ReportCommandWritesRobotNotPlaced()
    {
        _robotMock.Setup(robot => robot.IsPlaced).Returns(false);

        var success = _interpreter.InterpretInstruction("REPORT");

        _writerReaderMock.Verify(writerReader => writerReader.WriteLine("ROBOT HAS NOT BEEN PLACED ON THE TABLE PROPERLY YET"));
        Assert.That(success, Is.True);
    }

    [TestCase("PLACE 2, 2, NORTH")]
    [TestCase("PLACE 2,2,NORTH")]
    [TestCase("PLACE 2,,2,,NORTH")]
    [TestCase("   PLACE  2,  2,      NORTH     ")]
    public void PlaceCommandIssuedCorrectly(string instruction)
    {
        var success = _interpreter.InterpretInstruction(instruction);

        _robotMock.Verify(robot => robot.Place(new Position { X = 2, Y = 2, Direction = CompassDirection.North }));
        Assert.That(success, Is.True);
    }

    [TestCase("PLACEs 2, 2, NORTH")]
    [TestCase("PLACE 2,2,3,NORTH")]
    [TestCase("PLAC 2,2,EAST")]
    public void PlaceCommandIssuedIncorrectly(string instruction)
    {
        var success = _interpreter.InterpretInstruction(instruction);

        _robotMock.Verify(robot => robot.Place(It.IsAny<Position>()), Times.Never());
        Assert.That(success, Is.False);
    }
}
