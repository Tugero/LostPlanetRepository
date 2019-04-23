using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDemo : MonoBehaviour
{
    public GameObject Hitbox;
    private int counter;
    // Start is called before the first frame update
    void Start()
    {
        Hitbox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            if (counter < 5)
            {
                Hitbox.SetActive(true);
                counter++;
            }
            else
            {
                Hitbox.SetActive(false);
            }
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            counter = 0;
        }
    }


}
