using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlusPickup : MonoBehaviour
{
    Weapon weapon;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ola1");

        weapon = other.gameObject.GetComponent<Weapon>();
        if (weapon == null)
        {
            Debug.Log("ola2");

            weapon = other.gameObject.GetComponentInChildren<Weapon>();
        }
        if (weapon != null)
        {
            Debug.Log("ola3");
            weapon.weapon = new DamagePlus(weapon); //de esta manera el decorator funciona a partir del segundo golpe, y no muestra el daño en el inspector
            weapon.weapon = new DamagePlus(weapon);
            weapon.weapon = new DamagePlus(weapon);
            weapon.weapon = new DamagePlus(weapon);
            weapon.weapon = new DamagePlus(weapon);
            weapon.weapon = new DamagePlus(weapon);
            weapon.weapon = new DamagePlus(weapon);
            weapon.weapon = new DamagePlus(weapon);
            weapon.weapon = new DamagePlus(weapon);
            weapon.weapon = new DamagePlus(weapon);


        }
    }
}
