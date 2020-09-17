using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    [SerializeField] protected bool canAttackBreak = false;
    protected bool attackInit = false;

    public bool CanAttackBreak() 
    {
        return canAttackBreak;
    }

    public abstract bool CanAttack();
    public abstract void StartAttack();
    public abstract void Break();
}
