using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereLogic : MonoBehaviour
{
    bool m_game = true;

    AudioSource m_audioSource;
    [SerializeField]
    AudioClip m_gameOver;

    // Start is called before the first frame update
    void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject m_player = GameObject.FindGameObjectWithTag("Player");
        if (m_player == null && m_game == true)
        {
            if (m_audioSource)
            {
                m_audioSource.PlayOneShot(m_gameOver);
                m_game = false;
            }
        }
            
    }
}
