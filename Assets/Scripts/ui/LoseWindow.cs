using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoseWindow : BaseWindow
{
    private Button _closeButton;

    protected override void Init()
    {
        type = WindowsId.LoseWindow;  
        base.Init();

        AnimationClip clip = (AnimationClip)Resources.Load("Animations/windowScale");
        ChangeClip(clip);

        _closeButton = Body.GetComponentInChildren<Button>();
        _closeButton.onClick.AddListener(OnCloseBtnClick);

        Text _buttonText = _closeButton.GetComponentInChildren<Text>();
        _buttonText.text = (string)LocaleModel.Instance.GetString("CLOSE");

        Text label = Body.GetComponentInChildren<Text>();
        label.text = (string)LocaleModel.Instance.GetString("LOSE");
    }
   
    void OnCloseBtnClick()
    {
        Close();
        SceneManager.LoadScene(SceneId.MAIN);
    }
}
