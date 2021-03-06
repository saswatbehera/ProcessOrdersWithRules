﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;

namespace ProcessOrdersWithRules.Models
{
    public class BusinessLogic
    {
        #region Variables
        public static string LearningtoSki = "Learning to Ski";
        public static string FirstAid = "First Aid";
        public static string CustomerShipping = "CustomerShipping";
        public static string RoyaltyDepartment = "RoyaltyDepartment";
        #endregion Variables

        #region Public Methods

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
        /// This needs to be fetched from somekind of Repository
        /// Here, Just mocked the data
        /// </summary>
        /// <returns>List<Product></returns>
        public Products GetProducts()
        {
            string json = "{\"products\":[" +
                "{\"ProductId\":1,\"ProductName\":\"Meluha\",\"IsPhysicalProduct\":true,\"IsMembershipProduct\":false,\"IsVideoProduct\":false,\"IsBookProduct\":true,\"ProductPrice\":700,\"ProductMembershipType\":\"\"}," +
                "{\"ProductId\":2,\"ProductName\":\"Sherlock Holmes\",\"IsPhysicalProduct\":true,\"IsMembershipProduct\":false,\"IsVideoProduct\":false,\"IsBookProduct\":true,\"ProductPrice\":500,\"ProductMembershipType\":\"\"}," +
                "{\"ProductId\":3,\"ProductName\":\"Nagas\",\"IsPhysicalProduct\":true,\"IsMembershipProduct\":false,\"IsVideoProduct\":false,\"IsBookProduct\":true,\"ProductPrice\":900,\"ProductMembershipType\":\"\"}," +
                "{\"ProductId\":4,\"ProductName\":\"Activate Membership\",\"IsPhysicalProduct\":false,\"IsMembershipProduct\":true,\"IsVideoProduct\":false,\"IsBookProduct\":false,\"ProductPrice\":500,\"ProductMembershipType\":\"Activate\"}," +
                "{\"ProductId\":5,\"ProductName\":\"Upgrade Membership\",\"IsPhysicalProduct\":false,\"IsMembershipProduct\":true,\"IsVideoProduct\":false,\"IsBookProduct\":false,\"ProductPrice\":100,\"ProductMembershipType\":\"Upgrade\"}," +
                "{\"ProductId\":6,\"ProductName\":\"Learning to Ski\",\"IsPhysicalProduct\":true,\"IsMembershipProduct\":false,\"IsVideoProduct\":true,\"IsBookProduct\":false,\"ProductPrice\":200,\"ProductMembershipType\":\"\"}," +
                "{\"ProductId\":7,\"ProductName\":\"First Aid\",\"IsPhysicalProduct\":true,\"IsMembershipProduct\":false,\"IsVideoProduct\":true,\"IsBookProduct\":false,\"ProductPrice\":50,\"ProductMembershipType\":\"\"}," +
                "{\"ProductId\":8,\"ProductName\":\"Sholay\",\"IsPhysicalProduct\":true,\"IsMembershipProduct\":false,\"IsVideoProduct\":true,\"IsBookProduct\":false,\"ProductPrice\":100,\"ProductMembershipType\":\"\"}]}";

            MemoryStream ms = new MemoryStream(System.Text.ASCIIEncoding.ASCII.GetBytes(json));
            ms.Position = 0;

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Products));
            Products products = (Products)ser.ReadObject(ms);
            return products;
        }

        /// <summary>
        /// This needs to be fetched from somekind of Repository
        /// Here, Just mocked the data
        /// </summary>
        /// <returns>List<Order></returns>
        public Orders GetOrders()
        {
            string json = "{\"orders\":[" +
                "{\"OrderId\":1,\"OrderNumber\":\"ABC001\",\"CustomerName\":\"Steve\",\"CustomerDetails\":\"Address And Mobile\",\"ProductId\":1,\"Quantity\":2}," +
                "{\"OrderId\":2,\"OrderNumber\":\"XYZ123\",\"CustomerName\":\"Darren\",\"CustomerDetails\":\"Address And Mobile\",\"ProductId\":4,\"Quantity\":1}," +
                "{\"OrderId\":3,\"OrderNumber\":\"ABC456\",\"CustomerName\":\"James\",\"CustomerDetails\":\"Address And Mobile\",\"ProductId\":6,\"Quantity\":1}," +
                "{\"OrderId\":4,\"OrderNumber\":\"PQR789\",\"CustomerName\":\"Mark\",\"CustomerDetails\":\"Address And Mobile\",\"ProductId\":8,\"Quantity\":1}]}";

            MemoryStream ms = new MemoryStream(System.Text.ASCIIEncoding.ASCII.GetBytes(json));
            ms.Position = 0;

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Orders));
            Orders orders = (Orders)ser.ReadObject(ms);
            return orders;
        }

        #endregion Public Methods

        #region Private Methods

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
        /// The Main Business Rules will be Applied here
        /// Rule 1: If Physical Product, Customer Packing Slips Generated
        /// Rule 2: If Physical Product, Agent Commission Payment need to created
        /// Rule 3: If Membership Product, Membership either need to be Activated or Upgraded
        /// Rule 4: If Membership Product send a mail to customer also
        /// Rule 5: If "Learning to Ski" Video, then "First Aid" video is free with it
        /// Rule 6: If Book then additional Royalty Department Packing slip need to be generated.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private Executables ApplyBusinessRules(Product p)
        {
            Executables exq = new Executables();
            if (p.IsPhysicalProduct)
            {
                exq.IsPacking = true;
                exq.IsPhysicalProduct = true;
                exq.IsPackingSlipRequired = true;
                exq.AgentCommission = (int)(p.ProductPrice / 100) * 5;
            }

            if (p.IsMembershipProduct)
            {
                exq.IsMembership = true;
                exq.MembershipType.Add(p.ProductMembershipType);
            }

            List<Product> products = new List<Product>();
            products.Add(p);
            if (p.IsVideoProduct && p.ProductName == LearningtoSki)
            {
                Product p1 = new Product();
                p1 = GetProducts().products.FirstOrDefault(x => x.ProductName == FirstAid);
                products.Add(p1);
            }
            exq.Products = products;

            List<string> slips = new List<string>();
            slips.Add(PackingSlip.CustomerShipping.ToString());
            if (p.IsBookProduct)
            {
                slips.Add(PackingSlip.RoyaltyDepartment.ToString());
            }
            exq.PackingSlips = slips;

            return exq;
        }

        #endregion Private Methods

        #region Skipped Methods

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
        /// Product List may contain multiple items, as Free "First Aid" Video is given with "Learning to Ski" Video
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

        #endregion Skipped Methods
    }
}