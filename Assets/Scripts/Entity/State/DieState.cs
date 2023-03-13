using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entity.State
{
    public class DieState : EnemyState
    {

        public DieState(Enemy enemy) : base(enemy) { }

        public override void Enter()
        {
            // Set animator to die animation
            enemy.GetComponent<Collider2D>().enabled = false;
            enemy.IsAlive(false);
        }

        public override void Update()
        {
            // Do nothing

        }

        public override void Exit()
        {
            // Reset animator to default

        }
    }
}
