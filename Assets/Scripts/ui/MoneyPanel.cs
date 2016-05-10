using UnityEngine;
using UnityEngine.UI;

public class MoneyPanel : MonoBehaviour
{
	void Start ()
	{
	    Text Label = gameObject.GetComponentInChildren<Text>();
	    Label.text = DataModel.GetValue(Names.MONEY).ToString();
    }
}
