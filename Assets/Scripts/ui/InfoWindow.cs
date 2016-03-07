using UnityEngine;
using UnityEngine.UI;

public class InfoWindow : BaseWindow
{
    private Button closeButton;

    protected override void Init()
    {
        base.Init();
        closeButton = Body.GetComponentInChildren<Button>();
        closeButton.onClick.AddListener(OnCloseBtnClick);
    }

    void OnCloseBtnClick()
    {
        Hide(); 
    }

}
