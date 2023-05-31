using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSpawner : MonoBehaviour
{
    [SerializeField] int m_MaxColorCount;
    [SerializeField] int m_ColNum;
    [SerializeField] int m_RowNum;
    [SerializeField] float m_MinDistance;
    [SerializeField] float m_TimeRespawn;
    [SerializeField] List<ColorType> m_ColorTypes;
    BrickPooler m_MainPooler;
    List<BrickSpawnSlot> m_BrickSpawnSlots = new List<BrickSpawnSlot>();
    bool m_IsSpawnComplete;
    List<BrickSpawnSlot> slotClone = new List<BrickSpawnSlot>();
    void Awake()
    {
        Init();
    }
    void Init()
    {
        float halfMinDistance = m_MinDistance / 2f;
        for (int i = -1 * m_RowNum + 1; i < m_RowNum; i+=2)
        {
            for (int j = -1 * m_ColNum + 1; j < m_ColNum; j+=2)
            {
                GameObject newSlot = new GameObject();
                newSlot.gameObject.transform.parent = transform;
                newSlot.name = "Floor "+ transform.parent.name + ": Slot_" + (i + 1) + "_" + (j + 1);
                newSlot.AddComponent(typeof(BrickSpawnSlot));
                newSlot.transform.position = transform.position + new Vector3(halfMinDistance * j, 0, halfMinDistance * i);
                m_BrickSpawnSlots.Add(newSlot.GetComponent<BrickSpawnSlot>());        
            }
        }
        slotClone = new List<BrickSpawnSlot>();
        m_BrickSpawnSlots.ForEach((item) =>
        {
            slotClone.Add(item);
        });
        m_IsSpawnComplete = true;
    }
    public void AddColorType(ColorType a_color, BrickPooler a_brickPooler)
    {
        if (m_ColorTypes.Count >= m_MaxColorCount) return;
        for (int i = 0; i < m_ColorTypes.Count; i++)
        {
            if (m_ColorTypes[i].Equals(a_color)) return;
        }
        m_ColorTypes.Add(a_color);
        m_MainPooler = a_brickPooler;
        SpawnBrick(a_color);
    }
    public void SpawnBrick(ColorType a_colorType)
    {
        int typeCount = m_ColNum * m_RowNum / m_MaxColorCount;
        for (int i = typeCount; i > 0; i--)
        {
            int rand = Random.Range(0, slotClone.Count);
            BrickSpawnSlot slot = slotClone[rand];
            GameObject brick = m_MainPooler.Spawn(slot.transform, slot.transform.position, slot.transform.rotation, a_colorType);
            brick.GetComponent<Brick>().SetColor(a_colorType);
            slot.SetBrickSpawner(this);
            slotClone.Remove(slot);
        }

    }
    public void SpawnUpdate(BrickSpawnSlot a_BrickSpawnSlot)
    {
        int rand = Random.Range(0, m_ColorTypes.Count);
        GameObject brick = m_MainPooler.Spawn(a_BrickSpawnSlot.transform, a_BrickSpawnSlot.transform.position, a_BrickSpawnSlot.transform.rotation, m_ColorTypes[rand]);
        brick.GetComponent<Brick>().SetColor(m_ColorTypes[rand]);
    }
    public bool IsSpawnComplete()
    {
        return m_IsSpawnComplete;
    }
    public float GetTimeRespawn()
    {
        return m_TimeRespawn;
    }
    public List<Brick> GetListBrickByType(ColorType colorType)
    {
        List<Brick> bricks = new List<Brick>();
        m_BrickSpawnSlots.ForEach((slot)=>
        {
            if (slot.GetComponentInChildren<Brick>())
            {
                Brick brick = slot.GetComponentInChildren<Brick>();
                if (brick.GetColor().Equals(colorType))
                {
                    bricks.Add(brick);
                }    
            }
        });
        return bricks;
    }  
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        float halfMinDistance = m_MinDistance / 2f;
        // Draw column line
        for (int i = (m_ColNum * -1) + 1; i < m_ColNum; i += 2)
        {
            Vector3 begin = transform.position + new Vector3(halfMinDistance * i, 0, halfMinDistance * (m_RowNum - 1));
            Vector3 end = transform.position + new Vector3(halfMinDistance * i, 0, halfMinDistance * (m_RowNum * -1 + 1));
            Gizmos.DrawLine(begin, end);
        }
        // Draw row line
        for (int i = (m_RowNum * -1) + 1; i < m_RowNum; i += 2)
        {
            Vector3 begin = transform.position + new Vector3(halfMinDistance * (m_ColNum - 1), 0, halfMinDistance * i);
            Vector3 end = transform.position + new Vector3(halfMinDistance * (m_ColNum * -1 + 1), 0, halfMinDistance * i);
            Gizmos.DrawLine(begin, end);
        }
    }
}
