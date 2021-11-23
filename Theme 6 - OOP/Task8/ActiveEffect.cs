using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp4
{
    abstract class ActiveEffect
    {
        private int _duration;
        private int _currentTime;
        protected SoldierCombat Target;

        public bool Enabled { get; private set; }
        protected int Duration => _duration;

        public ActiveEffect(int duration, SoldierCombat target)
        {
            Enabled = true;
            _currentTime = 0;
            _duration = duration;
            Target = target;
        }

        public void Update()
        {
            _currentTime++;
            UseEffect();

            if (_currentTime == _duration || Target.Health < 0)
            {
                Enabled = false;
                Disable();
            }
        }
        
        public void Deactivate()
        {
            Enabled = false;
        }

        protected virtual void UseEffect()
        {

        }

        protected virtual void Disable()
        { }

        
    }

    class Burning : ActiveEffect
    {
        private float _damage;

        public Burning(SoldierCombat target, int duration = 3, float damage = 15) : base(duration, target)
        {
            _damage = damage;
        }

        protected override void UseEffect()
        {
            Target.TakeDamage(_damage);
        }
    }

    class Freezing : ActiveEffect
    {
        private float _damageModifier;

        public Freezing(SoldierCombat target, int duration = 3) : base(duration, target)
        {
            _damageModifier = target.Damage / 5;
        }

        protected override void UseEffect()
        {
            Target.ModifyDamage(-_damageModifier);
        }

        protected override void Disable()
        {
            Target.ModifyDamage(_damageModifier * Duration);
        }
    }

    class Demon : ActiveEffect
    {
        private float _damage;
        private WarlockCombat _caster;

        public Demon(SoldierCombat target, WarlockCombat caster, int duration = 5, float damage = 5) : base(duration, target)
        {
            _caster = caster;
            _damage = damage;
        }

        protected override void UseEffect()
        {
            Target.TakeDamage(_damage);
        }

        protected override void Disable()
        {
            _caster.RemoveDemon(this);
        }
    }}
