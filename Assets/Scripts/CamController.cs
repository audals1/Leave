using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public Player _player;
    public GameManager _gameManger;
    public float _offsetX;
    public float _offsetY = 1f ;
    public float _offsetZ;
    public float _camSpd = 10f;

    //1��Ī �׽�Ʈ
    float _rotateX; //x�� ���� �� ���庯��
    float _rotateY; //y�� ���� �� ���庯��
    float _rotateOffsetX; //x�� ���� ��
    float _rotateOffsetY;// Y�� ���� ��
    float _rotateXmax = 45f;//x�� ȸ�� �ִ밪
    float _rotateXmin = -45f;//x�� ȸ�� �ּҰ�

    void Start()
    {
        _player = FindObjectOfType<Player>();
        _gameManger = FindObjectOfType<GameManager>();
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
        Vector3 target = new Vector3(_player.transform.position.x + _offsetX, _player.transform.position.y + _offsetY, _player.transform.position.z + _offsetZ);
        transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * _camSpd);
    }
    public void STCam()
    {
        if(_gameManger._introEventFin)
        {
            _rotateOffsetX = -Input.GetAxis("Mouse Y") * _camSpd * Time.deltaTime; //���Ϲݴ� X�� ȸ����
            _rotateOffsetY = Input.GetAxis("Mouse X") * _camSpd * Time.deltaTime; //Y�� ȸ����
            _rotateY = transform.eulerAngles.y + _rotateOffsetY; //Y�� ȸ��
            _rotateX = _rotateX + _rotateOffsetX;
            _rotateX = Mathf.Clamp(_rotateX, _rotateXmin, _rotateXmax); //���� ȸ�� �� �ִ��ּ� ���� ����
            transform.eulerAngles = new Vector3(_rotateX, _rotateY, 0);
        }
    }
}
