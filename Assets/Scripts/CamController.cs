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

    //1인칭 테스트
    float m_rotateX; //x축 더한 값 저장변수
    float m_rotateY; //y축 더한 값 저장변수
    float m_rotateOffsetX; //x축 더할 값
    float m_rotateOffsetY;// Y축 더할 값
    float m_rotateXmax = 45f;//x축 회전 최대값
    float m_rotateXmin = -45f;//x축 회전 최소값

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
            m_rotateOffsetX = -Input.GetAxis("Mouse Y") * m_camSpd * Time.deltaTime; //상하반대 X축 회전값
            m_rotateOffsetY = Input.GetAxis("Mouse X") * m_camSpd * Time.deltaTime; //Y축 회전값
            m_rotateY = transform.eulerAngles.y + m_rotateOffsetY; //Y축 회전
            m_rotateX = m_rotateX + m_rotateOffsetX;
            m_rotateX = Mathf.Clamp(m_rotateX, m_rotateXmin, m_rotateXmax); //상하 회전 값 최대최소 범위 지정
            transform.eulerAngles = new Vector3(m_rotateX, m_rotateY, 0);
        }
    }
}
