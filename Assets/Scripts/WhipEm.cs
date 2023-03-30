using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhipEm : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "whip")
        {
            Debug.Log("Whipped em");
        }
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("didnt whip em but touched em");
        }
    }
}
