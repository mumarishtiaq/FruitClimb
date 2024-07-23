using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
   public void GameOverUI(string reasonOfDeath)
    {
        LeanTween.scale(ScriptReference.instance.gameOverPanel.gameObject, new Vector2(0.6f,0.5f), 1f);
        ScriptReference.instance.gameOverPanel.gameObject.transform.GetChild(1).GetComponent<Text>().text = reasonOfDeath;
        ScriptReference.instance.audioController.PlayGameOverAudio();
        Invoke("TImeScaleZero", 1);
    }

    void TImeScaleZero()
    {
        Time.timeScale = 0;
    }
}
