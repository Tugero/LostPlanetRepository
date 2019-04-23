using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Exploder : MonoBehaviour
{
    public GameObject Parent;
    public Animator Boom;
    public GameObject Explosion;
    public NavMeshAgent _NaveMeshAgent;

    private void OnTriggerEnter(Collider other)
    {
        if (Boom != null && _NaveMeshAgent != null)
        {
            if (other.tag == "Player")
            {
                Boom.SetInteger("Anime State", 1);
                _NaveMeshAgent.speed = 0;
            }
        }
    }

    public void Kaboom()
    {
        Instantiate(Explosion, transform.position, transform.rotation);
        Destroy(Parent);
    }
}
