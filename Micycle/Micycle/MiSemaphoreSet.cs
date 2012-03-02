namespace Micycle
{
    class MiSemaphoreSet
    {
        public int SendFromAToB;
        public int HasReachedB;
        public int AcceptIntoB;
        public int RejectFromB;

        public MiSemaphoreSet()
        {
            SendFromAToB = 0;
            HasReachedB = 0;
            AcceptIntoB = 0;
            RejectFromB = 0;
        }
    }
}
