using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MatchSettings 
{
    public static int MatchLenght = 50;
    public static MatchType MatchType = MatchType.QuickPlay;
}

public enum MatchType
{
    QuickPlay,
    Story,
    Multiplayer
}

