namespace MiUtil
{
    public struct MiControl
    {
        private int intValue;
        public MiControl(int intValue)
        {
            this.intValue = intValue;
        }

        public static implicit operator int(MiControl control)
        {
            return control.intValue;
        }
    }
}
