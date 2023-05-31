using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TriggerWin : MonoBehaviour
{
    [SerializeField] GameObject m_LayoutWin;
    [SerializeField] TextMeshProUGUI m_WinText;
    private void OnTriggerEnter(Collider other)
    {
        CharacterBase characterBase = other.transform.GetComponent<CharacterBase>();
        if (characterBase != null)
        {
            m_LayoutWin.SetActive(true);
            m_WinText.SetText("Player " + characterBase.GetColor() + " Win");
            Time.timeScale = 0;
        }
    }
}
