using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Rigidbody2D rb;
    private int speed = 25;

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            MoveUp();
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            MoveDown();
        }
    }

    private void MoveUp()
    {
        rb.AddRelativeForce(Vector2.up * speed);
    }
    private void MoveDown()
    {
        rb.AddRelativeForce(Vector2.down * speed);
    }
}
