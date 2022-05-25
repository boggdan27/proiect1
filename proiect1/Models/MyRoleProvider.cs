using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace proiect1.Models
{
    public class MyRoleProvider: RoleProvider
    {

         public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }
        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }
        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }
        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }
        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }
      
        public override string[] GetRolesForUser(string CNP)
        {
            using (var context = new ProiectEHEntities1())
            {
                var objuser = context.Medics.Where(x => x.CNP == CNP).FirstOrDefault();
                if(objuser == null)
                {
                    //este pacient
                    var pacient = context.Pacients.Where(x => x.CNP == CNP).FirstOrDefault();
                    string[] rez = new string[] { pacient.Role };
                    return rez;


                }
                else
                {
                    string[] rez = new string[] { objuser.Role };
                    return rez;

                }

            }

                throw new NotImplementedException();
        }
        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }
        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }
        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }
        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}