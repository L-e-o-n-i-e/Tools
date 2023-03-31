using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    Rigidbody rb;
    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            rb.transform.localPosition += new Vector3(1, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            rb.transform.localPosition += new Vector3(-1, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            rb.transform.localPosition += new Vector3(0, 0, 1);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            rb.transform.localPosition += new Vector3(0, 0, -1);
        }

    }
}
