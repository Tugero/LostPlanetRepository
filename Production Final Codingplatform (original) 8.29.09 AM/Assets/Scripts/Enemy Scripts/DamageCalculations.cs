using UnityEngine;
using UnityEngine.SceneManagement;

public class DamageCalculations : MonoBehaviour
{
    public float MHP;
    private Vector3 spawnPoint;
    public float CHP;
    public GameObject mob;
    public bool isFlipped;
    public GameObject player;
    public int lives =3;
    private Vector3 PlayerSpawn;
    public AudioSource sounds;
    public AudioClip attack;
    public bool isDead = false;

    void Start()
    { 
        CHP = MHP;
        isFlipped = false;
        player = GameObject.Find("Player");
        PlayerSpawn = player.transform.position;
    }
    public void AttackSound()
    {
        sounds.clip = attack;
        sounds.Play();
    }
    public void TakeDamage(float DMG)
    {
        if (gameObject.tag == "Enemy")
        {
            Debug.Log("Damge Taken");
            CHP -= DMG;
            Debug.Log("" + CHP);
            if (CHP <= 0)
            {
                Death();
                
            }
        }
        if (gameObject.tag == "Vines")
        {
            if (player.GetComponent<PlayerMover>().hasScicle)
            {
                Debug.Log("Damge Taken");
                CHP -= DMG;
                Debug.Log("" + CHP);
                if (CHP <= 0)
                {
                    Death();
                }
            }
        }

        if (gameObject.tag == "Player")
        {
            Debug.Log("Player Hurt");
            CHP -= DMG;
            Debug.Log("" + CHP);
            if (CHP <= 0)
            {
                lives -= 1;
                if (lives <= 0 )
                {
                    Death();
                }
                player.transform.position = PlayerSpawn;
                CHP = MHP;
                player.GetComponent<PlayerMover>().Player.SetInteger("Move",0);
                isDead = true;
            }
        }
    }

    public void Death()
    {
        if (gameObject.tag == "Enemy" || gameObject.tag == "Vines")
        {
            Debug.Log(gameObject);
            Debug.Log("Was Killed!");
            Destroy(mob);
        }
        if (gameObject.tag == "Player")
        {
            SceneManager.LoadScene(3);
            Screen.lockCursor = false;
            Debug.Log(gameObject);
            Debug.Log("Was Killed!");
            //Destroy(gameObject);
        }
    }

    public void ChangeGravity()
    {
      if (gameObject.tag == "Crystal")
        {
                Debug.Log("Crystal Hit");
                player.GetComponent<PlayerMover>().GravityFlipper();
                isFlipped = true;
        }
    }
}

