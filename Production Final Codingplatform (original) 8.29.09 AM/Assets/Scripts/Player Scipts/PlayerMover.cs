using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Add Controller Support!!!
public class PlayerMover : MonoBehaviour
{
    public Animator Player;
    public float PlayerSpeed;
    public float JumpForce;
    private float NS;
    private float EW;
    private float CameraX;
    private float CameraY;
    public float CameraYAngle;
    public float YAngleMin = -.22f;
    public float YAngleMax= .25f;
    public bool inAir;
    public bool hasJetPack;
    public bool hasParashute;
    public bool hasScicle;
    public bool hasPickaxe;
    private bool isFlipped;
    public float ParashuteStrength;
    public float JetPackStrength;
    public int JetPackFuel;
    public int jetPackEfficeincy = 5;
    public Rigidbody rb3D;
    public GameObject Camera;
    public Text FuelGage;
    public Vector3 Velocity;
    public float HP;
    public AudioSource Sounder;
    public AudioClip Jump;
    public AudioClip Step;
    public AudioClip shoe;
    public AudioClip foot;
    public GameObject Shute;
    public GameObject Jetpack;
    public GameObject JPEffect;
    private bool Controller = false;
    public GameObject gunspawn;
    // Start is called before the first frame update
    void Start()
    {
        Player = GetComponent<Animator>();
        rb3D = gameObject.GetComponent<Rigidbody>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        hasJetPack = false;
        hasParashute = false;
        hasScicle = false;
        hasPickaxe = false;
        //FuelGage.text = "Fuel: " + JetPackFuel;
        isFlipped = false;
        GetComponent<DialogueTrigger>().TriggerDialogue();
        Controller = false;
    }

    // Update is called once per frame
    void Update()
    {
        JetPackFuel = Mathf.Clamp(JetPackFuel, 0, 100);
        HP = GetComponent<DamageCalculations>().CHP;
        textContinue();
       // if (Input.GetKey(KeyCode.C))
           //Controller = true;
        if (Controller)
            Debug.Log("Controller Active");
    }

    void FixedUpdate()
    {
        if (HP > 0)
        {
            Velocity = rb3D.velocity;
            playerMover();
            //CameraRotater();
            JetPack();
            Parashute();
            CameraRotater();
            SickleAttack();
            PickAxeAttack();
        }
       /* if (Velocity.y > 10){
            Debug.Log("Slowing player down!");
            rb3D.AddForce(new Vector3(0, -200, 0));
         }*/
        if (isFlipped == true)
        {
            rb3D.AddForce(new Vector3(0, 50f, 0));
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (HP > 0)
        {
            if (collision.collider.tag == "Ground")
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    if (isFlipped == false)
                        rb3D.AddForce(new Vector3(0, JumpForce, 0), ForceMode.Impulse);
                    if (isFlipped == true)
                        rb3D.AddForce(new Vector3(0, -JumpForce, 0), ForceMode.Impulse);
                    Sounder.clip = Jump;
                    Sounder.Play();
                }
                //FuelGage.text = "Fuel: " + JetPackFuel;
                JetPackFuel += jetPackEfficeincy;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Ground")
        {
            inAir = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Ground")
        {
            //Jump controll
            if (Input.GetKey(KeyCode.Space))
            {
                //inAir = true;
                Player.SetInteger("Move", 5);
            }
            inAir = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("JetPack"))
        {
            other.gameObject.SetActive(false);
            hasJetPack = true;
            Debug.Log("Obtained Jetpack");
            Jetpack.SetActive(true);
            other.GetComponent<DialogueTrigger>().TriggerDialogue();

        }
        if (other.gameObject.CompareTag("Parashute"))
        {
            other.gameObject.SetActive(false);
            hasParashute = true;
            //SceneManager.LoadScene(4);
            //Screen.lockCursor = false;
            if (other != null)
            other.GetComponent<DialogueTrigger>().TriggerDialogue();

        }
        if (other.gameObject.CompareTag("WeaponUpOne"))
        {
            other.gameObject.SetActive(false);
            hasScicle = true;
            other.GetComponent<DialogueTrigger>().TriggerDialogue();

        }
        if (other.gameObject.CompareTag("Pickaxe"))
        {
            other.gameObject.SetActive(false);
            hasPickaxe = true;
            other.GetComponent<DialogueTrigger>().TriggerDialogue();

        }
        if (other.gameObject.CompareTag("GUN2"))
        {
            other.gameObject.SetActive(false);
            gunspawn.GetComponent<Shooting>().hasBeamGun = true;
            other.GetComponent<DialogueTrigger>().TriggerDialogue();

        }
        if (other.gameObject.CompareTag("Finish"))
        {
            SceneManager.LoadScene(4);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }

    public void Parashute()
    {
        if (isFlipped == false)
        {
            if (hasParashute)
            {
                if (Velocity.y < 5 && inAir == true)
                {
                    if (Input.GetKey(KeyCode.Space))
                    {
                        rb3D.drag = ParashuteStrength;
                        Shute.SetActive(true);
                    }
                }
            }
            if (Velocity.y >= 0)
            {
                rb3D.drag = 0;
                Shute.SetActive(false);
            }
        }
        if (isFlipped == true)
        {
            if (hasParashute)
            {
                if (Velocity.y > 5 && inAir == true)
                {
                    if (Input.GetKey(KeyCode.Space))
                    {
                        rb3D.drag = ParashuteStrength;
                        Shute.SetActive(true);
                    }
                }
            }
            if (Velocity.y <= 0)
            {
                rb3D.drag = 0;
                Shute.SetActive(false);
            }
        }
    }

    public void JetPack()
    {
        if (!hasJetPack)
            FuelGage.text = "";
        if (hasJetPack)
        {
            FuelGage.text = "Fuel: " + JetPackFuel;
            if (JetPackFuel > 1)
            if (Input.GetKey(KeyCode.Space)|| Input.GetButton("XButton"))
            {
                    JPEffect.SetActive(true);
                    //FuelGage.text = "Fuel: " + JetPackFuel;
                    if (isFlipped==false)
                    rb3D.AddForce(new Vector3(0, JetPackStrength, 0));
                    if (isFlipped == true)
                        rb3D.AddForce(new Vector3(0, -JetPackStrength, 0));
                    Debug.Log("Using Jetpack");
                    JetPackFuel -= jetPackEfficeincy;
            }
        }
        if (JetPackFuel == 1 || Input.GetKeyUp(KeyCode.Space))
        {
            JPEffect.SetActive(false);
        }
    }
    public void playerMover()
    {
        transform.position = transform.position + new Vector3(Camera.transform.forward.x,0,Camera.transform.forward.z) * NS * Time.deltaTime;
        transform.position = transform.position + Camera.transform.right * EW * Time.deltaTime;
        if (Input.GetKey(KeyCode.W))
        {
            NS = PlayerSpeed;
            if (!inAir)
            Player.SetInteger("Move", 1);
        }
        //Controller input
        if (Input.GetAxisRaw("DpadVertical") >0 && Controller)
        {
            NS = PlayerSpeed;
            if (!inAir)
                Player.SetInteger("Move", 1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            NS = -PlayerSpeed;
            if (!inAir)
                Player.SetInteger("Move", 4);
        }
        //Contoller input
        if (Input.GetAxisRaw("DpadVertical") < 0 && Controller)
        {
            NS = PlayerSpeed;
            if (!inAir)
                Player.SetInteger("Move", 1);
        }
        if (Input.GetKey(KeyCode.D))
        {
            EW = PlayerSpeed;
            if (!inAir)
                Player.SetInteger("Move", 2);
        }
        //Contoller Inpit
        if (Input.GetAxisRaw("DpadHorizontal") > 0 && Controller)
        {
            EW = PlayerSpeed;
            if (!inAir)
              Player.SetInteger("Move", 2);
        }
        if (Input.GetKey(KeyCode.A))
        {
            EW = -PlayerSpeed;
            if (!inAir)
                Player.SetInteger("Move", 3);
        }
        //Controller Input
        if (Input.GetAxisRaw("DpadHorizontal") < 0 && Controller)
        {
            EW = PlayerSpeed;
            if (!inAir)
                Player.SetInteger("Move", 2);
        }
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            NS = 0;
        }
        //Controller Input
        if (Input.GetAxisRaw("DpadVertical") == 0 && Controller)
        {
            NS = 0;
        }
        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            EW = 0;
        }
        //Controller Input
        if (Input.GetAxisRaw("DpadHorizontal") == 0 && Controller)
        {
            NS = 0;
        }
        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)&& !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            if (!inAir)
                Player.SetInteger("Move", 0);
        }
    }

    public void CameraRotater()
    {
        CameraX = Input.GetAxis("Mouse X");
        //Controller Input
        if(Controller)
        CameraX = Input.GetAxis("Right joystick Hroizontal");
        CameraY = -Input.GetAxis("Mouse Y");
        //Controller Input
        if(Controller)
        CameraY = Input.GetAxis("Right joystick Verticle");
        transform.Rotate(0, CameraX, 0);
        CameraYAngle = Camera.transform.rotation.x; 
        Camera.gameObject.transform.Rotate(CameraY, 0, 0);
        if (Camera.transform.localRotation.x >= YAngleMax)
        {
            Camera.gameObject.transform.Rotate(-CameraY, 0, 0);
        }
        if (Camera.transform.localRotation.x <= YAngleMin)
        {
            Camera.gameObject.transform.Rotate(-CameraY, 0, 0);
        }
    }

    private void SickleAttack()
    {
        if (hasScicle == true && !hasPickaxe)
        {
            if (Input.GetMouseButtonDown(1))
                Player.Play("SCIAttack");
            //Controller Input 
            if (Input.GetButton("AButton"))
                Player.Play("SCIAttack");
        }
    }
    private void PickAxeAttack()
    {
        if (hasPickaxe)
        {
            if (Input.GetMouseButtonDown(1))
                Player.Play("Pickaxe Attack");
            //Controller Input 
            if (Input.GetButton("AButton"))
                Player.Play("Pickaxe Attack");
        }
    }
    private void Steping()
    {
        if (!inAir)
        {
            int rando = Random.Range(1, 3);
            if (rando ==1)
            Sounder.clip = Step;
            if (rando == 2)
                Sounder.clip = shoe;
            if (rando == 3)
                Sounder.clip = foot;
            Sounder.Play();
        }
    }

    public void GravityFlipper()
    {
        if (isFlipped == false)
        {
            Debug.Log("Flipping Gravity");
            rb3D.useGravity = false;
            //rb3D.AddForce(new Vector3(0, 1f, 0));
            gameObject.transform.Rotate(new Vector3(180,transform.position.y+176,0));
            isFlipped = true;
            return;
        }
        if (isFlipped == true)
        {
            Debug.Log("Restoring Gravity");
            rb3D.useGravity = true;
            gameObject.transform.Rotate(new Vector3(180, transform.position.y + 174, 0));
            isFlipped = false;
            return;
        }
    }
    private void textContinue()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("b down");
            FindObjectOfType<DialogueManager>().DisplayNextSentence();
        }
    }
    public void KnockBack(Vector3 Direction)
    {
        Debug.Log(Direction.normalized*10);
        rb3D.AddForce(Direction.normalized * 500f);
    }

}
