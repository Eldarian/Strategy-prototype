using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Character : SelectableObject
{
    #region Field Definitions

    protected Stats stats;
    protected NavMeshAgent agent;
    [SerializeField] Transform objective;
    [SerializeField] float stopDistance;

    private float timer;
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
        stats = GetComponent<Stats>();
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
        if (other.gameObject.GetComponent<IAttackable>() != null && isAttackPerforming)
        {
            other.gameObject.GetComponent<IAttackable>().OnAttack(gameObject, definition.CreateAttack(stats));

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

                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                    {
                        isAttackPerforming = false;
                    }

                    if (hit.distance <= attackRange && !isAttackPerforming && canAttack)
                    {
                        animator.SetTrigger("Attack");
                        StartCoroutine(PerformAttack());
                    }
                }
            }
        }
    }
    protected IEnumerator PerformAttack()
    {
        animator.SetTrigger("Attack");
        isAttackPerforming = true;
        canAttack = false;
        timer = 0;
        while (timer < cooldown)
        {
            yield return null;
            timer += Time.deltaTime;
        }
        canAttack = true;
    }

    #endregion
}
