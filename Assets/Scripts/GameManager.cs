using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    GameObject _fadeImg;
    public bool _introEventFin;
    public Image _rungage;
    public GameObject _inven;
    public GameObject _uiFlower;
    public GameObject _uiFlash;
    public GameObject _uiNametag;
    List<float> _waitTimes = new List<float> { 1f, 1f, 0.6f, 0.6f, 0.3f, 0.3f, 0.2f, 0.2f };
    TaskCompletionSource<bool> _iKeyPressedTask;
    ItemInteract _interact;


    void Awake()
    {
        _fadeImg = GameObject.Find("Fade");
        _rungage = GameObject.Find("Rungage").GetComponent<Image>();
        _interact = FindObjectOfType<ItemInteract>();
        _uiFlash = GameObject.Find("Flashlight");
        _uiFlower = GameObject.Find("Dryfrower");
        _uiNametag = GameObject.Find("NameTag");
        _inven = GameObject.Find("ItemInven");
    }

    void Start()
    {
        _inven.SetActive(false);
        _fadeImg.gameObject.SetActive(false);
        StartCoroutine("Croutine_Intro");
        SoundManager.Instance.PlayBGM(SoundManager.ClipBGM.creep);
        CallPopup();
    }

    void Update()
    {
        
    }
    IEnumerator Croutine_Intro()
    {
        //´«±ôºýÀÓ
        for (int i = 0; i < _waitTimes.Count; i++)
        {
            yield return new WaitForSeconds(_waitTimes[i]);
            _fadeImg.SetActive(!_fadeImg.activeSelf);
        }
        _introEventFin = true;
    }

    async void CallPopup()
    {
        _iKeyPressedTask = new TaskCompletionSource<bool>();

        while (!_iKeyPressedTask.Task.IsCompleted)
        {
            await Task.Yield();

            if (Input.GetKeyDown(KeyCode.I))
            {
                CheckInvenActive();
                _iKeyPressedTask.SetResult(true);

                _iKeyPressedTask = new TaskCompletionSource<bool>();
            }
        }
    }



    void FillInven()
    {
        if(_interact._getFlower)
        {
            _uiFlower.SetActive(true);
        }
        if(_interact._getHandflash)
        {
            _uiFlash.SetActive(true);
        }
        if (_interact._getNameTag)
        {
            _uiNametag.SetActive(true);
        }
    }

    void CheckInvenActive()
    {
        if (!_inven.activeInHierarchy)
            _inven.SetActive(true);
        else
            _inven.SetActive(false);
    }
}
