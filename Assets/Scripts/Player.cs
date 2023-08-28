using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Player : MonoBehaviour
{
    public float _speed = 5f;
    public float _runSpd = 10f;
    Vector3 _dir;
    Rigidbody _rigid;
    GameObject _flash;
    ItemInteract _itemInteract;
    GameManager _gameManager;
    Dialog _dialog;

    public KeyCode flashKeyboard = KeyCode.F;

    void Start()
    {
        _flash = GameObject.Find("HandFlash");
        _itemInteract = FindObjectOfType<ItemInteract>();
        _rigid = GetComponent<Rigidbody>();
        _gameManager = FindObjectOfType<GameManager>();
        _dialog = FindObjectOfType<Dialog>();
        TurnOnFlash();
    }
    void FixedUpdate()
    {
        Move();
    }
    void Move()
    {
        if(_gameManager._introEventFin && _dialog._introDialogFin)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            float time = 0;
            float value = time + Time.deltaTime * 0.5f;
            if (Input.GetKey(KeyCode.LeftShift) && _gameManager._rungage.fillAmount > 0)
            {
                _dir = new Vector3(h, 0, v) * _runSpd * Time.deltaTime;
                _gameManager._rungage.fillAmount -= value;
            }
            else
            {
                _gameManager._rungage.fillAmount += value;
                _dir = new Vector3(h, 0, v) * _speed * Time.deltaTime;
            }
            //메인카메라 정면방향 가져와서 그 방향으로 틀기
            var camForward = Camera.main.transform.forward;
            camForward.y = 0f;
            transform.LookAt(transform.position + camForward);
            _dir = transform.TransformDirection(_dir);
            transform.position += _dir;
        }
    }
    async void TurnOnFlash()
    {
        if (!_itemInteract._getHandflash) return;
        while (!Input.GetKeyDown(flashKeyboard)) await Task.Yield();
        {
            if(_flash.activeInHierarchy)
            {
                _flash.SetActive(false);
            }
            else
            {
                _flash.SetActive(true);
            }
            TurnOnFlash();
        }
    }
    public void SetDie()
    {
        SoundManager.Instance.StopBGM(SoundManager.ClipBGM.creep);
        SoundManager.Instance.PlaySFX(SoundManager.ClipSFX.Gameover);
        Debug.Log("게임오버");
    }
}
