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

    private List<AbstractEnemy> _enemies;
    private EnemiesFactory _enemiesFactory;
    private int totalStagePower;
    private int currentStagePower = 0;
    private int _wavePower;

    private bool finished;

    private const int WAVE_COUNT = 6;
    private const float ENEMY_DELAY = 1.3f;
    private const int WAVE_DELAY = 5;

    void Awake()
    {
        _enemies = new List<AbstractEnemy>();
        _enemiesFactory = GetComponent<EnemiesFactory>(); 
    }

    void Start ()
    {
        Invoke("ShowInfoWindow", 0.9f);
        GetStagePower();
        Messenger.AddListener<AbstractEnemy>(EventTypes.DEAD, OnEnemyDead);
        if (!finished)
        {
            Invoke("EnemyTypeSelection", WAVE_DELAY);
        }
        Messenger.AddListener<bool>(EventTypes.STAGE_COMPLETED, OnStageCompleted);
    }

    void OnStageCompleted(bool isWin)
    {
        finished = true;
        CancelInvoke("EnemyTypeSelection");
        foreach (var enemy in _enemies)
        {
            enemy.Stop();
        }
    }

    void ShowInfoWindow()
    {
        Messenger.Broadcast<WindowsId>(EventTypes.SHOW_WINDOW, WindowsId.InfoWindow);
        StartCoroutine(Timer.Start(3.0f, false, () =>
        {
            Messenger.Broadcast<WindowsId>(EventTypes.HIDE_WINDOW, WindowsId.InfoWindow);
        }));
    }
    
    void GetStagePower()
    {
        var stage = (int)DataModel.GetValue(Names.STAGE);
        var startPower = int.Parse(DataModel.GetValue(Names.START_POWER).ToString());
        var bonusPower = int.Parse(DataModel.GetValue(Names.BONUS_POWER).ToString()); 
        totalStagePower = startPower + (stage - 1)*bonusPower;
        DataModel.SetValue(Names.STAGE_POWER, totalStagePower);
        _wavePower = totalStagePower/WAVE_COUNT;
    }

    void EnemyTypeSelection()
    {
        var enemies = DataModel.GetValue(Names.ENEMIES) as List<EnemyVO>;
        var stage = (int)DataModel.GetValue(Names.STAGE);

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
        EnemyVO enemyData = (EnemyVO)filteringEnemies.ElementAt(randomType).Clone();
        ApplyStageBonuses(enemyData);

        if (checkEnemyTypeInEnum(enemyData.type))
        {
            WaveSelection(enemyData); 
        }
        else
        {
            EnemyTypeSelection();
        }
    }

    void ApplyStageBonuses(EnemyVO enemyData)
    {
        float startPower = float.Parse(DataModel.GetValue(Names.START_POWER).ToString());
        float power = totalStagePower/startPower;
        enemyData.hp *= power;
        enemyData.power = (int)(enemyData.power * power);
    }

    void WaveSelection(EnemyVO enemyData)
    {
        if ((currentStagePower + _wavePower) >= totalStagePower)
        {
            _wavePower = totalStagePower - currentStagePower;
            finished = true; 
            Debug.Log("волны закончились");
        }
        currentStagePower += _wavePower; 
        double count = Math.Ceiling((double)_wavePower / enemyData.power);

        StartCoroutine(CreateWaves(count, enemyData));
        Invoke("EnemyTypeSelection", WAVE_DELAY);
    }
     
    IEnumerator CreateWaves(double count, EnemyVO enemyData)
    {
        while (count > 0)
        {
            yield return new WaitForSeconds(Random.Range(0, ENEMY_DELAY)); 
            CreateEnemy((EnemyTypes)Enum.Parse(typeof(EnemyTypes), enemyData.type), enemyData);
            count--;
        }
    }
     
    bool checkEnemyTypeInEnum(string enemyType)
    {
        return Enum.IsDefined(typeof (EnemyTypes), enemyType);
    }

    void CreateEnemy(EnemyTypes enemiesType, EnemyVO enemyData)
	{
        //EnemiesFactory.enemiesType enemiesType = Utils.RandomEnumValue<EnemyTypes>();  
        GameObject enemy = _enemiesFactory.Build(enemiesType);
        Type type = Type.GetType(enemiesType.ToString());
        AbstractEnemy enemyComponent = enemy.AddComponent(type) as AbstractEnemy;
        _enemies.Add(enemyComponent);
        enemyComponent.Data = enemyData;
        enemy.transform.position = new Vector3(Random.Range(-2.5f, 2.5f), 6.5f, 0);
        Messenger.Broadcast<int>(EventTypes.LAUNCH, enemyData.power);
    }

    void OnEnemyDead(AbstractEnemy deadEnemy)
    {
        foreach (var enemy in _enemies)
        {
            if (enemy == deadEnemy)
            {
                _enemies.Remove(enemy); 
                break;
            }
        }
        Debug.Log("_enemies.Count = " + _enemies.Count + ", finished = " + finished);
        if (finished && _enemies.Count == 0)
        {
            Messenger.RemoveListener<AbstractEnemy>(EventTypes.DEAD, OnEnemyDead);
            Messenger.Broadcast<bool>(EventTypes.STAGE_COMPLETED, true); 
            Messenger.Broadcast<WindowsId>(EventTypes.SHOW_WINDOW, WindowsId.WinWindow); 
        }
    }

    void OnDestroy()
    {
        DataModel.SetValue(Names.STAGE_POWER, 0);
        Messenger.RemoveListener<bool>(EventTypes.STAGE_COMPLETED, OnStageCompleted);
    }
}
