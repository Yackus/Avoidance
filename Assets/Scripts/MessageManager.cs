using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessageManager : MonoBehaviour
{
    [Header("Textbox")]
    public bool updateText = false;

    public GameObject button;
    public TextMeshProUGUI message_text;

    [Header("Messages")]
    public int currentMessage = 0;

    public bool noMoreMessages = false;

    public List<string> messages;

    public GameObject portrait;

    void Start()
    {
        noMoreMessages = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Update text if displaying
        if (updateText)
        {
            message_text.text = messages[currentMessage];
        }
    }

    /// <summary>
    /// Set as active and start displaying messages
    /// </summary>
    public void StartMessages()
    {
        updateText = true;
        button.transform.localPosition = new Vector3(0,-260,0);
    }

    /// <summary>
    /// Display the next text message
    /// </summary>
    public void NextText()
    {
        GameObject.Find("AudioManager").GetComponent<AudioManager>().PlayClick();
        if (currentMessage + 1 >= messages.Count)
        {
            noMoreMessages = true;
            updateText = false;
            button.transform.localPosition = new Vector3(0, 1000, 0);
        }
        else
        {
            currentMessage++;
        }
    }
}
