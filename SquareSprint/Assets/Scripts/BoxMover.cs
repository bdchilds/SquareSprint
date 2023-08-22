using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMover : MonoBehaviour
{
    Rigidbody2D body;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float v = 0;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            v = 1000;
        }

        Vector3 tempVect = new Vector3(0, v, 0);
        body.AddRelativeForce(tempVect);
        

        // set a constant velocity to the right
        var velocity = body.velocity;
        Vector3 newVelocity = new Vector3(5, velocity.y);
        body.velocity = newVelocity;
    }
}
