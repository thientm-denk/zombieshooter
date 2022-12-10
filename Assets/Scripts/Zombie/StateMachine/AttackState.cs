using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class AttackState : IState
{
    float attacking = 0f;
    public void OnEnter(ZombieBehavior enemy)
    {
        if (enemy.CheckCanAttack() && enemy.isAlive)
        {

            enemy.AttackPlayer();
        }
        else if(enemy.isAlive)
        {
            enemy.ChangeState(new RunToAttackState());
        }
    }

    public void OnExecute(ZombieBehavior enemy)
    {
        attacking += Time.deltaTime;
        if (attacking >= enemy.attackDelay) // dan hxong 0.3s sau truy duoi tiep
        {
            enemy.DeActiveAttackBox();
            enemy.ChangeState(new RunToAttackState());

        }

    }

    public void OnExit(ZombieBehavior enemy)
    {
        
    }
}
