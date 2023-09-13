using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeatCamera : MonoBehaviour
{

    public GameObject playerParent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float furthestX = 0;
        // loop through playerParent's children, find the child with the furthest forward X coordinate, and set the camera's x coordinate to that value
        // Check if the targetObject is not null
        if (playerParent != null)
        {
            // Loop through all children of targetObject
            for (int i = 0; i < playerParent.transform.childCount; i++)
            {
                Transform child = playerParent.transform.GetChild(i);
                if(child != null)
                {
                    if(child.transform.position.x > furthestX)
                    {
                        furthestX = child.transform.position.x;
                    }
                }
            }
        }

        // set the camera's x coordinate to furthestX
        var oldPosition = transform.position;
        var newPosition = oldPosition;
        newPosition.x = furthestX;
        if (Vector3.Distance(oldPosition, newPosition) > 2.0f)
        {
            transform.position = Vector3.Lerp(oldPosition, newPosition, 0.1f);
        } else
        {
            transform.position = newPosition;
        }
    }
}
