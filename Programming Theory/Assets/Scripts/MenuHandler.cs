using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : GameManager
{
    protected override void Awake()
    {
        base.Awake();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void StartGame()
    {
        LoadMain();
    }
    public void ExitGame()
    {
        Exitgame();
    }
}
