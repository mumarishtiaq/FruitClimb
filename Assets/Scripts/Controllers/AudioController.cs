using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] AudioSource audioSourcePlayer,audioSourceBot;
    [SerializeField] AudioClip HitGreen, HitBlue,GameOver,BlueCollected;
    void Start()
    {
        
        audioSourcePlayer = ScriptReference.instance.Player.GetComponent<AudioSource>();
        audioSourceBot = ScriptReference.instance.Bot.GetComponent<AudioSource>();
    }

    public void PlayGreenAudio()
    {
       
        audioSourcePlayer.PlayOneShot(HitGreen);
    }
    public void PlayBlueAudio()
    {
        
        audioSourcePlayer.PlayOneShot(HitBlue);
    }

   

    public void PlayGameOverAudio()
    {
        audioSourcePlayer.PlayOneShot(GameOver);
    }

    public void PlayBlueCollected()
    {
        audioSourceBot.PlayOneShot(BlueCollected);
    }



}
   
