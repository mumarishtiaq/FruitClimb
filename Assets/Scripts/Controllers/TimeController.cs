using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeController : MonoBehaviour
{
   public float timerValue = 1500;

     TMP_Text textToDisplay;
    int counter=0;
   
    void Start()
    {
        textToDisplay = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timerValue > 0)
        {
            timerValue -= Time.deltaTime;
        }
        else
        {
            timerValue = 0;
            /*if(counter!=1)
            {

            timerValue = 0;
            ScriptReference.instance.uiController.GameOverUI("Time Out");
            counter = 1;
            }*/

        }
        minutesAndSeconds(timerValue);
    }

    void minutesAndSeconds(float timeToDIsplay)
    {

       
        float minutes = Mathf.FloorToInt(timeToDIsplay / 60);
        float Seconds = Mathf.FloorToInt(timeToDIsplay % 60);


        textToDisplay.text = string.Format("{0:00}:{1:00}", minutes, Seconds).ToString();

        if(timeToDIsplay<15)
        {
            textToDisplay.color = Color.red;
        }
       


    }


}
