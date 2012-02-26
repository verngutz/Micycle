using System.Collections.Generic;

namespace MiUtil
{
    public delegate IEnumerator<int> MiScript();
    public delegate IEnumerator<int> MiScript(float src_x, float src_y, float dest_x, float dest_y);
}
