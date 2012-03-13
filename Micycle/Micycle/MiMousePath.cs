using System.Collections.Generic;

using Microsoft.Xna.Framework;

using FarseerPhysics.SamplesFramework;

using MiUtil;

namespace Micycle
{
    class MiMousePath
    {
        private Vector2 source;
        private Vector2 acceptWaitQueueTail;
        private List<Vector2> acceptWaitQueuePath;
        private Vector2 acceptWaitQueueHead;
        private Vector2 acceptDest;
        private Vector2 rejectWaitQueueTail;
        private List<Vector2> rejectWaitQueuePath;
        private Vector2 rejectWaitQueueHead;
        private Vector2 rejectDest;

        public Vector2 Source { get { return source; } }
        public Vector2 AcceptWaitQueueTail { get { return acceptWaitQueueTail; } }
        public List<Vector2> AcceptWaitQueuePath { get { return acceptWaitQueuePath; } }
        public Vector2 AcceptWaitQueueHead { get { return acceptWaitQueueHead; } }
        public Vector2 AcceptDest { get { return acceptDest; } }
        public Vector2 RejectWaitQueueTail { get { return rejectWaitQueueTail; } }
        public List<Vector2> RejectWaitQueuePath { get { return rejectWaitQueuePath; } }
        public Vector2 RejectWaitQueueHead { get { return rejectWaitQueueHead; } }
        public Vector2 RejectDest { get { return rejectDest; } }

        public MiMousePath(Vector2 source, Vector2 accept_wait_queue_tail, Vector2 accept_wait_queue_head, Vector2 accept_dest, Vector2 reject_wait_queue_tail, Vector2 reject_wait_queue_head, Vector2 reject_dest)
        {
            this.source = ConvertUnits.ToSimUnits(source - MiResolution.Center);
            this.acceptWaitQueueTail = ConvertUnits.ToSimUnits(accept_wait_queue_tail - MiResolution.Center);
            this.acceptWaitQueuePath = new List<Vector2>();
            this.acceptWaitQueueHead = ConvertUnits.ToSimUnits(accept_wait_queue_head - MiResolution.Center);
            this.acceptDest = ConvertUnits.ToSimUnits(accept_dest - MiResolution.Center);
            this.rejectWaitQueueTail = ConvertUnits.ToSimUnits(reject_wait_queue_tail - MiResolution.Center);
            this.rejectWaitQueuePath = new List<Vector2>();
            this.rejectWaitQueueHead = ConvertUnits.ToSimUnits(reject_wait_queue_head - MiResolution.Center);
            this.rejectDest = ConvertUnits.ToSimUnits(reject_dest - MiResolution.Center);
        }

        public void SetAcceptWaitQueuePath(params Vector2[] path)
        {
            foreach (Vector2 node in path)
                acceptWaitQueuePath.Add(node);
        }

        public void SetRejectWaitQueuePath(params Vector2[] path)
        {
            foreach (Vector2 node in path)
                rejectWaitQueuePath.Add(node);
        }
    }
}
