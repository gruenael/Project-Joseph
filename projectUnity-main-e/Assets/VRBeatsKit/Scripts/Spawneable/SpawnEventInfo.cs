﻿using UnityEngine;

namespace VRBeats
{
    public enum Direction
    {
        UpperLeft = 0,
        Up,
        UpperRight,
        Left,
        Center,
        Right,
        LowerLeft,
        Down,
        LowerRight
    }

    public enum ColorSide
    {
        Left,
        Right,
        Twin,
        GripL,
        GripR,
    }


    [System.Serializable]
    public class SpawnEventInfo
    {
        public Direction hitDirection = Direction.Up;
        public ColorSide colorSide = ColorSide.Right;
        public bool useSpark = true;
        public Vector3 position = Vector3.zero;
        public Vector3 rotation = Vector3.zero;
        public float speed = 2.0f;
        public int speedMultiplier = 1;
        public int twinLane = 1;
        public int gripLong = 1;
    }
}
