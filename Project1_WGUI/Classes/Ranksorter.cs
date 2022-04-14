using System.Collections.Generic;

namespace Project1_WGUI.Classes
{
    public class Ranksorter : IComparer<Ranking>
    {
        public int Compare(Ranking x, Ranking y)
        {
            if (x.pop > y.pop)
                return 1;
            else
                return -1;
        }
    }
}
