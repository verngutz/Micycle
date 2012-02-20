using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace MiUtil
{
    public class MiScriptEngine : MiComponent
    {
        private readonly List<MiScriptState> activeScripts = new List<MiScriptState>();
        public MiScriptEngine(MiGame game) : base(game) { }

        public void ExecuteScript(MiScript script)
        {
            MiScriptState scriptState = new MiScriptState(script);
            if (!scriptState.IsComplete())
            {
                activeScripts.Add(scriptState);
            }
        }

        public override void Update(GameTime gameTime)
        {
            for(int i = 0; i < activeScripts.Count; i++)
            {
                activeScripts[i].Execute(gameTime);
            }

            activeScripts.RemoveAll(s => s.IsComplete());
        }
    }
}
