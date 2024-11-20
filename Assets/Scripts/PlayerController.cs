using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float jumpForce = 100f;

    bool mayJump = true;
    [SerializeField]
    float speed = 5;
    [SerializeField]
    Transform groundChecker;
    [SerializeField]
    Transform RightWallChecker;
    [SerializeField]
    Transform LeftWallChecker;
    [SerializeField]
    LayerMask groundLayer;
    [SerializeField]
    LayerMask wallLayer;
    [SerializeField]
    float walkSpeed;
    [SerializeField]
    LayerMask finishWallLayer;
    [SerializeField]
    float slidingSpeed = -0.5f;
    [SerializeField]
    float wallSlideSpeed;
    [SerializeField]
    float wallHorizontalSpeed = 50;
    [SerializeField]
    float wallJumpForce = 30;
    [SerializeField]
    float offWallForce = 100;

    [SerializeField]
    public static float health = 3;

    float damage = .5f;
    float megaDamage = 1;

    bool isWallSliding = false;
    bool isWallJumping = false;
    bool isJumping;
    bool gameOver;


    void Start()
    {

    }

    void Update()
    {

        AudioSource audioSource = GetComponent<AudioSource>();
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        float jumpInput = Input.GetAxisRaw("Jump");

        isWallSliding = false;

        float xMove = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(xMove * walkSpeed, rb.velocity.y);
        rb.gravityScale = 10;

        if ((touchedRightWall() || touchedLeftWall()) && !IsGrounded() && xMove != 0)
        {
            isWallSliding = true;
        }

        if (isWallSliding)
        {
            WallSlide(rb, wallSlideSpeed);
        }

        if (jumpInput == 0)
        {
            mayJump = true;
        }


        if (jumpInput > 0 && mayJump && (IsGrounded() || isWallSliding))
        {
            if (IsGrounded())
            {
                print("Jumping");
                isJumping = true;
                rb.AddForce(Vector2.up * jumpForce);
                mayJump = false;
            }
            else if (isWallSliding && mayJump)
            {
                print("Walljumping");
                if (touchedRightWall())
                {
                    rb.velocity = new Vector2(-offWallForce, wallJumpForce);
                }
                else if (touchedLeftWall())
                {
                    rb.velocity = new Vector2(offWallForce, wallJumpForce);
                }
            }
            audioSource.Play(0);
        }

        if (IsGrounded() && mayJump)
        {
            isJumping = false;
        }


        if (rb.velocity.x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        if (rb.velocity.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }

        if (transform.position.y < -11)
        {
            respawn();
        }

         if (Physics2D.OverlapCircle(RightWallChecker.position, .2f, finishWallLayer))
        {
            print("finish");
            SceneManager.LoadScene(1);
        }



    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundChecker.position, .2f, groundLayer);
    }
    private bool touchedRightWall()
    {
        return Physics2D.OverlapCircle(RightWallChecker.position, 0.2f, wallLayer);
    }
    private bool touchedLeftWall()
    {
        return Physics2D.OverlapCircle(LeftWallChecker.position, 0.2f, wallLayer);
    }
    private void WallSlide(Rigidbody2D rb, float wallSlideSpeed)
    {
        rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
    }
    private void respawn()
    {
        transform.position = new Vector2(-18, 0.2f);
        if (health > 0)
        {
            health -= damage;
        }
        else
        {
            gameOver = true;
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy" && !isJumping)
        {
            respawn();
        }
        else if (other.tag == "Enemy" && isJumping)
        {
            other.gameObject.GetComponent<enemyController>().Kill();
        }

        if (other.tag == "BossBullet")
        {
            health -= damage;
            Destroy(other.gameObject);
        }else if (other.tag == "MegaBullet")
        {
            health -= megaDamage;

        }
       
    }


}
