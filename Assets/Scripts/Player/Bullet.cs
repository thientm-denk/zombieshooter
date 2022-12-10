using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update

    public int buletDamage = 10;
    Rigidbody rb;
    private bool hitTaget = false;
    RaycastHit collision;
    public void ChangeDamage(int newDamage)
    {
        buletDamage = newDamage;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Zombie")
        {
            AudioManager.Play(AudioName.ZombieHitted);
            if (collision.collider.name == "Z_Head")
            {
                collision.transform.parent.gameObject.GetComponent<Health>().GotHit(buletDamage);
            }
            else
            {
                collision.transform.parent.gameObject.GetComponent<Health>().GotHit(buletDamage);
            }

        }
        rb.velocity = new Vector3(0, 0, 0);
        Pool.instance.BackToPool(this.gameObject);
    }


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        hitTaget = false;
    }
    // Update is called once per frame
    void Update()
    {
        
        if (!HandleHitWithRaycast(out collision) && !hitTaget) // ban ko dinh thi ko lam gi 
        {
            return;
        }

        if(!hitTaget) // farme nay ban ray cast dinh -> chuyen trang thai sang true de frame sau moi xu ly vien dan
        {
            hitTaget = true;
            return; // frame nay return de ko xu ly dan
        }


        // ban dinh thi xu ly

        if(collision.collider == null) // ban nham (2 vien cung muc tieu, vien 1 tieu diet dich thi mat khung nen null pointer)
        {
            hitTaget = false;
            return;
        }
        if (collision.collider.name == "AttackHitBox") // bo qua attackhitbox
        {
            hitTaget = false;
            return;
        }
        if (collision.collider.tag == "Zombie")
        {
            AudioManager.Play(AudioName.ZombieHitted);
            if (collision.collider.name == "Z_Head")
            {
                collision.transform.parent.gameObject.GetComponent<Health>().GotHit(buletDamage*3);
            }
            else
            {
                collision.transform.parent.gameObject.GetComponent<Health>().GotHit(buletDamage);
            }
        }
        rb.velocity = new Vector3(0, 0, 0);
        Pool.instance.BackToPool(this.gameObject);


    }
    /// <summary>
    /// ban dinh thi true, tra ve coll
    /// </summary>
    /// <param name="coll"></param>
    /// <returns></returns>
    private bool HandleHitWithRaycast(out RaycastHit coll)
    {
        RaycastHit collision;

        if (Physics.Raycast(transform.position, transform.forward, out collision, rb.velocity.magnitude)) // ray doan truoc duong dan
        {

            //Debug.Log("Ban dinh: " + collision.collider.gameObject.name);
            coll = collision;
            return true;

            
        }

        coll = collision;
        return false;
        
        
    }
}
