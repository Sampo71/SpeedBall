using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float torqueAmount = 5f;
    public float jumpForce = 8f;
    public float airSpeed = 15f;

    private Rigidbody2D rb2d;
    private SpriteRenderer sr;
    public ParticleSystem DeathP; 
    private bool isGrounded = false;

    public float rayDistance = 10f;

    private bool canJump = true;
    private bool DeathDelayOn;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>() as Rigidbody2D;
        sr = GetComponent<SpriteRenderer>();
        rb2d.bodyType = RigidbodyType2D.Dynamic;
        DontDestroyOnLoad(this.gameObject);
    }


    void FixedUpdate()
    {
        Move();
        CheckGrounded();
        Jump();
        CheckDeath();   
    }

    void Move()
    {
        float direction = Input.GetAxisRaw("Rotation");
        rb2d.AddTorque(direction * torqueAmount );
        if(!isGrounded)
        {
            if((direction < 0 && rb2d.velocity.x < 0) || (direction > 0 && rb2d.velocity.x > 0)){

               Vector2 airMovement = new Vector2(direction, 0f) * airSpeed * -1;
               rb2d.AddForce(airMovement);
            }
        }
    }

    void CheckGrounded()
    {
        Vector2 origin = transform.position;
        LayerMask rayLayer = LayerMask.GetMask("Ground");
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, rayDistance, rayLayer);

        if (hit.collider != null)
        {
            Debug.DrawLine(origin, hit.point, Color.red);
            isGrounded = true;
        }
        else
        {
            Debug.DrawLine(origin, origin + Vector2.down * rayDistance, Color.green);
            isGrounded = false;
        }

    }

    IEnumerator JumpCoolDown()
    {
      canJump = false;
      yield return new WaitForSeconds(0.3f);    
      canJump = true;
      StopCoroutine(JumpCoolDown());
    }

    void Jump()
    {
        float YSpeed = rb2d.velocity.y;

        if(isGrounded && canJump && YSpeed >= -5 && Input.GetButton ("Jump"))
        {
            rb2d.AddForce(Vector3.up * jumpForce);

            StartCoroutine(JumpCoolDown());
        }
    }

    void CheckDeath()
    {
        if(transform.position.y < -30 && !DeathDelayOn)
        {
            Die();
        }
    }

    void Die()
    {
        StartCoroutine (DeathDelay());
    }

    IEnumerator DeathDelay()
    {
       sr.enabled = false;
       DeathDelayOn = true;
       DeathP.Play();
       yield return new WaitForSeconds(1.2f);
       sr.enabled = true;  
       transform.position = new Vector3(0,0,0);
       rb2d.velocity = new Vector3(0,0,0);
       DeathDelayOn = false;
       StopCoroutine (DeathDelay());
    }

    public void OnSceneLoaded()
    {
        transform.position = new Vector3(0,0,0);
    }

}   


