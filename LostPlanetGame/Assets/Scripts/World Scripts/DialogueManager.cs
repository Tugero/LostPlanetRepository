using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{ 
    public Text nameText;
    public Text dialogueText;
    public Image Portrait;
    private bool isTriggered;
    public GameObject player;
    public GameObject TextBox;
    private Queue<string> sentences;

    // Start is called before the first frame update
    void Start()
    {
       //sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        TextBox.SetActive(true);
        sentences = new Queue<string>();
        nameText.text = dialogue.name;
        Portrait.sprite = dialogue.image;
        dialogueText.text = "Test";
//        Debug.Log(sentences);
        sentences.Clear();
        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();

    }
    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }

    void EndDialogue()
    {
        TextBox.SetActive(false);
        Debug.Log("End of Convo");
    }

}
