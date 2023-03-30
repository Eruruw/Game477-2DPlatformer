using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using WC = TempWeaponController;
using IC = ItemController;

public class HPController : MonoBehaviour
{       
    public int CurrentHP;
    public int HPCap;
    public float DeathDuration = 3f;
    public bool IsStillKickinIt;

    WC wc;
    IC ic;

    bool CD;

    public void TakeDamageFromWeapon(Weapon weapon)
    {
        switch (weapon)
        {
            case Weapon.Whip:
                if (CurrentHP - WC.WhipDamage <= 0)
                {
                    CurrentHP = 0;
                    IsStillKickinIt = false;
                    GetComponent<EnemyStateMachine>().enabled = false;
                    StartCoroutine(Tragedy());
                }
                else
                {
                    CurrentHP -= WC.WhipDamage;
                }
                break;
            case Weapon.Fireball:
                if (CurrentHP - WC.FireballDamage <= 0)
                {
                    CurrentHP = 0;
                    IsStillKickinIt = false;
                    GetComponent<EnemyStateMachine>().enabled = false;
                    StartCoroutine(Tragedy());
                }
                else
                {
                    CurrentHP -= WC.FireballDamage;
                }
                break;
            case Weapon.HolyWater:
                if (CurrentHP - WC.HolyWaterDamage <= 0)
                {
                    CurrentHP = 0;
                    IsStillKickinIt = false;
                    GetComponent<EnemyStateMachine>().enabled = false;
                    StartCoroutine(Tragedy());
                }
                else
                {
                    CurrentHP -= WC.HolyWaterDamage;
                }
                break;
            case Weapon.Enemy:
                if (CurrentHP - WC.EnemyDamage <= 0)
                {
                    CurrentHP = 0;
                    IsStillKickinIt = false;
                    StartCoroutine(Tragedy());
                }
                else
                {
                    CurrentHP -= WC.EnemyDamage;
                }
                break;
            default:
                break;
        }
    }

    IEnumerator Tragedy()
    {
        Debug.Log("Currently dying");
        if (gameObject.tag != "Player")
        {
            gameObject.tag = "Untagged";
        }
        var physics = gameObject.GetComponent("Rigidbody2D");
        var hitbox = gameObject.GetComponent("BoxCollider2D");
        Destroy(physics);
        Destroy(hitbox);
        yield return new WaitForSeconds(DeathDuration);
        Debug.Log("Dead");
        if (gameObject.tag == "Player") {
            SceneManager.LoadScene("Start Scene");
        }
        Destroy(gameObject);
    }

    // lol
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
        IsStillKickinIt = true;
    }
}
