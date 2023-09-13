using SharpNeat.Phenomes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnitySharpNEAT;

public class NeatAgent : UnitySharpNEAT.UnitController
{
    public float jumpForce = 100;
    public float horizontalVelocity = 5;
    const int numRays = 15;
    public float activationThreshold = 1.0f;
    public Vector2 initialPosition = Vector2.zero;
    public double jumpValue = 0.0f;

    private double[] distances = new double[numRays];
    private int[] collisionTypes = new int[numRays];

    private int layerMask;

    public bool canJump = true;
    private float v = 0;
    private Vector2 position;
    bool isDead = false;


    public float best_fitness = 0.0f;

    Rigidbody2D body;
    BoxCollider2D boxCollider;
    public NeatSupervisor supervisor;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        var sprite = GetComponent<SpriteRenderer>();
        if(sprite != null)
        {
            sprite.color = Random.ColorHSV();
        }

        // include only layer 6
        layerMask = 1 << 6;
        body.constraints = RigidbodyConstraints2D.FreezeRotation;
        best_fitness = 0;
    }
    // Update is called once per frame
    /*void Update()
    {
        //jump if we can jump and we are touching the ground and the space bar is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }*/

    void Update() 
    {
        // set a constant velocity to the right
        var velocity = body.velocity;
        Vector3 newVelocity = new Vector3(horizontalVelocity, velocity.y);
        body.velocity = newVelocity;
        if (transform.position.x > best_fitness)
        {
            best_fitness = transform.position.x;
        }

    }

    public void Jump(){
        var velocity = body.velocity;
        var newVelocity = new Vector2(velocity.x, 0);
        //jump only if we are touching the ground
        if (isTouchingGround())
        {
            body.velocity = newVelocity;
            v = jumpForce;
            Vector3 tempVect = new Vector3(0, v, 0);
            body.AddRelativeForce(tempVect);
        }
    }

    // check if we are touching the ground by raycasting down
    public bool isTouchingGround(){
        float extraheight = 0.1f;
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, extraheight, layerMask);
        return hit.collider != null;
    }

    protected override void UpdateBlackBoxInputs(ISignalArray inputSignalArray)
    {
        Vector2 direction = Vector2.down;
        float rotationIncrement = 180.0f / (numRays - 1); // degrees
        Quaternion rotation = Quaternion.Euler(0, 0, rotationIncrement);
        // now we ray cast numRays of rays starting from the bottom in even increments up to the top
        for (int i = 0; i < numRays; i++)
        {
            float distance = 0;
            float type = 0;
            Color hitColor;
            var hit = Physics2D.Raycast(transform.position, direction, 999999999, (1 << 6) | (1 << 7));
            if (hit.collider != null)
            {
                distances[i] = hit.distance;
                if (hit.collider.gameObject != null)
                {

                    // code the 
                    if (hit.collider.gameObject.layer == 6)
                    {
                        collisionTypes[i] = 1;
                        hitColor = Color.blue;
                    }
                    else if (hit.collider.gameObject.layer == 7)
                    {
                        collisionTypes[i] = -1;
                        hitColor = Color.red;
                    }
                    else
                    {
                        collisionTypes[i] = 0;
                        hitColor = Color.black;
                    }

                    // raycast debug function
                    Debug.DrawLine(transform.position, hit.point, hitColor);
                }

            }
            else
            {
                distances[i] = 0;
                collisionTypes[i] = 0;
                Debug.DrawRay(transform.position, direction, Color.green);
            }
            // now rotate the direction vector by rotationIncrement
            direction = rotation * direction;
        }


        // the first input is y velocity
        inputSignalArray[0] = body.velocity.y;
        // second input is isGrounded()
        if (isTouchingGround())
        {
            inputSignalArray[1] = 1;
        } else
        {
            inputSignalArray[1] = 0;
        }

        // then we add the inputs for the numRays of raycasts
        for (int i = 2; i < numRays; i++) {
            inputSignalArray[i] = distances[i];
            inputSignalArray[2 * i] = collisionTypes[i];
        }
    }

    protected override void UseBlackBoxOutpts(ISignalArray outputSignalArray)
    {
        jumpValue = outputSignalArray[0];
        if (outputSignalArray[0] > activationThreshold)
        {
            Jump();
        }
    }

    public override float GetFitness()
    {
        return best_fitness;
    }

    protected override void HandleIsActiveChanged(bool newIsActive)
    {
        if (!newIsActive)
        {
            transform.position = initialPosition;
            best_fitness = 0;
        }
        

        gameObject.SetActive(newIsActive);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other != null)
        {
            transform.position = initialPosition;
        }
    }
}
