using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotSpawner : MonoBehaviour
{
    public GameObject shot;
    public Transform launcher;
    void Fire()
    {
        Instantiate(shot, launcher.position, launcher.rotation);
    }
}
