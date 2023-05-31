using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ColorData", menuName = "ScriptableObject/ColorData", order = 1)]
public class ColorData : ScriptableObject
{
    [SerializeField] List<ColorMat> m_ColorMats;
    public Material GetMaterial(ColorType color)
    {
        Material result = null;
        m_ColorMats.ForEach((colorMat) =>
        {
            if (colorMat.ColorType.Equals(color))
            {
                result = colorMat.Material;
            }
        });
        return result;
    }
}
[System.Serializable]
public class ColorMat
{
    public ColorType ColorType;
    public Material Material;
}
public enum ColorType
{ 
    NONE = 0,
    RED = 1,
    GREEN = 2,
    PURPLE = 3,
    YELLOW = 4
}
