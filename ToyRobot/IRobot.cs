using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyRobot;

public interface IRobot
{
    /// <summary>
    /// 
    /// </summary>
    bool IsPlaced { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="position"></param>
    void Place(Position position);

    /// <summary>
    /// 
    /// </summary>
    void Move();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="rotation"></param>
    void Rotate(Rotation rotation);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    Position Report();
}
