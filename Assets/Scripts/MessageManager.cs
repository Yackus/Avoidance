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
        button.SetActive(true);
    }

    /// <summary>
    /// Display the next text message
    /// </summary>
    public void NextText()
    {
        if (currentMessage + 1 >= messages.Count)
        {
            noMoreMessages = true;
            updateText = false;
            button.SetActive(false);
        }
        else
        {
            currentMessage++;
        }
    }
}
