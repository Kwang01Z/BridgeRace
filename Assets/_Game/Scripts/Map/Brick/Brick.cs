using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] ColorData m_ColorData;
    ColorType m_ColorType;
    public bool m_IsStair;
    public void SetColor(ColorType color)
    {
        m_ColorType = color;
        GetComponent<MeshRenderer>().material = m_ColorData.GetMaterial(color);
    }
    public ColorType GetColor()
    {
        return m_ColorType;
    }
    private void OnTriggerEnter(Collider other)
    {
        CharacterBase character = other.GetComponent<CharacterBase>();
        if (character)
        {
            character.OnTriggerBrick(this);
        }
    }
    public void SetIsStair(bool a_IsStair)
    {
        m_IsStair = a_IsStair;
    }
    public bool IsStair()
    {
        return m_IsStair;
    }
}
