using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class CollisionController : MonoBehaviour
{

   
    private void OnTriggerEnter(Collider other)
    {
        if(gameObject.name == "Player")
        {

            if(other.gameObject.tag=="GreenSphere")
            {
                PlayerHittedGreenSphere(other);
            }
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        #region commented
        /* if(collision.gameObject.tag == "BlueTile")
        {
            Debug.Log("hitted Blue");

            ScriptReference.instance.scoreManager.SubtractScore();
            ScriptReference.instance.scoreManager.SubScoreTemp();
            if(ScriptReference.instance.scoreManager.PointsCollected >= -3)
            {

                ScriptReference.instance.audioController.PlayBlueAudio();
            }
            else
            {
                ScriptReference.instance.uiController.GameOverUI("Mistake Exceeded");
            }
        }*/
        #endregion
        if(gameObject.name=="Player")
        {
            if (collision.gameObject.tag == "BlueSphere")
            {
                PlayerHittedBlueSphere();
            }
            //for Green stairs
            if (collision.gameObject.tag == "GreenStairs")
            {
                PlayerCreatingStairs(collision);
            }
        }

        if(gameObject.name == "Bot")
        {
            if (collision.gameObject.tag == "BlueSphere")
            {
                BotHittedBlueSphere(collision);
            }

            if(collision.gameObject.tag=="BlueStairs")
            {
                BotCreatingStairs(collision);
            }

        }
    }

    #region Custom Events

    #region Player
    //------When Player Hits the Green sphere--------
    void PlayerHittedGreenSphere(Collider other)
    {
        if (ScriptReference.instance.pLayerscoreManager.TempPointsCollected >= 10)
        {
            return;
        }
        Debug.Log("hitted Green");
        ScriptReference.instance.audioController.PlayGreenAudio();
        other.gameObject.GetComponent<MeshRenderer>().enabled = false;
        other.gameObject.GetComponent<Collider>().enabled = false;
        ScriptReference.instance.pLayerscoreManager.AddScore();
        ScriptReference.instance.pLayerscoreManager.TempAddScore();
        Destroy(other.gameObject, 1.5f);
    }

    //------When Player Hits the Blue sphere--------
    void PlayerHittedBlueSphere()
    {
        ScriptReference.instance.audioController.PlayBlueAudio();
    }

    //------When Player Wants to create stairs--------

    void PlayerCreatingStairs(Collision collision)
    {
        collision.gameObject.GetComponent<MeshRenderer>().enabled = true;
        ScriptReference.instance.audioController.PlayGreenAudio();
        collision.gameObject.tag = "Untagged";
        ScriptReference.instance.pLayerscoreManager.SubScoreTemp();
    }

    #endregion

    #region Bot
    
    //When Bot Hitted blue sphere
    void BotHittedBlueSphere(Collision collision)
    {
        if (collision.gameObject == ScriptReference.instance.tilesInstatioator.AllBlueSpheres[ScriptReference.instance.bOT_AIController.minDIstIndex])
        {
            collision.gameObject.GetComponent<MeshRenderer>().enabled = false;
            collision.gameObject.GetComponent<Collider>().enabled = false;
            ScriptReference.instance.audioController.PlayBlueCollected();
            ScriptReference.instance.BotscoreManager.AddScore();
            ScriptReference.instance.BotscoreManager.TempAddScore();
            ScriptReference.instance.tilesInstatioator.AllBlueSpheres.RemoveAt(ScriptReference.instance.bOT_AIController.minDIstIndex);
            Destroy(collision.gameObject, 1.5f);
        }
        else
        {
            Debug.Log("min point index " + ScriptReference.instance.bOT_AIController.minDIstIndex);
            collision.gameObject.GetComponent<Collider>().enabled = false;
            StartCoroutine(onCollider(collision));


        }
    }


    //when bot wants to createStairs

    void BotCreatingStairs(Collision collision)
    {
        //if (ScriptReference.instance.BotscoreManager.TempPointsCollected == 0) { return; }

        collision.gameObject.GetComponent<MeshRenderer>().enabled = true;
       ScriptReference.instance.audioController.PlayBlueCollected();
        collision.gameObject.tag = "Untagged";
        ScriptReference.instance.BotscoreManager.SubScoreTemp();

        Debug.Log(gameObject.name);

    }

    IEnumerator onCollider(Collision collision)
    {
        yield return new WaitForSeconds(1f);
        collision.gameObject.GetComponent<Collider>().enabled = true;
    }
    #endregion


    #endregion
}
