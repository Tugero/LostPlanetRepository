using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public Animator Enemy;

    private void OnTriggerEnter(Collider other)
    {
        if (Enemy != null && other.tag == "Enemy")
            return;
            if (other.tag == "Player")
            {
            Enemy.SetInteger("Anime State", 1);
            }
    }

    private void OnTriggerExit(Collider other)
    {
        if (Enemy != null)
            if (other.tag == "Player")
            {
                Enemy.SetInteger("Anime State", 2);
            }
    }
    public void stop()
    {
        Enemy.SetInteger("Anime State", 0);
    }
}
