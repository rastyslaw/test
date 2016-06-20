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
        
        Text[] labels = Body.GetComponentsInChildren<Text>();
        foreach (var label in labels)
        {
           label.text = (string)LocaleModel.Instance.GetString(label.name);
        }

        Text _buttonText = _closeButton.GetComponentInChildren<Text>();
        _buttonText.text = (string)LocaleModel.Instance.GetString("CLOSE");

        Dictionary<string, int> data = new Dictionary<string, int>();
        int money = GameUtils.GetStageMoney();
        data.Add("coin", money);
        int totalMoney = int.Parse(DataModel.GetValue(Names.MONEY).ToString());
        totalMoney += money;
        DataModel.SetValue(Names.MONEY, totalMoney);
        GameObject awardContainer = GameUtils.CreateAwardPanel(data);

        Transform t = awardContainer.transform;
        t.SetParent(Body.gameObject.transform, false);
        var rectTransform = awardContainer.GetComponent<RectTransform>();
        t.localPosition = new Vector3(-rectTransform.sizeDelta.x/2, 0);
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
