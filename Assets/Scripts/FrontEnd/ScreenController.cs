using UnityEngine;
using System.Collections.Generic;

public class ScreenController : MonoBehaviour {

    public List<ScreenController> Transitions;

    public virtual void Toggle(bool on)
    {
        gameObject.SetActive(on);
    }

    public void TransitionTo(int transition)
    {
        FrontEndManager.Instance.TransisitonTo(Transitions[transition]);
    }
}
