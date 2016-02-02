using UnityEngine;
using System.Collections;

public class ScreenView : MonoBehaviour {

	public void Toggle(bool on)
    {
        gameObject.SetActive(on);
    }
}
