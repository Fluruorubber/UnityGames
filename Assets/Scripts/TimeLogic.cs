using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeLogic : MonoBehaviour
{
    [SerializeField]
    Text m_timeText;
    float m_startTime;
    float m_time;
    int min;
    int sec;

    // Start is called before the first frame update
    void Start()
    {
        m_startTime = Time.realtimeSinceStartup;
    }

    // Update is called once per frame
    void Update()
    {
        SetTimeText();
    }

    void SetTimeText()
    {
        m_time = Time.realtimeSinceStartup - m_startTime;
        min = (int)(m_time) / 60;
        sec = (int)(m_time) % 60;
        m_timeText.text = "Time: " + string.Format("{0}:{1}", min.ToString("00"), sec.ToString("00"));

    }
}
