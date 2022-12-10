using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nade : Weapon
{


    private void Start()
    {
        MaxAmoutOfBulet = 5;
        reloadTime = 2f;
        buletSpeed = 0f;
        amoutLeft = MaxAmoutOfBulet;

    }

    public override void ClearLineRender()
    {
        throw new System.NotImplementedException();
    }


    public override void Shoot(Camera PlayerCamera)
    {
        throw new System.NotImplementedException();
    }

    public override void DrawLineRender(Camera PlayerCamera)
    {
        throw new System.NotImplementedException();
    }

    public override bool MinusBulet(int amount)
    {
        throw new System.NotImplementedException();
    }

    public override bool Reload()
    {
        throw new System.NotImplementedException();
    }

    public override bool PlayReloadSound()
    {
        throw new System.NotImplementedException();
    }
}
