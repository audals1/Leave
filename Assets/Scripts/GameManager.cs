using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    [SerializeField] GameObject m_fadeImg;
    public bool m_introEventFin;
    public Image m_rungage;
    public GameObject m_inven;
    public GameObject m_uiFlower;
    public GameObject m_uiFlash;
    public GameObject m_uiNametag;
    [SerializeField] ItemInteract m_interact;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Croutine_Intro");
        SoundManager.Instance.PlayBGM(SoundManager.ClipBGM.creep);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            CallPopup();
            FillInven();
        }
    }
    IEnumerator Croutine_Intro()
    {
        //´«±ôºýÀÓ
        yield return new WaitForSeconds(1f);
        m_fadeImg.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        m_fadeImg.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.6f);
        m_fadeImg.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.6f);
        m_fadeImg.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        m_fadeImg.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        m_fadeImg.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        m_fadeImg.gameObject.SetActive(false);
        m_introEventFin = true;
    }
    void CallPopup()
    {
        if(!m_inven.activeInHierarchy)
        {
            m_inven.SetActive(true);
        }
        else
        {
            m_inven.SetActive(false);
        }
    }
    void FillInven()
    {
        if(m_interact.m_getFlower)
        {
            m_uiFlower.SetActive(true);
        }
        if(m_interact.m_getHandflash)
        {
            m_uiFlash.SetActive(true);
        }
        if (m_interact.m_getNameTag)
        {
            m_uiNametag.SetActive(true);
        }
    }
}
