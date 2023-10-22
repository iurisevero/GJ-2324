using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    const string HideKey = "Hide";
    const string ShowKey = "Show";
    public Panel panel;
    // Start is called before the first frame update

    private void TogglePos(string pos)
    {
        Tweener t = panel.SetPosition(pos, true);
        t.duration = 0.5f;
        t.equation = EasingEquations.EaseOutQuad;
        t.timeType = EasingControl.TimeType.Real;
    }

    public void Show()
    {
        TogglePos(ShowKey);
        Player.Pause();
    }

    public void Hide()
    {
        TogglePos(HideKey);
        Player.UnPause();
    }
}
