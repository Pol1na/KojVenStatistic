using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KojVenStatistic.Entity
{
    public partial class User
    {
        public string FullName => $"{LastName} {FirstName}";
    }
}
