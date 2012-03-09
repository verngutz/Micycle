namespace Micycle
{
    class MiMousePath
    {
        private float sourceX;
        private float sourceY;
        private float sourceExitX;
        private float sourceExitY;
        private float waitQueueHeadX;
        private float waitQueueHeadY;
        private float acceptDestX;
        private float acceptDestY;
        private float rejectWaitQueueTailX;
        private float rejectWaitQueueTailY;
        private float rejectWaitQueueHeadX;
        private float rejectWaitQueueHeadY;
        private float rejectDestX;
        private float rejectDestY;

        public float SourceX { get { return sourceX; } }
        public float SourceY { get { return sourceY; } }
        public float SourceExitX { get { return sourceExitX; } }
        public float SourceExitY { get { return sourceExitY; } }
        public float WaitQueueHeadX { get { return waitQueueHeadX; } }
        public float WaitQueueHeadY { get { return waitQueueHeadY; } }
        public float AcceptDestX { get { return acceptDestX; } }
        public float AcceptDestY { get { return acceptDestY; } }
        public float RejectDestX { get { return rejectDestX; } }
        public float RejectDestY { get { return rejectDestY; } }
        public float RejectWaitQueueTailX { get { return rejectWaitQueueTailX; } }
        public float RejectWaitQueueTailY { get { return rejectWaitQueueTailY; } }
        public float RejectWaitQueueHeadX { get { return rejectWaitQueueHeadX; } }
        public float RejectWaitQueueHeadY { get { return rejectWaitQueueHeadY; } }

        public MiMousePath(float source_x, float source_y, float source_exit_x, float source_exit_y, float wait_queue_head_x, float wait_queue_head_y, float accept_dest_x, float accept_dest_y, float reject_dest_x, float reject_dest_y, float reject_wait_queue_tail_x, float reject_wait_queue_tail_y, float reject_wait_queue_head_x, float reject_wait_queue_head_y)
        {
            sourceX = source_x;
            sourceY = source_y;
            sourceExitX = source_exit_x;
            sourceExitY = source_exit_y;
            waitQueueHeadX = wait_queue_head_x;
            waitQueueHeadY = wait_queue_head_y;
            acceptDestX = accept_dest_x;
            acceptDestY = accept_dest_y;
            rejectDestX = reject_dest_x;
            rejectDestY = reject_dest_y;
            rejectWaitQueueTailX = reject_wait_queue_tail_x;
            rejectWaitQueueTailY = reject_wait_queue_tail_y;
            rejectWaitQueueHeadX = reject_wait_queue_head_x;
            rejectWaitQueueHeadY = reject_wait_queue_head_y;
        }
    }
}
