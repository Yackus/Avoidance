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
    public List<thoughtsOBJ> thoughtsList;

    public Stages currentStage;
    
    float startTime;

    public float timeBeforeMessage = 2;

    bool messageStarted = false;

    bool findMessageManager = false;
    bool findThoughtManager = false;

    bool loadNewStage = false;

    bool removeFade = false;
    bool startFade = false;

    public MessageManager m_MessageManager;
    public ThoughtManager m_ThoughtManager;

    public GameObject fade;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    private void OnLevelWasLoaded(int level)
    {
        if (!m_ThoughtManager && findThoughtManager)
        {
            GameObject tm = GameObject.Find("ThoughtManager");
            if (tm)
            {
                m_ThoughtManager = tm.GetComponent<ThoughtManager>();

                //m_ThoughtManager.m_progressManager = this;

                if (thoughtsList[(int)currentStage] && (thoughtsList[(int)currentStage]).thoughts.Count > 0 && m_ThoughtManager.toSpawn > 0)
                {
                    m_ThoughtManager.possibleThoughts = (thoughtsList[(int)currentStage]).thoughts;

                    m_ThoughtManager.toSpawn = (thoughtsList[(int)currentStage]).toSpawn;

                    m_ThoughtManager.spawnMode = (thoughtsList[(int)currentStage]).spawnMode;

                    m_ThoughtManager.canMoveThoughts = (thoughtsList[(int)currentStage]).canMoveThoughts;

                    m_ThoughtManager.spawnWall = (thoughtsList[(int)currentStage]).spawnWall;

                    m_ThoughtManager.timeTillWall = (thoughtsList[(int)currentStage]).timeTillWall;

                }
                else
                {
                    m_ThoughtManager.toSpawn = 0;
                }

                


            }
            else
            {
                Debug.Log("Failed to find thought manager");
            }
        }

        //Find the message manager and restart the timer
        if (!m_MessageManager && findMessageManager)
        {
            GameObject mm = GameObject.Find("MessageManager");
            if (mm)
            {
                m_MessageManager = mm.GetComponent<MessageManager>();

                m_MessageManager.messages = (messagesList[(int)currentStage]).messages;
            }
            else
            {
                Debug.Log("Failed to find message manager");
            }
        }

        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            NextStage();
        }

        if (startFade)
        {
            fade.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, fade.GetComponent<SpriteRenderer>().color.a + 0.005f);

            if (fade.GetComponent<SpriteRenderer>().color.a >= 1)
            {
                NextStage();
            }
        }

        if (removeFade)
        {
            fade.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, fade.GetComponent<SpriteRenderer>().color.a - 0.005f);

            if (fade.GetComponent<SpriteRenderer>().color.a <= 0)
            {
                removeFade = false;
            }
        }

        //Update the messages for each stage
        if (m_MessageManager && !messageStarted && Time.time - startTime >= timeBeforeMessage)
        {
            messageStarted = true;
            m_MessageManager.StartMessages();
        }

        //If no more messages are left, and it is specified, go to next stage
        if (m_MessageManager != null && messagesList[(int)currentStage].nextSceneOnClear && m_MessageManager.noMoreMessages)
        {
            if (m_ThoughtManager && m_ThoughtManager.clearToProgress)
            {
                if (m_ThoughtManager.toSpawn <= 0 && m_ThoughtManager.m_thoughts.Count <= 0)
                {
                    NextStage();
                }
            }
            else
            {
                NextStage();
            }
        }
    }

    /// <summary>
    /// Progresses the game to the next stage
    /// </summary>
    public void NextStage()
    {
        loadNewStage = true;
        startFade = true;

        //If needing to load new stage, its done here
        if (loadNewStage && fade.GetComponent<SpriteRenderer>().color.a >= 1)
        {
            loadNewStage = false;
            removeFade = true;
            startFade = false;

            currentStage++;
            if (m_MessageManager) m_MessageManager.noMoreMessages = false;
            m_MessageManager = null;
            findMessageManager = true;
            loadNewStage = true;
            messageStarted = false;

            m_ThoughtManager = null;
            findThoughtManager = true;

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
    }
}
