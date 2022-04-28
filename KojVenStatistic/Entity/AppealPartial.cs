using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KojVenStatistic.Entity
{
    public partial class Appeal 
    {
        public string RequestDateText => DateOfRequest.ToLongDateString();
        public string RequestTimeText => DateOfRequest.ToLongTimeString();
        public bool HasRecipe => Recipe != null;
        public string CreateRecipeText => HasRecipe ? "Редактирование рецепта" : "Выписывание рецепта";

        public string AppealDateText
        {
            get
            {
                var currentDate = DateTime.Now.Date;
                if (currentDate == DateOfRequest.Date) return "Сегодня";
                else if (currentDate == DateOfRequest.Date.AddDays(-1)) return "Завтра";
                else if (currentDate == DateOfRequest.Date.AddDays(-2)) return "Послезавтра";
                else if (currentDate == DateOfRequest.Date.AddDays(1)) return "Вчера";
                else if (currentDate == DateOfRequest.Date.AddDays(2)) return "Позавчера";
                else return RequestDateText;
            }
        }

        public bool IsActive => !DateOfFinish.HasValue;

        public string DiseaseText => Disease?.Name ?? "Не поставлен";

    }
}
