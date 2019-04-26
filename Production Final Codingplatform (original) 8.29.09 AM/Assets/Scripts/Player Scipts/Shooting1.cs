using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting1 : MonoBehaviour
{
    public float Range = 100.0f;
    public float BeamDPS = 1.0f;
    public float DMG;
    public Camera fpsCam;
    public Transform bulletSpawn;
    public GameObject Claw;
    public Transform bulletSpawn1;
    private float distance;
    private LineRenderer laserLine;
    public float upgrade1 = 1;
    private WaitForSeconds ticker = new WaitForSeconds(.25f);
    private bool damageProcessing = false;
    private float heatLevels = 10;
    private bool beamActive = false;
    private bool hasBeamGun = true;
    private bool hasHarpoonGun = false;
    private bool isFiring = false;

    // Update is called once per frame
    void Start()
    {
        laserLine = GetComponent<LineRenderer>();



    }
    void Update()
    {
        if (upgrade1 == 2)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Harpoon();
            }
        }
    }

    public void Harpoon()
    {
        laserLine.enabled = true;
        RaycastHit Fire;
        Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        laserLine.SetPosition(0, bulletSpawn.position);
        if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out Fire, Range))
        {
           // GameObject Hook = Instantiate(Claw, laserLine.transform.position, laserLine.transform.rotation);
            laserLine.SetPosition(1, (Fire.point + bulletSpawn.position) / 2);
            //Hook.transform.position = laserLine.transform.position;
            DamageCalculations target = Fire.collider.GetComponent<DamageCalculations>();
            if (isFiring == false)
            {
                isFiring = true;
                StartCoroutine(HarpoonTravel(target, Fire));
            }

        }
        else
        {
            laserLine.SetPosition(1, rayOrigin + (fpsCam.transform.forward * Range));
        }
    }






    private IEnumerator HarpoonTravel(DamageCalculations target, RaycastHit Fire)
    {
        yield return ticker;
        laserLine.SetPosition(2, (Fire.point));
        Fire.rigidbody.useGravity = false;
        Fire.rigidbody.AddForce(laserLine.transform.forward * -1000);

    }
}