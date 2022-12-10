using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitBox : MonoBehaviour
{
    // Start is called before the first frame update
  
    //float currentTime = 0f;
    //public float attackDelay;
    //public bool canHit;
    public int attackDamge = 10;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            AudioManager.Play(AudioName.PlayerHited);
            other.GetComponent<Health>().GotHit(attackDamge);
            other.GetComponent<Player>().uiManager.GotHitEffect(GetComponentInParent<ZombieBehavior>().attackDelay);
            UIManager.Instance.ChangeHealth(other.GetComponent<Health>().currentHealth);
            
            this.gameObject.SetActive(false);
      
        }
    }


    //private void OnEnable()
    //{
    //    currentTime = 0f;
    //    canHit = true;
    //}

   
    //void Start()
    //{
    //    attackDelay = GetComponentInParent<ZombieBehavior>().attackDelay;
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    currentTime += Time.deltaTime;

    //    if(currentTime >= attackDelay)
    //    {
    //        canHit = true;
    //    }
    //}

    //private void OnDisable()
    //{
    //    canHit = false;
    //}
}
