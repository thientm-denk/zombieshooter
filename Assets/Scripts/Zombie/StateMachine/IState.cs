using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState 
{
    // Bat dau vao state
    void OnEnter(ZombieBehavior enemy);
    //update state
    void OnExecute(ZombieBehavior enemy);
    // ket thuc ra khoi state
    void OnExit(ZombieBehavior enemy);
}
