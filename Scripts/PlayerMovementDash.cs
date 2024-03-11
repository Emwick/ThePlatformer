using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    public Text WINTEXT;

    public Rigidbody2D playerRb;
    public float speed;
    public float input;
    public SpriteRenderer spriteRenderer;
    public float jumpForce;

    public LayerMask groundLayer;
    private bool isGrounded;
    public Transform feetPosition;
    public float groundCheckCircle;

    public float JumpTime = 0.35f;
    public float jumpTimeCounter;
    private bool isJumping;

    public bool flippedLeft;
    public bool facingRight;

    void Update()
    {
        input = Input.GetAxisRaw("Horizontal");
        if(input < 0)
        {
            facingRight = false;
            Flip(false);
            
        }
        else if (input > 0)
        {
            facingRight = true;
            Flip(true);
            
        }

        isGrounded = Physics2D.OverlapCircle(feetPosition.position, groundCheckCircle, groundLayer);


        if (isGrounded == true && Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            jumpTimeCounter = JumpTime;
            playerRb.velocity = Vector2.up * jumpForce;
        }

        if (Input.GetButton("Jump") && isJumping == true)
        {
            if(jumpTimeCounter > 0)
            {
                playerRb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }
        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }
    }
    

    void FixedUpdate()
    {
        playerRb.velocity = new Vector2 (input * speed, playerRb.velocity.y);
    }

    void Flip (bool facingRight)
    {
        if(flippedLeft && facingRight)
        {
            transform.Rotate (0, -180, 0);
            flippedLeft = false;
        }
        if(!flippedLeft && !facingRight)
        {
            transform.Rotate(0, -180, 0);
            flippedLeft = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Powerup")
        {
            Destroy(collision.gameObject);
            jumpForce = 15f;
            GetComponent<SpriteRenderer>().color = Color.yellow;
            StartCoroutine(ResetPower());

        }

        if(collision.tag == "Win")
        {
            WINTEXT.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }
    private IEnumerator ResetPower()
    {
        yield return new WaitForSeconds(9);
        jumpForce = 10;
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}