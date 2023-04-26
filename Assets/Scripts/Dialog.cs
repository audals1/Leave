using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialog : SingletonMonoBehaviour<Dialog>
{
    [TextArea] public string[] m_introtexts;
    [TextArea] public string[] m_texts;
    public TextMeshProUGUI m_textUI;
    [SerializeField] int m_textIndex;
    [SerializeField] int m_itemtextIndex;
    [SerializeField] ItemInteract m_item;
    public bool m_isDaloging;
    public bool m_introDialogFin;
    public bool m_DalogFin;


    void Update()
    {
        ShowIntroText(0);
        IntroDialog();
        FlowerText();
        NameTagText();
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
    public void FlowerText()
    {
        if(m_item.m_getFlower)
        {
            if (Input.GetKeyDown(KeyCode.Space) && m_itemtextIndex <= 3)
            {
                m_itemtextIndex = 0;
                m_textUI.text = m_texts[m_itemtextIndex];
                m_textUI.gameObject.SetActive(true);
                m_itemtextIndex++;
            }
            if(m_itemtextIndex > 3)
            {
                m_textUI.gameObject.SetActive(false);
            }
        }
    }
    public void NameTagText()
    {
        if (m_item.m_getNameTag)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                for (int i = 3; i < 6; i++)
                {
                    m_textUI.text = m_texts[i];
                    m_textUI.gameObject.SetActive(true);
                }
            }
        }
    }
}
