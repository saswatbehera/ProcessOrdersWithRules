using System.Collections.Generic;

namespace ProcessOrdersWithRules.Models
{
    public class Executables
    {
        public bool IsPacking { get; set; }
        public List<Product> Products { get; set; }
        public bool IsPackingSlipRequired { get; set; }
        public List<string> PackingSlips { get; set; }
        public bool IsPhysicalProduct { get; set; }
        public int AgentCommission { get; set; }
        public bool IsMembership { get; set; }
        public List<string> MembershipType { get; set; }
    }
}