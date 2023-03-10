using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    private Rigidbody2D rb;
    private int speed = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //rb.AddRelativeForce(Vector2.left * speed, ForceMode2D.Force);
        rb.AddForce(Vector2.left * speed, ForceMode2D.Force);
    }
}
