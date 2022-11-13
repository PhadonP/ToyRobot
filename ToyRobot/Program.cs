using ToyRobot.Interpreter;
using ToyRobot.Robot;
using ToyRobot.WriterReader;

var robot = new Robot(5, 5);
var writerReader = new ConsoleWriterReader();
var interpreter = new RobotInterpreter(robot, writerReader);

writerReader.WriteLine("TOY ROBOT CHALLENGE");
writerReader.WriteLine("-------------------");

string? line;
while ((line = writerReader.ReadLine()) != null){
    var successfulCommand = interpreter.InterpretInstruction(line);
    if (!successfulCommand)
    {
        writerReader.WriteLine("COMMAND WAS INVALID");
    }
}