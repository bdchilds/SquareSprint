using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class PlayerCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // detect when a trigger is hit
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            transform.position = new Vector3(0, 0);
            Debug.Log("Died");
        }
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        foreach (Collider myCollider in GetComponents<Collider>())
        {
            if (collision.collider == myCollider)
            {
                // The 'collision.collider' is colliding with 'myCollider' on this object.
                // You can perform actions specific to this collider here.
                Debug.Log("Collided with " + myCollider.name);
            }
        }
    }*/
}
