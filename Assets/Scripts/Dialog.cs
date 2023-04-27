using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialog : SingletonMonoBehaviour<Dialog>
{
    [TextArea] public string[] m_introtexts;
    [TextArea] public string[] m_nametexts;
    [TextArea] public string[] m_flowertexts;
    [TextArea] public string[] m_flashtexts;
    public TextMeshProUGUI m_textUI;
    [SerializeField] int m_textIndex;
    public int m_itemtextIndex;
    [SerializeField] ItemInteract m_item;
    public bool m_isDaloging;
    public bool m_introDialogFin;
    public bool m_DalogFin;


    void Update()
    {
        ShowIntroText(0);
        IntroDialog();
    }
    public void ShowIntroText(int index)
    {
        if(GameManager.Instance.m_introEventFin && !m_introDialogFin)
        {
            m_textUI.text = m_introtexts[m_textIndex];
            m_textUI.gameObject.SetActive(true);
        }
    }
    public void IntroDialog()
    {
        if (GameManager.Instance.m_introEventFin && Input.GetKeyDown(KeyCode.Space) && !m_introDialogFin)
        {
            if (m_textIndex < 4) // 대화창 진행중
            {
                m_isDaloging = true;
                m_textIndex++;
                m_textUI.text = m_introtexts[m_textIndex];
            }
            else //대화창 다 돌음
            {
                m_introDialogFin = true;
                m_isDaloging = false;
                m_textUI.gameObject.SetActive(false);
            }
        }
    }
    public void ShowFlowerText(int index)
    {
        m_textUI.gameObject.SetActive(true);
        m_textUI.text = m_flowertexts[index];
    }
    public void ShowFlashText(int index)
    {
        m_textUI.gameObject.SetActive(true);
        m_textUI.text = m_flowertexts[index];
    }
    public void ShowNameTagText(int index)
    {
        m_textUI.gameObject.SetActive(true);
        m_textUI.text = m_nametexts[index];
    }
}
