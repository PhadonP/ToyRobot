using ToyRobot;

namespace ToyRobotTest;

public class RobotTest
{
    private const int TABLESIZE = 5;

    private IRobot _robot = new Robot(TABLESIZE);

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void NotPlacedOnInitialization()
    {
        Assert.That(_robot.IsPlaced, Is.False);
    }

    [Test]
    public void PlacedCorrectly([Range(0, 5)] int x, [Range(0, 5)] int y,
        [Range(0, 3)] CompassDirection direction)
    {
        var position = new Position
        {
            X = x,
            Y = y,
            Direction = direction
        };

        _robot.Place(position);

        Assert.That(_robot.IsPlaced, Is.True);
    }

    [TestCase(-1, 0, CompassDirection.North)]
    [TestCase(0, -1, CompassDirection.North)]
    [TestCase(6, 0, CompassDirection.North)]
    [TestCase(0, 6, CompassDirection.North)]
    [TestCase(6, 6, CompassDirection.North)]
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

    [Test]
    public void MovesInCorrectDirection()
    {

    }

    [Test]
    public void IgnoresMovesThatFallOffTable()
    {

    }

    [Test]
    public void MovesCorrectlyWithSequenceOfMoves()
    {

    }
}