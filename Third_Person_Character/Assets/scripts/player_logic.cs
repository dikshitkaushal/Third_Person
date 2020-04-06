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

    Animator m_animator;

    public float speed = 5.0f;
    public bool isjumping;

    [SerializeField]
    List<AudioClip> m_audioclips = new List<AudioClip>();
    AudioSource m_audiosource;

    // Start is called before the first frame update
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_charactercontroller = GetComponent<CharacterController>();
        m_audiosource = GetComponent<AudioSource>();
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
        if(m_animator)
        {
            m_animator.SetFloat("movement_x", m_horizontal);
            m_animator.SetFloat("movement_y", m_vertical);
        }
    }
    private void FixedUpdate()
    {
        if (isjumping)
        {
            m_jumpingheight.y = m_height;
            isjumping = false;
        }
        transform.forward = Camera.main.transform.forward;
        m_horizontalmovement = transform.right * m_horizontal * speed * Time.deltaTime;
        m_verticalmovement = Camera.main.transform.forward * m_vertical * speed * Time.deltaTime;
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
    public void playfootstepsound(int index)
    {
        //left 0 and right 1
        if (m_audioclips.Count > 0 && m_audiosource)
        {
            int random = Random.Range(0, m_audioclips.Count - 1);
            m_audiosource.PlayOneShot(m_audioclips[random]);
        }
    }
}
