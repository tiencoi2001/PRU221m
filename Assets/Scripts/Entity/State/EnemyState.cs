using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entity.State
{
    public abstract class EnemyState
    {
        protected Enemy enemy;

        public virtual void Enter() { }

        public virtual void Update() { }

        public virtual void Exit() { }
    }
}
