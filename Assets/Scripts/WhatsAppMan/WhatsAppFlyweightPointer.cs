using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhatsAppFlyweightPointer : MonoBehaviour
{
    public static readonly WhatsAppFlyweight BaseWhatsAppMan = new WhatsAppFlyweight
    {

        _physicalAttack = "_physicalAttack",
        _magicalAttack = "_magicalAttack",
        _running = "_isRunning",
        _speed=3
    };
    public static readonly WhatsAppFlyweight BaseWhatsAppManKnuckles = new WhatsAppFlyweight
    {
        _damage = 10,
        _minAttackPitch = 0.8f,
        _maxAttackPitch = 1.2f,
        _ignoreLayer = 10
    };

}
