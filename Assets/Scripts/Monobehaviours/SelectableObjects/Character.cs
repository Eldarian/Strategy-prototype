using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Character : SelectableObject
{
    #region Field Definitions

    protected NavMeshAgent agent;
    [SerializeField] Transform objective;
    [SerializeField] float stopDistance;

    [SerializeField] float cooldown = 1f;
    [SerializeField] AttackDefinition_SO definition;
    protected Animator animator;
    protected bool isAttackPerforming;
    protected bool canAttack;
    [SerializeField] float attackRange;

    #endregion

    #region Initializations
    public override void Start()
    {
        base.Start();
        agent = GetComponent<NavMeshAgent>();
        animator = transform.GetChild(0).gameObject.GetComponent<Animator>();
        canAttack = true;
    }

    #endregion


    
    public override void Update()
    {
        base.Update();
        PerformMovement();
    }

    

    public virtual void Heal()
    {

    }

    #region Navigation

    private void PerformMovement()
    {
        if (objective != null)
        {
            MoveToObjective();
        }
    }
    public void MoveToPoint(Vector3 point, float _stopDistance)
    {
        agent.SetDestination(point);
        if (Vector3.Distance(transform.position, point) < _stopDistance)
        {
            agent.velocity = Vector3.zero;
        }
    }

    public void MoveToObjective()
    {
        MoveToPoint(objective.position, stopDistance);  
    }

    public void SetObjective(Transform _objective)
    {
        objective = _objective;
    }

    protected Transform GetObjective()
    {
        return objective;
    }

    #endregion

    #region Selection
    public override void Select()
    {
        base.Select();
    }

    public override void Deselect()
    {
        base.Deselect();
    }

    #endregion


    #region Attack
    private void OnTriggerEnter(Collider other)
    {
        var attackedGameObject = other.gameObject;
        if(attackedGameObject.name == "Body")
        {
            attackedGameObject = attackedGameObject.transform.parent.gameObject;
        }
        if (attackedGameObject.GetComponent<IAttackable>() != null && isAttackPerforming)
        {
            attackedGameObject.GetComponent<IAttackable>().OnAttack(gameObject, definition.CreateAttack(stats));
            isAttackPerforming = false;
        }
    }

    protected void RangedAttack(Projectile prefab, float speed)
    {
        if (objective != null && objective.GetComponent<IAttackable>() != null)
        {
            stopDistance = attackRange - 1;
            if (Vector3.Distance(transform.position, objective.position) < attackRange)
            {
                transform.LookAt(GetObjective());
            }
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                if (hit.transform.gameObject.GetComponent<IAttackable>() == objective.GetComponent<IAttackable>())
                {
                    if (hit.distance <= attackRange && canAttack)
                    {
                        var projectile = Instantiate(prefab.gameObject, transform.position + transform.forward * 3, transform.rotation).GetComponent<Projectile>();
                        projectile.speed = speed;
                        projectile.SetAttack(definition.CreateAttack(stats));

                        canAttack = false;
                        StartCoroutine(WaitCooldown());
                    }
                }
            }
        }
    }

    protected void MeleeAttack()
    {
        if(objective != null && objective.GetComponent<IAttackable>() != null)
        {
            if(Vector3.Distance(transform.position, objective.position) < attackRange)
            {
                transform.LookAt(GetObjective());
            }
            RaycastHit hit;
            if(Physics.Raycast(transform.position, transform.forward, out hit))
            {
                if (hit.transform.gameObject.GetComponent<IAttackable>() == objective.GetComponent<IAttackable>())
                {
                    Debug.LogFormat("{0} looks at {1}", name, hit.transform.gameObject);

                    if (hit.distance <= attackRange && !isAttackPerforming && canAttack)
                    {
                        animator.SetTrigger("Attack");
                        isAttackPerforming = true;
                        canAttack = false;
                        StartCoroutine(WaitCooldown());
                    }
                }
            }
        }
    }
    protected IEnumerator WaitCooldown()
    {
        yield return new WaitForSeconds(cooldown);
        canAttack = true;
        isAttackPerforming = false;
    }

    #endregion
}
