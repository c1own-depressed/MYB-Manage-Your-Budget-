using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.BLLTests
{
    public class SettingsUnitTests
    {
        [Fact]
        public void TestTest1()
        {
            SettingsPageView settingsPageView = new SettingsPageView();
            Assert.IsNotNull(settingsPageView);
        }
        [Fact]
        public void TestTest2()
        {
            Assert.Equal(1, 1);
        }
        [Fact]
        public void TestTest3()
        {
            Assert.Equal(1, 1);
        }
    }
}
