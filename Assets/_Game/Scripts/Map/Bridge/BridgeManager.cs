using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeManager : MonoBehaviour
{
    [SerializeField] List<Stair> m_Stairs;
    private void Reset()
    {
        Stair[] stairs = GetComponentsInChildren<Stair>();
        for (int i = 0; i < stairs.Length; i++)
        {
            if (stairs[i] != null)
            {
                m_Stairs.Add(stairs[i]);
            }
        }
    }
    public List<Stair> GetStairs()
    {
        return m_Stairs;
    }
    public Stair GetNextStair(Stair stair)
    {
        Stair result = null; 
        for (int i = 0; i < m_Stairs.Count - 1; i++)
        {
            if (stair.Equals(m_Stairs[i]))
            {
                result = m_Stairs[i + 1];
            }
        }
        return result;
    }
    public bool HasNextStair(Stair stair)
    {
        Stair result = null;
        for (int i = 0; i < m_Stairs.Count - 1; i++)
        {
            if (stair.Equals(m_Stairs[i]))
            {
                result = m_Stairs[i + 1];
            }
        }
        return result!=null;
    }
}
