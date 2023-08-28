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

    //1인칭 테스트
    float _rotateX; //x축 더한 값 저장변수
    float _rotateY; //y축 더한 값 저장변수
    float _rotateOffsetX; //x축 더할 값
    float _rotateOffsetY;// Y축 더할 값
    float _rotateXmax = 45f;//x축 회전 최대값
    float _rotateXmin = -45f;//x축 회전 최소값

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
            _rotateOffsetX = -Input.GetAxis("Mouse Y") * _camSpd * Time.deltaTime; //상하반대 X축 회전값
            _rotateOffsetY = Input.GetAxis("Mouse X") * _camSpd * Time.deltaTime; //Y축 회전값
            _rotateY = transform.eulerAngles.y + _rotateOffsetY; //Y축 회전
            _rotateX = _rotateX + _rotateOffsetX;
            _rotateX = Mathf.Clamp(_rotateX, _rotateXmin, _rotateXmax); //상하 회전 값 최대최소 범위 지정
            transform.eulerAngles = new Vector3(_rotateX, _rotateY, 0);
        }
    }
}
