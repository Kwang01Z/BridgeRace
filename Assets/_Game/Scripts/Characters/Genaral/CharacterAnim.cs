using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnim : MonoBehaviour
{
    [SerializeField] Animator m_Animator;
    private void Reset()
    {
        m_Animator = GetComponent<Animator>();
    }
    public Animator GetAnimator()
    {
        return m_Animator;
    }
}
