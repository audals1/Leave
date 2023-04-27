using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStand : MonoBehaviour
{
    public float m_detectDist = 0.01f;//�νİŸ� �� �Ÿ� ������ ������ �׿���
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
            if (hit.collider.CompareTag("Player")) //Player �¾����� Ʈ���ȯ
                return true;
        }
        return false; //��� �������
    }
}
