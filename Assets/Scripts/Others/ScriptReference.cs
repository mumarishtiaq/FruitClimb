using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptReference : MonoBehaviour
{
    public static ScriptReference instance;
  
   
    public GameObject Player;
    public GameObject Bot;

    [Space]
    public AudioController audioController;
    [Space]
    public ScoreManager pLayerscoreManager;
    [Space]
    public ScoreManager BotscoreManager;
    [Space]
    public CollisionController collisionController;

    [Space]
    public TilesInstatioator tilesInstatioator;

    [Space]
    public UIController uiController;

    [Space]
    public GameOverPanel gameOverPanel;

    [Space]
    public BOT_AIController bOT_AIController;
    private void Reset()
    {
        instance = this;
        Player = GameObject.Find("Player");
        Bot = GameObject.Find("Bot");
        audioController = FindObjectOfType<AudioController>();

        pLayerscoreManager = Player.GetComponent<ScoreManager>();
        BotscoreManager = Bot.GetComponent<ScoreManager>();

        collisionController = Player.GetComponent<CollisionController>();
        tilesInstatioator = FindObjectOfType<TilesInstatioator>();
        uiController = FindObjectOfType<UIController>();
        gameOverPanel = FindObjectOfType<GameOverPanel>();
        bOT_AIController = FindObjectOfType<BOT_AIController>();



    }
    private void Awake()
    {
        if(!instance)
        {

            instance = this;
        }

        if(!Player)
        {
            Player = GameObject.Find("Player");
        }
        if(!Bot)
        {
            Player = GameObject.Find("Bot");
        }

        if(!audioController)
        {
            audioController = FindObjectOfType<AudioController>();
        }

        if(!pLayerscoreManager)
        {
            pLayerscoreManager = Player.GetComponent<ScoreManager>();
        }
        
        if(!BotscoreManager)
        {
            BotscoreManager = Bot.GetComponent<ScoreManager>();
        }

        if(!collisionController)
        {
            collisionController = Player.GetComponent<CollisionController>();
        }
        if (!tilesInstatioator)
        {
            tilesInstatioator = FindObjectOfType<TilesInstatioator>();
        }
        if (!uiController)
        {
            uiController = FindObjectOfType<UIController>();
        }
        if (!gameOverPanel)
        {
            gameOverPanel = FindObjectOfType<GameOverPanel>();
        }
        if (!bOT_AIController)
        {
            bOT_AIController = FindObjectOfType<BOT_AIController>();
        }
    }

}
