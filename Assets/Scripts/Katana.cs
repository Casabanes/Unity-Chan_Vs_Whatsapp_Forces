using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Katana : Weapon
{
    public override void Start()
    {
        _colliders = GetComponentsInChildren<BoxCollider>();
        //weapon = new DamagePlus(this); //de esta manera el decorator funciona
    }
}
