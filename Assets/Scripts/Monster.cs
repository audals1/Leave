using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    /// <summary>
    /// ���� ai FSM
    /// ���� enum 3���� idle, patrol, chase ����̱ͽŸ� ��������
    /// �νĹ��� ���� �� Ÿ�� �ν� �Լ� �ʿ�
    /// �����ͽ��� ���ݰ��ɰŸ� �Ǵ� �Լ� �߰�
    /// ��Ʈ���� ����Ʈ�� �迭 �ʿ�
    /// 
    /// </summary>
    #region Property
    public enum MonsterState
    {
        Idle,
        Patrol,
        Chase,
        Attack,
        Max
    };

    public float m_idletime; //�ð�
    protected float m_idleduration = 1f; //�ൿ���� ���ð�
    [SerializeField] float m_detectDist = 10f; //�÷��̾� �ν� ���� �Ÿ�
    protected float m_attackDist = 1f;
    protected bool m_isPatrol;

    [SerializeField] WayPoint[] m_wayPoints;
    protected int m_currentWayPoint;
    [SerializeField] protected Player m_player;//�÷��̾ü
    protected NavMeshAgent m_navAgent;
    protected Animator m_animator;
    public MonsterState m_state;//enum

    #endregion
    void Start()
    {
        m_navAgent = GetComponent<NavMeshAgent>();
        m_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        BehaviourProcess();
    }
    public void SetState(MonsterState state)
    {
        m_state = state;
    }
    protected bool FindTarget(Vector3 target)
    {
        Vector3 start = transform.position + Vector3.up * 1f;
        Vector3 end = target + Vector3.up * 1f;
        var dir = target - transform.position;
        RaycastHit hit;

        if (Physics.Raycast(start, dir.normalized, out hit, m_detectDist, 1 << LayerMask.NameToLayer("Ground") | 1 << LayerMask.NameToLayer("Player")))
        {
            if (hit.collider.CompareTag("Player")) //Player �¾����� Ʈ���ȯ
                return true;
        }
        return false; //��� �������
    }

    protected bool CheckArea(Vector3 target, float area) //�Ÿ� ���� üũ
    {
        var dist = target - transform.position;
        if (Mathf.Approximately(dist.sqrMagnitude, area) || dist.sqrMagnitude < area)
        {
            return true;
        }
        return false;
    }

    protected virtual void SetIdle(float time)
    {
        m_navAgent.isStopped = true;
        m_navAgent.ResetPath();
        m_isPatrol = false;
        SetState(MonsterState.Idle);
        m_idletime = m_idleduration - time;
    }

    IEnumerator Coroutine_SerchTarget()
    {
        while (m_state == MonsterState.Chase)
        {
            for (int i = 0; i < 10; i++)
            {
                yield return null;
            }
            m_navAgent.SetDestination(m_player.transform.position);
        }
    }

    void BehaviourProcess()
    {
        switch (m_state)
        {
            case MonsterState.Idle:
                m_idletime += Time.deltaTime;
                if(m_idletime >= m_idleduration) //�ൿ����
                {
                    if(FindTarget(m_player.transform.position)) 
                    {
                        if(CheckArea(m_player.transform.position, 2f))
                        {
                            Debug.Log("������ȯ");
                            SetState(MonsterState.Attack);
                            return;
                        }
                        SetState(MonsterState.Chase);
                        StartCoroutine("Coroutine_SerchTarget");
                        m_navAgent.stoppingDistance = m_attackDist;
                    }
                    else // �ֺ��� ���ΰ� ����
                    {
                        Debug.Log("��Ʈ�� ����Ʈ�� �̵�");
                        SetState(MonsterState.Patrol);
                    }
                    m_idletime = 0f;
                }

                break;

            case MonsterState.Patrol:
                if(!FindTarget(m_player.transform.position)) 
                {
                    if(!m_isPatrol) //waypoint �̵����� �ƴҶ�
                    {
                        m_navAgent.SetDestination(m_wayPoints[m_currentWayPoint].transform.position);
                        m_isPatrol = true;
                    }
                    else //waypoint �̵��� �϶�
                    {
                        if(CheckArea(m_wayPoints[m_currentWayPoint].transform.position,Mathf.Pow(m_navAgent.radius, 4f)))
                        {
                            m_currentWayPoint++;
                            if(m_currentWayPoint > m_wayPoints.Length - 1)
                            {
                                m_currentWayPoint = 0;
                            }
                            SetIdle(1f);
                        }
                    }
                }
                else //patrol�߿� �÷��̾� ã��
                {
                    Debug.Log("��Ʈ���� �÷��̾� �ν�");
                    SetIdle(0f);
                }

                break;

            case MonsterState.Chase:
                if(FindTarget(m_player.transform.position))
                {
                    if(CheckArea(m_player.transform.position, m_attackDist * 2))
                    {
                        SetState(MonsterState.Attack);
                    }
                }
                else //�߰��߿� �÷��̾� ��ħ
                {
                    Debug.Log("���ΰ� Ʀ");
                    SetIdle(1f);
                }

                break;

            case MonsterState.Attack:
                SetIdle(2f);
                break;
            
            default:
                break;
        }
    }
}
