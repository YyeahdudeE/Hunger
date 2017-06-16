using System;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;

namespace Hunger
{
    public class ModEntry : Mod
    {
        public override void Entry(IModHelper helper)
        {
            ModConfig config = helper.ReadConfig<ModConfig>();
            GameEvents.EighthUpdateTick += this.EighthUpdateTick;
        }

        private void EighthUpdateTick(object sender, EventArgs e)
        {
            ModConfig config = this.Helper.ReadConfig<ModConfig>();

            if (Game1.player == null || Game1.hasLoadedGame == false || Game1.isFestival()  || Game1.activeClickableMenu !=  null || Game1.currentMinigame != null || Game1.dialogueUp == true || Game1.eventUp == true || !Game1.game1.IsActive)
                return;

            float ticksPerHour = 8 * config.realSecondsPerInGame10Minutes * 6;

            if (Game1.currentLocation.name == "FarmHouse")
            {
                float gainAmount = 0f;
                switch (Game1.player.houseUpgradeLevel)
                {
                    case 0:
                        gainAmount = config.insidePlayerHomeRegenerationPerHourFarmLevel0 / ticksPerHour;
                        break;
                    case 1:
                        gainAmount = config.insidePlayerHomeRegenerationPerHourFarmLevel1 / ticksPerHour;
                        break;
                    case 2:
                        gainAmount = config.insidePlayerHomeRegenerationPerHourFarmLevel2 / ticksPerHour;
                        break;
                    case 3:
                        gainAmount = config.insidePlayerHomeRegenerationPerHourFarmLevel3 / ticksPerHour;
                        break;
                }
                if (gainAmount + Game1.player.stamina >= Game1.player.maxStamina)
                {
                    Game1.player.stamina = Game1.player.maxStamina;
                }
                else
                {
                    Game1.player.stamina += gainAmount;
                }
            }
            else
            {
                float drainAmount = 0f;
                switch (Game1.player.houseUpgradeLevel)
                {
                    case 0:
                        drainAmount = config.standingStillStaminaDrainPerHourFarmLevel0 / ticksPerHour;
                        break;
                    case 1:
                        drainAmount = config.standingStillStaminaDrainPerHourFarmLevel1/ ticksPerHour;
                        break;
                    case 2:
                        drainAmount = config.standingStillStaminaDrainPerHourFarmLevel2 / ticksPerHour;
                        break;
                    case 3:
                        drainAmount = config.standingStillStaminaDrainPerHourFarmLevel3 / ticksPerHour;
                        break;
                }

                if (Game1.player.isMoving() && !Game1.player.isRidingHorse())
                {
                    drainAmount *= config.walkingMultiplier;
                }
                if (Game1.currentLocation.name == "UndergroundMine")
                {
                    drainAmount *= config.inMineMultiplier;
                }
                if (Game1.timeOfDay >= 2400)
                {
                    drainAmount *= config.afterMidnightMultiplier;
                }

                Game1.player.stamina -= drainAmount;
            }
        }
    }

    class ModConfig
    {
        public float insidePlayerHomeRegenerationPerHourFarmLevel0 { get; set; } = 110;
        public float insidePlayerHomeRegenerationPerHourFarmLevel1 { get; set; } = 135;
        public float insidePlayerHomeRegenerationPerHourFarmLevel2 { get; set; } = 160;
        public float insidePlayerHomeRegenerationPerHourFarmLevel3 { get; set; } = 185;

        public float standingStillStaminaDrainPerHourFarmLevel0 { get; set; } = 3;
        public float standingStillStaminaDrainPerHourFarmLevel1 { get; set; } = 9;
        public float standingStillStaminaDrainPerHourFarmLevel2 { get; set; } = 12;
        public float standingStillStaminaDrainPerHourFarmLevel3 { get; set; } = 15;

        public float walkingMultiplier { get; set; } = 2;
        public float inMineMultiplier { get; set; } = 2;
        public float afterMidnightMultiplier { get; set; } = 1.5f;

        public float realSecondsPerInGame10Minutes { get; set; } = 7;
    }
}
