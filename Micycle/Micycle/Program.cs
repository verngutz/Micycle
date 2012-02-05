using System;

namespace Micycle
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Micycle game = new Micycle())
            {
                game.Run();
            }
        }
    }
#endif
}

