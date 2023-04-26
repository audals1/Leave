using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    /// <summary>
    /// 몬스터 ai FSM
    /// 상태 enum 3가지 idle, patrol, chase 방망이귀신만 공격있음
    /// 인식범위 지정 및 타겟 인식 함수 필요
    /// 빠따귀신은 공격가능거리 판단 함수 추가
    /// 패트롤할 포인트들 배열 필요
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

    public float m_idletime; //시간
    protected float m_idleduration = 1f; //행동개시 대기시간
    [SerializeField] float m_detectDist = 10f; //플레이어 인식 가능 거리
    protected float m_attackDist = 1f;
    protected bool m_isPatrol;

    [SerializeField] WayPoint[] m_wayPoints;
    protected int m_currentWayPoint;
    [SerializeField] protected Player m_player;//플레이어객체
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
            if (hit.collider.CompareTag("Player")) //Player 맞았으면 트루반환
                return true;
        }
        return false; //배경 맞은경우
    }

    protected bool CheckArea(Vector3 target, float area) //거리 도달 체크
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
                if(m_idletime >= m_idleduration) //행동개시
                {
                    if(FindTarget(m_player.transform.position)) 
                    {
                        if(CheckArea(m_player.transform.position, 2f))
                        {
                            Debug.Log("공격전환");
                            SetState(MonsterState.Attack);
                            return;
                        }
                        SetState(MonsterState.Chase);
                        StartCoroutine("Coroutine_SerchTarget");
                        m_navAgent.stoppingDistance = m_attackDist;
                    }
                    else // 주변에 주인공 없음
                    {
                        Debug.Log("패트롤 포인트로 이동");
                        SetState(MonsterState.Patrol);
                    }
                    m_idletime = 0f;
                }

                break;

            case MonsterState.Patrol:
                if(!FindTarget(m_player.transform.position)) 
                {
                    if(!m_isPatrol) //waypoint 이동중이 아닐때
                    {
                        m_navAgent.SetDestination(m_wayPoints[m_currentWayPoint].transform.position);
                        m_isPatrol = true;
                    }
                    else //waypoint 이동중 일때
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
                else //patrol중에 플레이어 찾음
                {
                    Debug.Log("패트롤중 플레이어 인식");
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
                else //추격중에 플레이어 놓침
                {
                    Debug.Log("주인공 튐");
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
