using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Santa_Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private Animator anim;
    private SpriteRenderer sp;

    [SerializeField] private float moveSpeed = 5f;
    private float dirX = 0f;
    private float dirY = 0f;
    private Vector2 PlayerInput;
    private Vector2 movement;

    private float activeMoveSpeed;

    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashLength = 0.5f, dashCooldown = 1f;

    private float dashCounter;
    private float dashCoolCounter;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();

        activeMoveSpeed = moveSpeed;
    }

    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        dirY = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Jump"))
        {
            if(dashCoolCounter <= 0 && dashCounter <= 0)
            {
                activeMoveSpeed = dashSpeed;
                dashCounter = dashLength;
            }
        }

        if (dashCounter > 0)
        {
            sp.enabled = false;
            dashCounter -= Time.deltaTime;

            if (dashCounter <= 0)
            {
                activeMoveSpeed = moveSpeed;
                dashCoolCounter = dashCooldown;
                sp.enabled = true;
            }

            if(PlayerInput.x == 0f && PlayerInput.y == 0f)
            {
                sp.enabled = true;
            }
        }

        if(dashCoolCounter > 0)
        {
            dashCoolCounter -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        PlayerInput = new Vector2(dirX, dirY).normalized;
        movement = PlayerInput * activeMoveSpeed;
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
