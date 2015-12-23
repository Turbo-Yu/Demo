using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.Interface;
using Demo.Service;
using NUnit.Framework;

namespace Demo.Test
{
    [TestFixture]
    public class CommonServiceTest : BaseTest
    {
        private ICommonService _instance;
        public override void SetUp()
        {
            _instance = new CommonService();
        }

        public override void TearDown()
        {
            _instance = null;
        }

        [Test(Description = "测试")]
        public void Test()
        {
            var a = _instance.Test();
        }
    }
}
