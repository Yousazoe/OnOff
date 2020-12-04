using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public float seftDestructionHeight = -6f;

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
        float h = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(h * moveSpeed,rb.velocity.y);

        if (h != 0)
        {
            transform.localScale = new Vector3(h,1,1);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && IsGround())
        {
            rb.AddForce(Vector3.up * jumpForce,ForceMode2D.Impulse);
        }
        
        anim.SetFloat("Move",Mathf.Abs(h));
        anim.SetBool("isGround",IsGround());


        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.S.SwitchAllColor();

            if (insideBlock)
            {
                Die();
            }
        }

        if (transform.position.y < seftDestructionHeight && sr.enabled)
        {
            Die();
        }
    }


    void Die()
    {
        GameManager.S.PlayerDie();
        sr.enabled = false;
        
        GameObject dfx = Instantiate(deadEffect,transform.position,Quaternion.identity);
        Destroy(dfx,1f);
    }

    public void Revive()
    {
        rb.velocity = Vector2.zero;
        sr.enabled = true;
    }
    
    
    bool IsGround()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position,0.3f,Vector2.down,0.35f,whatIsGround);

        return hit;
    }
    
}
