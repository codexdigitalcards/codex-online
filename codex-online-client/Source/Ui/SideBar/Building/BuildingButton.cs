using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codex_online
{
    public class BuildingButton : SideBarButton
    {
        protected static String Preparing { get; } = "Preparing...";

        protected Building Building;

        public BuildingButton(NezSpriteFont font) : base(font)
        {
            //addComponent(new BoxCollider(texture.Width, texture.Height));
            
        }

        /// <summary>
        /// When Base zone is updated reflect changes in UI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateHealth(int buildingHealth)
        {
            if (Building.Health > 0)
            {
                DisplayNumber.text = Building.Health.ToString();
            }
            else
            {
                DisplayNumber.text = String.Empty;
            }
        }
    }
}
