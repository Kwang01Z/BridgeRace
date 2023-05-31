using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair : MonoBehaviour
{
    [SerializeField] Brick m_Brick;
    public bool m_IsChecked;
    private void Start()
    {
        m_Brick.SetIsStair(true);
    }
    public void ChangeColor(ColorType colorType)
    {
        m_Brick.SetColor(colorType);
    }
    public Brick GetBrick()
    {
        return m_Brick;
    }
    public void SetCheck(bool isCheck)
    {
        m_IsChecked = isCheck;
    }
    public bool IsCheck()
    {
        return m_IsChecked;
    }
}
