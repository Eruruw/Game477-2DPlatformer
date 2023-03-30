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
        if (other.gameObject.tag == "Projectile")
        {
            Debug.Log("Popped em");
            HPCon.TakeDamageFromWeapon(Weapon.Fireball);
        }
    }

    void Start()
    {
        HPCon = GetComponent<HPController>();
    }
}
