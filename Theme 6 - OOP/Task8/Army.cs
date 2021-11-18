using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp4
{
    class Army
    {
        private List<Soilder> _soilderPool;
        private ConsoleColor _armyColor;
        private BattleGround _battleGround;
        public string Name { get; private set; }
        public List<Soilder> SoilderPool => _soilderPool;

        public Army(BattleGround battleGround, string name, ConsoleColor color, List<Coord2> soilderCoords, CellStatus status)
        {
            Name = name;
            _armyColor = color;
            Random random = new Random();
            int soilderClassCount = 5;
            _soilderPool = new List<Soilder>();
            _battleGround = battleGround;

            soilderCoords.ForEach(coord =>
            {
                _soilderPool.Add(new Soilder(this, random.Next(soilderClassCount), coord, color, status));
            });
        }

        public List<AttackData> Update(List<AttackData> attackDatas, List<ActiveEffect> effects)
        {
            List<Soilder> deadSoilders = new List<Soilder>();

            _soilderPool.ForEach(soilder =>
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
                _soilderPool.Remove(soilder);
                HonorTheFallen(soilder); });

            _soilderPool.ForEach(soilder =>
            {
                soilder.Graphics.Move(_battleGround);
                AttackData data = soilder.Combat.LocateTarget(_battleGround.Map);

                if (data != null)
                    attackDatas.Add(data);
            });

            return attackDatas;
        }

        public void HonorTheFallen(Soilder deadSoilder)
        {
            _soilderPool.Remove(deadSoilder);
            _battleGround.ClearCell(deadSoilder.Graphics.Position);
        }
    }
}
