using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectTrigger : MonoBehaviour
{
    [SerializeField] GameObject m_ghostCam;
    [SerializeField] GameObject m_ghostLight;
    [SerializeField] Player m_player;
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            m_ghostCam.SetActive(true);
            m_ghostLight.SetActive(true);
        }
    }
}
