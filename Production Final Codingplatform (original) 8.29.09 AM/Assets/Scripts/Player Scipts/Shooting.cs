using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float Range = 100.0f;
    public float BeamDPS = 1.0f;
    public float DMG;
    public Camera fpsCam;
    public Transform bulletSpawn;
    private float distance;
    private LineRenderer laserLine;
    public int upgrade1 = 0;
    private WaitForSeconds ticker = new WaitForSeconds(1f);
    private bool damageProcessing = false;
    private float heatLevels = 10;
    private bool beamActive = false;
    public bool hasBeamGun = false;
    public bool hasHarpoonGun = false;
    public GameObject Vines;
    public ParticleSystem GunEffect;
    public GameObject Flash;
    public AudioSource Gun;
    public AudioClip Laser;
    public float time;
    private bool shoot;
    private float shootDelay;
    private bool onCool;
    public GameObject Gun1;
    public GameObject Gun2;
    public GameObject Gun3;


    // Update is called once per frame
    void Start()
    {
        laserLine = GetComponent<LineRenderer> ();
        Vines = GameObject.FindWithTag("Vines");
        Gun.clip = Laser;
        GunEffect.Stop();
        shoot = false;
        shootDelay = 1;
        onCool = false;

    }
    void Update()
    {
        if (beamActive == false)
        {
            StartCoroutine(Reloading());
        }
        if (upgrade1 > 0 && upgrade1 < 2)
        {
            if (heatLevels > 0)
            {
                Electrify();
            }
            else
            {
                laserLine.enabled = false;
                beamActive = false;
            }
        }   
                     
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!onCool)
                {
                    Gun.clip = Laser;
                    Gun.Play();
                    Shoot();
                    time = .3f;
                    shoot = true;
                    shootDelay = 1;
                    onCool = true;
                }
            }
             if (Input.GetMouseButtonUp(0) && shootDelay <= 0)
                 onCool = false;
             if (onCool)
                 shootDelay = shootDelay - Time.deltaTime;
             if (shoot)
                 GunFlash();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwapWeapon();
        }
    }
 

    public void Electrify()
    {
         if (Input.GetMouseButton(0) && hasBeamGun == true)
        {
            laserLine.enabled = true;
            beamActive = true;
            RaycastHit Fire;
            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            laserLine.SetPosition(0, bulletSpawn.position);
            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out Fire, Range))
            {
                laserLine.SetPosition(1, Fire.point);
                DamageCalculations target = Fire.collider.GetComponent<DamageCalculations>();
                if (damageProcessing == false)
                {
                    damageProcessing = true;
                    StartCoroutine(BeamDamage(target));
                }
                
            }
            else
            {
                laserLine.SetPosition(1, rayOrigin + (fpsCam.transform.forward * Range));
            }
        }
        else
        {
            beamActive = false;
            laserLine.enabled = false;
        }
        
        //GameObject lightningStrike;
        // lightningStrike = Instantiate(lightning, bulletSpawn1.position, fpsCam.transform.rotation);
       


    }
   void Shoot()
    {
        RaycastHit Hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out Hit, Range))
        {
            Debug.Log(Hit.transform.name);
            DamageCalculations target = Hit.transform.GetComponent<DamageCalculations>();
            if (target != null && target.tag != "Vines" && target.tag != "MainCamera")
            {
                target.TakeDamage(DMG);
                target.ChangeGravity();
           }
        }
    }

    public void SwapWeapon()
    {
        if(upgrade1 == 0 && hasBeamGun == true)
        {
            upgrade1 = 1;
            Gun1.SetActive(false);
            Gun2.SetActive(true);
            Gun3.SetActive(false);
        }
        else if (upgrade1 == 0 && hasBeamGun == false && hasHarpoonGun == true)
        {
            upgrade1 = 2;
            Gun1.SetActive(false);
            Gun2.SetActive(false);
            Gun3.SetActive(true);
        }
        else if (upgrade1 == 1 && hasHarpoonGun == true)
        {
            upgrade1 = 2;
            Gun1.SetActive(false);
            Gun2.SetActive(false);
            Gun3.SetActive(true);
        }
        else if (upgrade1 == 1 && hasHarpoonGun == false)
        {
            upgrade1 = 0;
            Gun1.SetActive(true);
            Gun2.SetActive(false);
            Gun3.SetActive(false);

        }
        else if (upgrade1 == 2)
        {
            upgrade1 = 0;
            Gun1.SetActive(true);
            Gun2.SetActive(false);
            Gun3.SetActive(false);
        }



    }

    private IEnumerator BeamDamage(DamageCalculations target)
    {

        yield return ticker;
        //Debug.Log(damageProcessing);
        heatLevels -= 2;
        if (target != null)
        {
            target.TakeDamage(BeamDPS);
        }
        damageProcessing = false;


    }

    private IEnumerator Reloading()
    {
        yield return new WaitForSeconds(3.0f);
        heatLevels = 10;
    }

    void GunFlash()
    {

        GunEffect.Play();
        Flash.SetActive(true);
        if (time > 0)
        {
            time = time - Time.deltaTime;
        }
        if (time < 0)
        {
            GunEffect.Stop();
            Flash.SetActive(false);
            shoot = false;
            return;
        }
    }
}