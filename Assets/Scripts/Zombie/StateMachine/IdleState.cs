using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    float timer;
    float ranTime;
    /// <summary>
    /// khi vao idle state thi dung yen x giay
    /// </summary>
    /// <param name="enemy"></param>
    public void OnEnter(ZombieBehavior enemy)
    {
        //enemy.StopMoving();
        timer = 0;
        ranTime = Random.Range(1f, 2f);
    }

    public void OnExecute(ZombieBehavior enemy)
    {
        
        timer += Time.deltaTime;
        if(timer > ranTime && enemy.isAlive) // sau 1-2s thi bat dau di chuyen ve phia nguoi choi
        {
            enemy.ChangeState(new RunToAttackState());
        }
    
    }

    public void OnExit(ZombieBehavior enemy)
    {
        
    }
}
