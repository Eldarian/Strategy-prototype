using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Enemy : Character
{

    public AttackDefinition_SO definition;
    Animator animator;
    private bool isAttackPerforming;
    private bool canAttack;
    [SerializeField] float cooldown = 1f;
    private float timer;

    public override void Start()
    {
        base.Start();
        var fortress = GameObject.FindGameObjectWithTag("Finish");
        stopDistance = 7.5f;
        animator = transform.GetChild(0).gameObject.GetComponent<Animator>();
        stats = GetComponent<Stats>();
        canAttack = true;
    }

    public override void Update()
    {
        base.Update();
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            isAttackPerforming = false;
        }

        if (Vector3.Distance(transform.position, GetObjective().position) <= 15 && !isAttackPerforming && canAttack)
        {
            animator.SetTrigger("Attack");
            StartCoroutine(PerformAttack());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<IAttackable>() != null && isAttackPerforming)
        {
            other.gameObject.GetComponent<IAttackable>().OnAttack(gameObject, definition.CreateAttack(stats));

        }
    }

    IEnumerator PerformAttack()
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
}
