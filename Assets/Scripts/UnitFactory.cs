using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitFactory : MonoBehaviour
{
    float timer = 0;
    [SerializeField] float unitSpawnTime = 5;
    bool isTraining;

    Queue<Order> orders = new Queue<Order>();
    public void OrderUnit(GameObject prefab, Vector3 startPosition, Transform defaultObjective, float delay)
    {
        orders.Enqueue(new Order(prefab, startPosition, defaultObjective, delay));
    }

    public void OrderUnitInstantly(GameObject prefab, Vector3 startPosition, Transform defaultObjective)
    {
        SpawnUnit(new Order(prefab, startPosition, defaultObjective));
    }

    private void SpawnUnit(Order order)
    {
        Instantiate(order.prefab, order.startPosition, order.prefab.transform.rotation).GetComponent<Character>();
    }
    
    public float GetStatus()
    {
        if(isTraining)
        {
            return timer / unitSpawnTime;
        }
        return 0;
    }

    public void SetUnitSpawnTime(float time)
    {
        unitSpawnTime = time;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(!isTraining && orders.Count > 0)
        {
            StartCoroutine(PrepareUnit(orders.Dequeue()));
        }
    }
    IEnumerator PrepareUnit(Order order)
    {
        isTraining = true;
        timer = 0;
        yield return new WaitForSeconds(order.delay);
        SpawnUnit(order);
        isTraining = false;
    }
    private class Order
    {
        public readonly GameObject prefab;
        public readonly Vector3 startPosition;
        public readonly Transform defaultObjective;
        public readonly float delay;

        public Order(GameObject _prefab, Vector3 _startPosition, Transform _objective, float _delay)
        {
            prefab = _prefab;
            startPosition = _startPosition;
            defaultObjective = _objective;
            delay = _delay;
        }

        public Order(GameObject _prefab, Vector3 _startPosition, Transform _objective) : this(_prefab, _startPosition, _objective, 0)
        {

        }
    }
}