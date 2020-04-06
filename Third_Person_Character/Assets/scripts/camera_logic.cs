using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_logic : MonoBehaviour
{
    Transform player;
    Vector3 cameratarget;
    float m_rotation_y;
    float m_rotation_x;
    float m_scroll_z;
    /*float zoom = 80f;
    float zoom_change = 80f;*/
    const float max_value = 20f;
    const float min_value = -20f;
    /*const float max_value_z = 15.0f;
    const float min_value_z = 20.0f;*/

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        cameratarget = player.position;
        cameratarget.y += 1.9f;
        m_rotation_y += Input.GetAxis("Mouse X");
        m_rotation_x -= Input.GetAxis("Mouse Y");

        m_rotation_x = Mathf.Clamp(m_rotation_x, min_value, max_value);
        /*if (Input.GetButton("Fire2"))
        {

            m_rotation_y += Input.GetAxis("Mouse X");
            m_rotation_x -= Input.GetAxis("Mouse Y");

            m_rotation_x = Mathf.Clamp(m_rotation_x, min_value, max_value);

        }*/
        /* if(Input.GetKey(KeyCode.Return))
         {
             zoom -= zoom_change * Time.deltaTime;
         }
         if (Input.mouseScrollDelta.y > 0)
         {
             zoom += zoom_change * Time.deltaTime;
         }
         Debug.Log(zoom);*/
        /*m_scroll_z = Input.GetAxis("Mouse ScrollWheel");
        m_scroll_z = Mathf.Clamp(m_scroll_z, min_value_z, max_value_z);*/
    }
    private void LateUpdate()
    {
        Vector3 cameraoffset= new Vector3(0, 0, -2.99f);
        Quaternion camerarotation = Quaternion.Euler(m_rotation_x, m_rotation_y, 0);
        transform.position =  cameratarget + camerarotation * cameraoffset;
        /*transform.rotation = camerarotation;*/
        transform.LookAt(cameratarget);
       
    }
 
}
