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

        protected Base GameBase { get; set; }

        public BaseButton(NezSpriteFont font) : base(font)
        {
            DisplayName.text = baseName;
            DisplayNumber.text = GameBase.Health.ToString();
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
                    DisplayStatus.text = String.Empty;
                    break;
                case BaseStatus.Flying:
                    DisplayStatus.text = flying;
                    break;
            }
        }
    }
}