using ToyRobot.Directions;
using ToyRobot.Robot;

namespace ToyRobotTest;

internal class RobotTest
{
    //Use different x and y for testing purposes
    private const int XTableSize = 5;
    private const int YTableSize = 6;

    private IRobot _robot;

    [SetUp]
    public void Init()
    {
        _robot = new Robot(XTableSize, YTableSize);
    }

    [Test]
    public void NotPlacedOnInitialization()
    {
        Assert.That(_robot.IsPlaced, Is.False);
    }

    [Test]
    public void PlacedCorrectly([Range(0, 5)] int x, [Range(0, 5)] int y)
    {
        var position = new Position
        {
            X = x,
            Y = y,
            Direction = CompassDirection.North
        };

        _robot.Place(position);

        Assert.That(_robot.IsPlaced, Is.True);
    }

    [TestCase(-1, 0, CompassDirection.North)]
    [TestCase(0, -1, CompassDirection.North)]
    [TestCase(6, 0, CompassDirection.North)]
    [TestCase(0, 7, CompassDirection.North)]
    [TestCase(6, 7, CompassDirection.North)]
    public void PlacedIncorrectly(int x, int y, CompassDirection direction)
    {
        var position = new Position
        {
            X = x,
            Y = y,
            Direction = direction
        };

        _robot.Place(position);

        Assert.That(_robot.IsPlaced, Is.False);
    }

    [Test]
    public void PlacedIncorrectlyAfterBeingPlacedCorrectly()
    {
        var position1 = new Position
        {
            X = 0,
            Y = 0,
            Direction = CompassDirection.North
        };

        var position2 = new Position
        {
            X = -1,
            Y = 0,
            Direction = CompassDirection.North
        };

        _robot.Place(position1);
        //Robot should ignore second placement
        _robot.Place(position2);

        Assert.That(_robot.IsPlaced, Is.True);
        Assert.That(_robot.Report(), Is.EqualTo(position1));
    }

    [TestCase(CompassDirection.North, Rotation.Right, CompassDirection.East)]
    [TestCase(CompassDirection.East, Rotation.Right, CompassDirection.South)]
    [TestCase(CompassDirection.South, Rotation.Right, CompassDirection.West)]
    [TestCase(CompassDirection.West, Rotation.Right, CompassDirection.North)]
    [TestCase(CompassDirection.North, Rotation.Left, CompassDirection.West)]
    [TestCase(CompassDirection.East, Rotation.Left, CompassDirection.North)]
    [TestCase(CompassDirection.South, Rotation.Left, CompassDirection.East)]
    [TestCase(CompassDirection.West, Rotation.Left, CompassDirection.South)]
    public void RotatesCorrectly(CompassDirection startDirection, Rotation rotation, CompassDirection endDirection)
    {
        var startPosition = new Position
        {
            X = 0,
            Y = 0,
            Direction = startDirection
        };

        _robot.Place(startPosition);
        _robot.Rotate(rotation);
        var endPosition = _robot.Report();

        Assert.That(endPosition.Direction, Is.EqualTo(endDirection));
    }

    [Test]
    public void ReportThrowsWhileNotPlaced()
    {
        //Report method will throw if robot is not placed,
        //Application should check whether robot is placed before reporting to avoid exception
        Assert.That(_robot.Report, Throws.Exception);
    }

    [TestCase(2, 3, CompassDirection.North)]
    [TestCase(3, 2, CompassDirection.East)]
    [TestCase(2, 1, CompassDirection.South)]
    [TestCase(1, 2, CompassDirection.West)]
    public void MovesInCorrectDirection(int endX, int endY, CompassDirection direction)
    {
        var startPosition = new Position
        {
            X = 2,
            Y = 2,
            Direction = direction
        };

        _robot.Place(startPosition);
        _robot.Move();
        var endPosition = _robot.Report();

        var expectedPosition = new Position
        {
            X = endX,
            Y = endY,
            Direction = direction
        };

        Assert.That(endPosition, Is.EqualTo(expectedPosition));
    }

    [TestCase(3, 6, CompassDirection.North)]
    [TestCase(5, 3, CompassDirection.East)]
    [TestCase(3, 0, CompassDirection.South)]
    [TestCase(0, 3, CompassDirection.West)]
    public void IgnoresMovesThatFallOffTable(int x, int y, CompassDirection direction)
    {
        var startPosition = new Position
        {
            X = x,
            Y = y,
            Direction = direction
        };

        _robot.Place(startPosition);
        _robot.Move();
        var endPosition = _robot.Report();

        //Robot should have not moved
        Assert.That(endPosition, Is.EqualTo(startPosition));
    }

    [Test]
    public void MovesCorrectlyWithSequenceOfMoves()
    {
        var position = new Position
        {
            X = 0,
            Y = 0,
            Direction = CompassDirection.North
        };

        _robot.Place(position);

        //Move 4 spots up
        _robot.Move();
        _robot.Move();
        _robot.Move();
        _robot.Move();

        position = _robot.Report();
        var expectedPosition = new Position
        {
            X = 0,
            Y = 4,
            Direction = CompassDirection.North
        };

        Assert.That(position, Is.EqualTo(expectedPosition));

        //Try and move left but should be ignored
        _robot.Rotate(Rotation.Left);
        _robot.Move();

        expectedPosition = new Position
        {
            X = 0,
            Y = 4,
            Direction = CompassDirection.West
        };

        position = _robot.Report();
        Assert.That(position, Is.EqualTo(expectedPosition));

        //Move right 2 spaces
        _robot.Rotate(Rotation.Right);
        _robot.Rotate(Rotation.Right);
        _robot.Move();
        _robot.Move();

        expectedPosition = new Position
        {
            X = 2,
            Y = 4,
            Direction = CompassDirection.East
        };

        position = _robot.Report();
        Assert.That(position, Is.EqualTo(expectedPosition));
    }
}