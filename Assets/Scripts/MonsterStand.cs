using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStand : MonoBehaviour
{
    public float m_detectDist = 0.01f;//인식거리 이 거리 안으로 들어오면 겜오버
    [SerializeField] Player m_player;
    [SerializeField] Camera m_ghostCam;
    [SerializeField] GameObject m_ghostLight;
    void Update()
    {
        if(FindTarget(m_player.transform.position))
        {
            m_ghostCam.gameObject.SetActive(true);
            m_ghostLight.gameObject.SetActive(true);
            m_player.SetDie();
        }
    }
    bool FindTarget(Vector3 target)
    {
        Vector3 start = transform.position + Vector3.up * 1f;
        Vector3 end = target + Vector3.up * 1f;
        var dir = target - transform.position;
        RaycastHit hit;

        if (Physics.Raycast(start, dir.normalized, out hit, m_detectDist, 1 << LayerMask.NameToLayer("Player")))
        {
            if (hit.collider.CompareTag("Player")) //Player 맞았으면 트루반환
                return true;
        }
        return false; //배경 맞은경우
    }
}
