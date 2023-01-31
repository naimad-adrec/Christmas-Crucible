using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Santa_Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private Animator anim;

    [SerializeField] private float moveSpeed = 5f;
    private float dirX = 0f;
    private float dirY = 0f;
    private Vector2 PlayerInput;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        dirY = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        PlayerInput = new Vector2(dirX, dirY).normalized;
        movement = PlayerInput * moveSpeed;
        rb.velocity = movement;

        if (dirX != 0f || dirY != 0f)
        {
            anim.SetBool("IsRunning", true);
        }
        else
        {
            anim.SetBool("IsRunning", false);
        }
    }
}
