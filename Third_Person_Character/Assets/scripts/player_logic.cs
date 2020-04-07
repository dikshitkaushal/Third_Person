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
    public Camera fpcam;
    [SerializeField] Transform m_rightfoot;
    [SerializeField] Transform m_leftfoot;
    GameObject m_camera;
    camera_logic m_cameralogic;

    Vector3 m_verticalmovement;
    Vector3 m_horizontalmovement;
    Vector3 m_jumpingheight;

    Animator m_animator;

    public float speed = 5.0f;
    public bool isjumping;

    [SerializeField]
    List<AudioClip> m_pebbles_audioclips = new List<AudioClip>();
    [SerializeField]
    List<AudioClip> m_audio_grass_clips = new List<AudioClip>();
    [SerializeField]
    List<AudioClip> m_audio_water_clips = new List<AudioClip>();

    AudioSource m_audiosource;

    // Start is called before the first frame update
    void Start()
    {
        m_camera = Camera.main.gameObject;
        m_cameralogic = m_camera.GetComponent<camera_logic>();
        m_animator = GetComponent<Animator>();
        m_charactercontroller = GetComponent<CharacterController>();
        m_audiosource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        m_horizontal = Input.GetAxis("Horizontal");
        m_vertical = Input.GetAxis("Vertical");
        if (m_animator)
        {
            m_animator.SetFloat("movement_x", m_horizontal);
            m_animator.SetFloat("movement_y", m_vertical);
        }
        if (Input.GetKeyDown(KeyCode.Space) && m_charactercontroller.isGrounded)
        {
            isjumping = true;
           
           /* m_animator.SetTrigger("jump");*/
        }
       
    }
    private void FixedUpdate()
    {
        if(Mathf.Abs(m_vertical)>0.1f || Mathf.Abs(m_horizontal)>0.1f)
        {
            transform.forward= m_cameralogic.forwardvector();
        }
        if (isjumping)
        {
            m_jumpingheight.y = m_height;
            isjumping = false;
        }
       /* transform.forward = Camera.main.transform.forward;*/
        m_horizontalmovement = transform.right * m_horizontal * speed * Time.deltaTime;
        m_verticalmovement = Camera.main.transform.forward * m_vertical * speed * Time.deltaTime;
        m_jumpingheight.y -= m_gravity * Time.deltaTime;


        if (m_charactercontroller)
        {
            m_charactercontroller.Move(m_horizontalmovement + m_verticalmovement + m_jumpingheight);
        }

        if (m_charactercontroller.isGrounded)
        {
            m_jumpingheight.y = 0;
        }
    }
    public void playfootstepsound(int index)
    {
        //left 0 and right 1
        if(index==0)
        {
            RaycastTerrain(m_leftfoot.position);
        }
        else if(index==1)
        {
            RaycastTerrain(m_rightfoot.position);
        }
       
    }
    public void RaycastTerrain(Vector3 position)
    {
        LayerMask layer = LayerMask.GetMask("Terrain");
        Ray ray = new Ray(position, Vector3.down);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit,layer))
        {
            string hittag = hit.collider.gameObject.tag;
            if(hittag=="pebbles")
            {
                playrandomsound(m_pebbles_audioclips);
            }   
            else if(hittag=="water")
            {
                playrandomsound(m_audio_water_clips);
            }
            else if(hittag=="grass")
            {
                playrandomsound(m_audio_grass_clips);
            }
        }
    }
    public void playrandomsound(List<AudioClip> audioclips)
    {
        if (audioclips.Count > 0 && m_audiosource)
        {
            int random = Random.Range(0, audioclips.Count - 1);
            m_audiosource.PlayOneShot(audioclips[random]);
        }
    }
}
