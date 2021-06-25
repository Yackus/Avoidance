using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager_2D : MonoBehaviour
{
    [Header("Current Game")]
    public float m_currentScore = 0;
    public float requiredScore = 5;

    public float m_currentSpeed = 1;

    [Header("Bird")]
    public GameObject m_bird;
    public BirdMovement m_birdMovement;

    [Header("background")]
    public BackgroundParallax m_bgSript;

    [Header("UI")]
    public TextMeshProUGUI m_scoreText;

    [Header("Rings")]
    public float spawnTime;
    float lastSpawnTime = 0;

    public List<GameObject> m_allRings;

    public GameObject ringPrefab;

    public ProgressManager m_progressManager;

    // Start is called before the first frame update
    void Start()
    {
        lastSpawnTime = 0;

        GameObject PM = GameObject.Find("ProgressManager");

        if (PM)
        {
            m_progressManager = PM.GetComponent<ProgressManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Clear null gameobjects
        for (int i = m_allRings.Count - 1; i >= 0; i--)
        {
            if (!m_allRings[i]) m_allRings.RemoveAt(i);
        }

        //Spawn ring every x seconds
        if (Time.time - lastSpawnTime > spawnTime)
        {
            lastSpawnTime = Time.time;
            GameObject t = Instantiate(ringPrefab, new Vector3(10, Random.Range(-3, 3), 0), Quaternion.identity, null);
            m_allRings.Add(t);
        }
        
        //Update background speed
        m_bgSript.scrollSpeed = m_currentSpeed;

        //calc new movement vector
        Vector3 newMovement = new Vector3(m_currentSpeed, 0, 0);

        //Update the velocity of each ring, and calculate if player has gone through a ring
        foreach (GameObject _ring in m_allRings)
        {
            Rigidbody2D _ringRB = _ring.GetComponent<Rigidbody2D>();

            //Going through ring gives points
            if (_ringRB.gravityScale == 0 && Vector3.Distance(_ring.transform.position, m_bird.transform.position) < 0.5f)
            {
                _ringRB.gravityScale = 1;
                m_currentScore += 1;

                m_scoreText.text = "SCORE\n" + m_currentScore + " / " + requiredScore;

                if (m_progressManager && m_currentScore >= requiredScore)
                {
                    m_progressManager.NextStage();
                }
            }
            else if (_ringRB.gravityScale == 0)
            {
                _ringRB.velocity = -newMovement;
            }

            if (_ring.transform.position.x < -12 || _ring.transform.position.y < -12)
            {
                if (_ring.GetComponent<Ring>().attachedThought)
                {
                    Destroy(_ring.GetComponent<Ring>().attachedThought);
                }
                Destroy(_ring);
            }
        }
    }

    private void FixedUpdate()
    {
        
    }
}
