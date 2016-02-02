using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuController : ScreenController {

    void Awake()
    {

    }

	public void QuickPlay()
    {        
        SceneManager.LoadScene("Hockey", LoadSceneMode.Single);
    }

    public void LoadLeague()
    {

    }

    public void NewLeague()
    {

    }
}
