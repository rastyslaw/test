using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class UpgradesFill : MonoBehaviour
{
    [SerializeField]
    private GameObject content;

    [SerializeField]
    private GameObject prefab;

    private ToggleGroup toggleGroup;
    private List<UpgradeItem> items; 

    void Start()
    {
        items = new List<UpgradeItem>(); 
        toggleGroup = GetComponentInChildren<ToggleGroup>();
        FillContent();
    }

    public void OnTabChanged() 
    {
        FilterContent(toggleGroup.ActiveToggles().ElementAt(0).name); 
    }

    void FillContent()
    {
        List<UpgradeVO> upgrades = DataModel.GetValue(Names.UPGRADES) as List<UpgradeVO>;
        foreach (var upgradeData in upgrades)
        {
            GameObject item = WindowsFactory.InstantiatePrefab(prefab);
            UpgradeItem itemData = item.GetComponent<UpgradeItem>();
            itemData.Data = upgradeData; 
            item.transform.SetParent(content.transform);
            item.transform.localScale = Vector3.one;
            items.Add(itemData); 
        }
        OnTabChanged();
    }

    void FilterContent(string tabName)
    {
        foreach (var item in items)
        {
            item.gameObject.SetActive(item.Data.TabName == tabName); 
        }
    }
}
