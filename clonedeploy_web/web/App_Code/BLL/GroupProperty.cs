﻿using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;

namespace BLL
{
    public static class GroupProperty
    {
        public static bool AddGroupProperty(Models.GroupProperty groupProperty)
        {
            using (var uow = new DAL.UnitOfWork())
            {
              
                    uow.GroupPropertyRepository.Insert(groupProperty);
                    return uow.Save();
              
            }
        }

        public static Models.GroupProperty GetGroupProperty(int groupId)
        {
            using (var uow = new DAL.UnitOfWork())
            {
                return uow.GroupPropertyRepository.GetFirstOrDefault(x => x.GroupId == groupId);
            }
        }

      

        public static bool UpdateGroupProperty(Models.GroupProperty groupProperty)
        {
            using (var uow = new DAL.UnitOfWork())
            {
               
                    uow.GroupPropertyRepository.Update(groupProperty, groupProperty.Id);
                   return uow.Save();
               
            }
        }

        public static bool UpdateComputerProperties(Models.GroupProperty groupProperty)
        {
            foreach (var computer in BLL.Group.GetGroupMembers(groupProperty.GroupId))
            {
                if (Convert.ToBoolean(groupProperty.ImageEnabled))
                    computer.ImageId = groupProperty.ImageId;
            }
        }
       

      
    }
}