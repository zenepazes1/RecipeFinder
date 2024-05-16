using RecipeFinder.Core.Models;
using RecipeFinder.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeFinder.DataAccess.Mapping
{
    public static class UserMappingService
    {
        public static ApplicationUser MapToApplicationUser(ApplicationUserEntity userEntity)
        {
            return new ApplicationUser
            {
                Id = userEntity.Id,
                Email = userEntity.Email,
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName
            };
        }
    }
}
