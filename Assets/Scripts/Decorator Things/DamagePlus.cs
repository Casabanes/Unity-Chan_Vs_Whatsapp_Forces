using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlus : WeaponDecorator
{
    [SerializeField]
    private float _additionalDamage=5;
    public DamagePlus (Weapon weapon) : base(weapon)
    {
        if (weapon != null)
        {
            weapon.UpdateDamage(weapon.GetDamage()+_additionalDamage);
        }
    }
}
