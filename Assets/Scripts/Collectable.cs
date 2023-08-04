using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum collectableTypes { healthPot, manaPot, coin}

public class Collectable : MonoBehaviour
{
    public collectableTypes type = collectableTypes.coin;

    private SpriteRenderer sprite;
    private CircleCollider2D itemCollider;
    public bool hasBeenCollected;
    public int value;
    public GameObject Player;
    //Awake is called before the engine starts
    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        itemCollider = GetComponent<CircleCollider2D>();
    }
    private void Start()
    {
        Player = GameObject.Find("Player");
    }

    void show()
    {
        sprite.enabled = true;  
        itemCollider.enabled = true;  
        hasBeenCollected = false;
    }

    void hide()
    {
        sprite.enabled=false;
        itemCollider.enabled=false;
    }
    void collect()
    {
        hide();
        hasBeenCollected=true;

        switch (this.type)
        {
            case collectableTypes.healthPot:
                
                Player.GetComponent<PlayerController>().healthCollect(this.value);
                break;

            case collectableTypes.manaPot:
                
                Player.GetComponent<PlayerController>().manaCollect(this.value);
                break;

            case collectableTypes.coin:
                GameManager.shareInstance.CollectObject(this);
                GetComponent<AudioSource>().Play();
                break;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collect();
        }
    }
}
