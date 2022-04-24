using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KojVenStatistic.Entity
{
    public partial class Client
    {
        public string FullName => $"{LastName} {FirstName}";

        public string DateOfBirthText => DateOfBirth.ToLongDateString();

        public Appeal LastAppeal => Appeal.OrderBy(i=>i.DateOfRequest).Last();
    }
}
