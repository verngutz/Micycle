using Microsoft.Xna.Framework;

using FarseerPhysics.SamplesFramework;

using MiUtil;

namespace Micycle
{
    class MiMousePath
    {
        private Vector2 source;
        private Vector2 acceptWaitQueueTail;
        private Vector2 acceptWaitQueueHead;
        private Vector2 acceptDest;
        private Vector2 rejectWaitQueueTail;
        private Vector2 rejectWaitQueueHead;
        private Vector2 rejectDest;

        public Vector2 Source { get { return source; } }
        public Vector2 AcceptWaitQueueTail { get { return acceptWaitQueueTail; } }
        public Vector2 AcceptWaitQueueHead { get { return acceptWaitQueueHead; } }
        public Vector2 AcceptDest { get { return acceptDest; } }
        public Vector2 RejectWaitQueueTail { get { return rejectWaitQueueTail; } }
        public Vector2 RejectWaitQueueHead { get { return rejectWaitQueueHead; } }
        public Vector2 RejectDest { get { return rejectDest; } }

        public MiMousePath(Vector2 source, Vector2 accept_wait_queue_tail, Vector2 accept_wait_queue_head, Vector2 accept_dest, Vector2 reject_wait_queue_tail, Vector2 reject_wait_queue_head, Vector2 reject_dest)
        {
            this.source = ConvertUnits.ToSimUnits(source - MiResolution.Center);
            this.acceptWaitQueueTail = ConvertUnits.ToSimUnits(accept_wait_queue_tail - MiResolution.Center);
            this.acceptWaitQueueHead = ConvertUnits.ToSimUnits(accept_wait_queue_head - MiResolution.Center);
            this.acceptDest = ConvertUnits.ToSimUnits(accept_dest - MiResolution.Center);
            this.rejectWaitQueueTail = ConvertUnits.ToSimUnits(reject_wait_queue_tail - MiResolution.Center);
            this.rejectWaitQueueHead = ConvertUnits.ToSimUnits(reject_wait_queue_head - MiResolution.Center);
            this.rejectDest = ConvertUnits.ToSimUnits(reject_dest - MiResolution.Center);
        }
    }
}
