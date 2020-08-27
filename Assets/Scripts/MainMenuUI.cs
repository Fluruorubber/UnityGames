using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField]
    GameObject m_optionsMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnStartClicked()
    {
        Debug.Log("Start Clicked");
        SceneManager.LoadScene("GameScene");
    }

    public void OnOptionsClicked()
    {
        Debug.Log("Options Clicked");
        if (m_optionsMenu)
        {
            m_optionsMenu.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    public void OnQuitClicked()
    {
        Debug.Log("Quit Clicked");
        Application.Quit();
    }
}
