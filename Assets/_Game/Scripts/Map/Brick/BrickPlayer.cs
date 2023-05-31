using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickPlayer : MonoBehaviour
{
    [SerializeField] BrickPooler m_BrickPooler;
    [SerializeField] Transform m_OriginPos;
    public List<GameObject> m_Bricks = new List<GameObject>();
    void Start()
    {
    }

    public void Spawn(int a_BrickPos,ColorType a_Color)
    {
       GameObject brick = m_BrickPooler.Spawn(transform, m_OriginPos.position + transform.up * 0.32f * a_BrickPos, transform.rotation, a_Color);
        if (brick != null) m_Bricks.Add(brick);
    }
    public void DeSpawn(GameObject a_brick)
    {
        m_BrickPooler.Despawn(a_brick);
    }
    public void DeSpawnLast()
    {
        if (m_Bricks.Count <= 0) return;
        m_BrickPooler.Despawn(m_Bricks[m_Bricks.Count - 1]);
        m_Bricks.RemoveAt(m_Bricks.Count - 1);
    }
}
