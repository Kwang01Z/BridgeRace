using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSpawner : MonoBehaviour
{
    [SerializeField] int m_ColNum;
    [SerializeField] int m_RowNum;
    [SerializeField] float m_MinDistance;
    [SerializeField] List<BrickPooler> m_Bricks;
    [SerializeField] BrickPooler m_PlayerBrickPooler;
    [SerializeField] float m_TimeRespawn;
    List<BrickSpawnSlot> m_BrickSpawnSlots;
    private void Awake()
    {
        foreach (BrickPooler pooler in m_Bricks)
        {
            pooler.Init(transform);
        }
        m_PlayerBrickPooler.Init(transform);
        m_BrickSpawnSlots = new List<BrickSpawnSlot>();
    }
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        SpawnUpdate();
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
                newSlot.name = "Slot_" + (i + 1) + "_" + (j + 1);
                newSlot.AddComponent(typeof(BrickSpawnSlot));
                newSlot.transform.position = transform.position + new Vector3(halfMinDistance * j, 0, halfMinDistance * i);
                m_BrickSpawnSlots.Add(newSlot.GetComponent<BrickSpawnSlot>());        
            }
        }
        SpawnBrick();
    }

    void SpawnBrick()
    {
        List<BrickSpawnSlot> slotClone = new List<BrickSpawnSlot>();
        m_BrickSpawnSlots.ForEach((item) =>
        {
            slotClone.Add(item);
        });
        // Spawn Brick For Bots
        m_Bricks.ForEach((pooler) =>
        {
            int typeCount = m_ColNum * m_RowNum / 4;
            for (int i = typeCount; i > 0; i--)
            {
                int rand = Random.Range(0, slotClone.Count);
                BrickSpawnSlot slot = slotClone[rand];
                pooler.Spawn(slot.transform, slot.transform.position, slot.transform.rotation);
                slotClone.Remove(slot);
            }
        });
        // Spawn Brick for Player
        slotClone.ForEach((slot2) =>
        {
            m_PlayerBrickPooler.Spawn(slot2.transform, slot2.transform.position, slot2.transform.rotation);
        });
    }
    void SpawnUpdate()
    { 
        
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
