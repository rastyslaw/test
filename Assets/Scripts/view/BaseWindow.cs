using UnityEngine;
using UnityEngine.Experimental.Director;

public abstract class BaseWindow : IWindow
{
    protected WindowsId type; 
    protected GameObject Body;
    protected string Folder = "Prefabs/GUI/";
    protected string Name;

    protected virtual void Init()
    {
        var body = Resources.Load(Folder + (Name ?? ToString()));
        Body = WindowsFactory.Build(body as GameObject);
        Body.name = ToString();  
    }
    
    public virtual void Show()
    {
        if (Body == null)
        {
            Init();
        }
        Body.SetActive(true);
    }

    public virtual void Hide()
    {
        Body.SetActive(false);
    }

    protected virtual void ChangeClip(AnimationClip clip)
    {
        var clipPlayable = new AnimationClipPlayable(clip);
        Body.GetComponent<Animator>().Play(clipPlayable);
    }

    protected virtual void Close()
    {
        Messenger.Broadcast<WindowsId>(EventTypes.HIDE_WINDOW, type);  
    }

    public virtual void Prepare()
    {
        Init();
        Body.SetActive(false);
    }

    public virtual void Destroy()
    {
        Object.Destroy(Body);
    }
}