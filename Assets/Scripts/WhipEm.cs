using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhipEm : MonoBehaviour
{
    HPController HPCon;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Whip")
        {
            Debug.Log("Whipped em");
            HPCon.TakeDamageFromWeapon(Weapon.Whip);
        }
        if (other.gameObject.tag == "Fire")
        {
            Debug.Log("Popped em");
            HPCon.TakeDamageFromWeapon(Weapon.Fireball);
        }
        if (other.gameObject.tag == "Holy")
        {
            Debug.Log("Purified em");
            HPCon.TakeDamageFromWeapon(Weapon.HolyWater);
        }
    }

    void Start()
    {
        HPCon = GetComponent<HPController>();
    }
}
