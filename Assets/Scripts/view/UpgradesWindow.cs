using System.Collections.Generic;
using System.Linq;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesWindow : BaseWindow
{
    private Button closeButton;

    protected override void Init()
    {
        type = WindowsId.UpgradesWindow;
        base.Init();

        closeButton = Body.GetComponentInChildren<Button>();
        closeButton.onClick.AddListener(OnCloseBtnClick);
    }
    
    void OnCloseBtnClick()
    {
        Close();
    }
}
