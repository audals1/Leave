using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteract : MonoBehaviour
{
    public bool m_getFlower;
    public bool m_getNameTag;
    public bool m_getHandflash;
    public bool m_isdoorOpen;
    [SerializeField] GameObject m_flash;
    [SerializeField] Dialog m_dialog;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            int count = 0;
            if(m_getFlower)
            {
                while(count < m_dialog.m_flowertexts.Length - 1)
                {
                    m_dialog.m_textUI.text = m_dialog.m_flowertexts[count];
                    count++;
                }
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Flower"))
        {
            Debug.Log(other.tag + "À» È¹µæÇÒ±î¿ä?");
        }
        if (other.CompareTag("Nametag"))
        {
            Debug.Log(other.tag + "À» È¹µæÇÒ±î¿ä?");
        }
        if (other.CompareTag("Handflash"))
        {
            Debug.Log(other.tag + "À» È¹µæÇÒ±î¿ä?");
        }

    }
    void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Flower"))
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                m_dialog.ShowFlowerText(0);
                m_getFlower = true;
                other.gameObject.SetActive(false);
            }
        }
        if (other.CompareTag("Nametag"))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_getNameTag = true;
                other.gameObject.SetActive(false);
            }
        }
        if (other.CompareTag("Handflash"))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                
                m_getHandflash = true;
                other.gameObject.SetActive(false);
            }
        }
        if (other.CompareTag("Door"))
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                if (!m_isdoorOpen)
                {
                    other.transform.position = new Vector3(other.transform.position.x + 3, other.transform.position.y, other.transform.position.z);
                    m_isdoorOpen = true;
                }
                else
                {
                    other.transform.position = new Vector3(other.transform.position.x - 3, other.transform.position.y, other.transform.position.z);
                    m_isdoorOpen = false;
                }
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        Debug.Log("»óÈ£ÀÛ¿ë ¹üÀ§ ¹þ¾î³²");
    }
}
