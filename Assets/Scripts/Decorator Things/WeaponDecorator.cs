using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponDecorator : Weapon
{
    protected Weapon weapon;
    public WeaponDecorator (Weapon weaponD)
    {
        weapon = weaponD;
    }
}
