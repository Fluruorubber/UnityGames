using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HMBBLogic : MonoBehaviour
{
    NavMeshAgent m_navMeshAgent;
    Transform m_playerTransform;
    PlayerLogic m_playerLogic;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player)
        {
            m_playerTransform = player.transform;
            m_playerLogic = player.GetComponent<PlayerLogic>();
        }
        m_navMeshAgent = GetComponent<NavMeshAgent>();
        }


    // Update is called once per frame
    void FixedUpdate()
    {
        m_navMeshAgent.SetDestination(m_playerTransform.position);
        m_navMeshAgent.transform.right = m_navMeshAgent.velocity.normalized;
    }

    
}
