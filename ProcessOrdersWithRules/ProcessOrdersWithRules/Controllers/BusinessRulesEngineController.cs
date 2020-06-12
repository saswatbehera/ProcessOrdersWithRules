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

        private void ApplyTheMembershipAndSendEmail(List<string> membershipType)
        {
            throw new NotImplementedException();
        }

        private void GenerateCommissionPayment(int agentCommission, int orderId)
        {
            throw new NotImplementedException();
        }

        private void SendToPackingDepartment(List<Product> products, int orderId)
        {
            throw new NotImplementedException();
        }

        private void GeneratePackingSlips(List<string> packingSlips, int orderId)
        {
            throw new NotImplementedException();
        }

        private Executables ApplyBusinessRules(Product p)
        {
            throw new NotImplementedException();
        }
    }
}
