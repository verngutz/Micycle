namespace MiUtil
{
    public abstract class MiGameState : MiDrawableComponent
    {
        public MiEvent Upped;
        public MiEvent Downed;
        public MiEvent Lefted;
        public MiEvent Righted;
        public MiEvent Pressed;
        public MiEvent Cancelled;

        public MiGameState(MiGame game) : base(game) { }
    }
}
