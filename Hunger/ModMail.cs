using StardewModdingAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hunger
{
    class ModMail : IAssetEditor
    {
        public ModMail() { }

        public bool CanEdit<T>(IAssetInfo asset)
        {
            return asset.AssetNameEquals("Data\\mail");
        }

        public void Edit<T>(IAssetData asset)
        {
            var data = asset.AsDictionary<string, string>().Data;

            data["spring_3_1"] = "Hey there @,^Starting to feel HUNGRY yet? Maybe it's your REAL LIFE HUMAN boy/girl STOMACH reminding you that you gotta EAT to live.^These should help keep it quiet for a few days.^^   -Hunger %item object 194 5 %%";
            data["spring_9_1"] = "Hello agin,^The spirits seem to be warning me about RAIN in the near future, either that or I watch way too much TV...^EAT these to get the most out of your next rainy day, or SELL THEM, I DON'T CARE!^^   -Hunger %item object 731 2 %%";
            data["spring_15_1"] = "@,^Keep an eye out the next couple days for ANTIOXIDANT RICH BERRIES on bushes around town to fuel your busy life.^Here's a few to get you hooked...That was in poor TASTE, sorry!^^   -Hunger %item object 296 7 %%";
            data["spring_28_1"] = "Hey @,^How was your first month of being a REAL LIFE farmer? Just kidding, I know you can't answer me!^Hope this comes in handy for your next big seed plating session.You know BREAKFAST IS THE MOST IMPOR...Well see ya around.^^   -Hunger^P.Sssst. Check out that small bush behind the playground at exactly noon today!  %item object 201 1 %%";
            data["spring_1_2"] = "@,^LONG TIME NO SEE!...Who are we kidding, I've been watching you this whole time.^FRUIT SALAD?^^   -That Dragon, Hunger %item object 610 4 %%";
            data["fall_2_1"] = "@,^^One for you, one for a FRIEND.^^   -Hunger %item object 218 2 732 2 220 2 226 2 608 2 205 2 224 2 %%";
        }
    }
}
