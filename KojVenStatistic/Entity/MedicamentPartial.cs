using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KojVenStatistic.Entity
{
    public partial class Medicament
    {
        public Visibility BtnVisibility => AppData.AuthUser.PostId == 1 ? Visibility.Visible : Visibility.Collapsed;

    }
}
