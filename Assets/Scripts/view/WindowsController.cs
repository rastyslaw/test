using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class WindowsController : MonoBehaviour {
   
    private Dictionary<WindowsId, IWindow> windows;

    public IWindow CurrentWindow { get; private set; }

    private bool _created;

    void Awake()
    {
        if (!_created)
        {
            DontDestroyOnLoad(this.gameObject);
            _created = true;

            windows = new Dictionary<WindowsId, IWindow>();
        }
        else
        {
            Destroy(this.gameObject);
        }
        Messenger.AddListener<WindowsId>(EventTypes.SHOW_WINDOW, OpenWindow);
        Messenger.AddListener<WindowsId>(EventTypes.HIDE_WINDOW, HideWindow);
    }
    
    public void OnLevelWasLoaded(int unused)
    {
        CurrentWindow = null;
    }

    /*
    private void OpenWindow<T>() where T : BaseWindow
    {
        Open(typeof(T));
    }

    public void OpenWindow(string className) 
    {
        Open(Type.GetType(className.ToString()));
    } 
    */

    private void OpenWindow(WindowsId key)
    {
        if (CurrentWindow != null)
        {
            CurrentWindow.Hide();
        }

        if (!windows.ContainsKey(key))
        {
            windows.Add(key, Activator.CreateInstance(Type.GetType(key.ToString())) as BaseWindow);
        }
         
        CurrentWindow = windows[key];    
        CurrentWindow.Show();
    }
   
    //public void HideWindow<T>() where T : IWindow
    //{
     //   HideWindow(typeof(T));
    //}

    public void HideWindow(WindowsId key)
    {
        if (!windows.ContainsKey(key))
        {
            return;
        }
        windows[key].Hide(); 
    } 

    void OnDestroy() 
    {
        Messenger.RemoveListener<WindowsId>(EventTypes.SHOW_WINDOW, OpenWindow);
        Messenger.RemoveListener<WindowsId>(EventTypes.HIDE_WINDOW, HideWindow);
    }
}
