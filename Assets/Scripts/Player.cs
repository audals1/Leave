using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float m_speed = 10f;
    public float m_runSpd = 20f;
    Vector3 m_dir;
    Rigidbody m_rigid;
    // Start is called before the first frame update
    void Start()
    {
        m_rigid = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        Move();
    }
    void Move()
    {
        if(Dialog.Instance.m_isDalogFin)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            if(Input.GetKeyDown(KeyCode.LeftShift))
            {
                m_dir = new Vector3(h, 0, v) * m_runSpd * Time.deltaTime;
            }
            else
            {
                m_dir = new Vector3(h, 0, v) * m_speed * Time.deltaTime;
            }
            //메인카메라 정면방향 가져와서 그 방향으로 틀기
            var camForward = Camera.main.transform.forward;
            camForward.y = 0f;
            transform.LookAt(transform.position + camForward);
            m_dir = transform.TransformDirection(m_dir);
            m_rigid.MovePosition(m_rigid.position + m_dir);
            //transform.Translate(m_dir);
        }
    }
}
