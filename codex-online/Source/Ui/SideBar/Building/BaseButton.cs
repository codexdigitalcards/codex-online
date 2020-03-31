using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nez;
using Microsoft.Xna.Framework.Graphics;

namespace codex_online.SideBar
{
    public class BaseButton : BuildingButton
    {
        private static readonly String flying = "Flying";
        private static readonly String baseName = "Base";

        protected Base GameBase { get; set; }

        public BaseButton(NezSpriteFont font, Base gameBase) : base(font, gameBase)
        {
            GameBase = gameBase;
            DisplayName.text = baseName;
            DisplayNumber.text = GameBase.Health.ToString();
            gameBase.Updated += StatusUpdated;
            //addComponent(new BoxCollider(texture.Width, texture.Height
        }

        /// <summary>
        /// When Base zone is updated reflect changes in UI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StatusUpdated(object sender, EventArgs e)
        {
            switch (GameBase.Status)
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