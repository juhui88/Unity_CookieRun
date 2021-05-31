using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rigid; //
    public int jumpCount = 0;
    float bounce = 5f;

    public AudioSource mySfx;

    public AudioClip jumpSfx;

    SpriteRenderer spriteRenderer;

    Animator anim;

    GameObject player;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        this.player = GameObject.Find("player");


    }

    void OnCollisionEnter2D(Collision2D col)
    {
        jumpCount = 0;

        if (col.collider.tag == "obstacle")
        {
            print("충돌");
            onDamaged();
        }
    }

    void onDamaged()
    {
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        Invoke("OffDamaged", 3);

    }

    void OffDamaged()
    {
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    void Update()
    {
        float hor = Input.GetAxisRaw("Horizontal"); 

        rigid.velocity = new Vector2(hor * 3.0f, rigid.velocity.y);

        if (jumpCount < 2)
        {
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                anim.SetBool("isJumping", true);
                rigid.AddForce(Vector2.up * bounce, ForceMode2D.Impulse);
                jumpCount++;
                JumpSound();

            }  

            else
                anim.SetBool("isJumping", false);
            
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            anim.SetBool("isSliding", true);

        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
            anim.SetBool("isSliding", false);

        
        //방향상태
        if (Input.GetButtonDown("Horizontal"))
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;

        //animation walk
        if (rigid.velocity.normalized.x == 0)
            anim.SetBool("isWalking", false);
        else
            anim.SetBool("isWalking", true);


    }

    void JumpSound()
    {
        mySfx.PlayOneShot(jumpSfx);
    }

 

}
