using Microsoft.Xna.Framework.Graphics;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codex_online
{
    public class TechBuildingButton : BuildingButton
    {
        private static readonly String tech = "Tech ";
        private static readonly String one = "I";
        private static readonly String two = "II";
        private static readonly String three = "III";
        private static readonly String dollarSign = "$";

        protected String TechLevelString { get; set; }
        protected TechBuilding TechBuilding { get; set; }

        public TechBuildingButton(NezSpriteFont font, TechBuilding techBuilding) : base(font, techBuilding)
        {
            switch (techBuilding.Level)
            {
                case TechLevel.One:
                    TechLevelString = one;
                    break;
                case TechLevel.Two:
                    TechLevelString = two;
                    break;
                case TechLevel.Three:
                    TechLevelString = three;
                    break;
            }
            DisplayName.text = tech + TechLevelString;

            TechBuilding = techBuilding;
            TechBuilding.Updated += StatusUpdated;
        }

        private void StatusUpdated(object sender, EventArgs e)
        {
            switch (TechBuilding.Status)
            {
                case BaseBuildingStatus.Unbuilt:
                    DisplayStatus.text = dollarSign + TechBuilding.Price;
                    break;
                case BaseBuildingStatus.Building:
                    DisplayStatus.text = Preparing;
                    break;
                case BaseBuildingStatus.Active:
                    DisplayStatus.text = String.Empty;
                    break;
            }
        }
    }
}
