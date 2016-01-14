using System;
using Demo.Datas.Interface;
using Demo.Datas.Manager;
using Demo.Interface;
using Demo.Models;

namespace Demo.Service
{
    public class CommonService : ICommonService
    {
        public string Test()
        {
            return "test";
        }

        public bool InsertPerson(DbPerson person)
        {
            
            try
            {
                IRespository<DbPerson> instance = ManagerFactory.CreateInstance<DbPerson>();
                instance.Insert(person);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
