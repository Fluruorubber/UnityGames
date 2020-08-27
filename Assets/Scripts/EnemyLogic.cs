using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    Sleep,
    Run
}

public class EnemyLogic : MonoBehaviour
{
    GameObject m_player;
    PlayerLogic m_playerLogic;
    //float m_aggroRadius = 30.0f;
    EnemyState m_enemyState;
    CharacterController m_characterController;

    [SerializeField]
    float m_aggroRadius;
    
    float m_runSpeed = 40.0f;
    Vector3 m_movement;
    bool m_isLoading = false;

    // Start is called before the first frame update
    void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
        m_characterController = GetComponent<CharacterController>();
        if (m_player)
        {
            m_playerLogic = m_player.GetComponent<PlayerLogic>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_isLoading)
        {
            m_isLoading = false;
            return;
        }

        switch (m_enemyState)
        {
            case (EnemyState.Sleep):
                if (Vector3.Distance(m_player.transform.position, transform.position) < m_aggroRadius)
                {
                    SetState(EnemyState.Run);
                }
                break;

            case (EnemyState.Run):
                m_movement = transform.forward * m_runSpeed * Time.deltaTime;
                m_characterController.Move(m_movement);
                break;
        }
    }

    public void SetState(EnemyState enemyState)
    {
        m_enemyState = enemyState;
    }



    public void Save(int index)
    {
        // Position
        PlayerPrefs.SetFloat("EnemyPosX" + index, transform.position.x);
        PlayerPrefs.SetFloat("EnemyPosY" + index, transform.position.y);
        PlayerPrefs.SetFloat("EnemyPosZ" + index, transform.position.z);

        // State
        PlayerPrefs.SetInt("EnemyState" + index, (int)m_enemyState);

    }

    public void Load(int index)
    {
        m_characterController.enabled = true;

        float enemyPosX = PlayerPrefs.GetFloat("EnemyPosX" + index);
        float enemyPosY = PlayerPrefs.GetFloat("EnemyPosY" + index);
        float enemyPosZ = PlayerPrefs.GetFloat("EnemyPosZ" + index);

        transform.position = new Vector3(enemyPosX, enemyPosY, enemyPosZ);

        int enemyState = PlayerPrefs.GetInt("EnemyState" + index);
        SetState((EnemyState)enemyState);


        m_isLoading = true;
    }


}
