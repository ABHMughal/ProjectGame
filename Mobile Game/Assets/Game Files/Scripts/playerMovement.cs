using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float speedPower;
    public float jumpPower;
    
    private bool ground;
    private int action;
    private Rigidbody2D rigidBody;
    private Animator animator;
    private SpriteRenderer sprRenderer;

    void Start()
    {
        ground = false;
        action = 0;
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprRenderer = GetComponent<SpriteRenderer>();

    }



    // Update is called once per frame
    void Update()
    {
        rigidBody.freezeRotation = true;

        float inputX = Input.GetAxis("Horizontal");
        rigidBody.velocity = new Vector2(inputX * speedPower, rigidBody.velocity.y);

        //Debug.Log("In");
        if (inputX > 0)
        {
            sprRenderer.flipX = true;
        }
        else if (inputX < 0)
        {
            sprRenderer.flipX = false;
        }

        if (Input.GetKeyDown("space") && ground)
        {
            ground = false;
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpPower);
            animator.SetInteger("action", 2);
        }
        
        if(ground)
        {
            Debug.Log("Run");
            if (inputX != 0 && animator.GetInteger("action") == 0)
                animator.SetInteger("action", 1);
            else if (inputX == 0)
                animator.SetInteger("action", 0);
        }

      

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Ground"))
        {
            float inputX = Input.GetAxis("Horizontal");
            ground = true;
            animator.SetInteger("action", 0);
            if (inputX != 0)
                animator.SetInteger("action", 1);
        }
    }
}
