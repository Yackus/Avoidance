using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThoughtManager : MonoBehaviour
{
    public GameObject m_thoughtPref;

    public float spawnTime = 2;
    float lastSpawnTime;

    public GameObject canvas;

    public List<GameObject> m_thoughts = new List<GameObject>();

    public GameObject bird;

    public float toSpawn;

    public bool clearToProgress; 


    // Start is called before the first frame update
    void Start()
    {
        lastSpawnTime = 0;

        m_thoughts.Clear();
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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Room");
        }
    }

    private void FixedUpdate()
    {
        if (toSpawn > 0 && Time.time - lastSpawnTime > spawnTime)
        {
            toSpawn--;
            lastSpawnTime = Time.time;
            float w = Screen.width / 2;
            float h = Screen.height / 2;
            float x = Random.Range(-w, w);
            float y = Random.Range(-h, h);
            GameObject t = Instantiate(m_thoughtPref, Vector3.zero, Quaternion.identity, canvas.transform);
            t.transform.localPosition = new Vector3(x, y, 0);
            m_thoughts.Add(t);
        }
    }
}
