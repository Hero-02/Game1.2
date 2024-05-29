using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1._2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
            int deaths = 0; // Initialize the death counter
            bool playAgain = true;
            while (playAgain)
            {
                Console.WriteLine();
                Console.WriteLine("    __//\r\n    /.__.\r\n    \\ \\/ /\r\n '__/    \\\r\n  \\-      )\r\n   \\＿_/\r\n_____|_|____\r\n     \" \"");

                Console.WriteLine("[1] Start Game");
                Console.WriteLine("[2] Exit");

                char option = Console.ReadKey().KeyChar;
                Console.Clear();

                if (option == '1')
                {
                    int enemiesDefeated = 0;
                    int specialMovesUsed = 0;
                    int potionsConsumed = 0;
                    int playerHealth = 100;
                    int potions = rnd.Next(3, 5);
                    int maxLevels = 5;
                    int specialMove = rnd.Next(1, 2);
                    int poisonDuration = 0;
                    int burnDuration = 0;
                    int paralyzeDuration = 0;

                    for (int level = 1; level <= maxLevels; level++)
                    {
                        int enemyHealth = 20 + (level * 10);
                        int minEnemyDamage = level * 2 - 1;
                        int maxEnemyDamage = level * 2 + 1;

                        bool isAssassin = level == 2;
                        bool isSlime = level == 1;
                        bool isElemental = level == 3;
                        bool isDragon = level == 4;
                        bool isChickelle = level == maxLevels;

                        if (isSlime)
                        {
                            if (playerHealth > 0)
                            {
                                Console.WriteLine("A Slime appears!");

                                enemyHealth = 40;

                                if (rnd.Next(1, 3) == 1)
                                {
                                    Console.WriteLine("The slime poisoned you!");
                                    poisonDuration = 4;
                                }
                                else if (rnd.Next(1, 6) == 1)
                                {
                                    Console.WriteLine("Chickelle granted you a blessing （・<・ ）");
                                    specialMove++;
                                }
                            }

                        }
                        else if (isAssassin)
                        {
                            if (playerHealth > 0)
                            {
                                Console.WriteLine("An Assassin appears!");

                                if (rnd.Next(1, 6) == 1) // 1 in 6 chance to do 100 damage
                                {
                                    Console.WriteLine("The assassin caught you lackin!");
                                    playerHealth -= 100;
                                }
                                else if (rnd.Next(1, 6) == 1)
                                {
                                    Console.WriteLine("Chickelle granted you a blessing （・<・ ）");
                                    specialMove++;
                                }
                                else
                                {
                                    enemyHealth = 20;
                                }
                            }
                        }
                        else if (isElemental)
                        {
                            if (playerHealth > 0)
                            {
                                Console.WriteLine("An Elemental appears!");

                                enemyHealth = 50;

                                if (rnd.Next(1, 3) == 1)
                                {
                                    Console.WriteLine("The elemental paralyzed you!");
                                    paralyzeDuration = 3;
                                }
                                else if (rnd.Next(1, 6) == 1)
                                {
                                    Console.WriteLine("Chickelle granted you a blessing （・<・ ）");
                                    specialMove++;
                                }
                            }
                        }
                        else if (isDragon)
                        {
                            if (playerHealth > 0)
                            {
                                Console.WriteLine("A Dragon appears!");

                                enemyHealth = 60;
                                Console.WriteLine($"The Dragon attacks.");
                                if (rnd.Next(1, 3) <= 1)
                                {
                                    Console.WriteLine("The dragon burned you!");
                                    burnDuration = 5;
                                }
                                if (rnd.Next(1, 3) <= 1)
                                {
                                    Console.WriteLine("The dragon paralyzed you!");
                                    paralyzeDuration = 2;
                                }
                            }
                        }
                        else if (isChickelle)
                        {
                            if (playerHealth > 0)
                            {
                                Console.WriteLine("Chickelle, the Legendary Chicken, appears!");
                            }
                            enemyHealth = 1;
                            minEnemyDamage = 1;
                            maxEnemyDamage = 1;

                        }

                        while (playerHealth > 0 && enemyHealth > 0)
                        {
                            Console.WriteLine($"\nLevel {level}");
                            Console.WriteLine($"Player health: {playerHealth}");
                            Console.WriteLine($"Enemy health: {enemyHealth}");
                            Console.WriteLine("\n[0] Attack\n[1] Heal\n[2] Special \n[3] Flee");

                            // Apply status effects
                            if (poisonDuration > 0)
                            {
                                Console.WriteLine("\nYou're poisoned! (-2 health)");
                                playerHealth -= 2;
                                poisonDuration--;
                            }
                            if (burnDuration > 0)
                            {
                                Console.WriteLine("\nYou're burning! (-3 health)");
                                playerHealth -= 3;
                                burnDuration--;
                            }
                            if (paralyzeDuration > 0)
                            {
                                Console.WriteLine("\nYou're paralyzed! (Cannot attack)");
                                paralyzeDuration--;
                            }

                            char option2 = Console.ReadKey().KeyChar;
                            Console.Clear();

                            if (option2 == '0')
                            {
                                if (paralyzeDuration == 0)
                                {
                                    int playerDamage = rnd.Next(1, 7);
                                    bool playerMissed = rnd.Next(1, 11) == 1;
                                    bool enemyMissed = rnd.Next(1, 11) == 1;

                                    if (playerMissed)
                                    {
                                        Console.WriteLine("You missed");
                                    }
                                    else
                                    {
                                        if (isChickelle && rnd.Next(1, 21) <= 19) // 90% dodge chance for Chickelle
                                        {
                                            Console.WriteLine("Chickelle dodged your attack!");
                                        }
                                        else
                                        {
                                            enemyHealth -= playerDamage;
                                            Console.WriteLine($"You attacked for {playerDamage} damage");
                                        }
                                    }

                                    if (enemyMissed)
                                    {
                                        Console.WriteLine("The enemy missed");
                                    }
                                    else
                                    {
                                        int actualEnemyDamage = rnd.Next(minEnemyDamage, maxEnemyDamage);
                                        playerHealth -= actualEnemyDamage;
                                        Console.WriteLine($"The enemy attacked for {actualEnemyDamage} damage");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("You took damage! You can't attack.");
                                    int actualEnemyDamage = rnd.Next(minEnemyDamage, maxEnemyDamage);
                                    playerHealth -= actualEnemyDamage;
                                }
                            }
                            else if (option2 == '1')
                            {
                                if (potions > 0)
                                {
                                    int heal = new Random().Next(0, 35);
                                    Console.WriteLine($"You healed for {heal} health");
                                    playerHealth += heal;
                                    Console.WriteLine($"The enemy attacked for {maxEnemyDamage} damage");
                                    playerHealth -= maxEnemyDamage;

                                    if (playerHealth > 100)
                                    {
                                        playerHealth = 100;
                                    }

                                    potions--;
                                    potionsConsumed++;
                                }
                                else
                                {
                                    Console.WriteLine("You've run out of healing potions");
                                }
                            }
                            else if (option2 == '3')
                            {
                                Console.WriteLine("You fled the battle!");
                                break;
                            }
                            else if (option2 == '2')
                            {
                                if (specialMove > 0)
                                {
                                    int playerDamage = new Random().Next(10, 21);
                                    int specialDamage = playerDamage * 2;
                                    enemyHealth -= specialDamage;
                                    Console.WriteLine($"You unleashed Dragon's Fury for {specialDamage} damage!");
                                    specialMove--;
                                    specialMovesUsed++;
                                }
                                else
                                {
                                    Console.WriteLine("You've already used your special move!");
                                }
                            }

                            if (enemyHealth <= 0)
                            {
                                Console.WriteLine("\nYou defeated the enemy!");
                                enemiesDefeated++;
                                Console.WriteLine("\nContinue to the next level? (Y/N)");
                                while (true)
                                {
                                    char continueOption = Console.ReadKey().KeyChar;
                                    if (continueOption == 'Y' || continueOption == 'y')
                                    {
                                        break;
                                    }
                                    else if (continueOption == 'N' || continueOption == 'n')
                                    {
                                        Console.WriteLine("\nGame over. Thanks for playing!");
                                        Console.WriteLine("\nScoreboard:");
                                        Console.WriteLine($"Enemies Defeated: {enemiesDefeated}");
                                        Console.WriteLine($"Special Moves Used: {specialMovesUsed}");
                                        Console.WriteLine($"Potions Consumed: {potionsConsumed}");
                                        Console.WriteLine($"Number of Deaths: {deaths}"); // Display the death counter

                                        Console.WriteLine("\nDo you want to play again? (Y/N)");
                                        while (true)
                                        {
                                            char continueOption2 = Console.ReadKey().KeyChar;
                                            if (continueOption2 == 'Y' || continueOption2 == 'y')
                                            {
                                                Console.Clear();
                                                break;
                                            }
                                            else if (continueOption2 == 'N' || continueOption2 == 'n')
                                            {
                                                Console.WriteLine("\nGoodbye!");
                                                playAgain = false;
                                                break;
                                            }
                                            else
                                            {
                                                Console.WriteLine("\nInvalid input. Please choose (Y/N):");
                                            }
                                        }

                                        return;
                                    }
                                    else
                                    {
                                        Console.WriteLine("\nInvalid input. Please choose (Y/N):");
                                    }
                                }
                                Console.Clear();
                            }
                        }

                    }

                    if (playerHealth > 0)
                    {
                        Console.WriteLine("Congratulations, You Win!");
                        Console.WriteLine("\nScoreboard:");
                        Console.WriteLine($"Enemies Defeated: {enemiesDefeated}");
                        Console.WriteLine($"Special Moves Used: {specialMovesUsed}");
                        Console.WriteLine($"Potions Consumed: {potionsConsumed}");
                        Console.WriteLine($"Number of Deaths: {deaths}");
                    }
                    else
                    {
                        Console.WriteLine("Game over. Thanks for playing!");
                        Console.WriteLine("\nScoreboard:");
                        Console.WriteLine($"Enemies Defeated: {enemiesDefeated}");
                        Console.WriteLine($"Special Moves Used: {specialMovesUsed}");
                        Console.WriteLine($"Potions Consumed: {potionsConsumed}");
                        deaths++; // Increment the death counter
                        Console.WriteLine($"Number of Deaths: {deaths}");
                    }
                }
                else if (option == '2')
                {
                    Console.WriteLine("Goodbye!");
                    return;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please choose (1/2):");
                }
            }
        }
    }
}
