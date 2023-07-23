using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Player))]

public class FootWeapon : Weapon
{
    private Vector3 _direction;
    [SerializeField]
    private float _force=10;
    [SerializeField]
    private Player _player;
    public override void Start()
    {
        base.Start();
        _player = GetComponentInParent<Player>();
        if (_player == null)
        {
            Debug.LogError("Error fatal, el objeto no es hijo de un player");
        }
    }
    public override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        if (collision.gameObject.layer != _ignoreLayer && collision.gameObject.layer != 0)
        {
            IKnockBackable knockBackableObject = collision.gameObject.GetComponent<IKnockBackable>();
            if (knockBackableObject != null)
            {
                _direction = _player.transform.forward;
                knockBackableObject.KnockBack(_direction, _force);
            }
        }
    }
}

