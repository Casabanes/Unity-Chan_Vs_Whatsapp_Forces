using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DecoratorPowerUp : MonoBehaviour
{
    [SerializeField]
    protected WeaponDecorator _decoratorToAdd;
    [SerializeField]
    protected Weapon weapon;
    private void OnCollisionEnter(Collision collision)
    {
        weapon = collision.gameObject.GetComponent<Weapon>();
        if (weapon == null)
        {
            weapon = collision.gameObject.GetComponentInChildren<Weapon>();
        }
        if (weapon != null)
        {
            CreateDecorator();
        }
    }
    protected virtual void CreateDecorator()
    {

    }
}
