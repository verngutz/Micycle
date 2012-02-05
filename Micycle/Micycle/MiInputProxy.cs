using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace Micycle
{
    class MiInputProxy : GameComponent
    {
        private Queue<MiCommand> commands;

        public MiInputProxy(Micycle game)
            : base(game)
        {
            commands = new Queue<MiCommand>();
        }

        public MiCommand GetNextCommand()
        {
            return commands.Dequeue();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
