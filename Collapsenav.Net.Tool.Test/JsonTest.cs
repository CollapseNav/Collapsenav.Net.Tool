using Xunit;
using System.Collections.Generic;
using System.Linq;

namespace Collapsenav.Net.Tool.Test
{
    public class UserInfo
    {
        public string UserName { get; set; }
        public int Age { get; set; }
    }
    public class JsonTest
    {
        [Fact]
        public void ToObjCollectionTest()
        {
            string userInfo = @"
            [
                {
                    ""userName"": ""ABCD"",
                    ""age"": 23
                }
            ]";
            IEnumerable<UserInfo> user = userInfo.ToObjCollection<UserInfo>();
            Assert.True(user.First().UserName == "ABCD" && user.First().Age == 23);
        }


        [Fact]
        public void ToObjTest()
        {
            string userInfo = @"
            {
                ""userName"": ""ABCD"",
                ""age"": 23
            }";
            var user = userInfo.ToObj<UserInfo>();
            Assert.True(user.UserName == "ABCD" && user.Age == 23);
        }

        [Fact]
        public void ToJsonTest()
        {
            UserInfo user = new()
            {
                UserName = "ABCD",
                Age = 23
            };
            var jsonString = user.ToJson().Trim();
            Assert.True("{\"userName\":\"ABCD\",\"age\":23}" == jsonString);
        }
    }
}

