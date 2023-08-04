using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    menu,
    inGame,
    gameOver
}
public class GameManager : MonoBehaviour
{
    //Define a public State as the data inside GameState enum
    public GameState currentGameState = GameState.menu;
    //Declaration of SINGLETON
    public static GameManager shareInstance;

    private PlayerController pController;

    public int collectedObject;

    void Awake()
    {
        //if shared instance hasnt been assigned yet...
        if (shareInstance == null)
        {
            //this means that shareInstance is the share instance perse, the ruler of the script
            shareInstance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        pController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Menu") && currentGameState != GameState.inGame)
        {
            GameStart();
        }
    }
    //Game starter
    public void GameStart()
    {
        SetGameState(GameState.inGame);
    }
    //Game ending
    public void GameOver()
    {
        SetGameState(GameState.gameOver);
    }
    //Going back to menu
    public void BackToMenu()
    {
        SetGameState(GameState.menu);
    }
    private void SetGameState(GameState NewGameState)
    {
        if (NewGameState == GameState.menu)
        {
            //HACER: logica del menu
            MenuManager.shareInstance.hideGameOverMenu();
            MenuManager.shareInstance.hideInGameMenu();
            MenuManager.shareInstance.showMainMenu();
        }
        else if(NewGameState == GameState.inGame)
        {
            //HACER: preparar escena para empezar partida
            LevelManager.shareInstance.removeAllLevelBlocks();
            LevelManager.shareInstance.generateInitialBlocks();
            MenuManager.shareInstance.hideMainMenu();  
            MenuManager.shareInstance.hideGameOverMenu();
            MenuManager.shareInstance.showInGameMenu();
            collectedObject = 0;
            pController.StartGame();
        }
        else if(NewGameState == GameState.gameOver)
        {
            //HACER: preparar escena para el fin de la partida
            MenuManager.shareInstance.hideInGameMenu();
            MenuManager.shareInstance.hideMainMenu();
            MenuManager.shareInstance.showGameOverMenu();
        }

        this.currentGameState = NewGameState;
    }

    public void CollectObject(Collectable collectable)
    {
        collectedObject += collectable.value;
    }
}
