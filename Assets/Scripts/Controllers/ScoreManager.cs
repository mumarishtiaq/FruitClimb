using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;



public class ScoreManager : MonoBehaviour
{
    public int PointsCollected = 0;
    public int TempPointsCollected = 0;
    public TMP_Text pointsCollectedText;
 
    


    private void Start()
    {
        pointsCollectedText = GetComponentInChildren<TextMeshPro>();
       
    }

    

    public void AddScore()
    {
       
        PointsCollected += 1;
        
    } 
    public void TempAddScore()
    {
       
        
        TempPointsCollected += 1;
        pointsCollectedText.text = TempPointsCollected.ToString();
    }

    

    

    public void SubScoreTemp()
    {
        TempPointsCollected -= 1;
        pointsCollectedText.text = TempPointsCollected.ToString();
    }

    

    
}
