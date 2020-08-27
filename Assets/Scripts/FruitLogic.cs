using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum FruitState
{
    Active,
    Inactive
}

public class FruitLogic : MonoBehaviour
{
    const float ROTATION_SPEED = 2.0f;

    PlayerLogic m_playerLogic;
    GameObject m_player;
    MeshRenderer m_meshRenderer;
    Collider m_collider;
    AudioSource m_audioSource;
    FruitState m_fruitstate;

    [SerializeField]
    AudioClip m_wink;
    [SerializeField]
    Text m_scoreText;

    // Start is called before the first frame update
    void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
        if (m_player)
        {
            m_playerLogic = m_player.GetComponent<PlayerLogic>();
        }
        m_meshRenderer = GetComponent<MeshRenderer>();
        m_collider = GetComponent<Collider>();
        m_audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        transform.Rotate(0, ROTATION_SPEED, 0);
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            m_audioSource.PlayOneShot(m_wink);
            SetState(FruitState.Inactive);
            m_playerLogic.AddScore(5);

        }
    }


    void SetState(FruitState fruitState)
    {
        m_fruitstate = fruitState;
        if (m_fruitstate == FruitState.Active)
        {
            m_meshRenderer.enabled = true;
            m_collider.enabled = true;
        }
        else
        {
            m_meshRenderer.enabled = false;
            m_collider.enabled = false;
        }
    }


    public void Save(int index)
    {
        PlayerPrefs.SetInt("FruitState" + index, (int)m_fruitstate);

    }

    public void Load(int index)
    {
        int fruitState = PlayerPrefs.GetInt("FruitState" + index);
        SetState((FruitState)fruitState);
        m_playerLogic.AddScore(-4);

    }






}
