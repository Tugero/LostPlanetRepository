using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedNav : MonoBehaviour
{
    public GameObject Player;
    public Animator Pep;
    private float RotationChecker;
    public float CurrentRotation;
    private bool isRotating;
    private bool isInRange;

    void Start()
    {
        RotationChecker = transform.rotation.y;
        isInRange = false;
    }

    void Update()
    {
        // Changes animation if object is rotating
        CurrentRotation = transform.rotation.y - RotationChecker;
        if (CurrentRotation > 0f || CurrentRotation < 0f)
        {
            isRotating = true;
        }
        else
            isRotating = false;
        if (isRotating)
            Pep.SetInteger("Anime State", 1);
        if (!isRotating)
        {
            Pep.SetInteger("Anime State", 0);
            if (isInRange)
           Pep.SetInteger("Anime State", 2);
        }
        RotationChecker = transform.rotation.y;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        isInRange = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            isInRange = false;
    }

    void FixedUpdate()
    {
        // Looks at player by turning on Y axis
        if (Player !=null)
        transform.LookAt(new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z));
    }
}
