//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KojVenStatistic.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Disease
    {
        public Disease()
        {
            this.Appeal = new HashSet<Appeal>();
            this.Medicament = new HashSet<Medicament>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public int TypeDiseaseId { get; set; }
        public string Description { get; set; }
    
        public virtual ICollection<Appeal> Appeal { get; set; }
        public virtual TypeDisease TypeDisease { get; set; }
        public virtual ICollection<Medicament> Medicament { get; set; }
    }
}
