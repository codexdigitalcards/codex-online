namespace codex_online
{
    public abstract class FightableCard : Card, IAttackable
    {
        public int Health { get; set; }
        public int Attack { get; set; }
        public override bool CanAttack()
        {
            return !Tapped;
        }
    }
}
