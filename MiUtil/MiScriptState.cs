using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace MiUtil
{
    class MiScriptState
    {
        private double sleepTime;
        private MiScript script;
        private IEnumerator<int> scriptEnumerator;

        public MiScriptState(MiScript script)
        {
            this.script = script;
            scriptEnumerator = script();
            sleepTime = scriptEnumerator.Current;
        }

        public bool IsComplete()
        {
            return script == null;
        }

        public void Execute(GameTime gameTime)
        {
            if (sleepTime > 0)
            {
                sleepTime--;
            }

            if (sleepTime <= 0)
            {
                bool unfinished = false;
                do
                {
                    unfinished = scriptEnumerator.MoveNext();
                    sleepTime = scriptEnumerator.Current;
                }
                while (sleepTime <= 0 && unfinished);

                if (!unfinished)
                {
                    script = null;
                }
            }
        }
    }
}
