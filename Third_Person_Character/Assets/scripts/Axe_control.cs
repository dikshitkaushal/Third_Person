using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Axe_control : MonoBehaviour
{
    Animator m_animator;
    public GameObject axe;
    Rigidbody rb;
    public float throwforce = 50f;
    public float torqueforce = 1000f;
    public Transform parent;
    bool isreturning = false;
    public Transform targetpos;
    public Transform bezierpos;
    float t = 0.0f;
    Vector3 axepos;
    // Start is called before the first frame update
    void Start()
    {
        m_animator = GetComponent<Animator>();
        rb = axe.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            m_animator.SetTrigger("throwaxe");
        }
        if(Input.GetButtonDown("Fire2"))
        {
            m_animator.SetTrigger("returnaxe");
            
        }
        if(isreturning)
        {
            //calculation
            if (t < 1.0f)
            {
                t += Time.deltaTime;
                axe.transform.position= beziercalc(t, axepos, bezierpos.position, targetpos.position);
                rb.rotation = Quaternion.Slerp(rb.transform.rotation, targetpos.rotation, 50f * Time.deltaTime);
            }
            else
            {
                resetpos();
            }
        }
    }

    private void resetpos()
    {
        t = 0.0f;
        isreturning = false;
        axe.transform.parent = parent;
        axe.transform.position = targetpos.position;
        axe.transform.rotation = targetpos.rotation;

    }

    Vector3 beziercalc(float t,Vector3 oldpos,Vector3 curvepos,Vector3 newpos)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 p = (uu * oldpos) + (2 * u * t * curvepos) + (tt * newpos);
        return p;
    }

    public void axethrow()
    {
        isreturning = false;
        rb.isKinematic = false;
        axe.transform.parent = null;
        rb.AddForce(Camera.main.transform.TransformDirection(Vector3.forward) * throwforce, ForceMode.Impulse);
        rb.AddTorque(axe.transform.TransformDirection(Vector3.right)*torqueforce, ForceMode.Impulse);
    }
    public void axereturn()
    {
        axepos = axe.transform.position;
        isreturning = true;
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
    }
}
