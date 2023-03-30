using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WHIPEM : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("whipped em");
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("whipped em");
        }
    }

    void Update()
    {
        Debug.Log("I'm active");
    }
}
