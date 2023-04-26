using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    [SerializeField] GameObject m_fadeImg;
    [SerializeField] Camera m_mainCam;
    public bool m_introEventFin;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Croutine_Intro");
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
    IEnumerator Coroutin_IntroDialog()
    {
        yield return null;
        Dialog.Instance.ShowIntroText(0);
    }
}
