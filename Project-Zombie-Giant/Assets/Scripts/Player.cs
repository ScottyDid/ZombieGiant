using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    Rigidbody2D rb;
    //Player Speed
    public float speed;
    //Jump Force
    public float jumpForce;

    bool isGrounded = false;

    public Transform isGroundedChecker;
    public float checkGroundedRadius;
    public LayerMask groundLayer;

    public int defaultAdditionalJumps = 1;
    int additionalJumps;

    public float rememberGroundedFor;
    float lastTimeGrounded;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        CheckIfGrounded();
    }
    //Move the player left and right
    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        //move the player left and right using our speed
        float moveBy = x * speed;
        rb.velocity = new Vector2(moveBy, rb.velocity.y);
    }
    //Make the player jump
    void Jump()
    {
        //Press space to jump. Check to see if we're on the ground
        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || Time.time - lastTimeGrounded <= rememberGroundedFor || additionalJumps > 0))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            additionalJumps--;
        }
    }
    //Check if we're on the ground layer.
    void CheckIfGrounded()
    {
        Collider2D collider =
            Physics2D.OverlapCircle(isGroundedChecker.position, checkGroundedRadius, groundLayer);

        if (collider != null)
        {
            isGrounded = true;
            additionalJumps = defaultAdditionalJumps;
        }
            else
        {
            if (isGrounded)
            {
                lastTimeGrounded = Time.time;
            }
            isGrounded = false;

        }

    }
    //if we collide with an enemy of the bottom of a level then die and restart
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Fall"))
        {
            Die();
            RestartLevel();
        }
    }
    //make us freeze
    private void Die()
    {
        rb.bodyType = RigidbodyType2D.Static;

    }
    //restart the level.
    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
