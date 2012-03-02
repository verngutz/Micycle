using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace MiUtil
{
    public abstract class MiScreen : MiDrawableComponent
    {
        public MiButton ActiveButton { get; set; }

        protected bool exitSequenceMutex;
        protected bool entrySequenceMutex;

        public MiScreen(MiGame game)
            : base(game)
        {
            Enabled = false;
            Visible = false;
        }

        public virtual IEnumerator<ulong> EntrySequence() { yield return 0; }

        public virtual IEnumerator<ulong> Upped() { yield return 0; }
        public virtual IEnumerator<ulong> Downed() { yield return 0; }
        public virtual IEnumerator<ulong> Lefted() { yield return 0; }
        public virtual IEnumerator<ulong> Righted() { yield return 0; }
        public virtual IEnumerator<ulong> Pressed() { yield return 0; }
        public virtual IEnumerator<ulong> Cancelled() { yield return 0; }
    }
}
