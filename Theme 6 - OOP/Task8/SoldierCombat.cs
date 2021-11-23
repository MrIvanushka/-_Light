using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp4
{
    abstract class SoldierCombat
    {
        private float _health;
        private float _maxHealth;
        private float _damage;
        private Random _random;

        public float Health => _health;
        public float Damage => _damage;
        protected Soldier ThisSoldier { get; private set; }


        public SoldierCombat(Soldier thisSoilder, float health, float damage)
        {
            _maxHealth = health;
            _health = health;
            _damage = damage;
            ThisSoldier = thisSoilder;
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

        public virtual AttackData LocateTarget(in CellStatus[,] map)
        {
            Vector2 position = ThisSoldier.Graphics.Position;
            List<Vector2> enemyPositions = new List<Vector2>();

            for (int x = position.X - 1; x <= position.X + 1; x++)
            {
                for (int y = position.Y - 1; y <= position.Y + 1; y++)
                {
                    if (map[x, y] == ThisSoldier.TargetStatus)
                        enemyPositions.Add(new Vector2(x, y));
                }
            }

            if (enemyPositions.Count > 0)
                return new AttackData(this, enemyPositions[_random.Next(enemyPositions.Count)]);

            return null;

        }

        public virtual void Attack(SoldierCombat target, List<ActiveEffect> effects)
        {
            target.TakeDamage(_damage);
        }
    }

    class WizardCombat : SoldierCombat
    {
        public WizardCombat(Soldier thisSoilder, float health, float damage) : base(thisSoilder,health, damage)
        { }

        public override void Attack(SoldierCombat target, List<ActiveEffect> effects)
        {
            base.Attack(target, effects);
            effects.Add(new Burning(target));
        }
    }

    class WarriorCombat : SoldierCombat
    {
        private float _damageModifier = 10;

        public WarriorCombat(Soldier thisSoilder, float health, float damage) : base(thisSoilder, health, damage)
        { }

        public override void Attack(SoldierCombat target, List<ActiveEffect> effects)
        {
            base.Attack(target, effects);
            ModifyDamage(_damageModifier);
        }
    }

    class WarlockCombat : SoldierCombat
    {
        private List<Demon> _demons;
        private Random _random;

        public WarlockCombat(Soldier thisSoilder, float health, float damage) : base(thisSoilder, health, damage)
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
                _demons[0].Deactivate();
                _demons.Remove(_demons[0]);
            }
        }

        public override void Attack(SoldierCombat target, List<ActiveEffect> effects)
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

    class PristCombat : SoldierCombat
    {
        private float _charmPower;
        private int _chargeTime;
        private int _currentTime;
        private float _healingValue;

        public PristCombat(Soldier thisSoilder, float health, float healingValue) : base(thisSoilder, health, 0)
        {
            _charmPower = 5;
            _chargeTime = 10;
            _currentTime = 0;
            _healingValue = healingValue;
        }

        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage / _charmPower);
        }

        public override AttackData LocateTarget(in CellStatus[,] map)
        {

            List<Soldier> allies = ThisSoldier.GetNeighbourAllies(map);

            if (allies.Count > 0)
                allies.ForEach(ally => { if (ally.Combat.Health < 20f) ally.Combat.Heal(_healingValue); });
            
            return base.LocateTarget(map);
        }

        public override void Attack(SoldierCombat target, List<ActiveEffect> effects)
        {
            if (_currentTime < _chargeTime)
            {
                _currentTime++;
            }
            else
            {
                _currentTime = 0;
                target.TakeDamage(1000);
            }
        }
    }

    class FrostMageCombat : SoldierCombat
    {

        public FrostMageCombat(Soldier thisSoilder, float health, float damage) : base(thisSoilder, health, damage)
        { }

        public override void Attack(SoldierCombat target, List<ActiveEffect> effects)
        {
            base.Attack(target, effects);
            effects.Add(new Freezing(target, 2));
        }
    }
}
