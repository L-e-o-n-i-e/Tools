using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public Material effect;
    
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<MeshRenderer>().materials[0] = effect;
        GameObject.Destroy(this.gameObject);
    }
}
