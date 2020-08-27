using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLogic : MonoBehaviour
{
    CharacterController m_characterController;
    Animator m_animator;

    bool m_jump = false;
    bool m_isloading = false;
    float m_horizontalInput;
    float m_verticalInput;
    int m_score = 0;
    const float MOVEMENT_SPEED = 5.0f;
    const float GRAVITY = 0.05f;
    const float JUMP_HEIGHT = 0.4f;
    
    Vector3 m_movement;
    Vector3 m_height;

    [SerializeField]
    Text m_scoreText;

    AudioSource m_audioSource;
    [SerializeField]
    AudioClip m_saveSound;
    [SerializeField]
    AudioClip m_loadSound;
    [SerializeField]
    AudioClip m_dieSound;

    // Start is called before the first frame update
    void Start()
    {
        SetScoreText();
        m_characterController = GetComponent<CharacterController>();
        m_animator = GetComponent<Animator>();
        m_audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_isloading)
        {
            m_isloading = false;
            return;
        }

        m_horizontalInput = Input.GetAxisRaw("Horizontal");
        m_verticalInput = Input.GetAxisRaw("Vertical");
        m_movement = new Vector3(m_horizontalInput, 0, m_verticalInput);


        if (!m_jump && Input.GetKeyDown(KeyCode.Space))
        {
            m_jump = true;
        }
    }

    void FixedUpdate()
    {
        m_movement = m_movement * MOVEMENT_SPEED * Time.deltaTime;
        if (m_jump)
        {
            m_height.y = JUMP_HEIGHT;
            m_animator.SetBool("Jump", true);
            m_jump = false;
        }
        m_height.y = m_height.y - GRAVITY;
        if (m_characterController.enabled == true)
        {
            m_characterController.Move(m_movement + m_height);

        }

        m_animator.SetFloat("HorizontalInput", m_horizontalInput);
        m_animator.SetFloat("VerticalInput", m_verticalInput);

        if (m_characterController.isGrounded)
        {
            m_height.y = 0.0f;
            m_animator.SetBool("Jump", false);
        }

        
    }

    public void Save()
    {
        PlayerPrefs.SetFloat("PlayerPosX", transform.position.x);
        PlayerPrefs.SetFloat("PlayerPosY", transform.position.y);
        PlayerPrefs.SetFloat("PlayerPosZ", transform.position.z);
        PlayerPrefs.SetFloat("PlayerRotX", transform.rotation.eulerAngles.x);
        PlayerPrefs.SetFloat("PlayerRotY", transform.rotation.eulerAngles.y);
        PlayerPrefs.SetFloat("PlayerRotZ", transform.rotation.eulerAngles.z);
        m_audioSource.PlayOneShot(m_saveSound);
    }

    public void Load()
    {
        Reset();
        float playerPosX = PlayerPrefs.GetFloat("PlayerPosX");
        float playerPosY = PlayerPrefs.GetFloat("PlayerPosY");
        float playerPosZ = PlayerPrefs.GetFloat("PlayerPosZ");
        float playerRotX = PlayerPrefs.GetFloat("PlayerRotX");
        float playerRotY = PlayerPrefs.GetFloat("PlayerRotY");
        float playerRotZ = PlayerPrefs.GetFloat("PlayerRotZ");

        m_characterController.enabled = false;
        transform.position = new Vector3(playerPosX, playerPosY, playerPosZ);
        transform.rotation = Quaternion.Euler(playerRotX, playerRotY, playerRotZ);
        m_audioSource.PlayOneShot(m_loadSound);
        m_characterController.enabled = true;

        //m_isloading = true;
    }

    public void SetScoreText()
    {
        if (m_scoreText)
        {
            m_scoreText.text = "Score: " + m_score;
        }
    }


    public void AddScore(int score)
    {
        m_score += score;
        SetScoreText();
    }



    public void SetDie()
    {
        Debug.Log("die");
        m_animator.SetBool("Die", true);
        m_characterController.enabled = false;
        m_audioSource.PlayOneShot(m_dieSound);
    }


    void Reset()
    {
        m_animator.SetBool("Die", false);
        //m_characterController.enabled = true;
    }



    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Car")
        {
            SetDie();
        }
    }
}
