using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace YellowstonePathology.Business.User
{
    [XmlType("SystemUserCollection")]
    public class SystemUserCollection : ObservableCollection<SystemUser>
    {        
        private SystemUserCollection()
        {            

        }

		public SystemUser GetSystemUserById(int userId)
		{
			SystemUser user = (from su in this
										  where su.UserId == userId
										  select su).Single<SystemUser>();
			return user;
		}

        public SystemUser GetSystemUserByUserName(string userName)
        {
            SystemUser result = null;
            try
            {
                result = (from su in this
                      where su.UserName.ToUpper() == userName.ToUpper()
                      select su).Single<SystemUser>();                
            }
            catch
            {
                string message = string.Format("The UserName {0} cannot be found in the local SystemUser table.", userName);
                YellowstonePathology.Business.Logging.EmailExceptionHandler.HandleException(message);
            }            
            return result;                        
        }

        public SystemUserCollection GetPathologistUsers()
        {
            SystemUserCollection users = new SystemUserCollection();
            var query = from su in this where su.Active == true && su.IsUserInRole(SystemUserRoleDescriptionEnum.Pathologist) == true select su;
            foreach (SystemUser user in query)
            {
                users.Add(user);
            }
            return users;
        }

		public SystemUserCollection GetUsersByRole(SystemUserRoleDescriptionEnum systemUserRoleDescriptionEnum, bool isActive)
		{
			SystemUserCollection users = new SystemUserCollection();
			var query = from su in this where su.Active == isActive && su.IsUserInRole(systemUserRoleDescriptionEnum) == true || su.UserId == 0 select su;
			foreach (SystemUser user in query)
			{
				users.Add(user);
			}
			return users;
		}

		public void AddAllToUserList(SystemUserCollection users, bool asFirstItem)
		{
			SystemUser allItem = new SystemUser();
			allItem.DisplayName = "All";
			allItem.UserId = 0;

			if (asFirstItem)
			{
				users.Insert(0, allItem);
			}
			else
			{
				users.Add(allItem);
			}
		}

		public void AddUnassignedToUserList(SystemUserCollection users, bool asFirstItem)
		{
			SystemUser allItem = new SystemUser();
			allItem.DisplayName = "Unassigned";
			allItem.UserId = 0;

			if (asFirstItem)
			{
				users.Insert(0, allItem);
			}
			else
			{
				users.Add(allItem);
			}
		}
	}
}
