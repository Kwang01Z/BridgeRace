using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSpawnSlot : MonoBehaviour
{
    BrickSpawner m_BrickSpawner;
    bool m_JustSpawn;
    public void SetBrickSpawner(BrickSpawner gameObject)
    {
        m_BrickSpawner = gameObject;
    }
    private void Update()
    {
        if (m_BrickSpawner != null && m_BrickSpawner.IsSpawnComplete())
        {
            SpawnUpdateAuto();
        }
    }
    private void SpawnUpdateAuto()
    {
        if (transform.childCount > 0) return;
        if (!m_JustSpawn)
        {
            m_JustSpawn = true;
            StartCoroutine(Spawn());
        }    
    }
    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(m_BrickSpawner.GetTimeRespawn());
        m_BrickSpawner.SpawnUpdate(this);
        m_JustSpawn = false;
    }
}
