using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{

    private void Start()
    {
        MaxAmoutOfBulet = 6;
        reloadTime = 1f;
        buletSpeed = 40f;
        amoutLeft = MaxAmoutOfBulet;
        delayTime = 0.5f;
        damage = 30;
    }


    public override void ClearLineRender()
    {
        lr.positionCount = 0;
    }

    public override void DrawLineRender(Camera PlayerCamera)
    {
        lr.positionCount = 2;
        lr.SetPosition(0, ShootPoint.transform.position);

        RaycastHit[] allHits;
        allHits = Physics.RaycastAll(PlayerCamera.transform.position, PlayerCamera.transform.forward, 100f);
        foreach (var hit in allHits)
        {

            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Enemy") && hit.collider.GetComponentInParent<ZombieBehavior>().isAlive) // gap thang dau tien ko phai player
            {

                lr.SetPosition(1, hit.point);
                lr.startColor = Color.green;
                lr.endColor = Color.green;
                return;
            }
        }

        allHits = Physics.RaycastAll(PlayerCamera.transform.position, PlayerCamera.transform.forward, 100f);
        foreach (var hit in allHits)
        {

            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("ShootObject")) // gap thang dau tien ko phai player
            {
                //Debug.Log(hit.collider.name);
                lr.SetPosition(1, hit.point); // ve~

                if (hit.collider.tag == "Zombie")
                {
                    lr.startColor = Color.green;
                    lr.endColor = Color.green;
                }
                else
                {
                    lr.startColor = Color.red;
                    lr.endColor = Color.red;
                }
                return;
            }
        }
         lr.SetPosition(1, ShootPoint.transform.position + ShootPoint.transform.forward * 100f);
    }
    public override void Shoot(Camera PlayerCamera)
    {
        RaycastHit[] allHits;
        allHits = Physics.RaycastAll(PlayerCamera.transform.position, PlayerCamera.transform.forward, 100f);
        AudioManager.Play(AudioName.PistolShoot);
        foreach (var hit in allHits)
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Enemy") && hit.collider.GetComponentInParent<ZombieBehavior>().isAlive)
            {

                var bulet = Pool.instance.GetAndActivePooledObject(ShootPoint.transform.position);
                bulet.transform.LookAt(hit.point);
                bulet.GetComponent<Bullet>().ChangeDamage(damage);
                bulet.GetComponent<Rigidbody>().AddForce(bulet.transform.forward * buletSpeed, ForceMode.Impulse);
                ChangeAnimShoot();
                return;
            }
        }

        allHits = Physics.RaycastAll(PlayerCamera.transform.position, PlayerCamera.transform.forward, 100f);
        foreach (var hit in allHits)
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("ShootObject")) // gap thang dau tien ko phai player
            {

                var bulet = Pool.instance.GetAndActivePooledObject(ShootPoint.transform.position); // tao vien dan ngay hong sung
                bulet.transform.LookAt(hit.point);
                bulet.GetComponent<Bullet>().ChangeDamage(damage);
                bulet.GetComponent<Rigidbody>().AddForce(bulet.transform.forward * buletSpeed, ForceMode.Impulse); // ban
                ChangeAnimShoot();
                return;
            }
        }

        var buletx = Pool.instance.GetAndActivePooledObject(ShootPoint.transform.position);
        buletx.transform.rotation = ShootPoint.transform.rotation;
        buletx.GetComponent<Bullet>().ChangeDamage(damage);
        buletx.GetComponent<Rigidbody>().AddForce(buletx.transform.forward * buletSpeed, ForceMode.Impulse);
        ChangeAnimShoot();
    }

    public override bool MinusBulet(int amount)
    {
        amoutLeft -= amount;

        if (amoutLeft <= 0)
        {
            return false;

        }
        return true;
    }

    public override bool Reload()
    {
        amoutLeft = MaxAmoutOfBulet;
        return true;
    }

    public override bool PlayReloadSound()
    {
        AudioManager.Play(AudioName.PistolReload);
        return true;
    }
}
