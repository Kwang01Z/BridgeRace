using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] PlayerController m_PlayerController;
    Vector3 distance;
    void Start()
    {
        distance = transform.position - m_PlayerController.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = m_PlayerController.transform.position + distance;
    }
}
