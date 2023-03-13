using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entity.State
{
    public class AttackState : EnemyState
    {
        public AttackState(Enemy enemy) : base(enemy)
        {
        }

        public override void Enter()
        {
            // Set animator to attacking animation
        }

        public override void Update()
        {
            float distanceToPlayer = Vector3.Distance(enemy.transform.position, enemy.player.transform.position);
            if (distanceToPlayer <= enemy.attackRange)
            {
                enemy.MoveUnitsPerSecond = 0f;
                if (enemy.CoolDownAttack(Time.deltaTime))
                {
                    enemy.Attack(enemy.atk);
                }
            }
            else
            {
                enemy.MoveUnitsPerSecond = enemy.speed;

                // Transition to WalkState
                enemy.ChangeState(new WalkState(enemy));
            }
        }

        public override void Exit()
        {
            // Reset animator to default

        }
    }
}
