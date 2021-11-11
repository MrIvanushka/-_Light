using System;

namespace ConsoleApp3
{
    class Program
    {

        static void Main(string[] args)
        {
            double playerHP = 100;
            double bossHP = 100;
            double bossDamage = 10;
            double fireballDamage = 10;
            double frostArrowDamage = 5;
            double darkShotDamage = 30;
            double frostDamageModifier = 0.7;
            double charmDamageModifier = 0.4;
            double attackFluctuation = 0.3;
            int charmHold = 0;
            int fireballHold = 0;

            for (int turn = 1; playerHP > 0 && bossHP > 0; turn++)
            {
                Console.WriteLine($"[Ход {turn} ХП героя: {playerHP} ХП босса: {bossHP}]");
                Console.Write("Действие героя: ");
                Random random = new Random();
                double currentBossDamage = bossDamage;
                
                switch(Console.ReadLine())
                {
                    case "Charm":
                        currentBossDamage *= charmDamageModifier;
                        charmHold++;
                        fireballHold = 0;
                        Console.WriteLine("Герой готовит защитное заклинание");
                        break;
                    case "Fireball":
                        double fireballFluctuationInPercents = random.NextDouble() * attackFluctuation * (fireballHold + 1) - attackFluctuation / 2;
                        double fireballFluctuation = fireballFluctuationInPercents * fireballDamage;
                        double currentFireballDamage = fireballDamage + fireballFluctuation;
                        bossHP -= currentFireballDamage;
                        Console.WriteLine($"Герой выпускает огненный шар и наносит {currentFireballDamage} урона.");
                        charmHold = 0;
                        fireballHold++;
                        break;
                    case "FrostArrow":
                        double frostArrowFluctuationInPercents = random.NextDouble() * attackFluctuation - attackFluctuation / 2;
                        double frostArrowFluctuation = frostArrowDamage * frostArrowFluctuationInPercents / (fireballHold + 1);
                        double currentFrostArrowDamage = frostArrowDamage + frostArrowFluctuation;
                        bossHP -= currentFrostArrowDamage;
                        
                        if (fireballHold > 0)
                        {
                            Console.WriteLine($"Герой выпускает ледяную стрелу и наносит {currentFrostArrowDamage} урона. \n" +
                                "Босс сопротивляется заморозке.");
                        }  
                        else
                        {
                            Console.WriteLine($"Герой выпускает ледяную стрелу и наносит {currentFrostArrowDamage} урона. \n" +
                                "Босс замораживается и становится слабее.");
                            currentBossDamage *= frostDamageModifier;
                        }
                        fireballHold = 0;
                        charmHold = 0;
                        break;
                    case "DarkShot":
                        double darkShotFluctuationInPercents = random.NextDouble() * attackFluctuation - attackFluctuation / 2;
                        double darkShotFluctuation = darkShotFluctuationInPercents * darkShotDamage;
                        double currentDarkShotDamage = darkShotDamage + darkShotFluctuation;
                        
                        if (charmHold >= 2)
                        {

                            bossHP -= currentDarkShotDamage;
                            Console.WriteLine($"Герой обращается к магии тьмы и наносит {currentDarkShotDamage} урона. \n");
                        }
                        else
                        {
                            playerHP -= currentDarkShotDamage;
                            Console.WriteLine($"Герой не справляется с магией тьмы и получает {currentDarkShotDamage} урона. \n");
                        }
                        fireballHold = 0;
                        charmHold = 0;
                        break;
                    default:
                        Console.WriteLine("Герой путает заклинания и теряет время. \n");
                        break;
                }
                if (bossHP > 0)
                {
                    double bossDamageFluctuationInPercents = random.NextDouble() * attackFluctuation - attackFluctuation / 2;
                    double bossDamageFluctuation = bossDamageFluctuationInPercents * currentBossDamage;
                    currentBossDamage = currentBossDamage + bossDamageFluctuation;
                    Console.WriteLine($"Босс замахивается молотом и наносит {currentBossDamage} урона. \n");
                    playerHP -= currentBossDamage;
                }
            }
            if(bossHP > 0)
                Console.WriteLine("Герой был силён, но всё же не смог справиться с боссом.");
            else
                Console.WriteLine("Герой наносит последний удар, и босс падает на землю.");
        }
        
    }
}
