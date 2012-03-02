namespace Micycle
{
    class MiMousePath
    {
        private float sourceX;
        private float sourceY;
        private float sourceExitX;
        private float sourceExitY;
        private float destAcceptX;
        private float destAcceptY;
        private float destAcceptEntranceX;
        private float destAcceptEntranceY;
        private float destRejectX;
        private float destRejectY;
        private float destRejectExitX;
        private float destRejectExitY;
        private float destRejectEntranceX;
        private float destRejectEntranceY;

        public float SourceX { get { return sourceX; } }
        public float SourceY { get { return sourceY; } }
        public float SourceExitX { get { return sourceExitX; } }
        public float SourceExitY { get { return sourceExitY; } }
        public float DestAcceptX { get { return destAcceptX; } }
        public float DestAcceptY { get { return destAcceptY; } }
        public float DestAcceptEntranceX { get { return destAcceptEntranceX; } }
        public float DestAcceptEntranceY { get { return destAcceptEntranceY; } }
        public float DestRejectX { get { return destRejectX; } }
        public float DestRejectY { get { return destRejectY; } }
        public float DestRejectExitX { get { return destRejectExitX; } }
        public float DestRejectExitY { get { return destRejectExitY; } }
        public float DestRejectEntranceX { get { return destRejectEntranceX; } }
        public float DestRejectEntranceY { get { return destRejectEntranceY; } }

        public MiMousePath(float source_x, float source_y, float source_exit_x, float source_exit_y, float dest_accept_x, float dest_accept_y, float dest_accept_entrance_x, float dest_accept_entrance_y, float dest_reject_x, float dest_reject_y, float dest_reject_exit_x, float dest_reject_exit_y, float dest_reject_entrance_x, float dest_reject_entrance_y)
        {
            sourceX = source_x;
            sourceY = source_y;
            sourceExitX = source_exit_x;
            sourceExitY = source_exit_y;
            destAcceptX = dest_accept_x;
            destAcceptY = dest_accept_y;
            destAcceptEntranceX = dest_accept_entrance_x;
            destAcceptEntranceY = dest_accept_entrance_y;
            destRejectX = dest_reject_x;
            destRejectY = dest_reject_y;
            destRejectExitX = dest_reject_exit_x;
            destRejectExitY = dest_reject_exit_y;
            destRejectEntranceX = dest_reject_entrance_x;
            destRejectEntranceY = dest_reject_entrance_y;
        }
    }
}
