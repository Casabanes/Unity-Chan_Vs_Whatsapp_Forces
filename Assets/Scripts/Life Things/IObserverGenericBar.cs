
public interface IObserverGenericBar
{
    void RefreshValue(float value);
    void SetMaxValue(float value);
    void NotifyBarIsEmpty(bool barIsEmpty);

}
