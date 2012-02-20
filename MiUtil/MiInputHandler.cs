namespace MiUtil
{
    public class MiInputHandler : MiComponent
    {
        protected MiControllerState oldState;
        public MiScreen Focused;

        public MiInputHandler(MiGame game) : base(game) { }
    }
}
