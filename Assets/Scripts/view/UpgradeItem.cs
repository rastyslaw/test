using UnityEngine;
using UnityEngine.UI;

public class UpgradeItem : MonoBehaviour
{
    [SerializeField]
    private Text title;

    private UpgradeVO data;

    public UpgradeVO Data
    {
        get { return data; }
        set
        {
            data = value;
            title.text = data.Type;
        }
    }
}
