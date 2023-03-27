using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WC = TempWeaponController;
using IC = ItemController;

public class HPController : MonoBehaviour
{
    public int CurrentHP;
    public int HPCap;

    WC wc;
    IC ic;

    bool CD;

    void TakeDamageFromWeapon(WC.Weapon weapon)
    {
        switch (weapon)
        {
            case WC.Weapon.Whip:
                CurrentHP = CurrentHP - WC.WhipDamage; break;
            default:
                break;
        }
    }

    void HealHPFromItem(IC.Item item)
    {
        switch (item)
        {
            case IC.Item.HPPotion: 
                CurrentHP = CurrentHP + IC.HPPotion_HealAmount; break;
            default:
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        CD = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("0"))
        {
            if (!CD)
                TakeDamageFromWeapon(WC.Weapon.Whip);
            StartCoroutine(DamageCD());
            Debug.Log("OOh ouch! - The Bastard");
        }
    }

    IEnumerator DamageCD()
    {
        CD = true;
        yield return new WaitForSeconds(3);
        CD = false;
    }
}
