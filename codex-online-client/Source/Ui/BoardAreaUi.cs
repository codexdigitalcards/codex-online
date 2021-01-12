using Nez;

namespace codex_online
{
    public abstract class BoardAreaUi : Entity
    {
        protected CodexNetClient NetworkClient { get; set; }
        public Name AreaName { get; protected set; }

        public virtual void CardDropped(CardUi card)
        {
            
        }

        public virtual void BoardClicked()
        {

        }
    }
}
