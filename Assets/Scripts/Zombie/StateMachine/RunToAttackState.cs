using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunToAttackState : IState
{
    public void OnEnter(ZombieBehavior enemy)
    {
        if (enemy.CheckCanAttack() && enemy.isAlive) // dan hduoc thi qua danh
        {
            enemy.ChangeState(new AttackState());
        }
    }

    public void OnExecute(ZombieBehavior enemy)
    {
        if (enemy.CheckCanAttack() && enemy.isAlive) // co the danh thi danh
        {
            enemy.ChangeState(new AttackState());
        }
        else if (enemy.isAlive) // ko thi tiep tuc chay danh nguoi choi
        {
            enemy.RunToPlayer();
        }
        
        
    }

    public void OnExit(ZombieBehavior enemy)
    {
        
    }

    
    
}
