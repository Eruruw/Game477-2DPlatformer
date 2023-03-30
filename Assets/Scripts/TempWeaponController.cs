using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Weapon
{
    Whip,
    Fireball,
    HolyWater,
}

public class TempWeaponController : MonoBehaviour
{
    public static int WhipDamage = 1;
    public static int FireballDamage = 1;
    public static int HolyWaterDamage = 2;
}
