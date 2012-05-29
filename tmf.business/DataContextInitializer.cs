using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tmf.business;
using tmf.entities;

namespace tmf.business
{
    public class DataContextInitializer : CreateDatabaseIfNotExists<tmfwebContext>
    {
        protected override void Seed(tmfwebContext context)
        {
            //MembershipCreateStatus Status;
            //Membership.CreateUser("Demo", "123456", "demo@demo.com", null, null, true, out Status);
            //Roles.CreateRole("Admin");
            //Roles.AddUserToRole("Demo", "Admin");

            //// Campus

            //var campuses = new List<Campus>
            //                 {
            //                     new Campus
            //                         {
            //                             Ville = "Toulouse"
            //                         },
            //                     new Campus
            //                         {
            //                             Ville = "Paris"
            //                         },
            //                     new Campus
            //                         {
            //                             Ville = "Test",
            //                             Age = 7
            //                         }
            //                 };

            //foreach (var campus in campuses)
            //{
            //    context.Campus.Add(campus);
            //}

            //context.SaveChanges();

            base.Seed(context);
        }
    }
}