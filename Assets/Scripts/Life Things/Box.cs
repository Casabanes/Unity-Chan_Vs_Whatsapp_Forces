using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]

public class Box : LiveEntityBase,IKnockBackable
{
    [SerializeField]
    private float _timeToDestroyWhenDie=2;
    [SerializeField]
    private Rigidbody _rb;
    [SerializeField]
    private BoxCollider _bc;
    [SerializeField]
    MeshRenderer _mr;

    public override void Start()
    {
        base.Start();
        _rb = GetComponent<Rigidbody>();
        if (_rb == null)
        {
            Debug.LogError("Error Fatal, falta asignar RigidBody al objeto");
            return;
        }
        _bc = GetComponent<BoxCollider>();
        if (_bc == null)
        {
            Debug.LogError("Error Fatal, falta asignar BoxCollider al objeto");
            return;
        }
        _mr = GetComponent<MeshRenderer>();
        if (_mr == null)
        {
            Debug.LogError("Error Fatal, falta asignar MeshRenderer al objeto");
            return;
        }
    }
    public override void Death(Vector3 particlePosition)
    {
        base.Death(particlePosition);
        StartCoroutine(WaitToDestroy());
    }
    public void KnockBack(Vector3 direction, float force)
    {
        _rb.AddForce(direction*force,ForceMode.Impulse);
    }

    private IEnumerator WaitToDestroy()
    {
        _rb.useGravity = false;
        _bc.enabled = false;
        _mr.enabled = false;
        yield return new WaitForSeconds(_timeToDestroyWhenDie);
        DestroySelf();
    }
  
}
