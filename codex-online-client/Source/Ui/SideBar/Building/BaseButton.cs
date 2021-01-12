using System;
using Nez;

namespace codex_online
{
    public class BaseButton : BuildingButton
    {
        private static readonly String flying = "Flying";
        private static readonly String baseName = "Base";

        public BaseButton(NezSpriteFont font, int startingHealth, bool owner) : base(font, owner)
        {
            TopDisplay.Text = baseName;
            MiddleDisplay.Text = startingHealth.ToString();
            //addComponent(new BoxCollider(texture.Width, texture.Height
        }

        /// <summary>
        /// When Base zone is updated reflect changes in UI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void UpdateStatus(BaseStatus status)
        {
            switch (status)
            {
                case BaseStatus.Nothing:
                    BottomDisplay.Text = String.Empty;
                    break;
                case BaseStatus.Flying:
                    BottomDisplay.Text = flying;
                    break;
            }
        }
    }
}