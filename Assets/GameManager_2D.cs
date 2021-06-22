using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager_2D : MonoBehaviour
{
    [Header("Current Game")]
    public float m_currentScore = 0;

    public float m_currentSpeed = 1;

    [Header("Bird")]
    public GameObject m_bird;
    public BirdMovement m_birdMovement;

    [Header("background")]
    public BackgroundParallax m_bgSript;

    [Header("UI")]
    public TextMeshProUGUI m_scoreText;

    [Header("Rings")]
    public List<GameObject> m_allRings;

    public GameObject ringPrefab;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = m_allRings.Count - 1; i >= 0; i--)
        {
            if (!m_allRings[i]) m_allRings.RemoveAt(i);
        }


        m_bgSript.scrollSpeed = m_currentSpeed;

        Vector3 newMovement = new Vector3(m_currentSpeed, 0, 0);

        foreach (GameObject _ring in m_allRings)
        {
            Rigidbody2D _ringRB = _ring.GetComponent<Rigidbody2D>();

            if (_ringRB.gravityScale == 0 && Vector3.Distance(_ring.transform.position, m_bird.transform.position) < 0.5f)
            {
                _ringRB.gravityScale = 1;
                m_currentScore += 1;

                m_scoreText.text = "SCORE\n" + m_currentScore;
            }
            else if (_ringRB.gravityScale == 0)
            {
                _ringRB.velocity = -newMovement;
            }

            if (_ring.transform.position.x < -12)
            {
                Destroy(_ring);
            }
        }
    }

    private void FixedUpdate()
    {
        
    }
}
