using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    [SerializeField] List<BrickSpawner> m_BrickSpawners = new List<BrickSpawner>();
    [SerializeField] List<BridgeManager> m_BridgeManagers = new List<BridgeManager>();
    [SerializeField] BrickPooler m_Pooler;
    GameObject m_MainSpawner;
    private void Reset()
    {
        LoadBrickSpawners();
    }
    private void Awake()
    {
        m_MainSpawner = new GameObject();
        m_MainSpawner.name = "MainSpawner";
        m_MainSpawner.transform.parent = transform;
    }
    void LoadBrickSpawners()
    {
        m_BrickSpawners = new List<BrickSpawner>();
        BrickSpawner[] brickSpawners = GetComponentsInChildren<BrickSpawner>();
        if (brickSpawners.Length > 0)
        {
            for (int i = 0; i < brickSpawners.Length; i++)
            {
                m_BrickSpawners.Add(brickSpawners[i]);
            }
        }
    }
    public void InstantiateBrick(ColorType colorType)
    {
        m_BrickSpawners.ForEach((spawner) =>
        {
            spawner.AddColorType(colorType, m_Pooler);
        });
    }
    public List<Brick> GetListBrickByType(ColorType colorType)
    {
        List<Brick> result = new List<Brick>();
        m_BrickSpawners.ForEach((brickSpawner)=>
        {
            result.AddRange(brickSpawner.GetListBrickByType(colorType));
        });
        return result;
    }
    public bool IsSpawnComplete()
    {
        bool result = true;
        m_BrickSpawners.ForEach((brickSpawner)=>
        {
            if (!brickSpawner.IsSpawnComplete()) result = false;
        });
        return result;
    }
    public void DeSpawn(GameObject a_gameObject)
    {
        m_Pooler.Despawn(a_gameObject);
    }
    public BrickPooler GetPooler()
    {
        return m_Pooler;
    }
    public List<BrickSpawner> GetBrickSpawners()
    {
        return m_BrickSpawners;
    }
    public List<BridgeManager> GetBridgeManagers()
    {
        return m_BridgeManagers;
    }
}
