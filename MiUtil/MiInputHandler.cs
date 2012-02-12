namespace MiUtil
{
    public class MiInputHandler : MiComponent
    {
        protected MiControllerState oldState;
        public MiGameState Focused;

        public MiInputHandler(MiGame game) : base(game) { }
    }
}
