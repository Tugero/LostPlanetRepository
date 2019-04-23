using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransition : MonoBehaviour
{
    public GameObject Player;
    public GameObject LoadLevel;
    private void OnTriggerEnter(Collider other)
    {
        if (tag == "Lava")
        {
            LoadLevel.GetComponent<Loadlevel>().Lose();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            return;
        }
        if (other.tag == "Player" && tag != "Lava")
        {
            if (Player.GetComponent<PlayerMover>().hasParashute)
            {
                LoadLevel.GetComponent<Loadlevel>().Level2();
            }
            if (Player.GetComponent<PlayerMover>().hasJetPack)
            {
                LoadLevel.GetComponent<Loadlevel>().Level3();
            }
            else if (!Player.GetComponent<PlayerMover>().hasParashute)
            {
                LoadLevel.GetComponent<Loadlevel>().Lose();
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
            }
        }

    }
}
