using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField]
    public GameObject ShootPoint;
    public int MaxAmoutOfBulet;
    public float reloadTime;
    public float buletSpeed;
    public int amoutLeft;
    public float delayTime;
    public int damage;


    // Animator of gun
    [SerializeField]
    public Animator gunAnim;
    [SerializeField]
    public LineRenderer lr;

    public abstract void Shoot(Camera PlayerCamera);
    public abstract void DrawLineRender(Camera PlayerCamera);
    public abstract void ClearLineRender();
    /// <summary>
    /// Tru dan trong bang, tra ve true la tru binh thuong, false la het dan
    /// </summary>
    /// <param name="amount"></param>
    /// <returns></returns>
    public abstract bool MinusBulet(int amount);
    public abstract bool Reload();
    public abstract bool PlayReloadSound();


    public void ChangeAnimShoot()
    {
        gunAnim.ResetTrigger("Shoot");
        gunAnim.SetTrigger("Shoot");

    }
}
