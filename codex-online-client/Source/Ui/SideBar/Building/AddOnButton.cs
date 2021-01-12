using Nez;
using System;

namespace codex_online
{
    public class AddOnButton : BuildingButton
    {
        private static readonly String addOnString = "Add On";

        public AddOnButton(NezSpriteFont font, bool owner) : base(font, owner)
        {
            TopDisplay.Text = addOnString;
            AddComponent(new BoxCollider(CellWidth, CellHeight));
        }

        public void UpdateStatus(BaseBuildingStatus addOnStatus, AddOnType addOnType)
        {
            switch (addOnStatus)
            {
                case BaseBuildingStatus.Building:
                    BottomDisplay.Text = addOnType + Environment.NewLine + Preparing;
                    break;
                case BaseBuildingStatus.Active:
                    BottomDisplay.Text = addOnType.ToString();
                    break;
                case BaseBuildingStatus.Unbuilt:
                    BottomDisplay.Text = string.Empty;
                    break;
            }
        }
    }
}
