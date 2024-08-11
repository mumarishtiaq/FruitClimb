using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Levels")]
public class LevelTemplate : ScriptableObject
{
    public int level;
    public bool isOwned;
    public int unlockPrice;
    public int starsEarned;
    [Space]
    public  int bonusonStar1;
    public  int bonusonStar2;
    public  int bonusonStar3;
}


