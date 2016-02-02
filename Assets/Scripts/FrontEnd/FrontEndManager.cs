using UnityEngine;
using System.Collections.Generic;

public class FrontEndManager : Singleton<FrontEndManager> {

    public List<ScreenController> ScreenList;
    public ScreenController BootScreen;

    Stack<ScreenController> m_ScreenStack = new Stack<ScreenController>();
    ScreenController m_CurrentScreen;    

    void Awake()
    {
        m_CurrentScreen = BootScreen;
        m_CurrentScreen.Toggle(true);
    }

    public void TransisitonTo(ScreenController nextScreen)
    {
        m_ScreenStack.Push(m_CurrentScreen);
        m_CurrentScreen.Toggle(false);
        m_CurrentScreen = nextScreen;
        m_CurrentScreen.Toggle(true);
    }

    public void Back()
    {
        m_CurrentScreen.Toggle(false);
        m_CurrentScreen = m_ScreenStack.Pop();
        m_CurrentScreen.Toggle(true);
    }
}
