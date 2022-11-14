using ToyRobot.Interpreter;
using ToyRobot.Robot;
using ToyRobot.Runner;
using ToyRobot.WriterReader;

var robot = new Robot(5, 5);
var writerReader = new ConsoleWriterReader();
var interpreter = new RobotInterpreter(robot, writerReader);
var runner = new LineRunner(interpreter, writerReader);

runner.Run();