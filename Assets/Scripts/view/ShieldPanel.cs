using UnityEngine;
using System.Collections;

public class ShieldPanel : MonoBehaviour
{
    private float _maxValue;
    private float _curValue;

    private GameObject _shieldBar;

    void Start()
    {
        object totalShielValue = DataModel.GetValue(Names.TOTAL_SHIELD_VALUE);
        _curValue = _maxValue = totalShielValue == null ? 40 : float.Parse(totalShielValue.ToString());
        _shieldBar = transform.Find("fill").gameObject;
        Messenger.AddListener<float>(EventTypes.DAMAGE, OnGetDamage);

        UpdateScale();
    }

    void OnGetDamage(float damage)
    {
        Debug.Log("щит поглотил " + damage + " урона!");
        _curValue -= damage;
        if (_curValue < 0)
        {
            _curValue = 0;
            Messenger.RemoveListener<float>(EventTypes.DAMAGE, OnGetDamage);
        }
        DataModel.SetValue(Names.CURRENT_SHIELD_VALUE, _curValue); 
        UpdateScale();
    }

    void UpdateScale()
    {
        _shieldBar.transform.localScale = new Vector3(_curValue / _maxValue, _shieldBar.transform.localScale.y, _shieldBar.transform.localScale.z);
    }
}
