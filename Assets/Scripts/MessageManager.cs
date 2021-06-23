using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessageManager : MonoBehaviour
{

    public bool display = false;

    public GameObject button;
    public TextMeshProUGUI message_text;

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
        if (display)
        {
            message_text.text = messages[currentMessage];
        }
    }

    public void StartMessages()
    {
        display = true;
        button.SetActive(true);
    }

    public void NextText()
    {
        if (currentMessage + 1 >= messages.Count)
        {
            noMoreMessages = true;
            display = false;
            button.SetActive(false);
        }
        else
        {
            currentMessage++;
        }
    }
}
