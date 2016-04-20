using UnityEngine;
using System.Collections;

public class EnemiesController : MonoBehaviour
{
    [SerializeField]
    private float duration = 4f;
    
    private EnemiesFactory _enemiesFactory;

    void Awake()
    {
        _enemiesFactory = GetComponent<EnemiesFactory>(); 
    }

    void Start ()
    {
        InvokeRepeating("CreateEnemy", duration, duration);
        Messenger.Broadcast<WindowsId>(EventTypes.SHOW_WINDOW, WindowsId.InfoWindow);
        StartCoroutine(Timer.Start(3.0f, true, () =>
        {
            Messenger.Broadcast<WindowsId>(EventTypes.HIDEW_WINDOW, WindowsId.InfoWindow); 
        }));
    }
    
    void CreateEnemy()
	{
        EnemiesFactory.enemiesType enemiesType = Utils.RandomEnumValue<EnemiesFactory.enemiesType>();  
        GameObject enemy = _enemiesFactory.Build(enemiesType);
        enemy.transform.position = new Vector3(Random.Range(-2.5f, 2.5f), 6.5f, 0);
	}
}
