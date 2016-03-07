using UnityEngine;

public class HealthPanel : MonoBehaviour
{
    private float _maxHealth;
    private float _curHealth;

    private GameObject _healtBar;

    void Start ()
    {
        _curHealth = _maxHealth = (float) DataModel.GetValue(Names.HP);
        _healtBar = transform.Find("fill").gameObject;
        Messenger.AddListener<float>(EventTypes.DAMAGE, OnGetDamage);

        updateScale();
    }
	
	void OnGetDamage(float damage)
    {
	    Debug.Log("получил " + damage + " урона!");
	    _curHealth -= damage;
	    if (_curHealth < 0)
	    {
	        _curHealth = 0;
            Messenger.RemoveListener<float>(EventTypes.DAMAGE, OnGetDamage);
        }
        updateScale();
    }

    void updateScale()
    {
        _healtBar.transform.localScale = new Vector3(_curHealth / _maxHealth, _healtBar.transform.localScale.y, _healtBar.transform.localScale.z);
    }
}
