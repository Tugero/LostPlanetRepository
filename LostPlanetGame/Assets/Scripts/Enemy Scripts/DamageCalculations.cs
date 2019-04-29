using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DamageCalculations : MonoBehaviour
{
    public float MHP;
    private Vector3 spawnPoint;
    public float CHP;
    public GameObject mob;
    public bool isFlipped;
    public GameObject player;
    public int lives = 3;
    private Vector3 PlayerSpawn;
    public AudioSource sounds;
    public AudioClip attack;
    public bool isDead = false;
    public GameObject Enemy;
    public GameObject HealthPanel;
    public GameObject HP100;
    public GameObject HP90;
    public GameObject HP75;
    public GameObject HP50;
    public GameObject HP25;
    public GameObject HP0;
    public Text Lives;


    void Start()
    {
        if(Lives != null)
        Lives.text = "Lives x"+lives;
        CHP = MHP;
        setHealthBar();
        isFlipped = false;
        player = GameObject.Find("Player");
        PlayerSpawn = player.transform.position;
        Enemy = GameObject.FindWithTag("Enemy");
    }
    void Update()
    {
        if(CHP > MHP)
        {
            CHP = MHP;
        }
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
            setHealthBar();
            Debug.Log("" + CHP);
            if (CHP <= 0)
            {
                lives -= 1;
                Lives.text = "Lives x"+lives;
                if (lives <= 0)
                {
                    Death();
                }
                player.transform.position = PlayerSpawn;
                CHP = MHP;
                player.GetComponent<PlayerMover>().Player.SetInteger("Move", 0);
                setHealthBar();
                isDead = true;
                Enemy.GetComponent<MeleeAttack>().stop();
            }
        }
    }

    public void Death()
    {
        if (gameObject.tag == "Enemy" || gameObject.tag == "Vines")
        {
            Debug.Log(gameObject);
            Debug.Log("Was Killed!");
            float current = player.GetComponent<DamageCalculations>().CHP;
            float full = player.GetComponent<DamageCalculations>().MHP;
            full = full * 0.15f;
            setHealthBar();
            player.GetComponent<DamageCalculations>().CHP += full;

            Destroy(mob);
        }
        if (gameObject.tag == "Player")
        {
            SceneManager.LoadScene(3);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
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
    public void setHealthBar()
    {
        if (HealthPanel != null)
        {
            if (CHP >= MHP)
            {
                HP100.SetActive(true);
                HP90.SetActive(false);
                HP75.SetActive(false);
                HP50.SetActive(false);
                HP25.SetActive(false);
                HP0.SetActive(false);

            }
            else if (CHP < MHP && CHP > MHP * 0.85)
            {
                HP100.SetActive(false);
                HP90.SetActive(true);
                HP75.SetActive(false);
                HP50.SetActive(false);
                HP25.SetActive(false);
                HP0.SetActive(false);

            }
            else if (CHP < MHP * 0.85 && CHP > MHP * 0.70)
            {
                HP100.SetActive(false);
                HP90.SetActive(false);
                HP75.SetActive(true);
                HP50.SetActive(false);
                HP25.SetActive(false);
                HP0.SetActive(false);

            }
            else if (CHP < MHP * 0.70 && CHP > MHP * 0.50)
            {
                HP100.SetActive(false);
                HP90.SetActive(false);
                HP75.SetActive(false);
                HP50.SetActive(true);
                HP25.SetActive(false);
                HP0.SetActive(false);

            }
            else if (CHP < MHP * 0.50 && CHP > MHP * 0.10)
            {
                HP100.SetActive(false);
                HP90.SetActive(false);
                HP75.SetActive(false);
                HP50.SetActive(false);
                HP25.SetActive(true);
                HP0.SetActive(false);

            }
            else if (CHP < MHP * 0.10)
            {
                HP100.SetActive(false);
                HP90.SetActive(false);
                HP75.SetActive(false);
                HP50.SetActive(false);
                HP25.SetActive(false);
                HP0.SetActive(true);
            }
        }


    }

}

