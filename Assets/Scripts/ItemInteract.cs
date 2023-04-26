using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteract : MonoBehaviour
{
    public bool m_getFlower;
    public bool m_getNameTag;
    public bool m_getHandflash;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Flower"))
        {
            Debug.Log(other.tag + "�� ȹ���ұ��?");
        }
        if (other.CompareTag("Nametag"))
        {
            Debug.Log(other.tag + "�� ȹ���ұ��?");
        }
        if (other.CompareTag("Handflash"))
        {
            Debug.Log(other.tag + "�� ȹ���ұ��?");
        }

    }
    void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Flower"))
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                Dialog.Instance.m_itemtextIndex = 0;
                Dialog.Instance.m_textUI.gameObject.SetActive(true);
                m_getFlower = true;
                other.gameObject.SetActive(false);
            }
        }
        if (other.CompareTag("Nametag"))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Dialog.Instance.m_itemtextIndex = 3;
                Dialog.Instance.m_textUI.gameObject.SetActive(true);
                m_getNameTag = true;
                other.gameObject.SetActive(false);
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        Debug.Log("��ȣ�ۿ� ���� ���");
    }
}
