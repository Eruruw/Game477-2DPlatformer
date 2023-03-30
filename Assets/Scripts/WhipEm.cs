using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhipEm : MonoBehaviour
{
    HPController HPCon;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "whip")
        {
            Debug.Log("Whipped em");
            HPCon.TakeDamageFromWeapon(Weapon.Whip);
        }
    }

    void Start()
    {
        HPCon = GetComponent<HPController>();
    }
}
