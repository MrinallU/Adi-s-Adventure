using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P : MonoBehaviour
{
    [SerializeField] float jumpSpeed = 5f; 
    [SerializeField] float runSpeed = 6f; 
    Rigidbody2D myRigidBody;
    Animator myAnimator; 
    //Alive state for now
    bool isAlive = true;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeet;


    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(25f, 25f); 
    float gravityScaleAtStart; 



    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>(); 
        myFeet = GetComponent<BoxCollider2D>();

        gravityScaleAtStart = myRigidBody.gravityScale; 
    }

    // Update is called once per frame
    void Update()
    {
        if(!isAlive) { return;  }
        Jump(); 
        run();
        FlipSprite();
        ClimbLadder();
        Die(); 
    }
    private void ClimbLadder()
    {
        if (!myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myAnimator.SetBool("Climbing", false) ;
            myRigidBody.gravityScale = gravityScaleAtStart; 
            return; //Keep going with the command
        }
        float controlThrow = Input.GetAxis("Vertical");
        Vector2 climbVelocity = new Vector2(myRigidBody.velocity.x, controlThrow * climbSpeed);
        myRigidBody.velocity = climbVelocity;
        myRigidBody.gravityScale = 0f; 
        bool playerHasVerticalSpeed = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("Climbing", playerHasVerticalSpeed ); 
        //Might want to set the rest of the animations to false later on. 
    }
    private void Jump()
    {
        
        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return; // Alows the player to jump and carries on the code; 
          
        } 
        {
            LayerMask.GetMask("Ground");
            print("is not numpin");
        }
        if (Input.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocityToAdd = new Vector2(1f, jumpSpeed);
            myRigidBody.velocity += jumpVelocityToAdd;
            print("is not numpin");
        }
    }
    private void Die()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            isAlive = false; 
            myAnimator.SetTrigger("Die");
            GetComponent<Rigidbody2D>().velocity = deathKick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
  
        } 
    }
    private void run() 
    {
        bool playerHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        float controlThrow = Input.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody.velocity.y  );
        print(playerVelocity);
        myRigidBody.velocity = playerVelocity;
       
        myAnimator.SetBool("Running", true);
        if(playerHorizontalSpeed == true)
        {
            myAnimator.SetBool("Running", true);
            myAnimator.SetBool("Idle", false); 
        }
        else
        {
            myAnimator.SetBool("Running", false);
            myAnimator.SetBool("Idle", true); 
        }
    }
    private void FlipSprite()
    {
        // Rverese the scaling of the x axiusign the Math.abs() and their is also the Mathf.Sign which is the thing that rverses the player
        bool playerHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if (playerHorizontalSpeed == true)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x) + Mathf.Sign(myRigidBody.velocity.x), 2f); //flips the x of using the sign using the transform
        }

    }
}
