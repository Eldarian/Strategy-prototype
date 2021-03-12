using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foutain : MonoBehaviour
{
    [SerializeField] float healRange;
    [SerializeField] int healPower;
    SphereCollider healer;

    // Start is called before the first frame update
    void Start()
    {
        healer = gameObject.AddComponent<SphereCollider>();
        healer.isTrigger = true;
        healer.radius = healRange;
    }

    // Update is called once per frame
    private void OnTriggerStay(Collider other)
    {
        var healedGameObject = other.gameObject;
        if (healedGameObject.name == "Body")
        {
            healedGameObject = healedGameObject.transform.parent.gameObject;
        }
        if (healedGameObject.GetComponent<IHealable>() != null)
        {
            healedGameObject.GetComponent<IHealable>().OnHeal(healPower);
        }
    }
}
