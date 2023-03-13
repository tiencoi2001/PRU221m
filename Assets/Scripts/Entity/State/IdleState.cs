using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entity.State
{
    public class IdleState : EnemyState
    {

        public IdleState(Enemy enemy)
        {
            this.enemy = enemy;
        }

        public override void Enter()
        {
            // Set animator to idle animation
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
