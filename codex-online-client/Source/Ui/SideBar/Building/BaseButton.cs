using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nez;
using Microsoft.Xna.Framework.Graphics;

namespace codex_online
{
    public class BaseButton : BuildingButton
    {
        private static readonly String flying = "Flying";
        private static readonly String baseName = "Base";

        public BaseButton(NezSpriteFont font, int startingHealth) : base(font)
        {
            TopDisplay.text = baseName;
            MiddleDisplay.text = startingHealth.ToString();
            //addComponent(new BoxCollider(texture.Width, texture.Height
        }

        /// <summary>
        /// When Base zone is updated reflect changes in UI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateStatus(BaseStatus status)
        {
            switch (status)
            {
                case BaseStatus.Nothing:
                    BottomDisplay.text = String.Empty;
                    break;
                case BaseStatus.Flying:
                    BottomDisplay.text = flying;
                    break;
            }
        }
    }
}