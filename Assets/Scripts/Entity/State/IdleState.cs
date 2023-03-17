using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entity.State
{
    public class IdleState : EnemyState
    {

        public IdleState(Enemy enemy) : base(enemy) { }

        public override void Enter()
        {
            // Set animator to idle animation
            enemy.GetComponent<Animator>().SetBool("IsIdle", true);
        }

        public override void Update()
        {
            // Move towards player

            float step = enemy.MoveUnitsPerSecond * Time.deltaTime;
            //Find position of player and approach him
            Vector3 point = new Vector3(enemy.player.transform.position.x, enemy.player.transform.position.y, -Camera.main.transform.position.z);
            enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, point, step);

            float distanceToPlayer = Vector3.Distance(enemy.transform.position, enemy.transform.position);
            if (distanceToPlayer <= enemy.attackRange)
            {
                enemy.ChangeState(new AttackState(enemy));
            }
            else
            {
                enemy.MoveUnitsPerSecond = enemy.speed;
                enemy.ChangeState(new WalkState(enemy));
            }
        }

        public override void Exit()
        {
            // Reset animator to default
            enemy.GetComponent<Animator>().SetBool("IsIdle", false);

        }
    }
}
