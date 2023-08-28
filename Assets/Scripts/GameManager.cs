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
    [SerializeField] ItemInteract _interact;


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
    }
    IEnumerator Croutine_Intro()
    {
        //��������
        yield return new WaitForSeconds(1f);
        _fadeImg.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        _fadeImg.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.6f);
        _fadeImg.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.6f);
        _fadeImg.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        _fadeImg.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        _fadeImg.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        _fadeImg.gameObject.SetActive(false);
        _introEventFin = true;
    }

    async void CallPopup()
    {
        while (!Input.GetKeyDown(KeyCode.I))
        {
            await Task.Yield();
        }

        if (!_inven.activeInHierarchy)
        {
            _inven.SetActive(true);
        }
        else
        {
            _inven.SetActive(false);
        }

        CallPopup();
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
}
