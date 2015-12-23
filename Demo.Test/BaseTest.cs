using System;
using NUnit.Framework;

namespace Demo.Test
{
    public abstract class BaseTest
    {
        [SetUp]
        public abstract void SetUp();

        [TearDown]
        public abstract void TearDown();
    }
}
