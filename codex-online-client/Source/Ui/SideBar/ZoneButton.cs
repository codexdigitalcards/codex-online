using Nez;

namespace codex_online
{
    public class ZoneButton : SideBarEntity
    {
        public ZoneButton(NezSpriteFont font, Name name, int count, bool owner) : base(font, owner)
        {
            AreaName = name;
            TopDisplay.Text = NameDictionary.Dictionary[name];
            MiddleDisplay.Text = count.ToString();
        }

        public void UpdateZone(int count)
        {
            MiddleDisplay.Text = count.ToString();
        }
    }
}
