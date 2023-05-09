using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Brick", menuName = "ScriptableObject/BrickPooler", order = 1)]
public class BrickPooler : ScriptableObject
{
    [SerializeField] GameObject m_MainBrick;
    ObjectSpawner m_ObjectSpawner;
    public void Init(Transform a_parent)
    {
        m_ObjectSpawner = new ObjectSpawner(a_parent, m_MainBrick, 5);
    }
    public void Spawn(Transform a_parent, Vector3 a_pos, Quaternion a_quat)
    {
        m_ObjectSpawner.Spawn(a_parent, a_pos, a_quat);
    }
    public void Despawn(Transform a_root, GameObject a_obj)
    {
        m_ObjectSpawner.Despawn(a_root, a_obj);
    }
}
