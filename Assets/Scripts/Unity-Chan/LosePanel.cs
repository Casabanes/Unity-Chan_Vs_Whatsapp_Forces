using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LosePanel : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private Buttons _continueButton;
    [SerializeField]
    private Buttons _mainMenuButton;
    private void OnValidate()
    {
        _animator.GetComponent<Animator>();
        if (_animator == null)
        {
            Debug.LogError("Error fatal este objeto no tiene animator");
            return;
        }
    }
    private void Start()
    {
        EnableAndDisableButtons(false);
        EventManager.Suscribe(EventManager.EventType.GameOver, Defeat);

    }
    public void Defeat(params object[] parameters)
    {
        EnableAndDisableButtons(true);
        _animator.enabled = true;
    }
    public void EnableAndDisableButtons(bool OnOff)
    {
        _continueButton.gameObject.SetActive(OnOff);
        _mainMenuButton.gameObject.SetActive(OnOff);
    }
}
