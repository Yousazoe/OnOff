using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpSpeed;
    public float seftDestructionHeight = -6f;

    int remainJump;

    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;
    public LayerMask whatIsGround;
    public GameObject deadEffect;

    public bool insideBlock;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        //Move
        float h = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(h * moveSpeed, rb.velocity.y);

        //Change Face Direction
        if(h != 0)
        {
            transform.localScale = new Vector3(h, 1, 1);
        }

        //Apply Jump
        if(Input.GetKeyDown(KeyCode.UpArrow) && remainJump > 0)
        {
            remainJump--;
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            AudioManager.S.PlayPlayerSFX(0);
        }

        if(IsGround())
        {
            remainJump = 1;
        }

        //Set Animator
        anim.SetFloat("Move", Mathf.Abs(h));
        anim.SetBool("isGround", IsGround());

        //Switch Color
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.S.SwitchAllColor();
            AudioManager.S.PlayPlayerSFX(1);
            if(insideBlock)
            {
                Die();
            }
        }

        //Self Destruction
        if(transform.position.y < seftDestructionHeight && sr.enabled)
        {
            Die();
        }
    }

    public void Die()
    {
        GameManager.S.PlayerDie();
        AudioManager.S.PlayPlayerSFX(2);
        sr.enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;

        GameObject dfx = Instantiate(deadEffect, transform.position, Quaternion.identity);
        Destroy(dfx, 1f);
    }

    public void Revive()
    {
        GetComponent<BoxCollider2D>().enabled = true;
        rb.velocity = Vector2.zero;
        sr.enabled = true;
    }

    bool IsGround()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, 0.3f, Vector2.down, 0.35f, whatIsGround);
        return hit;
    }
}
