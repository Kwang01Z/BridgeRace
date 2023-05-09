using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiBotController : MonoBehaviour
{
    [SerializeField] UnityEngine.AI.NavMeshAgent m_NavMeshAgent;
    void Reset()
    {
        m_NavMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                m_NavMeshAgent.SetDestination(hit.point);
            }
        }
    }
}
