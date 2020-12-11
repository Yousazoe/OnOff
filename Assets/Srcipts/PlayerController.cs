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

        Debug.Log("Before Dash " +  dashObj.activeInHierarchy);
        Debug.Log("Before Dash " +  dashObj.activeSelf);
        
        if (!isDashing)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                Debug.Log("Dash Before SetActive" +  dashObj.GetComponent<ParticleSystem>().isPlaying
                );
                
                dashObj.SetActive(true);
                
                Debug.Log("Dash After SetActive "+ dashObj.GetComponent<ParticleSystem>().isPlaying
                );
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
