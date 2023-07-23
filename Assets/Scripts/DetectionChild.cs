using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DetectionChild : MonoBehaviour, ChildsObservable
{
    [SerializeField]
    public TypeOfRange _typeOfRange;
    [SerializeField]
    protected int _layerToCheck;
    private ChidlsObserver _detectionChildObserver;

    public virtual void Suscribe(ChidlsObserver detectionChildObserver)
    {
        _detectionChildObserver = detectionChildObserver;
    }
    public virtual void Notify(TypeOfRange _typeOfRange, bool InOrOut)
    {
        _detectionChildObserver.Notify(_typeOfRange, InOrOut);
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == _layerToCheck)
        {
            Notify(_typeOfRange, true);
        }
    }
    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == _layerToCheck)
        {
            Notify(_typeOfRange, false);
        }
    }

    public enum TypeOfRange
    {
        melee, range, chase
    }
}
