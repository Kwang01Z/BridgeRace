using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AiBotController : CharacterBase
{
    [SerializeField] NavMeshAgent m_NavMeshAgent;
    [SerializeField] int m_BrickCountMax;
    [SerializeField] MapManager m_MapManager;
    IState<AiBotController> m_CurrentState;
    int m_CurrentBridgeIndex = -1;
    int m_OldBridgeIndex = -1;
    bool m_IsFullBrick;
    protected override void Reset()
    {
        base.Reset();
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
    }
    protected override void Start()
    {
        base.Start();
        ChangeState(new BotIdle());
        m_BrickCountMax = Random.Range(5, 10);
    }
    protected override void UpdateMore()
    {
        base.UpdateMore();
        UpdateState();
        m_CurrentState?.OnExecute(this);
    }
    protected override void OnTriggerBrickMore(Brick a_brick)
    {
        base.OnTriggerBrickMore(a_brick);
        if (m_BrickCount < m_BrickCountMax)
        {
            return;
        }
    }
    public void ChangeState(IState<AiBotController> a_NewState)
    {
        if (m_CurrentState != null && m_CurrentState.GetType().Equals(a_NewState.GetType())) return;
        m_CurrentState?.OnExit(this);
        m_CurrentState = a_NewState;
        m_CurrentState?.OnEnter(this);
    }
    void UpdateState()
    {
        if (m_Bricks.Count > 0)
        {
            ChangeState(new BotRun());
        }
        else
        {
            ChangeState(new BotIdle());
        }
        if (m_BrickCount >= m_BrickCountMax)
        {
            m_IsFullBrick = true;
            if (m_CurrentBridgeIndex < 0)
            {
                m_CurrentBridgeIndex = Random.Range(0, m_CurrentFloor.GetBridgeManagers().Count);
            }
        }
        else if (m_BrickCount <= 0)
        {
            m_IsFullBrick = false;
        }
    }
    public void Move()
    {
        if (IsCollisionWithFloor())
        {
            if (m_IsFullBrick && m_CurrentBridgeIndex >= 0)
            {
                Stair currentstair = m_CurrentFloor.GetBridgeManagers()[m_CurrentBridgeIndex].GetStairs()[0];
                if (m_OldBridgeIndex >= 0)
                {
                    currentstair = m_CurrentFloor.GetBridgeManagers()[m_OldBridgeIndex].GetStairs()[0];
                }
                m_NavMeshAgent.SetDestination(currentstair.transform.position);
            }
            else
            {
                ReLoadCurrentBrick();
                if (m_CurrentBrick && m_BrickCount < m_BrickCountMax)
                {
                    m_NavMeshAgent.SetDestination(m_CurrentBrick.transform.position);
                }
            }
        }
    }
    public override void OnTriggerStair(Stair a_stair)
    {
        base.OnTriggerStair(a_stair);
        MoveToNextStair(a_stair);
    }
    void MoveToNextStair(Stair a_stair)
    {
        if (!a_stair) return;
        if (m_BrickCount > 0)
        {
            print(m_CurrentBridgeIndex);
            if (m_CurrentBridgeIndex >= 0 && m_CurrentFloor.GetBridgeManagers()[m_CurrentBridgeIndex].HasNextStair(a_stair))
            {
                Stair nextstair = m_CurrentFloor.GetBridgeManagers()[m_CurrentBridgeIndex].GetNextStair(a_stair);
                m_NavMeshAgent.SetDestination(nextstair.transform.position+nextstair.transform.up);
            }
            else
            {
                if (m_MapManager.GetNextFloor(m_CurrentFloor) != null)
                {
                    m_NavMeshAgent.SetDestination(m_MapManager.GetNextFloor(m_CurrentFloor).transform.position);
                    m_OldBridgeIndex = -1;
                    m_CurrentBridgeIndex = -1;
                    m_IsFullBrick = false;
                }
                else
                {
                    print(m_CurrentFloor);
                    ChangeState(new BotIdle());
                }
            }
        }
        else
        {
            m_NavMeshAgent.SetDestination(m_CurrentFloor.transform.position);
            m_OldBridgeIndex = m_CurrentBridgeIndex;
        } 
    }
    public NavMeshAgent GetAgent()
    {
        return m_NavMeshAgent;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, m_NavMeshAgent.nextPosition);
    }
}
