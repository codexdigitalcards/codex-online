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

        public TechLevel TechLevel { get; }

        public TechBuildingButton(NezSpriteFont font, TechLevel level, bool owner) : base(font, owner)
        {
            TechLevel = level;
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
            TopDisplay.Text = tech + TechLevelString;
            AddComponent(new BoxCollider(CellWidth, CellHeight));
        }

        //TODO: buildable
        public void UpdateBuilding(int health, bool buildable, BaseBuildingStatus buildingStatus, int cost)
        {
            switch (buildingStatus)
            {
                case BaseBuildingStatus.Unbuilt:
                    BottomDisplay.Text = dollarSign + cost;
                    break;
                case BaseBuildingStatus.Building:
                    BottomDisplay.Text = Preparing;
                    break;
                case BaseBuildingStatus.Active:
                    BottomDisplay.Text = String.Empty;
                    break;
            }
            UpdateHealth(health);
        }
    }
}
