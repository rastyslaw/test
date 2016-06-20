using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameUtils : MonoBehaviour
{
    public static string ReplaceTail(string count, string text)
    {
        int number1 = Int32.Parse(count.Substring(count.Length - 1, 1));
        int number2 = count.Length > 1 ? Int32.Parse(count.Substring(0)) : 0;
        string pattern = @"{.+}";
        Regex regex = new Regex(pattern);
        var match = regex.Match(text);
        string totalString = text.Substring(0, match.Index);
        var result = match.Value.Substring(1, match.Length - 2);
        string[] data = result.Split('|');

        if (number2 == 11 || number2 == 12 || number2 == 13 || number2 == 14)
        {
            totalString += data[2];
        }
        else if (number1 == 1)
        {
            totalString += data[0];
        }
        else if (number1 == 2 || number1 == 3 || number1 == 4)
        {
            totalString += data[1];
        }
        else
        {
            totalString += data[2];
        }
        return count + " " + totalString;
    }

    public static GameObject CreateAwardPanel(Dictionary<string, int> data)
    {
        GameObject awardContainer = new GameObject();
        awardContainer.AddComponent<RectTransform>();
        var prefab = Resources.Load("Prefabs/GUI/Award");
      
        float cumulativeX = 0f;
        const float GAP = 26.0f;

        foreach (var awardData in data)
        {
            float awardCumulativeX = 0f;

            GameObject award = WindowsFactory.InstantiatePrefab(prefab as GameObject);
            Texture2D texture = Resources.Load("gui/" + awardData.Key) as Texture2D;
            Sprite newSprite = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height),
                new Vector2(0.5f, 0.5f), 128f);
            Image sprRenderer = award.GetComponentInChildren<Image>();
            sprRenderer.sprite = newSprite;
            awardCumulativeX += sprRenderer.preferredWidth;

            Text label = award.GetComponentInChildren<Text>();
            label.text = awardData.Value.ToString();
            awardCumulativeX += label.preferredWidth;

            Transform transform = award.transform;
            transform.localPosition = new Vector3(cumulativeX, 0);
            transform.SetParent(awardContainer.gameObject.transform, false);
            RectTransform rectTransform = award.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(awardCumulativeX, rectTransform.sizeDelta.y);

            cumulativeX += awardCumulativeX;
            cumulativeX += GAP;
        }
        RectTransform awardContainerTransform = awardContainer.GetComponent<RectTransform>();
        awardContainerTransform.sizeDelta = new Vector2(cumulativeX, awardContainerTransform.sizeDelta.y);
        return awardContainer;
    }

    public static int GetStageMoney()
    {
        var stage = (int)DataModel.GetValue(Names.STAGE);
        var startStageMoney = int.Parse(DataModel.GetValue(Names.START_STAGE_MONEY).ToString());
        var bonusStageMoney = int.Parse(DataModel.GetValue(Names.BONUS_STAGE_MONEY).ToString());
        float randomtageMoney = float.Parse(DataModel.GetValue(Names.RANDOM_STAGE_MONEY).ToString());
        int money = startStageMoney + bonusStageMoney*stage;
        int diff = (int)(money * randomtageMoney);
        return Random.Range(money - diff, money + diff);
    }
}