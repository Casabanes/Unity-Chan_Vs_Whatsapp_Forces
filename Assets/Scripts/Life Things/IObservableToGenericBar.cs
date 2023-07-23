
public interface IObservableToGenericBar
{
    void Suscribe(IObserverGenericBar observer);
    void NotifyValueToObservers(float value);
    void NotifyIsEmptyToObserver(bool barIsEmpty);

}
