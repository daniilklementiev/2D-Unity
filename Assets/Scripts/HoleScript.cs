using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleScript : MonoBehaviour
{
    // [SerializeField]
    private float forceFactor = 300f;
    private Rigidbody2D body; // reference to component
    
    void Start()
    {
        body = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            body.AddForce(Vector2.up * forceFactor);
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            body.AddTorque(1f);
        }
    }
}
