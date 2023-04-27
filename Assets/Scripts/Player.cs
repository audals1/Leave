using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float m_speed = 5f;
    public float m_runSpd = 10f;
    Vector3 m_dir;
    Rigidbody m_rigid;
    [SerializeField] GameObject m_flash;
    [SerializeField] ItemInteract m_itemInteract;
    // Start is called before the first frame update
    void Start()
    {
        m_rigid = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        Move();
        TurnOnFlash();
    }
    void Move()
    {
        if(GameManager.Instance.m_introEventFin && Dialog.Instance.m_introDialogFin)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            if(Input.GetKey(KeyCode.LeftShift))
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
        }
    }
    void TurnOnFlash()
    {
        if(Input.GetKeyDown(KeyCode.L) && m_itemInteract.m_getHandflash)
        {
            if(m_flash.activeInHierarchy)
            {
                m_flash.SetActive(false);
            }
            else
            {
                m_flash.SetActive(true);
            }
        }
    }
    public void SetDie()
    {
        SoundManager.Instance.StopBGM(SoundManager.ClipBGM.creep);
        SoundManager.Instance.PlaySFX(SoundManager.ClipSFX.GhostScream);
        Debug.Log("게임오버");
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
    }
}
