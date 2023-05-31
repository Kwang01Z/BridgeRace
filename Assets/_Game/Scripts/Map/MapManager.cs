using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] List<Floor> m_Floors;
    public List<Floor> GetFloors()
    {
        return m_Floors;
    }
    public Floor GetNextFloor(Floor a_floor)
    {
        Floor result = null;
        for (int i = 0; i < m_Floors.Count - 1; i++)
        {
            if (a_floor.Equals(m_Floors[i]))
            {
                result = m_Floors[i + 1];
            }
        }
        return result;
    }
}
