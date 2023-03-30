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
    bool IsStillKickinIt;

    void TakeDamageFromWeapon(Weapon weapon)
    {
        switch (weapon)
        {
            case Weapon.Whip:
                if (CurrentHP - WC.WhipDamage <= 0)
                {
                    CurrentHP = 0;
                    IsStillKickinIt = false;
                    Debug.Log("Tragic");
                }
                else
                {
                    CurrentHP -= WC.WhipDamage;
                }
                break;
            default:
                break;
        }
    }

    // Need to subtract item from inventory
    void HealHPFromItem(Item item)
    {
        if (CurrentHP == HPCap || !IsStillKickinIt)
        {
            // When/if inventory system is implemented, do not remove the item from the player's inventory
            return;
        }
        switch (item)
        {
            case Item.HPPotion: 
                if (CurrentHP + IC.HPPotion_HealAmount > HPCap) 
                {
                    CurrentHP = HPCap;
                }
                else 
                {
                    CurrentHP += IC.HPPotion_HealAmount; 
                }
                break;
            default:
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        CD = false;
        IsStillKickinIt = true;
    }

    // Update is called once per frame
    void Update()
    {
        /* TEST CODE       
        if (Input.GetKeyDown("0"))
        {
            if (!CD)
            {
                TakeDamageFromWeapon(Weapon.Whip);
                StartCoroutine(DamageCD());
                Debug.Log("OOh ouch! - The Bastard");
            }
        }
        if (Input.GetKeyDown("9"))
        {
            HealHPFromItem(Item.HPPotion);
        }
        */
    }

    IEnumerator DamageCD()
    {
        CD = true;
        yield return new WaitForSeconds(3);
        CD = false;
    }
}
