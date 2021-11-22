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
<<<<<<< HEAD
            while (armies[0].SoldierCount > 0 && armies[1].SoldierCount > 0);
=======
            while (armies[0].SoilderPool.Count > 0 && armies[1].SoilderPool.Count > 0);
            Console.SetCursorPosition(0, 30);
>>>>>>> ed10156cd7623c627a7f73c7d813d0cbc9d3ccfd
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
