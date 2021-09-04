using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GranCook.Interfaces
{
    public interface IGrandma
    {
        string Name { get; }
        IngredientTypes Wildcard { get; }
    }
}
