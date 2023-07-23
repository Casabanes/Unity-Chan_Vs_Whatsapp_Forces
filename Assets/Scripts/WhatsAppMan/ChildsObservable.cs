using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ChildsObservable
{

    void Suscribe(ChidlsObserver brain);
    void Notify(DetectionChild.TypeOfRange typeOfRange,bool InOrOut);
}
