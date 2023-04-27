using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    Animator m_anim;
    bool m_isOpen;
    // Start is called before the first frame update
    void Start()
    {
        m_anim = GetComponent<Animator>();
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Interact"))
        {
            if (Input.GetKeyDown(KeyCode.Space) && !m_isOpen)
            {
                m_anim.SetBool("Open", true);
                m_isOpen = true;
            }
            else if(Input.GetKeyDown(KeyCode.Space) && m_isOpen)
            {
                m_anim.SetBool("Open", false);
                m_isOpen = false;
            }
        }
        
    }
}
