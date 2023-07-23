using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent (typeof(BoxCollider))]
public abstract class Weapon : MonoBehaviour
{
    [SerializeField]
    protected BoxCollider[] _colliders;
    [SerializeField]
    protected float _damage;
    [SerializeField]
    protected AudioSource _audioSource;
    [SerializeField]
    protected AudioClip[] _attackSound;
    [SerializeField]
    protected float _minAttackPitch;
    [SerializeField]
    protected float _maxAttackPitch;
    [SerializeField]
    protected int _ignoreLayer;
    public bool _isActive;
    public Weapon weapon;
    public virtual void Start()
    {
        _colliders = GetComponents<BoxCollider>();
        weapon = this;
    }

    public virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != _ignoreLayer && collision.gameObject.layer != 0)
        {
            ContactPoint particlePosition = collision.contacts[0];
            IDamageable idamageableObject = collision.gameObject.GetComponent<IDamageable>();
            if (idamageableObject != null)
            {
                Debug.Log(GetDamage());
                idamageableObject.TakeDamage(_damage, particlePosition.point);
            }
            OnOffColliders(false);
        }
    }
    public virtual void AttackSound()
    {
        _audioSource.clip = _attackSound[Random.Range(0, _attackSound.Length - 1)];
        _audioSource.pitch = Random.Range(_minAttackPitch, _maxAttackPitch);
        _audioSource.Play();
    }
    public virtual void OnOffColliders(bool onOff)
    {
        for (int count = 0; count < _colliders.Length; count++)
        {
            _colliders[count].enabled = onOff;
        }
    }
    public virtual void UpdateDamage(float newDamage)
    {
        _damage = newDamage;
    }
    public virtual float GetDamage()
    {
        return _damage;
    }
}
