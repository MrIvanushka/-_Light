using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp4
{
    abstract class ActiveEffect
    {
        private int _duration;
        private int _currentTime;
        protected SoilderCombat _target;

        public bool Enabled { get; private set; }
        protected int Duration => _duration;

        public ActiveEffect(int duration, SoilderCombat target)
        {
            Enabled = true;
            _currentTime = 0;
            _duration = duration;
            _target = target;
        }

        public void Update()
        {
            _currentTime++;
            UseEffect();
            if (_currentTime == _duration)
            {
                Enabled = false;
                Disable();
            }
        }

        protected virtual void UseEffect()
        {

        }

        protected virtual void Disable()
        { }

        public void SetActive(bool flag)
        {
            Enabled = flag;
        }
    }

    class Burning : ActiveEffect
    {
        private float _damage;

        public Burning(SoilderCombat target, int duration = 3, float damage = 5) : base(duration, target)
        {
            _damage = damage;
        }

        protected override void UseEffect()
        {
            _target.TakeDamage(_damage);
        }
    }

    class Freezing : ActiveEffect
    {
        private float _damageModifier;

        public Freezing(SoilderCombat target, int duration = 3) : base(duration, target)
        {
            _damageModifier = target.Damage / 5;
        }

        protected override void UseEffect()
        {
            _target.ModifyDamage(-_damageModifier);
        }

        protected override void Disable()
        {
            _target.ModifyDamage(_damageModifier * Duration);
        }
    }

    class Demon : ActiveEffect
    {
        private float _damage;
        private Warlock _caster;

        public Demon(SoilderCombat target, Warlock caster, int duration = 5, float damage = 5) : base(duration, target)
        {
            _caster = caster;
            _damage = damage;
        }

        protected override void UseEffect()
        {
            _target.TakeDamage(_damage);
        }

        protected override void Disable()
        {
            _caster.RemoveDemon(this);
        }
    }}
