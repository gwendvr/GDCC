using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    public static List<(string Name, int Score)> Scoreboard = new List<(string, int)>();
    public static int NbrPlayer;
}