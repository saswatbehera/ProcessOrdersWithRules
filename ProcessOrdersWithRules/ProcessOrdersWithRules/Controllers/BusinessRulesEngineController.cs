using ProcessOrdersWithRules.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace ProcessOrdersWithRules.Controllers
{
    public class BusinessRulesEngineController : ApiController
    {
        public string ProcessOrder(int orderId, bool isExecute)
        {
            Order o = new Order();

            Product p = new Product();

            Executables exq = new Executables();
            MapExecutables(p, orderId, isExecute, ref exq);
            return "Order Processed Sucessfully";
        }


        /// <summary>
        /// This is the method targeted by Unit Tests.
        /// It contains the BusinessLogic
        /// </summary>
        /// <param name="p"></param>
        /// <param name="orderId"></param>
        /// <param name="isExecute"></param>
        /// <param name="exq"></param>
        public void MapExecutables(Product p, int orderId, bool isExecute, ref Executables exq)
        {
            exq = ApplyBusinessRules(p);

            if (isExecute)
            {
                GoExecute(exq, orderId);
            }
        }


        /// <summary>
        /// This is the Final Execution Method.
        /// This methods directs all the departments to execute a processing
        /// </summary>
        /// <param name="exq"></param>
        /// <param name="orderId"></param>
        private void GoExecute(Executables exq, int orderId)
        {
            if (exq.IsPackingSlipRequired)
            {
                GeneratePackingSlips(exq.PackingSlips, orderId);
            }

            if (exq.IsPhysicalProduct && exq.Products.Count > 0)
            {
                SendToPackingDepartment(exq.Products, orderId);
            }

            if (exq.IsPhysicalProduct && exq.AgentCommission > 0)
            {
                GenerateCommissionPayment(exq.AgentCommission, orderId);
            }

            if (exq.IsMembership)
            {
                ApplyTheMembershipAndSendEmail(exq.MembershipType);
            }
        }


        /// <summary>
        /// As per Requirement, this method will Activate or Upgrade Membership
        /// The Send Email logic will be written here
        /// Skipping the Implementation intentionaly
        /// </summary>
        /// <param name="membershipType"></param>
        private void ApplyTheMembershipAndSendEmail(List<string> membershipType)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// As per Requirement, this method will Generate a Commission Payment
        /// Commission Payment is for the Physical Products
        /// Skipping the Implementation intentionaly
        /// </summary>
        /// <param name="agentCommission"></param>
        /// <param name="orderId"></param>
        private void GenerateCommissionPayment(int agentCommission, int orderId)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// As per Requirement, this method will Send Product List to Packing and Shipping Department
        /// Product List may contain multiple items, as Free First Aid Video is given with Learning to Ski Video
        /// Skipping the Implementation intentionaly
        /// </summary>
        /// <param name="products"></param>
        /// <param name="orderId"></param>
        private void SendToPackingDepartment(List<Product> products, int orderId)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// As per Requirement, this method will Generate Packing Slips
        /// Packing Slips can be of 2 types in case of Books, CustomerShipping or RoyaltyDepartment
        /// Skipping the Implementation intentionaly 
        /// </summary>
        /// <param name="packingSlips"></param>
        /// <param name="orderId"></param>
        private void GeneratePackingSlips(List<string> packingSlips, int orderId)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// The Main Business Rules will be Applied here
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private Executables ApplyBusinessRules(Product p)
        {
            throw new NotImplementedException();
        }
    }
}
