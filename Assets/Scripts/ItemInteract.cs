using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteract : MonoBehaviour
{
    public bool m_getFlower;
    public bool m_getNameTag;
    public bool m_getHandflash;
    public bool m_flowerDialogFin;
    public bool m_flashDialogFin;
    public bool m_nametagDialogFin;
    public bool m_DoorOpen;
    [SerializeField] GameObject m_flash;
    [SerializeField] Dialog m_dialog;

    void Update()
    {

    }
    IEnumerator Coroutin_FlowerText()
    {
        yield return new WaitForSeconds(2f);
        m_dialog.m_textUI.text = m_dialog.m_flowertexts[1];
        yield return new WaitForSeconds(2f);
        m_dialog.m_textUI.text = m_dialog.m_flowertexts[2];
        m_dialog.gameObject.SetActive(false);
    }
    IEnumerator Coroutin_NametagText()
    {
        yield return new WaitForSeconds(2f);
        m_dialog.m_textUI.text = m_dialog.m_nametexts[1];
        yield return new WaitForSeconds(2f);
        m_dialog.m_textUI.text = m_dialog.m_nametexts[2];
        m_dialog.gameObject.SetActive(false);
    }
    IEnumerator Coroutin_FlashText()
    {
        yield return new WaitForSeconds(2f);
        m_dialog.gameObject.SetActive(false);
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
                StartCoroutine("Coroutin_FlowerText");
            }
        }
        if (other.CompareTag("Nametag"))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_dialog.ShowNameTagText(0);
                m_getNameTag = true;
                other.gameObject.SetActive(false);
                StartCoroutine("Coroutin_NametagText");
            }
        }
        if (other.CompareTag("Handflash"))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_dialog.ShowFlashText(0);
                m_getHandflash = true;
                other.gameObject.SetActive(false);
                StartCoroutine("Coroutin_FlashText");
            }
        }
        if(other.CompareTag("Door"))
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                other.gameObject.transform.position = new Vector3(other.transform.position.x + 3, other.transform.position.y, other.transform.position.z);
            }
        }
    }
}
