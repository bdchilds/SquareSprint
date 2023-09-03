using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoxMover : MonoBehaviour
{

    public float jumpForce = 100;
    public float horizontalVelocity = 5;

    private bool isTouchingGround = false;

    private int layerMask;



    private bool canJump = true;

    Rigidbody2D body;
    BoxCollider2D boxCollider;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();

        // include only layer 6
        layerMask = 1 << 6;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;
        position.y -= boxCollider.size.y / 2;
        position.x -= boxCollider.size.x / 2;
        Vector2 direction = Vector2.down;
        float distance = 0.1f;

        float raycastStepSize = boxCollider.size.x / 10;
        // create 10 raycasts from one side of the cube to the other
        RaycastHit2D[] hits = new RaycastHit2D[20];
        for (int i = 0; i <= 10; i++) {
            var hit = Physics2D.Raycast(position, direction, distance, layerMask);
            // Debug.DrawRay(position, direction, Color.green);
            hits[i] = hit;
            position.x += raycastStepSize;
        }
        isTouchingGround = false;
        foreach(var hit in hits)
        {
            if( hit.collider != null )
                isTouchingGround = true;
        }

        if(!isTouchingGround ) {
            Debug.DrawRay(transform.position, Vector2.up, Color.red);
        }

        // if the input is down it needs to only trip again once space is released

        if (!isTouchingGround)
        {
            canJump = true;
        }

        float v = 0;
        if (isTouchingGround && Input.GetKey(KeyCode.Space) && canJump)
        {
            
            var velocity = body.velocity;
            var newVelocity = new Vector2(velocity.x, 0);
            body.velocity = newVelocity;
            canJump = false;
            v = jumpForce;
            Vector3 tempVect = new Vector3(0, v, 0);
            body.AddRelativeForce(tempVect);
        }

        
        
    }

    void FixedUpdate()
    {
        // set a constant velocity to the right
        var velocity = body.velocity;
        Vector3 newVelocity = new Vector3(horizontalVelocity, velocity.y);
        body.velocity = newVelocity;

        
    }
}
