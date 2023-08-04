using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager shareInstance;
    public Canvas menuCanvas;
    public Canvas inGameCanvas;
    public Canvas gameOverCanvas;

    private void Awake()
    {
        if (shareInstance == null)
        {
            shareInstance = this;
        }
        inGameCanvas.enabled = false;
        gameOverCanvas.enabled = false;
        menuCanvas.enabled = true;
    }

    public void showMainMenu(){menuCanvas.enabled = true;}
    public void hideMainMenu(){menuCanvas.enabled = false;}
    public void showInGameMenu() { inGameCanvas.enabled = true;}
    public void hideInGameMenu() { inGameCanvas.enabled = false;}
    public void showGameOverMenu() { gameOverCanvas.enabled = true;}
    public void hideGameOverMenu() { gameOverCanvas.enabled = false;}

    public void exitMainMenu() 
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
