using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Player : MonoBehaviour
{
    // Socre
    public int score;
    

    // WEAPON MANAGER
    public int currentWeapon = 1; // 1 sung truong , 2 sung luc, 3 luu dan
    Weapon weapon;
    Nade nade;
    Assault assault;
    Pistol pistol;
    // UI MANAGER
    public UIManager uiManager;

    // SHOOT SUPPORT
    [SerializeField]
    Camera PlayerCamera;
    [SerializeField]
    GameObject ShootPoint;
    public LayerMask layerMaskShoot;
    public float buttletSpeed;
    public bool canShoot;
    // Gun information
    public const int DEFAULT_BULET_AMOUNT = 6;
    public float reloadTime = 3f;
    public int bulletAmount = DEFAULT_BULET_AMOUNT;

    // Reload and Bulit Sup
    public bool isReloading;
    public bool outOfBulet;


    // Delay support
    public float delayTime = 0.1f;
    public bool isDelay;

    // Pool bulet
    Pool objectPool;

    // Animator of gun
    [SerializeField]
    Animator gunAnim;
    //private int indexAnim = 0; // idle = 0, shoot = 1

    [SerializeField]
    LineRenderer lr;

    //Is alive or not
    public bool isAlive;

    // Start is called before the first frame update
    void Start()
    {
        pistol = GetComponentInChildren<Pistol>();
        assault = GetComponentInChildren<Assault>();
        pistol.gameObject.SetActive(false);
        pistol.gameObject.SetActive(false);
        objectPool = Pool.instance;
        ChangeWeapon(assault);


        OnInit();
       
    }

    // Update is called once per frame
    void Update()
    {


        //if (!isAlive)
        //{
        //    return;
        //}


        //CheckAlive();
        //if (Time.timeScale == 0)
        //{
        //    return;
        //}

        //uiManager.ChangeBulet(bulletAmount);
        //if (isReloading)
        //{
        //    uiManager.ActiveReloadMessage();
        //}
        //else
        //{
        //    uiManager.DeActiveReloadMessage();
        //}

        //if (canShoot)
        //{
        //    DrawLineRender();
        //}
        //else
        //{
        //    ClearLineRender();
        //}




        //if (canShoot && Input.GetMouseButton(0) && !isReloading)//  nhap chuot thi ban (neu ban dc)
        //{
        //    ShootV2();
        //    MinusBulet(1); // tru 1 vien dan moi lan shoot het dan thi outObBulet = true
        //    canShoot = false;

        //    //Debug.Log(bulletAmount);
        //}
        //if(!canShoot && !isReloading && !outOfBulet) // ko het dan, khong reaload nhung ko the ban -> dang delay
        //{
        //    if (!isDelay)
        //    {
        //        isDelay = true;
        //        Invoke(nameof(Cooler), delayTime);
        //    }
        //}

        //if (outOfBulet) // het dan thi gai dan
        //{

        //    if (!isReloading)
        //    {
        //        isReloading = true;
        //        Invoke(nameof(Reload),3f);
        //    }
        //}

        //if(!isReloading && Input.GetKeyDown(KeyCode.R) && bulletAmount < DEFAULT_BULET_AMOUNT) // chua gai dan ma ban R 
        //{
        //    canShoot = false;
        //    isReloading = true;
        //    Invoke(nameof(Reload), 3f);
            
        //}



        if (!isAlive) // chet
        {
            return;
        }
        CheckAlive();
        if (Time.timeScale == 0) // pause
        {
            return;
        }

        

        uiManager.ChangeBuletLeftText(weapon.amoutLeft, weapon.MaxAmoutOfBulet); // so luong dan

        if (isReloading)
        {
            uiManager.ActiveReloadMessage();
        }
        else
        {
            uiManager.DeActiveReloadMessage();
        }

        if (canShoot)
        {
            weapon.DrawLineRender(PlayerCamera);
        }
        else
        {
            weapon.ClearLineRender();
        }




        if (canShoot && Input.GetMouseButton(0) && !isReloading)//  nhap chuot thi ban (neu ban dc)
        {
            ShootV3();
            MinusBuletV2(1); // tru 1 vien dan moi lan shoot het dan thi outObBulet = true

            canShoot = false;

            //Debug.Log(bulletAmount);
        }
        if(!canShoot && !isReloading && !outOfBulet) // ko het dan, khong reaload nhung ko the ban -> dang delay
        {
            if (!isDelay)
            {
                isDelay = true;
                Invoke(nameof(CoolerV2), weapon.delayTime);
            }
        }

        if (outOfBulet) // het dan thi gai dan
        {

            if (!isReloading)
            {
                isReloading = true;
                weapon.PlayReloadSound();
                Invoke(nameof(ReloadV2), weapon.reloadTime);
            }
        }

        if(!isReloading && Input.GetKeyDown(KeyCode.R) && weapon.amoutLeft < weapon.MaxAmoutOfBulet) // chua gai dan ma ban R 
        {
            canShoot = false;
            isReloading = true;
            weapon.PlayReloadSound();
            Invoke(nameof(ReloadV2), weapon.reloadTime);
            
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeWeapon(assault);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeWeapon(pistol);
        }
    }

    private void OnInit()
    {
        score = 0;
        isAlive = true;
        uiManager = UIManager.Instance;
        canShoot = true;
        bulletAmount = DEFAULT_BULET_AMOUNT;
        outOfBulet = false;
        isReloading = false;
        isDelay  = false;
        uiManager.StartGameInit();
    }

   
    #region Shoot Method
    private void Shoot()
    {
        var bulet = objectPool.GetAndActivePooledObject(ShootPoint.transform.position);
        bulet.transform.rotation = ShootPoint.transform.rotation;
        bulet.GetComponent<Rigidbody>().AddForce(bulet.transform.forward * buttletSpeed, ForceMode.Impulse);
        ChangeAnimShoot();

    }

    private void MinusBulet(int amount)
    {
        bulletAmount -= amount;
        
        if (bulletAmount <= 0)
        {

            canShoot = false;
            outOfBulet = true;

        }
        
    }

    private void Reload()
    {
        bulletAmount = DEFAULT_BULET_AMOUNT;
        isReloading = false;
        canShoot = true;
        outOfBulet = false;
    }
    private void Cooler()
    {
        canShoot = true;
        isDelay = false;
    }

    #endregion

    #region Anim and LineRender

    public void ChangeAnimShoot()
    {
        gunAnim.ResetTrigger("Shoot");
        gunAnim.SetTrigger("Shoot");
        
    }
 
    public void DrawLineRender()
    {
        lr.positionCount = 2;
        lr.SetPosition(0, ShootPoint.transform.position);

        RaycastHit[] allHits;
        allHits = Physics.RaycastAll(PlayerCamera.transform.position, PlayerCamera.transform.forward, 100f);
        foreach (var hit in allHits)
        {

            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Enemy")) // gap thang dau tien ko phai player
            {
                Debug.Log(hit.collider.name);
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

    }
    public void ClearLineRender()
    {
        lr.positionCount = 0;
    }

    #endregion

    private bool CheckAlive()
    {
        if (gameObject.GetComponent<Health>().currentHealth == 0) // chet
        {

            isAlive = false;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            UIManager.Instance.UpdateHighScore(score);
            UIManager.Instance.ShowLoseMenu();


            return false;
        }
        return true;
    }

    #region Shooting V2 With Ray

    private void ShootV2()
    {
 

        // This would cast rays only against colliders in layer 8, so we just inverse the mask.
        // Uu tien enemy hon
        RaycastHit[] allHits;
        allHits = Physics.RaycastAll(PlayerCamera.transform.position, PlayerCamera.transform.forward, 100f);
        foreach (var hit in allHits)
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Enemy")) 
            {

                var bulet = objectPool.GetAndActivePooledObject(ShootPoint.transform.position); 
                bulet.transform.LookAt(hit.point);

                bulet.GetComponent<Rigidbody>().AddForce(bulet.transform.forward * buttletSpeed, ForceMode.Impulse); 
                ChangeAnimShoot();
                return;
            }
        }

        allHits = Physics.RaycastAll(PlayerCamera.transform.position, PlayerCamera.transform.forward, 100f);
        foreach (var hit in allHits)
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("ShootObject")) // gap thang dau tien ko phai player
            {
                
                var bulet = objectPool.GetAndActivePooledObject(ShootPoint.transform.position); // tao vien dan ngay hong sung
                bulet.transform.LookAt(hit.point);

                bulet.GetComponent<Rigidbody>().AddForce(bulet.transform.forward * buttletSpeed, ForceMode.Impulse); // ban
                ChangeAnimShoot();
                return;
            }
        }



        Shoot();

     
        

    }

    #endregion

    #region Gun Manager
    private void OnInitChangeWeapon()
    {
        canShoot = true;
        outOfBulet = false;
        isReloading = false;
        isDelay = false;
        CancelInvoke();
    }
    private void ChangeWeapon(Weapon weapon)
    {
        if(this.weapon != weapon && this.weapon != null)
        {
            this.weapon.gameObject.SetActive(false);
        }

        if(this.weapon != weapon)
        {
            this.weapon = weapon;
            weapon.gameObject.SetActive(true);
            OnInitChangeWeapon();
            
        }
    }

    private void ShootV3()
    {
        weapon.Shoot(PlayerCamera);
    }

    private void MinusBuletV2(int amount)
    {
        if (!weapon.MinusBulet(amount)) // false la het dan
        {
            canShoot = false;
            outOfBulet = true;
        };

      
    }

    private void ReloadV2()
    {
        weapon.Reload();
        isReloading = false;
        canShoot = true;
        outOfBulet = false;
        uiManager.ChangeBuletLeftText(weapon.amoutLeft, weapon.MaxAmoutOfBulet);
    }
    private void CoolerV2()
    {
        canShoot = true;
        isDelay = false;
    }

    #endregion


    public void addScore()
    {
        
        score+= Config.scoreForEachZombie;
        UIManager.Instance.ChangeIngameScore(score);

    }
}
