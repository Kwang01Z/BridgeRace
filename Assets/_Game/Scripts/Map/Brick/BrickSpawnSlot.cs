using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSpawnSlot : MonoBehaviour
{
    bool m_HasBrick;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetBrickCheck(gameObject.transform.childCount > 0);
    }
    public void SetBrickCheck(bool a_HasBrick)
    {
        m_HasBrick = a_HasBrick;
    }
    public bool HasBrick()
    {
        return m_HasBrick;
    }
}
