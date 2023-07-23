using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour, IObserverGenericBar
{
    private float _maxLife;

    [SerializeField]
    private GameObject _entityForValidate;
    private IObservableToGenericBar _entity;
    [SerializeField]
    private Image _lifeBar;
    private void OnValidate()
    {
        _entity =_entityForValidate.GetComponent<IObservableToGenericBar>();
        if (_entityForValidate !=null)
        {
            _entity.Suscribe(this);
        }
        else
        {
            Debug.LogError("Error fatal, no se asigno entidad a esta Lifebar");
            return;
        }
    }

    void Start()
    {
     
        _lifeBar = GetComponent<Image>();
        if (_lifeBar == null)
        {
            Debug.LogError("Error fatal Lifebar no tiene el componente Image");
        }
    }
    public void RefreshValue(float value)
    {
        _lifeBar.fillAmount = value / _maxLife;
    }

    public void SetMaxValue(float value)
    {
        _maxLife = value;
    }

    public void NotifyBarIsEmpty(bool barIsEmpty)
    {
        throw new System.NotImplementedException();
    }
}
