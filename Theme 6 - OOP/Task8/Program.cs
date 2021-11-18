using System;
using System.Collections.Generic;
using System.Threading;

namespace ConsoleApp4
{
    class Program
    {

        static void Main(string[] args)
        {
            BattleGround battleGround = new BattleGround(50, 30);
            Army[] armies = battleGround.CreateArmies(20, 7);
            List<AttackData> attackDatas = new List<AttackData>();
            List<ActiveEffect> effects = new List<ActiveEffect>();

            do
            {
                attackDatas = armies[0].Update(attackDatas, effects);
                attackDatas = armies[1].Update(attackDatas, effects);
                Thread.Sleep(200);
                
                List<ActiveEffect> disabledEffects = new List<ActiveEffect>();
                
                effects.ForEach(effect => { 
                    if (effect.Enabled) 
                        effect.Update(); 
                    else 
                        disabledEffects.Add(effect);
                });
                disabledEffects.ForEach(effect => effects.Remove(effect));
            }
            while (armies[0].SoilderPool.Count > 0 || armies[0].SoilderPool.Count > 0);
            Console.SetCursorPosition(0, 30);
        }
    }

    class AttackData
    {
        public SoilderCombat Attacker { get; private set; }
        public Coord2 TargetPoint { get; private set; }

        public AttackData(SoilderCombat attacker, Coord2 targetPoint)
        {
            Attacker = attacker;
            TargetPoint = targetPoint;
        }
    }
}
