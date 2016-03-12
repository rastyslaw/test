using System.Reflection.Emit;
using System.Timers;
using UnityEngine;
using UnityEngine.Experimental.Director;
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
        label.text = "Уровень 1";

        timer = new System.Timers.Timer(DELAY); 
        timer.Elapsed += new ElapsedEventHandler(OnTimerComplete);
        //timer.Start();
    }

    void ChangeClip(AnimationClip clip)
    {
        var clipPlayable = new AnimationClipPlayable(clip);
        Body.GetComponent<Animator>().Play(clipPlayable);  
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
