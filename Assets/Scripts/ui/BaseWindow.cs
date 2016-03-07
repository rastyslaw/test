using UnityEngine;
using System.Collections;

public abstract class BaseWindow : IWindow
{
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