using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PlayerController : CharacterBase
{
    [SerializeField] float m_Speed;
    [SerializeField] VariableJoystick m_VariableJoystick;
    [SerializeField] Rigidbody m_Rigidbody;
    bool m_CanMove;
    Vector3 m_JoyStickDirection;
    Vector3 m_MoveVelocity;
    protected override void Start()
    {
        base.Start();
        Physics.IgnoreLayerCollision(3,8);
        m_CanMove = true;
    }
    protected override void UpdateMore()
    {
        base.UpdateMore();
        m_JoyStickDirection = new Vector3(m_VariableJoystick.Horizontal, m_VariableJoystick.Vertical);
        if (m_JoyStickDirection.magnitude > 0.5f)
        {
            m_CanMove = true;
            m_CharacterAnim.GetAnimator().SetTrigger("Run");
            m_MoveVelocity = new Vector3(m_VariableJoystick.Horizontal, 0, m_VariableJoystick.Vertical) * m_Speed; ;
            RotateObject(m_MoveVelocity);       
        }
        else
        {
            m_Rigidbody.velocity = Vector3.zero;
            m_CanMove = false;
            m_CharacterAnim.GetAnimator().SetTrigger("Idle");
        }    
    }
    private void FixedUpdate()
    {
        Move();
    }
    void Move()
    {
        if (!m_CanMove) return;
        m_Rigidbody.velocity = m_MoveVelocity;
    }
    public override void OnTriggerStair(Stair a_stair)
    {
        base.OnTriggerStair(a_stair);
        Brick brick = a_stair.GetBrick();
        transform.position = new Vector3(transform.position.x, brick.transform.position.y + 0.31f, transform.position.z);
        TriggerNextStair();
    }
    
    private void RotateObject(Vector3 rot)
    {
        if (rot.magnitude == 0) return;
        Quaternion rotation = Quaternion.LookRotation(rot * -1, Vector3.up);
        transform.rotation = rotation;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position+transform.forward * -0.5f, transform.position + transform.forward * -0.5f + transform.up*-1f);
    }
}
