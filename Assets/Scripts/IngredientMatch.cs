using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GranCook
{
    public class IngredientMatch
    {
        public int Index { get; set; }
        public int IngredientType { get; set; }

        public IngredientMatch(int pIndex, int pIngredientType)
        {
            Index = pIndex;
            IngredientType = pIngredientType;
        }
    }
}
