using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_logic : MonoBehaviour
{
    CharacterController m_charactercontroller;
    float m_horizontal;
    float m_vertical;
    float m_gravity = 0.98f;
    float m_height = 0.3f;

    Vector3 m_verticalmovement;
    Vector3 m_horizontalmovement;
    Vector3 m_jumpingheight;

    public float speed = 5.0f;
    public bool isjumping;

    // Start is called before the first frame update
    void Start()
    {
        m_charactercontroller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        m_horizontal = Input.GetAxis("Horizontal");
        m_vertical = Input.GetAxis("Vertical");
        if(Input.GetKeyDown(KeyCode.Space) && m_charactercontroller.isGrounded)
        {
            isjumping = true;
        }
    }
    private void FixedUpdate()
    {
        if (isjumping)
        {
            m_jumpingheight.y = m_height;
            isjumping = false;
        }

        m_horizontalmovement = transform.right * m_horizontal * speed * Time.deltaTime;
        m_verticalmovement = transform.forward * m_vertical * speed * Time.deltaTime;
        m_jumpingheight.y -= m_gravity * Time.deltaTime;

        
        if (m_charactercontroller)
        {
            m_charactercontroller.Move(m_horizontalmovement + m_verticalmovement + m_jumpingheight);
        }

        if(m_charactercontroller.isGrounded)
        {
            m_jumpingheight.y = 0;
        }
    }
}
