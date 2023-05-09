using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner
{
    protected Stack<GameObject> m_FreeInstances = new Stack<GameObject>();
    protected GameObject m_Original;

    public ObjectSpawner(Transform a_parent, GameObject a_original, int a_initialSize)
    {
        m_Original = a_original;
        m_FreeInstances = new Stack<GameObject>(a_initialSize);

        for (int i = 0; i < a_initialSize; ++i)
        {
            GameObject obj = GameObject.Instantiate(a_original);
            obj.name = obj.name + "_" + i;
            Despawn(a_parent, obj);
        }
    }
    public GameObject Spawn(Transform a_parent, Vector3 a_pos, Quaternion a_quat)
    {
        GameObject ret = m_FreeInstances.Count > 0 ? m_FreeInstances.Pop() : GameObject.Instantiate(m_Original);
        ret.transform.parent = a_parent;
        ret.transform.position = a_pos;
        ret.transform.rotation = a_quat;
        ret.SetActive(true);
        return ret;
    }
    public void Despawn(Transform a_root, GameObject a_obj)
    {
        a_obj.SetActive(false);
        a_obj.transform.parent = a_root;
        m_FreeInstances.Push(a_obj);
    }
}
