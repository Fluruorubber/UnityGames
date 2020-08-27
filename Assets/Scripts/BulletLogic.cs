using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletLogic : MonoBehaviour
{
    Rigidbody m_rigidbody;
    GameObject m_player;
    GameObject[] m_balloon;
    GameObject[] m_bomb;
    const float BULLET_SPEED = 10.0f;


    [SerializeField]
    Text m_scoreText;

    PlayerLogic m_playerLogic;

    // Start is called before the first frame update
    void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
        m_balloon = GameObject.FindGameObjectsWithTag("Balloon");
        m_bomb = GameObject.FindGameObjectsWithTag("Bomb");
        
        m_rigidbody = GetComponent<Rigidbody>();
        if (m_rigidbody)
        {
            m_rigidbody.velocity = transform.up * BULLET_SPEED;
        }

        if (m_player)
        {
            m_playerLogic = m_player.GetComponent<PlayerLogic>();
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Balloon" )
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            m_playerLogic.AddScore(10);
        }

        if (other.tag == "Bomb" && m_player)
        {
            Destroy(gameObject);
            Destroy(m_player);

            for (int i=0; i<m_balloon.Length; i++)
            {
                Destroy(m_balloon[i]);
            }

            for (int i=0; i<m_bomb.Length; i++)
            {
                Destroy(m_bomb[i]);
            }
            
        }

    }


}
