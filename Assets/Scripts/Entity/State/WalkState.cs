using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace Assets.Scripts.Entity.State
{
    public class WalkState : EnemyState
    {

        public WalkState(Enemy enemy)
        {
            this.enemy = enemy;
        }

        public override void Enter()
        {
            // Set animator to walking animation
        }

        public override void Update()
        {
            // Move towards player
        }

        public override void Exit()
        {
            // Reset animator to default
        }
    }
}
