using System;

namespace ConsoleApp3
{
    class Program
    {

        static void Main(string[] args)
        {
            double playerHP = 100, bossHP = 100, bossDamage = 10;
            double fireballDamage = 10, frostArrowDamage = 5, darkShotDamage = 30;
            int charmHold = 0;
            int fireballHold = 0;

            Console.WriteLine("Герой отправляется на бой с боссом. Он подготовился и выучил 4 заклинания: \n" +
                $"Fireball - наносит {fireballDamage} урона огнём и усиливается с каждым применением; \n" +
                $"FrostArrow - наносит {frostArrowDamage} урона и замораживает босса, если конечно тот не горит; \n" +
                "Charm - оберег, снижающий урон по герою во время действия; \n" +
                "DarkShot - самое мощное и опасное заклинание, к нему надо быть готовым.");

            for (int turn = 1; playerHP > 0 && bossHP > 0; turn++)
            {
                Console.WriteLine($"[Ход {turn} ХП героя: {playerHP} ХП босса: {bossHP}]");
                Console.Write("Действие героя: ");

                Random rand = new Random();
                double currentBossDamage = bossDamage;
                switch(Console.ReadLine())
                {
                    case "Charm":
                        currentBossDamage /= 3;
                        charmHold++;
                        fireballHold = 0;
                        Console.WriteLine("Герой готовит защитное заклинание");
                        break;
                    case "Fireball":
                        double currentFireballDamage = fireballDamage + (rand.NextDouble() * fireballDamage / 3 - fireballDamage / 6) * (fireballHold + 1);
                        bossHP -= currentFireballDamage;
                        Console.WriteLine($"Герой выпускает огненный шар и наносит {currentFireballDamage} урона.");
                        charmHold = 0;
                        fireballHold++;
                        break;
                    case "FrostArrow":
                        double currentFrostArrowDamage = frostArrowDamage + (rand.NextDouble() * frostArrowDamage / 3 - frostArrowDamage / 6) / (fireballHold + 1);
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
                            currentBossDamage *= 0.7;
                        }
                        fireballHold = 0;
                        charmHold = 0;
                        break;
                    case "DarkShot":
                        double currentDarkShotDamage = darkShotDamage + (rand.NextDouble() * darkShotDamage / 3 - darkShotDamage / 6);
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
                    currentBossDamage = currentBossDamage + (rand.NextDouble() * currentBossDamage / 3 - currentBossDamage / 6);
                    Console.WriteLine($"Босс замахивается молотом и наносит {currentBossDamage} урона. \n");
                    playerHP -= currentBossDamage;
                }
            }
            if(bossHP > 0)
                Console.WriteLine("Герой был силён, но всё же не смог справиться с боссом.");
            else
                Console.WriteLine("Герой наносит последний удар, и босс падает на землю.");
            Console.ReadLine();
        }
        
    }
}
