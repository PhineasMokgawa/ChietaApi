using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CHIETAMIS.Organisations
{
    [Table("tbl_Organisation_Postal_Address")]
    public class OrganisationPostalAddress: Entity<int>
    {
        public int userId { get; set; }
        public bool sameasphysical { get; set; }
        public int organisationId { get; set; }
        public string addressline1 { get; set; }
        public string addressline2 { get; set; }
        public string suburb { get; set; }
        public string area { get; set; }
        public string district { get; set; }
        public string municipality { get; set; }
        public string province { get; set; }
        public string postcode { get; set; }
    }
}
