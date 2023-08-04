using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerController : MonoBehaviour
{
    //player variables
    public float jumpForce = 8f;
    public float speed = 6f;
    public Vector3 startPosition;
    public LayerMask groundMask;
    Rigidbody2D playerRb;
    Animator playerAnimator;
    const string STATE_ALIVE = "alive";
    const string STATE_ON_GROUND = "onGround";
    const string STATE_JUMP = "jump";
    private int healthpoints, manapoints;

    public const int
        INIT_HEALTH = 1, INIT_MANA = 2,
        MAX_HEALTH = 3, MAX_MANA = 4,
        MIN_HEALTH = 0, MIN_MANA = 0,
       JUMP_FORCE = 2, V_JUMP = 1;
    //Awake is called when the script instance is being loaded.
    void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        
    }
    //Start is called before the first frame update
    void Start()
    {
        playerAnimator.SetBool(STATE_ALIVE, true);
        playerAnimator.SetBool(STATE_ON_GROUND, true);
        healthpoints = INIT_HEALTH;
        manapoints = INIT_MANA;
        startPosition = new Vector3(-20f, -0.2f, 0f);
    }

    public void StartGame()
    {
        playerAnimator.SetBool(STATE_ALIVE, true);
        playerAnimator.SetBool(STATE_ON_GROUND, true);
        Invoke("restartPosition", 0.1f);
    }

    void restartPosition()
    {
        transform.position = startPosition;
        playerRb.velocity = Vector3.zero;
        GameObject mainCamera = GameObject.Find("Main Camera");
        mainCamera.GetComponent<Camera>().Reset();
    }

    void FixedUpdate()
    {
        if (GameManager.shareInstance.currentGameState == GameState.inGame) {
            if (playerRb.velocity.x < speed)
            {
                playerRb.velocity = new Vector2(speed, playerRb.velocity.y);
            }
        }
        else //if we arent ingame
        {
            playerRb.velocity = new Vector2(0, playerRb.velocity.y);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager.shareInstance.currentGameState == GameState.inGame)
        {
            playerMoves();
            if (Input.GetButtonDown("Jump"))
            {
                Jump(false);
            }
            if (Input.GetButtonDown("VJump"))
            {
                Jump(true);
            }
            playerAnimator.SetBool(STATE_ON_GROUND, TouchingGround());
            playerAnimator.SetFloat(STATE_JUMP, playerRb.velocity.y);

            Debug.DrawRay(transform.position, Vector2.down * 1.5f, Color.red);
        }
    }

    //executes the action of moving to the right/to the left freely
    void playerMoves()
    {
        /*if(Input.GetKey(KeyCode.D) && playerRb.velocity.x < speed) 
        { playerRb.AddForce(Vector2.right * speed, ForceMode2D.Impulse); }
        if(Input.GetKey(KeyCode.A) && playerRb.velocity.x > -(speed)) 
        { playerRb.AddForce(Vector2.left * speed, ForceMode2D.Impulse); }*/
    }
    //executes the action of jumping
    void Jump(bool vJump)
    {
        float jumpFactor = jumpForce;
        if (vJump && manapoints>=V_JUMP && TouchingGround())
        {
            manapoints -= V_JUMP;
            jumpFactor *= JUMP_FORCE;
        }
        if (TouchingGround())
        {
            GetComponent<AudioSource>().Play();
            playerRb.AddForce(Vector2.up * jumpFactor, ForceMode2D.Impulse);
        }
    }

    //indicates if the player is touching the ground or not
    bool TouchingGround()
    {
        if (Physics2D.Raycast(transform.position, Vector2.down, 1.5f, groundMask))
        {

            return true;
        } else
        {
            return false;
        };
    }

    public void Death()
    {
        if (getTraveledPosition() > PlayerPrefs.GetFloat("maxscore", 0f))
        {
            PlayerPrefs.SetFloat("maxscore", getTraveledPosition());
        }
        playerAnimator.SetBool(STATE_ALIVE, false);
        playerRb.velocity = Vector3.zero;
        GameManager.shareInstance.GameOver();
    }

    public void healthCollect(int healthAdd)
    {
        healthpoints += healthAdd;
        if (healthpoints >= MAX_HEALTH)
        {
            healthpoints = MAX_HEALTH;
        }
        if(healthpoints == 0)
        {
            Death(); 
        }
    }
    public void manaCollect(int manaAdd)
    {
        manapoints += manaAdd;
        if (manapoints >= MAX_MANA)
        {
            manapoints = MAX_MANA;
        }
    }
    public int getHealth()
    {
        return healthpoints;
    }
    public int getMana()
    {
        return manapoints;
    }
    public float getTraveledPosition()
    {
        return (this.transform.position.x - startPosition.x) + (GameManager.shareInstance.collectedObject * 3);
    }
}
