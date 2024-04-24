using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveTarget : MonoBehaviour
{
    private bool isAlive = true;
    public void ReactToHit()
    {
        if (!isAlive)
            return;

        WanderingAI enemyAI = GetComponent<WanderingAI>();
        Animator enemyAnimator = GetComponent<Animator>();
        if(enemyAnimator != null)
        {
            enemyAnimator.SetTrigger("Die");
        }
        if (enemyAI != null)
        {
            enemyAI.ChangeState(EnemyStates.dead);
        }
        Messenger.Broadcast(GameEvent.ROBO_ENEMY_DEAD);
        isAlive = false;
        //StartCoroutine(Die());
    }
    private IEnumerator Die()
    {
        // Enemy falls over and disappears after two seconds

        //iTween.RotateAdd(this.gameObject, new Vector3(-75, 0, 0), 1);
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }

    private void DeadEvent()
    {
        Destroy(this.gameObject);
    }
}
