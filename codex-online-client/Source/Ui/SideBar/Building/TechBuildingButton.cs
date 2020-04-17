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

        public TechBuildingButton(NezSpriteFont font, TechLevel level) : base(font)
        {
            switch (level)
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
            TopDisplay.text = tech + TechLevelString;
        }

        private void UpdateStatus(TechBuilding techBuilding)
        {
            switch (techBuilding.Status)
            {
                case BaseBuildingStatus.Unbuilt:
                    BottomDisplay.text = dollarSign + techBuilding.Price;
                    break;
                case BaseBuildingStatus.Building:
                    BottomDisplay.text = Preparing;
                    break;
                case BaseBuildingStatus.Active:
                    BottomDisplay.text = String.Empty;
                    break;
            }
        }
    }
}
