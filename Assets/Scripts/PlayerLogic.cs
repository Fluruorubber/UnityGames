using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLogic : MonoBehaviour
{
    CharacterController m_characterController;

    float m_horizontalInput;
    float m_verticalInput;
    int m_score = 0;

    const float MOVENEMT_SPEED = 5.0f;
    const float GRAVITY = 0.05f;
    const float JUMP_HEIGHT = 0.4f;

    Vector3 m_movement;
    Vector3 m_heightMovement;

    bool m_isjumping = false;

    Camera m_camera;

    [SerializeField]
    Text m_scoreText;


    // Start is called before the first frame update
    void Start()
    {
        m_characterController = GetComponent<CharacterController>();
        m_camera = Camera.main;
        SetScoreText();
    }

    // Update is called once per frame
    void Update()
    {
        m_horizontalInput = Input.GetAxis("Horizontal");
        m_verticalInput = Input.GetAxis("Vertical");
        m_movement = new Vector3(m_horizontalInput, 0, m_verticalInput);

        if (!m_isjumping && Input.GetKeyDown(KeyCode.Space))
        {
            m_isjumping = true;
        }

        RotateCharacterTowardsMouseCursor();
    }


    void FixedUpdate()
    {
        m_movement = m_movement * MOVENEMT_SPEED * Time.deltaTime;
        if (m_isjumping)
        {
            m_heightMovement.y = JUMP_HEIGHT;
            m_isjumping = false;
        }

        m_heightMovement.y = m_heightMovement.y - GRAVITY;
        m_characterController.Move(m_movement + m_heightMovement);

        SetScoreText();
    }

    void RotateCharacterTowardsMouseCursor()
    {
        Vector3 mousePosInScreenSpace = Input.mousePosition;
        Vector3 playerPosInScreenSpace = m_camera.WorldToScreenPoint(transform.position);
        Vector3 directionInScreenSpace = mousePosInScreenSpace - playerPosInScreenSpace;

        float angle = Mathf.Atan2(directionInScreenSpace.y, directionInScreenSpace.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(90-angle, Vector3.up);
    }




    void SetScoreText()
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
}
