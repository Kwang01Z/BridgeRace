using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotRun : IState<AiBotController>
{
    public void OnEnter(AiBotController t)
    {
        t.GetCharacterAnim().GetAnimator().SetTrigger("Run");
    }

    public void OnExecute(AiBotController t)
    {
        t.Move();
    }

    public void OnExit(AiBotController t)
    {
        t.GetCharacterAnim().GetAnimator().ResetTrigger("Run");
    }
}
