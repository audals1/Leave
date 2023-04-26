using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    [SerializeField] GameObject m_player;
    public float m_offsetX;
    public float m_offsetY = 1f ;
    public float m_offsetZ;
    public float m_camSpd = 10f;

    //1��Ī �׽�Ʈ
    float m_rotateX; //x�� ���� �� ���庯��
    float m_rotateY; //y�� ���� �� ���庯��
    float m_rotateOffsetX; //x�� ���� ��
    float m_rotateOffsetY;// Y�� ���� ��
    float m_rotateXmax = 45f;//x�� ȸ�� �ִ밪
    float m_rotateXmin = -45f;//x�� ȸ�� �ּҰ�

    void Start()
    {
        transform.rotation = Quaternion.identity;    
    }

    void FixedUpdate()
    {
        FollowTarget();            
    }
    void Update()
    {
        STCam();    
    }
    public void FollowTarget()
    {
        Vector3 target = new Vector3(m_player.transform.position.x + m_offsetX, m_player.transform.position.y + m_offsetY, m_player.transform.position.z + m_offsetZ);
        transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * m_camSpd);
    }
    public void STCam()
    {
        if(GameManager.Instance.m_introEventFin)
        {
            m_rotateOffsetX = -Input.GetAxis("Mouse Y") * m_camSpd * Time.deltaTime; //���Ϲݴ� X�� ȸ����
            m_rotateOffsetY = Input.GetAxis("Mouse X") * m_camSpd * Time.deltaTime; //Y�� ȸ����
            m_rotateY = transform.eulerAngles.y + m_rotateOffsetY; //Y�� ȸ��
            m_rotateX = m_rotateX + m_rotateOffsetX;
            m_rotateX = Mathf.Clamp(m_rotateX, m_rotateXmin, m_rotateXmax); //���� ȸ�� �� �ִ��ּ� ���� ����
            transform.eulerAngles = new Vector3(m_rotateX, m_rotateY, 0);
        }
    }
}
