using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float torqueAmount = 1f;
    public float jumpForce = 8f;

    private Rigidbody2D rb2d;
    public bool isGrounded = false;

    public float rayDistance = 10f;

    public bool canJump = true;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>() as Rigidbody2D;
        rb2d.bodyType = RigidbodyType2D.Dynamic;
    }

    void Awake()
    {
    }

    void FixedUpdate()
    {
        Move();
        CheckGrounded();
        Jump();   
    }

    void Move()
    {
        float direction = Input.GetAxisRaw("Rotation");
        rb2d.AddTorque(direction * torqueAmount);
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

        if(isGrounded && canJump && YSpeed >= 0 && Input.GetButton ("Jump"))
        {
            rb2d.AddForce(Vector3.up * jumpForce);

            StartCoroutine(JumpCoolDown());
        }
    }

}   


