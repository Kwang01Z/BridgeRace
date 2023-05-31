using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickPooler : MonoBehaviour
{
    [SerializeField] Brick m_MainBrick;
    ObjectSpawner m_ObjectSpawner;
    private void Awake()
    {
        Init();
    }
    public void Init()
    {
        m_ObjectSpawner = new ObjectSpawner(transform, m_MainBrick.gameObject, 5);
    }
    public GameObject Spawn(Transform a_parent, Vector3 a_pos, Quaternion a_quat, ColorType color)
    {
        GameObject obj = m_ObjectSpawner.Spawn(a_parent, a_pos, a_quat);
        obj.GetComponent<Brick>().SetColor(color);
        return obj;
    }
    public void Despawn(GameObject a_obj)
    {
        m_ObjectSpawner.Despawn(transform, a_obj);
    }
}
