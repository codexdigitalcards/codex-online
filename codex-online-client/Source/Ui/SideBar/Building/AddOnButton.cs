using Microsoft.Xna.Framework.Graphics;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codex_online
{
    public class AddOnButton : BuildingButton
    {
        private static readonly String addOnString = "Add On";

        public AddOnButton(NezSpriteFont font) : base(font)
        {
            DisplayName.text = addOnString;
        }

        protected void UpdateStatus(AddOn addOn)
        {
            switch (addOn.Status)
            {
                case BaseBuildingStatus.Building:
                    DisplayStatus.text = addOn.Type + Environment.NewLine + Preparing;
                    break;
                case BaseBuildingStatus.Active:
                    DisplayStatus.text = addOn.Type.ToString();
                    break;
                case BaseBuildingStatus.Unbuilt:
                    DisplayStatus.text = string.Empty;
                    break;
            }
        }
    }
}
