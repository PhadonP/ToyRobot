﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyRobot
{
    public readonly struct Position
    {
        public int X { get; init; }
        public int Y { get; init; }
        public CompassDirection Direction { get; init; }
    }
}