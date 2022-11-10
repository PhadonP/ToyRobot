using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ToyRobot;

public class Robot : IRobot
{
    public bool IsPlaced { get; private set; } = false;

    private readonly int _xtableSize;
    private readonly int _ytableSize;

    private Position _position;

    public Robot(int xTableSize, int yTableSize)
    {
        _xtableSize = xTableSize;
        _ytableSize = yTableSize;
    }


    public void Move()
    {
        if (!IsPlaced)
        {
            return;
        }

        int dx = 0;
        int dy = 0;

        switch (_position.Direction)
        {
            case (CompassDirection.North):
                dx = 0;
                dy = 1;
                break;
            case (CompassDirection.East):
                dx = 1;
                dy = 0;
                break;
            case (CompassDirection.South):
                dx = 0;
                dy = -1;
                break;
            case (CompassDirection.West):
                dx = -1;
                dy = 0;
                break;
        }

        var newPosition = new Position
        {
            X = _position.X + dx,
            Y = _position.Y + dy,
            Direction = _position.Direction
        };

        if (ValidPosition(newPosition))
        {
            _position = newPosition;
        }
    }

    public void Place(Position position)
    {
        if (!ValidPosition(position))
        {
            return;
        }

        _position = position;
        IsPlaced = true;
    }

    public Position Report()
    {
        if (!IsPlaced)
        {
            throw new Exception("Robot has not been placed on the table");
        }

        return _position;
    }

    public void Rotate(Rotation rotation)
    {
        if (!IsPlaced)
        {
            return;
        }

        //This variable determines which way through the CompassDirection enumerable the direction will move in
        var compassMovement = 1;

        if (rotation == Rotation.Left)
        {
            compassMovement = -1;
        }

        var newCompassDirection = (int) _position.Direction + compassMovement;

        //Wrap around the enumerable
        if (newCompassDirection == 4)
        {
            newCompassDirection = 0;
        } 
        //The modulus sign does not work for this problem as -1 % 4 != 3 but -1 % 4 == -1
        else if (newCompassDirection == -1)
        {
            newCompassDirection = 3;
        }

        _position = _position with
        {
            Direction = (CompassDirection) newCompassDirection
        };
    }

    private bool ValidPosition(Position position)
    {
        return position.X >= 0 && position.X <= _xtableSize && position.Y >= 0 && position.Y <= _ytableSize;
    }
}
