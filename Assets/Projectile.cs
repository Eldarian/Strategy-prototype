using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Attack attack;
    public float speed = 10;

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    public void SetAttack(Attack _attack)
    {
        attack = _attack;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.LogFormat("BOOOOOOOOOOM by {0}", other.gameObject);

        var attackedGameObject = other.gameObject;
        if (attackedGameObject.name == "Body")
        {
            attackedGameObject = attackedGameObject.transform.parent.gameObject;
        }
        if (attackedGameObject.GetComponent<IAttackable>() != null && attackedGameObject != attack.GetAttacker()) 
        {
            attackedGameObject.GetComponent<IAttackable>().OnAttack(gameObject, attack);
            Destroy(gameObject);
        }
    }
}
