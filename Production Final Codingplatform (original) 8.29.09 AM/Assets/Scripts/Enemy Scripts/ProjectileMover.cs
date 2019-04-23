using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMover : MonoBehaviour
{
    public GameObject player;
    public float speed;
    private Vector3 PlayerLastKnownCoord;
    private Rigidbody rb;
    private bool impact;
    public SphereCollider harmless;


    void Start()
    {
        player = GameObject.Find("Player");
        transform.LookAt(new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z));
        PlayerLastKnownCoord = player.transform.position;
        rb = gameObject.GetComponent<Rigidbody>();
        rb.useGravity = false;
        impact = false;
    }
    void FixedUpdate()
    {
        //put the players direct location for PlayerLastKnownCoord for heat seeking!
        if (!impact)
            rb.AddForce(transform.forward * speed);
       //transform.position = Vector3.MoveTowards(transform.position, PlayerLastKnownCoord, speed);
        //Could be used for heat seeking projectiles!
        //transform.LookAt(new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z));
    }

    private void OnCollisionEnter(Collision collision)
    {
        rb.useGravity = true;
        rb.AddForce(new Vector3(0, 0, 0));
        impact = true;
        harmless.enabled = false;
        Destroy(gameObject, 5.0f);
    }
}

