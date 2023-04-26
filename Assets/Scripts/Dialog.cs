using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialog : SingletonMonoBehaviour<Dialog>
{
    [TextArea] public string[] m_introtext;
    public TextMeshProUGUI m_textUI;
    [SerializeField] int m_textIndex;
    public bool m_isDaloging;
    public bool m_isDalogFin;

    void Update()
    {
        ShowText();
        NextText();
    }
    void ShowText()
    {
        if(GameManager.Instance.m_introEventFin && !m_isDaloging && !m_isDalogFin)
        {
            m_textUI.text = m_introtext[0];
            m_textUI.gameObject.SetActive(true);
        }
        
    }
    void NextText()
    {
        if (GameManager.Instance.m_introEventFin && Input.GetKeyDown(KeyCode.Space) && !m_isDalogFin)
        {
            if (m_textIndex < m_introtext.Length - 1) // 대화창 진행중
            {
                m_isDaloging = true;
                m_textIndex++;
                m_textUI.text = m_introtext[m_textIndex];
            }
            else //대화창 다 돌음
            {
                m_textUI.gameObject.SetActive(false);
                m_isDalogFin = true;
                m_isDaloging = false;
            }
        }
    }
    void ResetDialog()
    {
        m_isDalogFin = false;
        m_isDaloging = false;
    }
}
