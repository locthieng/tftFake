using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants
{
    internal static readonly float EPSILON = 0.00001f;

    public static string AppName { get { return ""; } }
    public static string PackageName { get { return "com.amzg.game"; } }
    public static string FacebookPageURL { get { return ""; } }
    public static string AppURL { get { return "PC Build"; } }
    public static string StoreURL { get { return "PC Build"; } }

    public static class Tag
    {
        public static string MainCharacter = "Player";
        public static string BadGuy = "BadGuy";
        public static string Trap = "Trap";
        public static string Material = "Material";
        public static string Loot = "Loot";
        public static string NPC = "NPC";
    }

    public static class Layer
    {
        public static string Obstacle = "Obstacle";
        public static string DisabledGround = "DisabledGround";
        public static string PathPoint = "PathPoint";
    }

    public static class Save
    {
        public static string SnowMesh = "snow_";
        public static string ice = "ice_";
    }
}