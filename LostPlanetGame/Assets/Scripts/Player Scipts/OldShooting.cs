using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldShooting : MonoBehaviour
{
    public float DMG = 35.0f;
    public float Range = 100.0f;
    public Camera fpsCam;
    public GameObject Vines;
    public ParticleSystem GunEffect;
    public GameObject Flash;
    public AudioSource Gun;
    public AudioClip Laser;
    public float time;
    private bool shoot;
    private float shootDelay;
    private bool onCool;


    void Start()
    {
        Vines = GameObject.FindWithTag("Vines");
        Gun.clip = Laser;
        GunEffect.Stop();
        shoot = false;
        shootDelay = 1;
        onCool = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
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
        if (Input.GetKeyUp(KeyCode.F) && shootDelay <=0)
            onCool = false;
        if (onCool)
            shootDelay = shootDelay - Time.deltaTime;
        if (shoot)
            GunFlash();
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

    void GunFlash()
    {

        GunEffect.Play();
        Flash.SetActive(true);
        if (time > 0)
        {
            time=time - Time.deltaTime;
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