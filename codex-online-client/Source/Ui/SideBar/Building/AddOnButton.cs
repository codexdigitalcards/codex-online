using Nez;
using System;

namespace codex_online
{
    public class AddOnButton : BuildingButton
    {
        private static readonly String addOnString = "Add On";

        public AddOnButton(NezSpriteFont font) : base(font)
        {
            TopDisplay.Text = addOnString;
        }

        protected void UpdateStatus(AddOn addOn)
        {
            switch (addOn.Status)
            {
                case BaseBuildingStatus.Building:
                    BottomDisplay.Text = addOn.Type + Environment.NewLine + Preparing;
                    break;
                case BaseBuildingStatus.Active:
                    BottomDisplay.Text = addOn.Type.ToString();
                    break;
                case BaseBuildingStatus.Unbuilt:
                    BottomDisplay.Text = string.Empty;
                    break;
            }
        }
    }
}
