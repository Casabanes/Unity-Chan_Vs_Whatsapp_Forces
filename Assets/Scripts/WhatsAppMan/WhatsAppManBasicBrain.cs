using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhatsAppManBasicBrain : WhatsAppManBaseBrain, ChidlsObserver
{
	[SerializeField]
	private List<GameObject> _childsObjects;
	private List<ChildsObservable> _childs = new List<ChildsObservable>();
	private List<SphereCollider> _childsCC = new List<SphereCollider>();
	private Dictionary<DetectionChild.TypeOfRange,bool> _boolList = new Dictionary<DetectionChild.TypeOfRange, bool>();

	[SerializeField]
	private float _distance;
	[SerializeField]
	private float _minDistanceToPlayer;
	[SerializeField]
	private WhatsAppMan _whatsAppMan;
	[SerializeField]
	private DetectionChild.TypeOfRange[] _priorityAction;
	private IAction _action;

	private void OnValidate()
	{
		_childs.Clear();
		_childsCC.Clear();
		_boolList.Clear();
		for (int i = 0; i < _childsObjects.Count; i++)
		{
			_childs.Add(_childsObjects[i].GetComponent<ChildsObservable>());
			_childsCC.Add(_childsObjects[i].GetComponent<SphereCollider>());
			if (_childs[i] != null)
			{
				_childs[i].Suscribe(this);
			}
			else
			{
				Debug.LogError("Error fatal, el child numero:" + i + " no contiene el script con IObservable");
				return;
			}
		}
	}
	public void Notify(DetectionChild.TypeOfRange typeOfRange, bool InOrOut)
	{
		SelectAction(typeOfRange,InOrOut);
	}
	private void SelectAction(DetectionChild.TypeOfRange typeOfRange, bool InOrOut)
	{
			_boolList[typeOfRange] = InOrOut;
	}
	public IAction TakeDecision()
	{
		_action = null;
		CalculateDistance();
		if (_priorityAction != null && _priorityAction.Length>0)
        {
            for (int i = 0; i <_priorityAction.Length; i++)
            {
                switch (_priorityAction[i])
                {
					case (DetectionChild.TypeOfRange.melee):
						_action = CheckMeleeAttack();
						break;
					case (DetectionChild.TypeOfRange.range):
						_action = CheckRangeAttack();
						break;
					case (DetectionChild.TypeOfRange.chase):
						_action = CheckChase();
						break;
					default:
						_action = _actionIdle;
						break;
				}
            }
		}
        if (_action == null)
        {
			_action= _actionIdle;
        }
			return _action;
	}
	private float CalculateDistance()
    {
		return _distance = Vector3.Distance(_player.transform.position, _whatsAppMan.transform.position);
    }
	public override void OrdersToBody()
	{
		_whatsAppMan.ChangeAction(TakeDecision());
		
	}
	private IAction CheckMeleeAttack()
	{
		if (_action != null)
		{
			return _action;
		}
		if (_boolList.ContainsKey(DetectionChild.TypeOfRange.melee))
		{
			if (_boolList[(DetectionChild.TypeOfRange.melee)])
			{
				return _actionPunch;
			}
		}

		return null;
	}
	private IAction CheckRangeAttack()
	{
		if (_action != null)
		{
			return _action;
		}
		if (_boolList.ContainsKey(DetectionChild.TypeOfRange.range))
			{
				if (_boolList[(DetectionChild.TypeOfRange.range)])
				{
					return _actionMagic;
				}
		}
		return null;
	}
	private IAction CheckChase()
	{
		if (_action != null)
		{
			return _action;
		}
			if (_boolList.ContainsKey(DetectionChild.TypeOfRange.chase))
			{
				if (_boolList[(DetectionChild.TypeOfRange.chase)] && CalculateDistance()> _minDistanceToPlayer)
				{
					return _actionRun;
				}
			}
		return null;
	}
}
