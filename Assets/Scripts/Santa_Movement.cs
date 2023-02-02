using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;
using CodeMonkey.Utils;

public class Santa_Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private Animator anim;
    private SpriteRenderer sp;

    private enum State { Normal, Attacking };

    [SerializeField] private float moveSpeed = 5f;
    private float activeMoveSpeed;

    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashLength = 0.5f, dashCooldown = 1f;
    private float dashCounter;
    private float dashCoolCounter;

    private float dirX = 0f;
    private float dirY = 0f;

    private Vector2 PlayerInput;
    private Vector2 movement;
    private State state;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();

        activeMoveSpeed = moveSpeed;
    }

    private void Update()
    {
        //switch (state){

        //case State.Normal:
                HandleMovement();
                HandleAttack();
        //break;

        //case State.Attacking:
        //HandleAttack();

        //        break;
        //}
    }

    private void FixedUpdate()
    {
        PlayerInput = new Vector2(dirX, dirY).normalized;
        movement = new Vector2(PlayerInput.x * activeMoveSpeed * Time.fixedDeltaTime, PlayerInput.y * activeMoveSpeed * Time.fixedDeltaTime);
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

    private void HandleMovement()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        dirY = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Jump") && (dirX != 0 || dirY != 0))
        {
            if (dashCoolCounter <= 0 && dashCounter <= 0)
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

            if (PlayerInput.x == 0f && PlayerInput.y == 0f)
            {
                sp.enabled = true;
            }
        }

        if (dashCoolCounter > 0)
        {
            dashCoolCounter -= Time.deltaTime;
        }
    }

    private void HandleAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = UtilsClass.GetMouseWorldPosition();
            Vector3 mouseDir = (mousePos - transform.position).normalized;
            float attackOffset = 3f;
            Vector3 attackPosition = transform.position + mouseDir * attackOffset;
            Debug.Log(mouseDir);

            //state = State.Attacking;
        }
    }

    private void cursorPosition()
    {

    }
}
