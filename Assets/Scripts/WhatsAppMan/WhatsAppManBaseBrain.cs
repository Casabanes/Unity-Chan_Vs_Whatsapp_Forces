using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Player))]
[RequireComponent(typeof(WhatsAppMan))]

public abstract class WhatsAppManBaseBrain : MonoBehaviour
{
    protected Player _player;
    protected IAction _actionIdle;
    protected IAction _actionRun;
    protected IAction _actionPunch;
    protected IAction _actionMagic;
    protected WhatsAppMan _body;

    public WhatsAppManBaseBrain SetPlayer(Player player)
    {
        _player = player;
        return this;
    }
    public WhatsAppManBaseBrain SetIdle(IAction action)
    {
        _actionIdle = action;
        return this;
    }
    public WhatsAppManBaseBrain SetRun(IAction action)
    {
        _actionRun = action;
        return this;
    }
    public WhatsAppManBaseBrain SetPunch(IAction action)
    {
        _actionPunch = action;
        return this;
    }
    public WhatsAppManBaseBrain SetMagic(IAction action)
    {
        _actionMagic = action;
        return this;
    }
    public WhatsAppManBaseBrain SetBody(WhatsAppMan whatsAppMan)
    {
        _body=whatsAppMan;
        return this;
    }
    public virtual void OrdersToBody()
    {

    }
}
