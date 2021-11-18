using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp4
{
    abstract class SoilderCombat
    {
        protected Soilder ThisSoilder;
        private float _health;
        private float _maxHealth;
        private float _damage;
        private Random _random;
        private Army enemyArmy;

        public float Health => _health;
        public float Damage => _damage;



        public SoilderCombat(Soilder thisSoilder, float health, float damage)
        {
            _maxHealth = health;
            _health = health;
            _damage = damage;
            ThisSoilder = thisSoilder;
            _random = new Random();
        }

        public void ModifyDamage(float modifier)
        {
            _damage += modifier;

        }

        public virtual void TakeDamage(float damage)
        {
            if (damage > 0)
                _health -= damage;

        }

        public void Heal(float healValue)
        {
            _health += healValue;
            if (_health > _maxHealth)
                _health = _maxHealth;
        }

        public virtual AttackData LocateTarget(CellStatus[,] map)
        {
            Coord2 position = ThisSoilder.Graphics.Position;
            List<Coord2> enemyPositions = new List<Coord2>();

            for (int x = position.X - 1; x <= position.X + 1; x++)
            {
                for (int y = position.Y - 1; y <= position.Y + 1; y++)
                {
                    if (map[x, y] == ThisSoilder.Graphics.TargetStatus)
                        enemyPositions.Add(new Coord2(x, y));
                }
            }

            if (enemyPositions.Count > 0)
                return new AttackData(this, enemyPositions[_random.Next(enemyPositions.Count)]);

            return null;

        }

        public virtual void Attack(SoilderCombat target, List<ActiveEffect> effects)
        {
            target.TakeDamage(_damage);
        }
    }

    class Wizard : SoilderCombat
    {
        public Wizard(Soilder thisSoilder, float health, float damage) : base(thisSoilder,health, damage)
        { }

        public override void Attack(SoilderCombat target, List<ActiveEffect> effects)
        {
            base.Attack(target, effects);
            effects.Add(new Burning(target));
        }
    }

    class Warrior : SoilderCombat
    {
        private float _damageModifier = 10;

        public Warrior(Soilder thisSoilder, float health, float damage) : base(thisSoilder, health, damage)
        { }

        public override void Attack(SoilderCombat target, List<ActiveEffect> effects)
        {
            base.Attack(target, effects);
            ModifyDamage(_damageModifier);
        }
    }

    class Warlock : SoilderCombat
    {
        private List<Demon> _demons;
        private Random _random;

        public Warlock(Soilder thisSoilder, float health, float damage) : base(thisSoilder, health, damage)
        {
            _random = new Random();
            _demons = new List<Demon>();
        }

        public override void TakeDamage(float damage)
        {
            int target = _random.Next(0, _demons.Count + 1);

            if (target == 0)
            {
                base.TakeDamage(damage);
            }
            else
            {
                _demons[0].SetActive(false);
                _demons.Remove(_demons[0]);
            }
        }

        public override void Attack(SoilderCombat target, List<ActiveEffect> effects)
        {
            Demon newDemon = new Demon(target, this);
            effects.Add(newDemon);
            _demons.Add(newDemon);
        }

        public void RemoveDemon(Demon demon)
        {
            _demons.Remove(demon);
        }
    }

    class Prist : SoilderCombat
    {
        private float _charmPower;
        private int _chargeTime;
        private int _currentTime;
        private float _healingValue;

        public Prist(Soilder thisSoilder, float health, float healingValue) : base(thisSoilder, health, 0)
        {
            _charmPower = 5;
            _chargeTime = 20;
            _currentTime = 0;
            _healingValue = healingValue;
        }

        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage / _charmPower);
        }

        public override AttackData LocateTarget(CellStatus[,] map)
        {

            List<Soilder> allies = ThisSoilder.GetNeighbourAllies(map);

            if (allies.Count > 0)
            {
                allies.ForEach(ally => { if (ally.Combat.Health < 20f) ally.Combat.Heal(_healingValue); });
                return null;
            }
            else
            {
                return base.LocateTarget(map);
            }
        }

        public override void Attack(SoilderCombat target, List<ActiveEffect> effects)
        {
            if (_currentTime < _chargeTime)
            {
                _currentTime++;
            }
            else
            {
                target.TakeDamage(1000);
            }
        }
    }

    class FrostMage : SoilderCombat
    {

        public FrostMage(Soilder thisSoilder, float health, float damage) : base(thisSoilder, health, damage)
        { }

        public override void Attack(SoilderCombat target, List<ActiveEffect> effects)
        {
            base.Attack(target, effects);
            effects.Add(new Freezing(target, 2));
        }
    }
}
