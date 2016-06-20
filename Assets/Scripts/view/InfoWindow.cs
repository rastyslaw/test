using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class InfoWindow : BaseWindow
{
    private Button closeButton;
    private System.Timers.Timer timer;
    private const int DELAY = 2000;

    protected override void Init()
    {
        type = WindowsId.InfoWindow;  
        base.Init();
        //closeButton = Body.GetComponentInChildren<Button>();
        //closeButton.onClick.AddListener(OnCloseBtnClick);
        
        AnimationClip clip = (AnimationClip) Resources.Load("Animations/windowAlpha");
        ChangeClip(clip); 

        Text label = Body.GetComponentInChildren<Text>();
        string stage = DataModel.GetValue(Names.STAGE).ToString();
        label.text = LocaleModel.Instance.GetParamsString("STAGE", new [] {stage, "982"});

        timer = new System.Timers.Timer(DELAY); 
        timer.Elapsed += new ElapsedEventHandler(OnTimerComplete);
        //timer.Start();
        
        string test = "12rhdrh";
        string.Intern(test);
    }
    
    private void OnTimerComplete(object source, ElapsedEventArgs e)
    {
        OnCloseBtnClick();  
    }
    
    void OnCloseBtnClick()
    {
        timer.Dispose();
        Close();  
    }

}
