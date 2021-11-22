using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ConsoleApp4
{
    class Army
    {
        private List<Soldier> _soldierPool;
        private ConsoleColor _armyColor;
        private BattleGround _battleGround;
        public string Name { get; private set; }
        public int SoldierCount => _soldierPool.Count;

        public Army(BattleGround battleGround, string name, ConsoleColor color, List<Vector2> soilderCoords, CellStatus status)
        {
            Name = name;
            _armyColor = color;
            Random random = new Random();
            int soilderClassCount = 5;
            _soldierPool = new List<Soldier>();
            _battleGround = battleGround;

            soilderCoords.ForEach(coord =>
            {
                _soldierPool.Add(new Soldier(this, random.Next(soilderClassCount), coord, color, status));
            });
        }

        public List<AttackData> Update(List<AttackData> attackDatas, List<ActiveEffect> effects)
        {
            List<Soldier> deadSoilders = new List<Soldier>();

            _soldierPool.ForEach(soilder =>
            {

                attackDatas.ForEach(data =>
                {
                    if (soilder.Graphics.Position == data.TargetPoint)
                        data.Attacker.Attack(soilder.Combat, effects);
                });
                soilder.Graphics.DisableIcon(_battleGround);

                if (soilder.Combat.Health < 0)
                    deadSoilders.Add(soilder);
            });
            attackDatas = new List<AttackData>();

            deadSoilders.ForEach(soilder => { 
                _soldierPool.Remove(soilder);
                HonorTheFallen(soilder); });

            CellStatus[,] battleMap = _battleGround.GetBattleMap();

            _soldierPool.ForEach(soilder =>
            {
                soilder.Graphics.Move(_battleGround, in battleMap);
                AttackData data = soilder.Combat.LocateTarget(in battleMap);

                if (data != null)
                    attackDatas.Add(data);
            });

            return attackDatas;
        }

        public List<Soldier> GetNeighbourAllies(CellStatus[,] map, Vector2 position)
        {
            List<Soldier> neighbourAllies = new List<Soldier>();

            _soldierPool.ForEach(soilder => {
                int deltaPosX = Math.Abs(soilder.Graphics.Position.X - position.X);
                int deltaPosY = Math.Abs(soilder.Graphics.Position.Y - position.Y);
                if (deltaPosX <= 1 && deltaPosY <= 1)
                    neighbourAllies.Add(soilder);
            });

            return neighbourAllies;
        }

        public void HonorTheFallen(Soldier deadSoilder)
        {
            _soldierPool.Remove(deadSoilder);
            _battleGround.ClearCell(deadSoilder.Graphics.Position);
        }
    }
}
