using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyRobot.Interpreter;

public interface IRobotInterpreter
{
    /// <summary>
    /// Interprets an instruction and performs instruction onto robot
    /// </summary>
    /// <param name="instruction">Instruction to interpret</param>
    /// <returns>True if instruction is valid, else false</returns>
    bool InterpretInstruction(string instruction);
}
