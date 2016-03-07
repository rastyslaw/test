using System;
using UnityEngine;
using Random = System.Random;

public class Utils : MonoBehaviour
{
    public static T RandomEnumValue<T>()
    {
        Array values = Enum.GetValues(typeof (T)); 
        Random random = new Random();
        T randomAttackType = (T) values.GetValue(random.Next(values.Length));
        return randomAttackType;  
    }
}
