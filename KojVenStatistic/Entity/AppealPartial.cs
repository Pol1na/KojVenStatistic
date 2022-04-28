using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KojVenStatistic.Entity
{
    public partial class Appeal 
    {
        public string RequestDateText => DateOfRequest.ToLongDateString();
        public string RequestTimeText => DateOfRequest.ToLongTimeString();
        public bool HasRecipe => Recipe != null;
        public string CreateRecipeText => HasRecipe ? "Редактирование рецепта" : "Выписывание рецепта";
    }
}
