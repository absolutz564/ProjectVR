using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameController._instance.TriggerColision(other);
        Debug.Log("Colidiu com " + other.name + "  " + other.tag);
        Destroy(other);
    }
}
