using System.Collections.Generic;

public class DataModel
{
    private static Dictionary<string, object> data = new Dictionary<string, object>();
   
    public static object GetValue(string key)
    {
        if (data.ContainsKey(key))
        {
            return data[key];
        }
        return null; 
    }

    public static void SetValue(string key, object value)
    {
        if (data.ContainsKey(key))
        {
            data[key] = value; 
        }
        else
        {
            data.Add(key, value);
        }
        Messenger.Broadcast<DataVO>(EventTypes.DATA_UPDATE, new DataVO(key, value)); 
    }
}
