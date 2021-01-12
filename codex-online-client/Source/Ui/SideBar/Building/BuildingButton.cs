using Nez;
using System;

namespace codex_online
{
    public class BuildingButton : SideBarEntity
    {
        protected static String Preparing { get; } = "Preparing...";

        protected Building Building;

        public BuildingButton(NezSpriteFont font, bool owner) : base(font, owner)
        {
            //addComponent(new BoxCollider(texture.Width, texture.Height));
            
        }

        /// <summary>
        /// When Base zone is updated reflect changes in UI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void UpdateHealth(int buildingHealth)
        {
            if (Building.Health > 0)
            {
                MiddleDisplay.Text = Building.Health.ToString();
            }
            else
            {
                MiddleDisplay.Text = String.Empty;
            }
        }
    }
}
