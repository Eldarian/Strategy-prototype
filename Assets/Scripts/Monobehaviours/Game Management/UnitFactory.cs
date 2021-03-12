using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitFactory : MonoBehaviour
{
    private class Order
    {
        public readonly GameObject prefab;
        public readonly Vector3 startPosition;
        public readonly Transform defaultObjective;
        public readonly float delay;
        public int level;

        public Order(GameObject _prefab, Vector3 _startPosition, Transform _objective, int _level, float _delay)
        {
            prefab = _prefab;
            startPosition = _startPosition;
            defaultObjective = _objective;
            delay = _delay;
            level = _level;
        }

        public Order(GameObject _prefab, Vector3 _startPosition, Transform _objective) : this(_prefab, _startPosition, _objective, 0, 0) { }
    }

    #region Fields

    float timer = 0;
    float unitSpawnTime;
    Transform unitsParent;
    bool isTraining;
    Queue<Order> orders = new Queue<Order>();
    #endregion

    #region Init
    void Start()
    {
        unitsParent = GameObject.Find("Units").transform;
    }

    #endregion

    #region Getters and Setters
    public int GetStatus() //for statusBar
    {
        if (isTraining)
        {
            return (int)((timer / unitSpawnTime) * 100);
        }
        return 0;
    }
    #endregion

    #region Spawn methods

    void Update()
    {
        timer += Time.deltaTime;
        if (!isTraining && orders.Count > 0)
        {
            StartCoroutine(PrepareUnit(orders.Dequeue()));
        }
    }
    IEnumerator PrepareUnit(Order order)
    {
        isTraining = true;
        unitSpawnTime = order.delay;
        timer = 0;
        yield return new WaitForSeconds(order.delay);
        var unit = SpawnUnit(order);
        unit.GetStats().SetLevel(order.level);
        isTraining = false;
    }

    public void OrderUnit(GameObject prefab, Vector3 startPosition, Transform defaultObjective, float delay, int level)
    {
        orders.Enqueue(new Order(prefab, startPosition, defaultObjective, level, delay));
    }

    public Character SpawnUnitInstantly(GameObject prefab, Vector3 startPosition, Transform defaultObjective)
    {
        return SpawnUnit(new Order(prefab, startPosition, defaultObjective));
    }

    private Character SpawnUnit(Order order)
    {
        return Instantiate(order.prefab, order.startPosition, order.prefab.transform.rotation, unitsParent).GetComponent<Character>();
    }

    #endregion
}