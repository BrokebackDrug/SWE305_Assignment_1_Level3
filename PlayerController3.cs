using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController3 : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;

    public Animator anim;

  


    //private PolygonCollider2D poly2D;
    public Collider2D coll;
    public float speed;
    public float jumpForce;
    public LayerMask Ground;

    //public float attackStart, attackEnd;
    public int playerdamage;//player
    public float random;


    private bool PlayerJump;
    private bool jumpPressed;
    //private bool attackState;
    

    public AudioSource  jumpAudio, hurtAudio, pickAudio, deathAudio;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //poly2D = GetComponent<PolygonCollider2D>();     
    }
    void Update()
    {
        
        //Jump
        if (!PlayerJump)
        {
            if (Input.GetButtonDown("Jump") )
             {
                
                jumpPressed = true;
             }
        }
         /*  
        //Attack
        if (attackState == false)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                attackAudio.Play();
                anim.SetTrigger("Attack");
                
                StartCoroutine(PlayAttack());

            }

        }
        */


    }
    /*
    IEnumerator PlayAttack()
    {
        attackState = true;
        yield return new WaitForSeconds(attackStart);
        poly2D.enabled = true;
        yield return new WaitForSeconds(attackEnd);
        poly2D.enabled = false;
        attackState = false;
    }
    */
    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
        SwitchAnim();
        
    }


    

    void Movement()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float faceDirection = Input.GetAxisRaw("Horizontal");
        //character move
        if(horizontalMove != 0)
        {
            rb.velocity = new Vector2(horizontalMove * speed *Time.fixedDeltaTime, rb.velocity.y);
            anim.SetFloat("Running", Mathf.Abs(faceDirection));
        }

        if(faceDirection != 0)
        {
            transform.localScale = new Vector3(faceDirection, 1, 1);
        }
        //character jump
        if (!PlayerJump)
        {
            if (jumpPressed && coll.IsTouchingLayers(Ground))
            {
                jumpAudio.Play();
                jumpPressed = false;
                rb.velocity = new Vector2(rb.velocity.x, jumpForce * Time.fixedDeltaTime);
                anim.SetBool("Jumping", true);
                PlayerJump = true;

            }
        }
            

    }
    
    void SwitchAnim()
    {
        
        if (anim.GetBool("Jumping"))
        {
            anim.SetBool("Idle", false);
            if (rb.velocity.y < 0.1)
            {
                anim.SetBool("Jumping", false);
                anim.SetBool("Falling", true);
            }
        }else if (coll.IsTouchingLayers(Ground))
        {
            anim.SetBool("Falling", false);
            anim.SetBool("Idle", true);
            PlayerJump = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       
        //suprise box
        if (other.tag == "SupriseBox")
        {
            
            pickAudio.Play();
            //random ability
            random =  Random.Range(0f, 1f);
            //damage++;
            if (random <= 0.5)
            {
                playerdamage++;
            }

            //speed++;
            else
                speed *= 1.5f;
            Destroy(other.gameObject);
           
        }
        /*
        //attack enemy
        if (other.gameObject.CompareTag("Enemy") )
        {
            other.GetComponent<Enemy>().Hurt(playerdamage);
        }
        */
    }
 





}
