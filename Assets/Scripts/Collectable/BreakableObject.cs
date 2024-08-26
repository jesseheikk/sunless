using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    [SerializeField] AudioSource breakSound;

    Animator animator;
    BoxCollider2D boxCollider;

    void Start()
    {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void Break()
    {
        if (breakSound)
        {
            breakSound.Play();
        }
        animator.SetTrigger("Break");
        gameObject.layer = LayerMask.NameToLayer("Ghost");
    }
}
