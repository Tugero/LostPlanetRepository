using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomText : MonoBehaviour
{
    private bool wasTriggered = false;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(wasTriggered == false)
        {
            GetComponent<DialogueTrigger>().TriggerDialogue();
            wasTriggered = true;
        }
    }
}
