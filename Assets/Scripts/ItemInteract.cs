using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteract : MonoBehaviour
{
    public bool _getFlower;
    public bool _getNameTag;
    public bool _getHandflash;
    public bool _flowerDialogFin;
    public bool _flashDialogFin;
    public bool _nametagDialogFin;
    public bool _DoorOpen;
    [SerializeField] GameObject _flash;
    [SerializeField] Dialog _dialog;

    IEnumerator Coroutin_FlowerText()
    {
        yield return new WaitForSeconds(2f);
        _dialog._textUI.text = _dialog._flowertexts[1];
        yield return new WaitForSeconds(2f);
        _dialog._textUI.text = _dialog._flowertexts[2];
        _dialog.gameObject.SetActive(false);
    }
    IEnumerator Coroutin_NametagText()
    {
        yield return new WaitForSeconds(2f);
        _dialog._textUI.text = _dialog._nametexts[1];
        yield return new WaitForSeconds(2f);
        _dialog._textUI.text = _dialog._nametexts[2];
        _dialog.gameObject.SetActive(false);
    }
    IEnumerator Coroutin_FlashText()
    {
        yield return new WaitForSeconds(2f);
        _dialog.gameObject.SetActive(false);
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
                _dialog.ShowFlowerText(0);
                _getFlower = true;
                other.gameObject.SetActive(false);
                StartCoroutine("Coroutin_FlowerText");
            }
        }
        if (other.CompareTag("Nametag"))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _dialog.ShowNameTagText(0);
                _getNameTag = true;
                other.gameObject.SetActive(false);
                StartCoroutine("Coroutin_NametagText");
            }
        }
        if (other.CompareTag("Handflash"))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _dialog.ShowFlashText(0);
                _getHandflash = true;
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
