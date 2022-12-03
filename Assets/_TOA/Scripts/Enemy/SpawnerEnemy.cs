using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnerEnemy : MonoBehaviour
{
    public GameObject Target = null;
    public SO_DataEnemy sO_DataEnemy;
    public Transform AreaMove;
    public float AreaSize;
    public Vector3 OffsetAreaMove;
    [Header("Spawn random enemy")]
    [Tooltip("Check to spawn random enemy")]
    public bool RandomModel;
    [Tooltip("Check to make enemy move to random location")]
    public bool RandomMove;
    [Header("Spawn a specific enemy")]
    public int ID_Model = 0;
    //PointSpawner
    public PointSpawner[] pointsInstance;

    private string enemyName;
    #region UnityFunction
    private void Reset()
    {
        pointsInstance = GetComponentsInChildren<PointSpawner>();
        AreaMove = gameObject.transform.GetChild(0);
        sO_DataEnemy = Resources.Load<SO_DataEnemy>("SO_Data/SO_EnemyData/DataEnemy");
        AreaSize = 5;
        RandomModel = true;
        RandomMove = true;
    }
    private void Start()
    {
        if (RandomModel)
        {
            int index;
            for (int i = 0; i < pointsInstance.Length; i++)
            {
                index = Random.Range(0, sO_DataEnemy.enemyDatas.Length);
                CreateEnemy(i, index);              
            }
        }
        else
        {
            for (int i = 0; i < pointsInstance.Length; i++)
            {
                CreateEnemy(i, ID_Model);
            }
        }
        // logic Code :
        // => tạo 1 mảng point (ẩn point di) sau đó instance model tương ứng vs điểm point ( kiểm tra model mà ẩn thì hiện chúng lên và setparent là point)
        // có 2 loại tạo model .loại 1 => random id_model theo SO_data theo từng point;
        //                      loai 2 => chỉ định 1 loại id_model cho tất cả các point trong vùng;
        // sau đó set base info cho từng enemy tương ứng;
        // => mục đích : khi start sẽ khởi tạo và ẩn chúng đi , khi player Entertrigger vô vùng sẽ hiện chúng lên và khi exitTrigger sẽ ẩn chúng đi
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(Target == null)
            {
                Target = other.gameObject;
            }  
            for (int i = 0; i < pointsInstance.Length; i++)
            {
                pointsInstance[i].gameObject.SetActive(true);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Target = null;
            //for (int i = 0; i < pointsInstance.Length; i++)
            //{
            //    pointsInstance[i].gameObject.SetActive(false);
            //}
        }
    }
    #endregion


    #region PublicFunction
    public bool CheckInFront(Transform a, Transform b)
    {
        Vector3 dirToTarget = Vector3.Normalize(b.position - a.position);
        var dot = Vector3.Dot(a.forward, dirToTarget);
        return dot > 0.1f;
    }
    #endregion


    #region PrivateFunction
    private void SetDefaultComponent(GameObject go)
    {
        if (!go.activeSelf) go.SetActive(true);
        if (!go.GetComponent<NavMeshAgent>())
        {
            go.AddComponent<NavMeshAgent>();
            go.GetComponent<NavMeshAgent>().obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
        }

        if (!go.GetComponent<BaseInfoEnemy>()) go.AddComponent<BaseInfoEnemy>();
    }

    private void CreateEnemy(int i, int modelIndex)
    {
        pointsInstance[i].gameObject.SetActive(false);
        GameObject go = Instantiate(sO_DataEnemy.enemyDatas[modelIndex].Model,
            pointsInstance[i].transform.position, pointsInstance[i].transform.rotation, pointsInstance[i].transform);
        SetDefaultComponent(go);
        
        var dataInfo = sO_DataEnemy.enemyDatas[modelIndex];
        int playerLevel = PlayerStats.Instance.Level;
        go.GetComponent<BaseInfoEnemy>().SetBaseInfo(dataInfo.BaseHP + (0.1f * dataInfo.BaseHP * (playerLevel - 1)),
            dataInfo.MinAtckDamage + (10 * (playerLevel - 1)), dataInfo.MaxAtkDamage + (10 * (playerLevel - 1)),
            dataInfo.AtkRange, dataInfo.EvadeChance,
            dataInfo.ExpOnDeath);
        if (!go.GetComponent<EnemyHealth>()) go.AddComponent<EnemyHealth>();

        ConfigAI(go, modelIndex);
      
        //Add this enemy icon to compass
        CompassBar.Instance.AddEnemyMarker(go.GetComponent<EnemyMarker>());
    }
    private void ConfigAI(GameObject go, int modelIndex)
    {
        switch (modelIndex)
        {
            case 0:
                {
                    //Black Wolf - Tank
                    enemyName = "Black Werewolf";
                    //Add health bar
                    FloatingHPBar.Create(new Vector3(go.transform.position.x, go.transform.position.y + 2f, go.transform.position.z), go.transform, enemyName);
                    go.GetComponent<NavMeshAgent>().speed = 3f;
                    break;
                }
            case 1:
                {
                    //Grey Wolf - Speed
                    enemyName = "Grey Werewolf";
                    //Add health bar
                    FloatingHPBar.Create(new Vector3(go.transform.position.x, go.transform.position.y + 1.8f, go.transform.position.z), go.transform, enemyName);
                    go.GetComponent<NavMeshAgent>().speed = 5f;
                    break;
                }
            case 2:
                {
                    //Orange Wolf - Elite
                    enemyName = "(Elite) Werewolf";
                    //Add health bar
                    FloatingHPBar.Create(new Vector3(go.transform.position.x, go.transform.position.y + 2.6f, go.transform.position.z), go.transform, enemyName);
                    go.GetComponent<NavMeshAgent>().speed = 4f;
                    break;
                }
            case 3:
                {
                    //Ghost
                    enemyName = "(Melee) Ghost";
                    //Add health bar
                    FloatingHPBar.Create(new Vector3(go.transform.position.x, go.transform.position.y + 2f, go.transform.position.z), go.transform, enemyName);
                    go.GetComponent<NavMeshAgent>().speed = 10f;
                    break;
                }
            default:
                {
                    //Add health bar
                    FloatingHPBar.Create(new Vector3(go.transform.position.x, go.transform.position.y + 2f, go.transform.position.z), go.transform, enemyName);
                    break;
                }
        }
    }
    #region Gizmos
    private void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AreaMove.position + OffsetAreaMove, AreaSize);

        Gizmos.color = Color.green;
        for (int i = 0; i < pointsInstance.Length; i++)
        {
            Gizmos.DrawSphere(pointsInstance[i].transform.position, 0.5f);
        }
    }
    #endregion
    #endregion



}
