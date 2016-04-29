using UnityEngine;
using System.Collections;
using DG.Tweening;

public class StageProgress : MonoBehaviour {

    private int _totalProgress;
    private int _curProgress;

    private GameObject _bg;

    void Awake()
    {
        _bg = transform.Find("fill").gameObject;
    }

    void Start ()
    {
        Messenger.AddListener<DataVO>(EventTypes.DATA_UPDATE, OnDataUpdate);
    }
	
	void OnDataUpdate(DataVO data)
    {
	    if (data.Key == Names.STAGE_POWER)
	    {
            Messenger.RemoveListener<DataVO>(EventTypes.DATA_UPDATE, OnDataUpdate);
            _curProgress = _totalProgress = (int)data.Value;
            Messenger.AddListener<int>(EventTypes.LAUNCH, OnLaunch);
        }
	}

    void OnLaunch(int power)
    {
        _curProgress -= power;
	    if (_curProgress < 0)
	    {
            _curProgress = 0;
            Messenger.RemoveListener<int>(EventTypes.LAUNCH, OnLaunch); 
        }
        UpdateScale();
    }

    void UpdateScale() 
    {
        //_bg.transform.localScale = new Vector3((float)_curProgress / _totalProgress, _bg.transform.localScale.y, _bg.transform.localScale.z);
        const float scaleCoff = 18.0f;
        float newScaleX = (float)_curProgress/_totalProgress;
        float time = (_bg.transform.localScale.x - newScaleX) * scaleCoff;
        _bg.transform.DOScaleX((float)_curProgress / _totalProgress, time);
    }

    void Destroy()
    {
        Messenger.RemoveListener<DataVO>(EventTypes.DATA_UPDATE, OnDataUpdate); 
        Messenger.RemoveListener<int>(EventTypes.LAUNCH, OnLaunch);
    }
}
