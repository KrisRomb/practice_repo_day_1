//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Агентство_Недвижимости
{
    using System;
    using System.Collections.Generic;
    
    public partial class Land_Demands
    {
        public int ID { get; set; }
        public string Address_City { get; set; }
        public string Address_Street { get; set; }
        public string Address_House { get; set; }
        public string Address_Number { get; set; }
        public Nullable<decimal> Min_Price { get; set; }
        public Nullable<decimal> Max_Price { get; set; }
        public int ID_Agent { get; set; }
        public int ID_Client { get; set; }
        public Nullable<double> Min_Area { get; set; }
        public Nullable<double> Max_Area { get; set; }
    
        public virtual Agents Agents { get; set; }
        public virtual Clients Clients { get; set; }
    }
}
