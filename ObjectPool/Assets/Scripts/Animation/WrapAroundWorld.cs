using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrapAroundWorld : MonoBehaviour
{
    public float minX , maxX , minY, maxY ;

    void Update()
    {
        if (gameObject != null)
        {
            Vector3 pos = transform.position;
           
            if (pos.x < minX )
            {
                transform.position = new Vector3(maxX, pos.y, 0);
            }
            if( pos.x > maxX )
            {
                transform.position = new Vector3(maxX, pos.y, 0);
            }
            if ( pos.y < minY )
            {
                transform.position = new Vector3(pos.x, maxY, 0);
            }
            if( pos.y > maxY)
            {
                transform.position = new Vector3(pos.x, minY, 0);
            }
        }
    }
}
