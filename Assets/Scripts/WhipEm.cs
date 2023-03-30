using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhipEm : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Whipped em");
    }
}
