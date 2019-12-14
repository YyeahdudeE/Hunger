using System;
using System.IO;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;

namespace Hunger
{
    public class ModEntry : Mod
    {
        private float ticksPerHour = 0f;
        private float gainAmount = 0f;
        private float drainAmount = 0f;
        private ModConfig config;

        public override void Entry(IModHelper helper)
        {
            this.config = helper.ReadConfig<ModConfig>();
            ticksPerHour = 8 * config.RealSecondsPerInGame10Minutes * 6;
            Helper.Events.GameLoop.UpdateTicked += GameLoop_UpdateTicked;
            Helper.Events.GameLoop.GameLaunched += GameLoop_GameLaunched;
        }

        private void GameLoop_GameLaunched(object sender, GameLaunchedEventArgs e)
        {
            if (config.HungerMailEnabled)
            {
                Helper.Content.AssetEditors.Add(new ModMail());
            }
        }

        private void GameLoop_UpdateTicked(object sender, UpdateTickedEventArgs e)
        {

            if (e.IsMultipleOf(8))
            {

                if (Game1.player == null || Game1.hasLoadedGame == false || Game1.isFestival() || Game1.activeClickableMenu != null || Game1.currentMinigame != null || Game1.dialogueUp == true || Game1.eventUp == true || !Game1.game1.IsActive)
                    return;

                if (Game1.currentLocation.Name == "FarmHouse")
                {
                    gainAmount = 0f;
                    switch (Game1.player.HouseUpgradeLevel)
                    {
                        case 0:
                            gainAmount = config.InsidePlayerHomeRegenerationPerHourFarmLevel0 / ticksPerHour;
                            break;
                        case 1:
                            gainAmount = config.InsidePlayerHomeRegenerationPerHourFarmLevel1 / ticksPerHour;
                            break;
                        case 2:
                            gainAmount = config.InsidePlayerHomeRegenerationPerHourFarmLevel2 / ticksPerHour;
                            break;
                        case 3:
                            gainAmount = config.InsidePlayerHomeRegenerationPerHourFarmLevel3 / ticksPerHour;
                            break;
                        default:
                            break;
                    }
                    if (gainAmount + Game1.player.stamina >= Game1.player.maxStamina.Value)
                    {
                        Game1.player.stamina = Game1.player.maxStamina.Value;
                    }
                    else
                    {
                        Game1.player.stamina += gainAmount;
                    }
                }
                else
                {
                    drainAmount = 0f;
                    switch (Game1.player.HouseUpgradeLevel)
                    {
                        case 0:
                            drainAmount = config.StandingStillStaminaDrainPerHourFarmLevel0 / ticksPerHour;
                            break;
                        case 1:
                            drainAmount = config.StandingStillStaminaDrainPerHourFarmLevel1 / ticksPerHour;
                            break;
                        case 2:
                            drainAmount = config.StandingStillStaminaDrainPerHourFarmLevel2 / ticksPerHour;
                            break;
                        case 3:
                            drainAmount = config.StandingStillStaminaDrainPerHourFarmLevel3 / ticksPerHour;
                            break;
                    }

                    if (Game1.player.isMoving() && !Game1.player.isRidingHorse())
                    {
                        drainAmount *= config.WalkingMultiplier;
                    }
                    if (Game1.currentLocation.Name == "UndergroundMine")
                    {
                        drainAmount *= config.InMineMultiplier;
                    }
                    if (Game1.timeOfDay >= 2400)
                    {
                        drainAmount *= config.AfterMidnightMultiplier;
                    }

                    Game1.player.stamina -= drainAmount;
                }
            }
        }

    }

    class ModConfig
    {
        public float InsidePlayerHomeRegenerationPerHourFarmLevel0 { get; set; } = 110;
        public float InsidePlayerHomeRegenerationPerHourFarmLevel1 { get; set; } = 135;
        public float InsidePlayerHomeRegenerationPerHourFarmLevel2 { get; set; } = 160;
        public float InsidePlayerHomeRegenerationPerHourFarmLevel3 { get; set; } = 185;

        public float StandingStillStaminaDrainPerHourFarmLevel0 { get; set; } = 3;
        public float StandingStillStaminaDrainPerHourFarmLevel1 { get; set; } = 9;
        public float StandingStillStaminaDrainPerHourFarmLevel2 { get; set; } = 12;
        public float StandingStillStaminaDrainPerHourFarmLevel3 { get; set; } = 15;

        public float WalkingMultiplier { get; set; } = 2;
        public float InMineMultiplier { get; set; } = 2;
        public float AfterMidnightMultiplier { get; set; } = 1.5f;

        public float RealSecondsPerInGame10Minutes { get; set; } = 7;

        public bool HungerMailEnabled { get; set; } = true;
    }
}
