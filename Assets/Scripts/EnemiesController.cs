using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Random = UnityEngine.Random;

public class EnemiesController : MonoBehaviour
{
    private int prevEnemyId = -1;

    private EnemiesFactory _enemiesFactory;
    private int totalStagePower;
    private int currentStagePower = 0;
    private int wavePower;

    private bool finished;

    private const int WAVE_COUNT = 5;
    private const float ENEMY_DELAY = 0.9f;
    private const int WAVE_DELAY = 5;

    void Awake()
    {
        _enemiesFactory = GetComponent<EnemiesFactory>(); 
    }

    void Start ()
    {
        Messenger.Broadcast<WindowsId>(EventTypes.SHOW_WINDOW, WindowsId.InfoWindow);
        StartCoroutine(Timer.Start(3.0f, false, () =>
        {
            Messenger.Broadcast<WindowsId>(EventTypes.HIDEW_WINDOW, WindowsId.InfoWindow); 
        }));
        GetStagePower();
        if (!finished)
        {
            Invoke("EnemyTypeSelection", WAVE_DELAY);
        }
    }

    void GetStagePower()
    {
        int stage = (int)DataModel.GetValue(Names.STAGE);
        int startPower = int.Parse(DataModel.GetValue(Names.START_POWER).ToString()); 
        int bonusPower = int.Parse(DataModel.GetValue(Names.BONUS_POWER).ToString()); 
        totalStagePower = startPower + (stage - 1)*bonusPower;
        wavePower = totalStagePower/WAVE_COUNT;
    }

    void EnemyTypeSelection()
    {
        List<EnemyVO> enemies = DataModel.GetValue(Names.ENEMIES) as List<EnemyVO>;
        int stage = (int)DataModel.GetValue(Names.STAGE);

        //IEnumerable<EnemyVO>  filteringEnemies = enemies.Where(item => stage >= item.stageId);

        IEnumerable<EnemyVO> filteringEnemies = 
            from enemy in enemies
            where stage >= enemy.stageId 
            select enemy; 

        int randomType = Random.Range(0, filteringEnemies.Count());
        while (prevEnemyId == randomType)
        {
            randomType = Random.Range(0, filteringEnemies.Count());
        } 
        prevEnemyId = randomType;
        EnemyVO enemyData = filteringEnemies.ElementAt(randomType);

        if (checkEnemyTypeInEnum(enemyData.type))
        {
            WaveSelection(enemyData); 
        }
        else
        {
            EnemyTypeSelection();
        }
    }

    void WaveSelection(EnemyVO enemyData)
    {
        if ((currentStagePower + wavePower) > totalStagePower)
        {
            wavePower = totalStagePower - currentStagePower;
            finished = true; 
            Debug.Log("волны закончились");
        }
        currentStagePower += wavePower; 
        double count = Math.Ceiling((double)wavePower / enemyData.power);

        StartCoroutine(CreateWaves(count, enemyData.type));
        Invoke("EnemyTypeSelection", WAVE_DELAY);
    }
     
    IEnumerator CreateWaves(double count, string enemyType)
    {
        while (count > 0)
        {
            yield return new WaitForSeconds(Random.Range(0, ENEMY_DELAY)); 
            CreateEnemy((EnemyTypes)Enum.Parse(typeof(EnemyTypes), enemyType));
            count--;
        }
    }
     
    bool checkEnemyTypeInEnum(string enemyType)
    {
        return Enum.IsDefined(typeof (EnemyTypes), enemyType);
    }

    void CreateEnemy(EnemyTypes enemiesType)
	{
        //EnemiesFactory.enemiesType enemiesType = Utils.RandomEnumValue<EnemyTypes>();  
        GameObject enemy = _enemiesFactory.Build(enemiesType);
        enemy.transform.position = new Vector3(Random.Range(-2.5f, 2.5f), 6.5f, 0);
	}
}
