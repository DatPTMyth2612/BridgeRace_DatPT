using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Win : UICanvas
{
    public void MainMenuButton()
    {
        UIManager.Ins.OpenUI<MainMenu>();
        Close(0);
    }
}
