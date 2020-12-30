using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float dashSpeed;
    public float dashTime;
    
    public float jumpForce;
    public float seftDestructionHeight = -6f;
    
    public GameObject dashObj;
    public ParticleSystem ps;
    
    private bool isDashing;
    private float startDashTime;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;

    public Transform groundCheck;
    public LayerMask whatIsGround;
    public GameObject deadEffect;


    public bool insideBlock;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        sr = GetComponentInChildren<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        // Move
        //right h =1 left h = -1 no input h = 0
        //GetAxis GetAxisRaw
        float h = Input.GetAxisRaw("Horizontal");
        
        rb.velocity = new Vector2(h * moveSpeed,rb.velocity.y);

        
        //Change Face Direction
        if (h != 0)
        {
            transform.localScale = new Vector3(h,1,1);
        }
        

        //Apply Jump
        if (Input.GetKeyDown(KeyCode.UpArrow) && IsGround())
        {
            rb.AddForce(Vector3.up * jumpForce,ForceMode2D.Impulse);
            AudioManager.S.PlayPlayerSFX(0);
        }
        
        //Set Animator
        anim.SetFloat("Move",Mathf.Abs(h));
        anim.SetBool("isGround",IsGround());


        //Switch Color
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.S.SwitchAllColor();
            AudioManager.S.PlayPlayerSFX(1);

            if (insideBlock)
            {
                Die();
            }
        }
        
        if (!isDashing)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                dashObj.SetActive(true);
                
                isDashing = true;
                startDashTime = dashTime;
            }
        }
        else
        {
            startDashTime -= Time.deltaTime;
            if (startDashTime <= 0)
            {
                isDashing = false;
                dashObj.SetActive(false);
            }
            else
            {
                rb.velocity = transform.right * dashSpeed;
            }
        }
        

        if (transform.position.y < seftDestructionHeight && sr.enabled)
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

        ps.Stop();
        
        
        GameObject dfx = Instantiate(deadEffect, transform.position, Quaternion.identity);
        Destroy(dfx,2f);
    }

    public void Revive()
    {
        GetComponent<BoxCollider2D>().enabled = true;
        ps.Play();
        rb.velocity = Vector2.zero;
        sr.enabled = true;
    }
    
    
    bool IsGround()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position,0.3f,Vector2.down,0.35f,whatIsGround);

        return hit;
    }
    
}
