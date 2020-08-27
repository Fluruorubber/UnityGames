using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunLogic : MonoBehaviour
{
    [SerializeField]
    Transform m_bulletSpawn;

    [SerializeField]
    GameObject m_bulletObject;

    [SerializeField]
    Text m_ammoText;


    const int MAX_BULLETCOUNT = 10;
    int m_bulletCount = 10;
    int m_score = 0;

    GameObject m_player;
    PlayerLogic m_playerLogic;

    AudioSource m_audioSource;
    [SerializeField]
    AudioClip m_reloadSound;
    [SerializeField]
    AudioClip m_shotSound;

    // Start is called before the first frame update
    void Start()
    {
        SetAmmoText();
        m_audioSource = GetComponent<AudioSource>();
        m_player = GameObject.FindGameObjectWithTag("Player");
        if (m_player)
        {
            m_playerLogic = m_player.GetComponent<PlayerLogic>();
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && m_bulletCount > 0)
        {
            FireGun();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadText();
        }



    }


    void FireGun()
    {
        Instantiate(m_bulletObject, m_bulletSpawn.position, m_bulletSpawn.rotation);
        --m_bulletCount;
        SetAmmoText();
        PlaySound(m_shotSound);
    }

    void SetAmmoText()
    {
        if (m_ammoText)
        {
            m_ammoText.text = "Bullet: " + m_bulletCount;
        }
    }


    void ReloadText()
    {
        m_bulletCount = MAX_BULLETCOUNT;
        SetAmmoText();
        PlaySound(m_reloadSound);
        m_playerLogic.AddScore(-50);
    }


    void PlaySound(AudioClip audioclip)
    {
        if (m_audioSource)
        {
            m_audioSource.PlayOneShot(audioclip);

        }
    }
}