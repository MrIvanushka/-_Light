using System;
using System.Collections.Generic;
using System.Threading;

namespace ConsoleApp4
{
    class Program
    {

        static void Main(string[] args)
        {
            BattleGround battleGround = new BattleGround(100, 50);
            Army[] armies = battleGround.CreateArmies(150, 50);
            EffectPool effects = new EffectPool();
            List<AttackData> causedDamageList = new List<AttackData>();
            int fps = 5;

            do
            {
                for(int i = 0; i < armies.Length; i++)
                {
                    List<AttackData> recievedDamageList = causedDamageList;
                    armies[i].TakeDamageToSoldiers(recievedDamageList, effects);
                    causedDamageList = armies[i].MoveSoldiers();
                }
                effects.Update();
                Thread.Sleep(1000 / fps);
            }
            while (armies[0].SoldierCount > 0 && armies[1].SoldierCount > 0);
        }
    }

    class EffectPool : List<ActiveEffect>
    {
        public void Update()
        {
            List<ActiveEffect> disabledEffects = new List<ActiveEffect>();

            ForEach(effect => {
                if (effect.Enabled)
                    effect.Update();
                else
                    disabledEffects.Add(effect);
            });
            disabledEffects.ForEach(effect => Remove(effect));
        }
    }

    class AttackData
    {
        public SoldierCombat Attacker { get; private set; }
        public Vector2 TargetPoint { get; private set; }

        public AttackData(SoldierCombat attacker, Vector2 targetPoint)
        {
            Attacker = attacker;
            TargetPoint = targetPoint;
        }
    }
}
