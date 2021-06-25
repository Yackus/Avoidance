using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThoughtManager : MonoBehaviour
{
    public GameObject m_thoughtPref;

    public float spawnTime = 2;
    float lastSpawnTime;

    public GameObject canvas;

    public List<GameObject> m_thoughts = new List<GameObject>();

    public List<string> possibleThoughts = new List<string>();

    public GameObject bird;

    public float toSpawn;

    public bool clearToProgress;

    public thoughtsOBJ.SpawnModes spawnMode;

    //public ProgressManager m_progressManager;

    public GameManager_2D m_gameManager;


    // Start is called before the first frame update
    void Start()
    {
        lastSpawnTime = 0;

        m_thoughts.Clear();

        //if (!m_progressManager)
        //{
        //    GameObject pm = GameObject.Find("ProgressManager");
        //    if (pm)
        //    {
        //        m_progressManager = pm.GetComponent<ProgressManager>();
        //    }
        //    else
        //    {
        //        Debug.Log("Failed to find progress manager");
        //    }
        //}

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

        switch (spawnMode)
        {
            case thoughtsOBJ.SpawnModes.random:
                if (toSpawn > 0 && Time.time - lastSpawnTime > spawnTime)
                {
                    float x = Random.Range(-w+100, w-100);
                    float y = Random.Range(-h+100, h-100);

                    SpawnThought(new Vector3(x,y,0));
                }
                break;

            case thoughtsOBJ.SpawnModes.fromEdges:
                if (toSpawn > 0 && Time.time - lastSpawnTime > spawnTime)
                {
                    float x = (w + 100) * (Random.value > 0.5f ? 1 : -1) ;
                    float y = (h + 100) * (Random.value > 0.5f ? 1 : -1);

                    SpawnThought(new Vector3(x, y, 0));
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
                            SpawnThought(Vector3.zero, ringScript);
                        }
                    }
                }
                break;

            default:
                break;
        }

        
    }

    private void SpawnThought(Vector3 _pos, Ring _ringToAttach = null)
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
    }
}
