using UnityEngine;

public class HealthPanel : MonoBehaviour
{
    private float _maxHealth;
    private float _curHealth;

    private GameObject _healtBar;

    void Start ()
    {
        _curHealth = _maxHealth = float.Parse(DataModel.GetValue(Names.HP).ToString()); 
        _healtBar = transform.Find("fill").gameObject;
        Messenger.AddListener<DataVO>(EventTypes.DATA_UPDATE, OnDataUpdate);
        UpdateScale();
    }

    void OnDataUpdate(DataVO data)
    {
        if (data.Key == Names.CURRENT_SHIELD_VALUE)
        {
            if (int.Parse(data.Value.ToString()) == 0) 
            {
                Messenger.AddListener<float>(EventTypes.DAMAGE, OnGetDamage);
                Messenger.RemoveListener<DataVO>(EventTypes.DATA_UPDATE, OnDataUpdate); 
            }    
        }
    }

	void OnGetDamage(float damage)
    {
	    Debug.Log("получил " + damage + " урона!");
	    _curHealth -= damage;
        if (_curHealth < 0)
	    {
	        _curHealth = 0;
            Messenger.RemoveListener<float>(EventTypes.DAMAGE, OnGetDamage);
            Messenger.Broadcast<bool>(EventTypes.STAGE_COMPLETED, false);
            Messenger.Broadcast<WindowsId>(EventTypes.SHOW_WINDOW, WindowsId.LoseWindow);
        }
        UpdateScale();
    }

    void UpdateScale()
    {
        _healtBar.transform.localScale = new Vector3(_curHealth / _maxHealth, _healtBar.transform.localScale.y, _healtBar.transform.localScale.z);
    }
}
