using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ChidlsObserver 
{
    void Notify(DetectionChild.TypeOfRange _typeOfRange, bool InOrOut);
}
