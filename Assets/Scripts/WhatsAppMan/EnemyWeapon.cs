using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider))]
public abstract class EnemyWeapon : MonoBehaviour
{
    [SerializeField]
    protected BoxCollider[] _colliders;
    [SerializeField]
    protected AudioSource _audioSource;
    [SerializeField]
    protected AudioClip[] _attackSound;
    public bool _isActive;
    public virtual void Start()
    {
        _colliders = GetComponents<BoxCollider>();
    }

    public virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != WhatsAppFlyweightPointer.BaseWhatsAppManKnuckles._ignoreLayer && collision.gameObject.layer != 0)
        {
            ContactPoint particlePosition = collision.contacts[0];
            IDamageable idamageableObject = collision.gameObject.GetComponent<IDamageable>();
            if (idamageableObject != null)
            {
                idamageableObject.TakeDamage(WhatsAppFlyweightPointer.BaseWhatsAppManKnuckles._damage, particlePosition.point);
            }
            OnOffColliders(false);
        }
    }
    public virtual void AttackSound()
    {
        _audioSource.clip = _attackSound[Random.Range(0, _attackSound.Length - 1)];
        _audioSource.pitch = Random.Range(WhatsAppFlyweightPointer.BaseWhatsAppManKnuckles._minAttackPitch, WhatsAppFlyweightPointer.BaseWhatsAppManKnuckles._maxAttackPitch);
        _audioSource.Play();
    }
    public virtual void OnOffColliders(bool onOff)
    {
        for (int count = 0; count < _colliders.Length; count++)
        {
            _colliders[count].enabled = onOff;
        }
    }
}