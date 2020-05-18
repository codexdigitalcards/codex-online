using Nez;

namespace codex_online
{
    public class ZoneButton : SideBarEntity
    {
        public ZoneButton(NezSpriteFont font, string name, int count) : base(font)
        {
            TopDisplay.Text = name;
            MiddleDisplay.Text = count.ToString();
        }

        public void UpdateZone(int count)
        {
            MiddleDisplay.Text = count.ToString();
        }
    }
}
