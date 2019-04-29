using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHurt : MonoBehaviour
{
    public GameObject player;
    public float DMG;
    private float health;
    private Rigidbody PlayerRB;
    public Vector3 knockback;

    private void Start()
    {
        player = GameObject.Find("Player");
        health = player.GetComponent<DamageCalculations>().CHP;
        PlayerRB = player.GetComponent<PlayerMover>().rb3D;
    }
    private void Update()
    {
        health = player.GetComponent<DamageCalculations>().CHP;
        if (health <= 0)
            ded();
        knockback = player.transform.position - transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player.GetComponent<DamageCalculations>().TakeDamage(DMG);
            if (gameObject.CompareTag("Knockback"))
            {
                knockback = player.transform.position-transform.position;
                //Debug.Log("Test!");
                player.GetComponent<PlayerMover>().KnockBack(knockback);
            }
        }
        if (gameObject.tag == "Player")
        {
            if (other.tag == "Enemy" || other.tag == "Vines")
            {
               other.GetComponent<DamageCalculations>().TakeDamage(DMG);
            }
        }
    }

    public void ded()
    {
        player.GetComponent<Animator>().SetInteger("Move", 6);
    }
}
