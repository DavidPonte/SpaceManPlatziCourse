using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameView : MonoBehaviour
{
    private PlayerController playerController;
    public TextMeshProUGUI coinsText, scoreText, maxScoreText;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        int coins = GameManager.shareInstance.collectedObject;
        float score = playerController.getTraveledPosition();
        float maxScore = PlayerPrefs.GetFloat("maxscore");

            coinsText.text = "Monedas: " + coins.ToString();
            scoreText.text = "Score: " + score.ToString("f1");
            maxScoreText.text = "Max. Score: " + maxScore.ToString("f1");
    }
}
