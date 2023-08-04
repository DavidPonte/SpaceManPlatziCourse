using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatform : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Animator animator = GetComponent<Animator>();
        animator.enabled = true;
    }
}
