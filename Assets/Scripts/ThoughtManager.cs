using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThoughtManager : MonoBehaviour
{
    public GameObject m_thoughtPref;

    public float spawnTime = 0.5f;
    float lastSpawnTime;

    float startDelay = 4.0f;

    public GameObject canvas;

    public List<GameObject> m_thoughts = new List<GameObject>();

    public List<string> possibleThoughts = new List<string>();

    public GameObject bird;

    public float toSpawn;

    public bool clearToProgress;

    public bool canMoveThoughts;

    public thoughtsOBJ.SpawnModes spawnMode;

    //public ProgressManager m_progressManager;

    public GameManager_2D m_gameManager;

    public bool spawnWall;
    public float timeTillWall;

    float startTime;


    // Start is called before the first frame update
    void Start()
    {
        lastSpawnTime = 0;

        m_thoughts.Clear();

        bird = GameObject.Find("Bird");


        if (!m_gameManager)
        {
            GameObject gm = GameObject.Find("GameManager");
            if (gm)
            {
                m_gameManager = gm.GetComponent<GameManager_2D>();
            }
            else
            {
                Debug.Log("Failed to find game manager");
            }
        }

        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = m_thoughts.Count-1; i >= 0; i--)
        {
            GameObject t = m_thoughts[i];

            if (t == null)
            {
                m_thoughts.RemoveAt(i);
            }
        }
    }

    private void FixedUpdate()
    {

        float w = Screen.width / 2;
        float h = Screen.height / 2;

        if (spawnWall)
        {
            if (toSpawn > 0 && Time.time - startTime > timeTillWall)
            {
                float x = Random.Range(w, w + 100);
                float y = Random.Range(-h + 100, h - 100);

                SpawnThought(new Vector3(x, y, 0), new Vector3(-300,0));
            }

            if (Time.time - startTime > timeTillWall + 5)
            {
                m_gameManager.m_progressManager.NextStage();
            }
        }

        switch (spawnMode)
        {
            case thoughtsOBJ.SpawnModes.random:
                if (toSpawn > 0 && Time.time - lastSpawnTime > spawnTime + startDelay)
                {
                    startDelay = 0;
                    float x = Random.Range(-w+100, w-100);
                    float y = Random.Range(-h+100, h-100);

                    SpawnThought(new Vector3(x,y,0), Vector3.zero);
                }
                break;

            case thoughtsOBJ.SpawnModes.onRings:
                foreach (GameObject _ring in m_gameManager.m_allRings)
                {
                    if (_ring)
                    {
                        Ring ringScript = _ring.GetComponent<Ring>();

                        if (ringScript && ringScript.attachedThought == null && ringScript.noMoreRings == false)
                        {
                            SpawnThought(Vector3.zero, Vector3.zero, ringScript);
                        }
                    }
                }
                break;

            default:
                break;
        }

        
    }

    private void SpawnThought(Vector3 _pos, Vector3 _vel, Ring _ringToAttach = null)
    {
        toSpawn--;
        lastSpawnTime = Time.time;
        
        GameObject t = Instantiate(m_thoughtPref, Vector3.zero, Quaternion.identity, canvas.transform);
        if (possibleThoughts.Count > 0) t.GetComponent<Thought>().text.GetComponent<TextMeshProUGUI>().text = possibleThoughts[Random.Range(0, possibleThoughts.Count)];
        t.transform.localPosition = _pos;
        m_thoughts.Add(t);

        if (_ringToAttach != null)
        {
            _ringToAttach.attachedThought = t;
            _ringToAttach.bird = bird;
        }

        t.GetComponent<Thought>().canMove = canMoveThoughts;

        t.GetComponent<Rigidbody2D>().velocity = _vel;

        t.transform.SetAsFirstSibling();
    }
}
