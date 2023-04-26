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
    }
    void OnTriggerExit(Collider other)
    {
        Debug.Log("»óÈ£ÀÛ¿ë ¹üÀ§ ¹þ¾î³²");
    }
}
