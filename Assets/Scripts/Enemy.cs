using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float movingSpeed = 1.5f;
    public int enemyDamage = 1;
    Rigidbody2D rb;
    public bool lookingRight = false;
    private Vector3 startPosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = this.transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = startPosition;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        float currentRunningSpeed = movingSpeed;
        if(lookingRight)
        {
            currentRunningSpeed = movingSpeed;
            this.transform.eulerAngles = new Vector3(0, 180, 0);
        } else {
            currentRunningSpeed = -movingSpeed;
            this.transform.eulerAngles = Vector3.zero;
        }
        
        if(GameManager.shareInstance.currentGameState == GameState.inGame) {
            rb.velocity = new Vector2 (currentRunningSpeed, rb.velocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Player":
                collision.gameObject.GetComponent<PlayerController>().healthCollect(-enemyDamage);
                return;
            case "Ground":
                lookingRight = !lookingRight;
                return;
            case "coin":
                return;
        }
    }
}
