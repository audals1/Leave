using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PressStart : MonoBehaviour
{
    [SerializeField] GameObject m_introText;
    public bool m_isPresskey;
    IEnumerator Coroutin_Start()
    {
        int count = 0;
        while(count < 3)
        {
            m_isPresskey = true;
            m_introText.SetActive(false);
            yield return new WaitForSeconds(0.3f);
            m_introText.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            count++;
        }
        SceneManager.LoadScene("Game");
    }
    void PressAnykey()
    {
        if (Input.anyKey)
        {
            StartCoroutine("Coroutin_Start");
        }
    }
    void Update()
    {
        PressAnykey();
    }
}
