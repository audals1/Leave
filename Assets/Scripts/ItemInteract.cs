using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteract : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            Debug.Log(other.name + "을 획득할까요?");
        }
    }
    void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Item"))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                other.gameObject.SetActive(false);
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        Debug.Log("상호작용 범위 벗어남");
    }
}
