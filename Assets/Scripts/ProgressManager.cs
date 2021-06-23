using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressManager : MonoBehaviour
{
    public enum Stages
    {
        menu = -1,
        room1 = 0,
        game1,
        room2,
        game2,
        room3,
        game3,
        room4,
        end
    };

    public List<messagesOBJ> messagesList;

    public Stages currentStage;

    float startTime;

    public float timeBeforeMessage = 2;

    bool messageStarted = false;

    bool findMessageManager = false;

    bool loadNewStage = false;

    public MessageManager m_MessageManager;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (loadNewStage)
        {
            loadNewStage = false;

            switch (currentStage)
            {
                case Stages.menu:
                    break;

                case Stages.room1:
                    SceneManager.LoadScene("Scene_Room");
                    break;

                case Stages.game1:
                    SceneManager.LoadScene("Scene_Game");
                    break;

                case Stages.room2:
                    SceneManager.LoadScene("Scene_Room");
                    break;

                case Stages.game2:
                    SceneManager.LoadScene("Scene_Game");
                    break;

                case Stages.room3:
                    SceneManager.LoadScene("Scene_Room");
                    break;

                case Stages.game3:
                    SceneManager.LoadScene("Scene_Game");
                    break;

                case Stages.room4:
                    SceneManager.LoadScene("Scene_Room");
                    break;

                case Stages.end:
                    break;

                default:
                    break;
            }
        }

        if (!m_MessageManager && findMessageManager)
        {
            GameObject mm = GameObject.Find("MessageManager");
            if (mm)
            {
                m_MessageManager = mm.GetComponent<MessageManager>();
            }

            startTime = Time.time;
        }

        if (m_MessageManager && !messageStarted && Time.time - startTime >= timeBeforeMessage)
        {
            messageStarted = true;
            m_MessageManager.messages = (messagesList[(int)currentStage]).messages;
            m_MessageManager.StartMessages();
        }

        if (m_MessageManager != null && messagesList[(int)currentStage].nextSceneOnClear && m_MessageManager.noMoreMessages)
        {
            NextStage();
        }
    }

    public void NextStage()
    {
        Debug.Log("Next!");
        currentStage++;
        if (m_MessageManager) m_MessageManager.noMoreMessages = false;
        m_MessageManager = null;
        findMessageManager = true;
        loadNewStage = true;
        messageStarted = false;
    }
}
