namespace Micycle
{
    class MiSemaphoreSet
    {
        public int SendFromAToB;
        public int HasReachedWaitQueueTail;
        public int HasReachedWaitQueueHead;
        public int Accept;
        public int Reject;

        public MiSemaphoreSet()
        {
            SendFromAToB = 0;
            HasReachedWaitQueueTail = 0;
            HasReachedWaitQueueHead = 0;
            Accept = 0;
            Reject = 0;
        }
    }
}
