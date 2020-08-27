using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager m_Instance = null;

    PlayerLogic m_playerLogic;
    FruitLogic[] m_fruitLogic;

    void Awake()
    {
        SetupGameLogicSingleton();
    }

    // Start is called before the first frame update
    void Start()
    {
        m_playerLogic = GameObject.FindObjectOfType<PlayerLogic>();
        m_fruitLogic = GameObject.FindObjectsOfType<FruitLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveGame();
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            LoadGame();
        }
    }

    void SetupGameLogicSingleton()
    {
        if (m_Instance == null)
        {
            m_Instance = this;
        }
        else if (m_Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public GameManager Instance()
    {
        return m_Instance;
    }

    public void SaveGame()
    {
        m_playerLogic.Save();
        for (int index = 0; index < m_fruitLogic.Length; ++index)
        {
            m_fruitLogic[index].Save(index);
        }
        PlayerPrefs.Save();
    }

    public void LoadGame()
    {
        m_playerLogic.Load();
        for (int index = 0; index < m_fruitLogic.Length; ++index)
        {
            m_fruitLogic[index].Load(index);
        }
    }



}
