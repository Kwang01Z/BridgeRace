using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotIdle : IState<AiBotController>
{
    public void OnEnter(AiBotController t)
    {
        t.GetCharacterAnim().GetAnimator().SetTrigger("Idle");
        t.GetAgent().velocity = Vector3.zero;
    }

    public void OnExecute(AiBotController t)
    {
        
    }

    public void OnExit(AiBotController t)
    {
        t.GetCharacterAnim().GetAnimator().ResetTrigger("Idle");
    }

}
