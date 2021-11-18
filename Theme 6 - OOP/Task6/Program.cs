using System;
using System.Collections.Generic;
using System.Threading;

namespace ConsoleApp3
{
    class Program
    {

        static void Main(string[] args)
        {
            ConsoleArena thisApp = new ConsoleArena("Arena");

            do
            {
                thisApp.Update();
            }
            while (thisApp.Enabled == true);
        }
    }

    class ConsoleArena
    {
        
        private string _title;
        private Fighter[] _fighters;
        private string _exitCommand;
        
        public bool Enabled { get; private set; }

        public ConsoleArena() : this("Untitled")
        { }

        public ConsoleArena(string title)
        {
            Enabled = true;
            _title = title;
            _fighters = new Fighter[5];
            _exitCommand = "Exit";
        }

        public void InitFighters()
        {
            _fighters[0] = new Warrior("Громмаш", 100, 20);
            _fighters[1] = new Wizard("Кадгар", 50, 40);
            _fighters[2] = new Warlock("Гул'дан", 80);
            _fighters[3] = new Prist("Андуин", 100);
            _fighters[4] = new FrostMage("Джайна", 70, 30);
        }

        public void Update()
        {
            InitTitle();

            Fighter[] currentRoundFighters = new Fighter[2];
            
            for(int i = 0; i < currentRoundFighters.Length; i++)
            {
                currentRoundFighters[i] = GetFighter();
                
                if (currentRoundFighters[i] == null)
                    return;
            }

            if (currentRoundFighters[0].Name == currentRoundFighters[1].Name)
            {
                PrintErrorMessage("Вы выбрали одного и того же героя.");
                return;
            }

            ShowFight(currentRoundFighters);
 
            Console.WriteLine("Нажмите любую клавишу...");
            Console.ReadKey();
        }

        private void InitTitle()
        {
            Console.Clear();
            InitFighters();

            foreach (var fighter in _fighters)
                Console.WriteLine(fighter);

            Console.WriteLine($"Для выхода напишите {_exitCommand}.");
        }

        private void ShowFight(Fighter[] currentRoundFighters)
        {
            List<ActiveEffect> effects = new List<ActiveEffect>();
            Fighter fighterA = currentRoundFighters[0];
            Fighter fighterB = currentRoundFighters[1];

            do
            {
                Console.Clear();
                Console.WriteLine($"[{fighterA.Name}: {fighterA.Health} хп, {fighterB.Name}: {fighterB.Health} хп]");

                if (effects.Count > 0)
                {
                    effects.ForEach(effect =>
                    {
                        if (effect.Enabled)
                            effect.Update();
                    });
                }

                fighterA.Attack(fighterB, effects);
                fighterB.Attack(fighterA, effects);

                int delayInMilliseconds = 2000;
                Thread.Sleep(delayInMilliseconds);
            }
            while (fighterA.Health > 0 && fighterB.Health > 0);

            if (fighterA.Health <= 0 && fighterB.Health <= 0)
                Console.WriteLine("Ничья");
            else if (fighterA.Health <= 0)
                Console.WriteLine(fighterB.Name + "  победил");
            else
                Console.WriteLine(fighterA.Name + "  победил");
        }

        private Fighter GetFighter()
        {
            string name = GetVariable("Введите имя гладиатора: ");
            
            foreach(var fighter in _fighters)
            {
                if (fighter.Name == name)
                    return fighter;
            }

            if (name != _exitCommand)
                PrintErrorMessage($"Гладиатор с именем {name} не найден.");
            else
                Enabled = false;

            return null;
        }

        private string GetVariable(string message)
        {
            Console.Write(message);
            string exportLetter = Console.ReadLine();
            
            if (exportLetter == "Exit")
                Enabled = false;

            return exportLetter;
        }

        private void PrintErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Ошибка: " + message);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Нажмите любую клавишу...");
            Console.ReadKey();
        }
    }

    abstract class Fighter
    {  
        public readonly string Name;

        private float _health;
        private float _damage;

        public float Health => _health;
        public float Damage => _damage;

        

        public Fighter(string name, float health, float damage)
        {
            Name = name;
            _health = health;
            _damage = damage;
        }

        public void ModifyDamage(float modifier)
        {
            if (modifier + _damage < 0)
                throw new Exception("неправильная модификация урона");

            _damage += modifier;

        }

        public virtual void TakeDamage(float damage)
        {
            _health -= damage;
        }

        public virtual void Attack(Fighter target, List<ActiveEffect> effects)
        {
            target.TakeDamage(_damage);
        }

        public override string ToString()
        {
            return Name + " здоровье: " + _health.ToString() + " урон: " + _damage.ToString();
        }
    }

    class Wizard : Fighter
    {
        public Wizard(string name, float health, float damage) : base(name, health, damage)
        { }

        public override void Attack(Fighter target, List<ActiveEffect> effects)
        {
            Console.WriteLine($"{Name} выпускает огненный шар в {target.Name} и поджигает его");
            base.Attack(target, effects);
            effects.Add(new Burning(target));
        }

        public override string ToString()
        {
            return  "Маг " + base.ToString();
        }
    }

    class Warrior : Fighter
    {
        private float _damageModifier = 10;

        public Warrior(string name, float health, float damage) : base(name, health, damage)
        { }

        public override void Attack(Fighter target, List<ActiveEffect> effects)
        {
            Console.WriteLine($"{Name} бьёт {target.Name} и накапливает ярость");
            base.Attack(target, effects);
            ModifyDamage(_damageModifier);
        }

        public override string ToString()
        {
            return "Воин " + base.ToString();
        }
    }

    class Warlock : Fighter
    {
        private List<Demon> _demons;
        private Random _random;

        public Warlock(string name, float health) : base(name, health, 100)
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
                Console.WriteLine("Демон берёт удар на себя");
            }
        }

        public override void Attack(Fighter target, List<ActiveEffect> effects)
        {
            Demon newDemon = new Demon(target, this);
            effects.Add(newDemon);
            _demons.Add(newDemon);
            Console.WriteLine($"{Name} призывает демонов");
        }

        public void RemoveDemon(Demon demon)
        {
            _demons.Remove(demon);
        }

        public override string ToString()
        {
            return "Чернокнижник " + base.ToString();
        }
    }

    class Prist : Fighter
    {
        private float _charmPower;
        private int _chargeTime;
        private int _currentTime;

        public Prist(string name, float health) : base(name, health, 100)
        {
            _charmPower = 5;
            _chargeTime = 10;
            _currentTime = 0;
        }

        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage / _charmPower);
            Console.WriteLine($"{Name} отражает атаку оберегом");
        }

        public override void Attack(Fighter target, List<ActiveEffect> effects)
        {
            if (_currentTime < _chargeTime)
            {
                Console.WriteLine($"{Name} ждёт и копит силы");
                _currentTime++;
            }
            else
            {
                Console.WriteLine($"{Name} создаёт мощный луч света и испепеляет врагов");
                target.TakeDamage(1000);
            }
        }

        public override string ToString()
        {
            return "Жрец " + base.ToString();
        }
    }

    class FrostMage : Fighter
    {

        public FrostMage(string name, float health, float damage) : base(name, health, damage)
        { }

        public override void Attack(Fighter target, List<ActiveEffect> effects)
        {
            Console.WriteLine($"{Name} выпускает ледяную стрелу в {target.Name}");
            base.Attack(target, effects);
            effects.Add(new Freezing(target, 2));
        }

        public override string ToString()
        {
            return "Ледяной маг " + base.ToString();
        }
    }

    abstract class ActiveEffect
    {
        private int _duration;
        private int _currentTime;
        protected Fighter _target;

        public bool Enabled { get; private set; }
        protected int Duration => _duration;

        public ActiveEffect(int duration, Fighter target)
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

        public Burning(Fighter target, int duration = 3, float damage = 5) : base(duration, target)
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

        public Freezing(Fighter target, int duration = 3) : base(duration, target)
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

        public Demon(Fighter target, Warlock caster, int duration = 5, float damage = 5) : base(duration, target)
        {
            _caster = caster;
            _damage = damage;
        }

        protected override void UseEffect()
        {
            _target.TakeDamage(_damage);
            Console.WriteLine($"Демон атакует {_target.Name}");
        }

        protected override void Disable()
        {
            _caster.RemoveDemon(this);
        }
    }
}