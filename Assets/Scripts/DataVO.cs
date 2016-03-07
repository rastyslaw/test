public class DataVO
{
    private string _key;
    private object _value;

	public DataVO(string key, object value)
    {
        _key = key;
        _value = value; 
    }

    public string Key
    {
        get { return _key; }
    }

    public object Value
    {
        get { return _value; }
    }
}
