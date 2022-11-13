using ToyRobot.Directions;
using ToyRobot.Robot;
using ToyRobot.WriterReader;

namespace ToyRobot.Interpreter;

public class RobotInterpreter : IRobotInterpreter
{
    public IRobot Robot { get; set; }
    public IWriterReader WriterReader { get; set; }

    public RobotInterpreter(IRobot robot, IWriterReader writerReader) 
    { 
        Robot = robot;
        WriterReader = writerReader;
    }

    public bool InterpretInstruction(string instruction)
    {
        var command = instruction.Trim().ToUpper();

        switch(command)
        {
            case "MOVE":
                Robot.Move();
                return true;
            case "LEFT":
                Robot.Rotate(Rotation.Left);
                return true;
            case "RIGHT":
                Robot.Rotate(Rotation.Right);
                return true;
            case "REPORT":
                if (Robot.IsPlaced)
                {
                    var position = Robot.Report();
                    WriterReader.WriteLine(position.ToString().ToUpper());
                }
                else
                {
                    WriterReader.WriteLine("ROBOT HAS NOT BEEN PLACED ON THE TABLE PROPERLY YET");
                }
                return true;

        }

        //Deal with the command PLACE
        //-----------------------------

        var separators = new char[] { ' ', ',' };
        var splitCommand = command.Split(separators, StringSplitOptions.RemoveEmptyEntries);

        if (splitCommand.Length != 4 || splitCommand[0].ToUpper() != "PLACE")
        {
            return false;
        }

        if (int.TryParse(splitCommand[1], out var x) 
            && int.TryParse(splitCommand[2], out var y) 
            && Enum.TryParse(splitCommand[3], true, out CompassDirection direction))
        {
            var position = new Position
            {
                X = x,
                Y = y,
                Direction = direction
            };

            Robot.Place(position);
            return true;
        }

        return false;
    }
}
