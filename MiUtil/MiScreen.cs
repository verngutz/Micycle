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

        public virtual IEnumerator<int> EntrySequence() { yield return 0; }

        public virtual IEnumerator<int> Upped() { yield return 0; }
        public virtual IEnumerator<int> Downed() { yield return 0; }
        public virtual IEnumerator<int> Lefted() { yield return 0; }
        public virtual IEnumerator<int> Righted() { yield return 0; }
        public virtual IEnumerator<int> Pressed() { yield return 0; }
        public virtual IEnumerator<int> Cancelled() { yield return 0; }
    }
}
