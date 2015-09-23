using System.Collections.Generic;
using System.Linq;
using Eisk.Models;

namespace Eisk.DataAccess
{
    public static class UserDataAccess
    {
        private static List<UserInfo> _userInMemoryDataSource;

        private static List<UserInfo> UserInMemoryDataSource
        {
            get
            {
                if (_userInMemoryDataSource == null)
                {
                    _userInMemoryDataSource = new List<UserInfo>();

                    //add members
                    for (var i = 0; i < 5; i++)
                        _userInMemoryDataSource.Add(
                            new UserInfo
                            {
                                UserName = "member" + (i + 1) + "@member.com",
                                Password = "any"
                            });

                    //add administrators
                    for (var i = 0; i < 5; i++)
                        _userInMemoryDataSource.Add(
                            new UserInfo
                            {
                                UserRole = UserRole.Administrator.ToString(),
                                UserName = "admin" + (i + 1) + "@admin.com",
                                Password = "any"
                            });
                }

                return _userInMemoryDataSource;
            }
        }

        public static IEnumerable<UserInfo> GetAll()
        {
            return UserInMemoryDataSource.ToList();
        }

        public static UserInfo GetByUserName(string userName)
        {
            return UserInMemoryDataSource.FirstOrDefault(u => u.UserName == userName);
        }

        public static UserInfo Validate(string userName, string password)
        {
            return UserInMemoryDataSource.FirstOrDefault(u => u.UserName == userName && u.Password == password);
        }
    }
}