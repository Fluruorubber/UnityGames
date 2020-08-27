using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLogic : MonoBehaviour
{
    
    GameObject m_player;


    // Start is called before the first frame update
    void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
    }

    void LateUpdate()
    {
        Vector3 cameraPos = transform.position;
        cameraPos.x = (float)(m_player.transform.position.x - 3.27);
        //cameraPos.y = (float)(m_player.transform.position.y + 2.0);
        if (cameraPos.y< m_player.transform.position.y || cameraPos.y-m_player.transform.position.y > 2.0)
        {
            cameraPos.y = (float)(m_player.transform.position.y + 2.0);
        }
        cameraPos.z = (float)(m_player.transform.position.z - 1.00);
        transform.position = cameraPos;
    } 
}
