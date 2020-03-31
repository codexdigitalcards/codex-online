using Microsoft.Xna.Framework.Graphics;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codex_online.SideBar
{
    public class AddOnButton : BuildingButton
    {
        private static readonly String addOnString = "Add On";

        protected Player Player { get; set; }

        public AddOnButton(NezSpriteFont font, Player player) : base(font, null)
        {
            Player = player;
            DisplayName.text = addOnString;
            Building = Player.AddOn;
            Player.Updated += StatusUpdated;
            Player.Updated += AddOnUpdated;
        }

        protected void StatusUpdated(object sender, EventArgs e)
        {
            if (Player.AddOn != null)
            {
                if (Player.AddOn.Status == BaseBuildingStatus.Building)
                {
                    DisplayStatus.text = Player.AddOn.Name + Environment.NewLine + Preparing;
                }
                else
                {
                    DisplayStatus.text = Player.AddOn.Name.ToString();
                }                
            }
        }

        protected void AddOnUpdated(object sender, EventArgs e)
        {
            Building = Player.AddOn;
        }
    }
}
