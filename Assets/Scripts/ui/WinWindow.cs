using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinWindow : BaseWindow
{
    private Button _closeButton;

    protected override void Init()
    {
        type = WindowsId.LoseWindow;
        base.Init();

        AnimationClip clip = (AnimationClip) Resources.Load("Animations/windowScale");
        ChangeClip(clip);

        _closeButton = Body.GetComponentInChildren<Button>();
        _closeButton.onClick.AddListener(OnCloseBtnClick);

        Text _buttonText = _closeButton.GetComponentInChildren<Text>();
        _buttonText.text = (string) LocaleModel.Instance.GetString("CLOSE");

        Text label = Body.GetComponentInChildren<Text>();
        label.text = (string) LocaleModel.Instance.GetString("WIN");
       
        Dictionary<string, int> data = new Dictionary<string, int>();
        data.Add("coin", 13);
        data.Add("cash", 4);
        data.Add("energy", 99);
        GameObject awardContainer = GameUtils.CreateAwardPanel(data);

        Transform t = awardContainer.transform;
        t.SetParent(Body.gameObject.transform, false);
        var rectTransform = awardContainer.GetComponent<RectTransform>();
        t.localPosition = new Vector3(-rectTransform.sizeDelta.x/2, 20.0f);
        t.localRotation = Quaternion.identity;
        t.localScale = Vector3.one;
    }

    void OnCloseBtnClick()
    {
        Close();
        var stage = (int)DataModel.GetValue(Names.STAGE);
        stage++;
        DataModel.SetValue(Names.STAGE, stage);
        PlayerPrefs.SetInt(Names.STAGE, stage);
        SceneManager.LoadScene(SceneId.MAIN);
    }
}
