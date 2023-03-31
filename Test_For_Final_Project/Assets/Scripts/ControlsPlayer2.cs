using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsPlayer2 : MonoBehaviour
{
    Rigidbody rb;
    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            rb.transform.localPosition += new Vector3(1, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            rb.transform.localPosition += new Vector3(-1, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            rb.transform.localPosition += new Vector3(0, 0,1);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            rb.transform.localPosition += new Vector3(0, 0, -1);
        }

    }
}
