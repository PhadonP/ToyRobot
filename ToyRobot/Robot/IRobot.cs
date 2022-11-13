using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToyRobot.Directions;

namespace ToyRobot.Robot;

public interface IRobot
{
    /// <summary>
    /// True if the robot has been placed on the table, False if not.
    /// </summary>
    bool IsPlaced { get; }

    /// <summary>
    /// Places or moves the robot to a specific position on the table. Invalid positions are ignored.
    /// </summary>
    /// <param name="position">Position to move the robot to</param>
    void Place(Position position);

    /// <summary>
    /// Moves the robot forward in the direction it is facing. Moves that move the robot off the table are ignored.
    /// </summary>
    void Move();

    /// <summary>
    /// Rotates robot 90 degrees clockwise or anti-clockwise.
    /// </summary>
    /// <param name="rotation">Rotate robot left or right</param>
    void Rotate(Rotation rotation);

    /// <summary>
    /// Returns the current position of the robot.
    /// </summary>
    /// <returns>Position of robot</returns>
    /// <exception cref="Exception">Thrown when robot has not been placed on the table yet.</exception>
    Position Report();
}
