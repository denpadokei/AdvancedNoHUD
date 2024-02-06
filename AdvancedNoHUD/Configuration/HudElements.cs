namespace AdvancedNoHUD.Configuration
{
    public class HudElements
    {
        public virtual bool Combo { get; set; } = true;
        public virtual bool Score { get; set; } = true;
        public virtual bool Rank { get; set; } = true;
        public virtual bool Multiplier { get; set; } = true;
        public virtual bool Progress { get; set; } = true;
        public virtual bool EnargyBar { get; set; } = true;

        public void SetAll(bool combo, bool score, bool rank, bool multiplier, bool progress, bool health)
        {
            this.Combo = combo;
            this.Score = score;
            this.Rank = rank;
            this.Multiplier = multiplier;
            this.Progress = progress;
            this.EnargyBar = health;
        }

        public HudElements()
        {

        }

        public HudElements(bool combo, bool score, bool rank, bool multiplier, bool progress, bool health)
        {
            this.Combo = combo;
            this.Score = score;
            this.Rank = rank;
            this.Multiplier = multiplier;
            this.Progress = progress;
            this.EnargyBar = health;
        }
    }
}
