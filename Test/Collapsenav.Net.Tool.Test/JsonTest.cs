using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Collapsenav.Net.Tool.Test;
public class UserInfo
{
    public string UserName { get; set; }
    public int Age { get; set; }
}
public class UserInfoTwo
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
        user = userInfo.ToObj(typeof(UserInfo)) as UserInfo;
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
    [Fact]
    public void JsonMapTest()
    {
        UserInfo user = new()
        {
            UserName = "ABCD",
            Age = 23
        };
        var user2 = user.JsonMap<UserInfoTwo>();
        Assert.True(user.UserName == user2.UserName && user.Age == user2.Age);

        List<UserInfo> users = new()
        {
            new UserInfo { UserName = "1", Age = 1 },
            new UserInfo { UserName = "2", Age = 2 },
            new UserInfo { UserName = "3", Age = 3 },
            new UserInfo { UserName = "4", Age = 4 },
        };
        var users2 = users.JsonMap<IEnumerable<UserInfoTwo>>();
        Assert.True(users.First().UserName == users2.First().UserName && users.First().Age == users2.First().Age);
    }

    [Fact]
    public void JsonMapNoObjectTest()
    {
        UserInfo user = new()
        {
            UserName = "ABCD",
            Age = 23
        };
        var user2 = user.JsonMap<UserInfo, UserInfoTwo>();
        Assert.True(user.UserName == user2.UserName && user.Age == user2.Age);

        List<UserInfo> users = new()
        {
            new UserInfo { UserName = "1", Age = 1 },
            new UserInfo { UserName = "2", Age = 2 },
            new UserInfo { UserName = "3", Age = 3 },
            new UserInfo { UserName = "4", Age = 4 },
        };
        var users2 = users.JsonMap<IEnumerable<UserInfo>, IEnumerable<UserInfoTwo>>();
        Assert.True(users.First().UserName == users2.First().UserName && users.First().Age == users2.First().Age);
    }

    [Fact]
    public void JsonMapFromObjToObjTest()
    {
        var obj1 = new TestJsonObj1 { Age = 1, UserName = "1", Id = 1 };
        var obj2 = new TestJsonObj2 { Age = 2, UserName = "2" };
        var temp = obj1.JsonMap(obj2);
        Assert.True(temp.Age == 1);
        Assert.True(temp.UserName == "1");
        var temp2 = obj2.JsonMap(obj1);
        Assert.True(temp2.Age == 2);
        Assert.True(temp2.UserName == "2");
        Assert.True(temp2.Id == 1);
    }

    [Fact]
    public void JsonNodeToObjTest()
    {
        var obj1 = new TestJsonObj1 { Age = 1, UserName = "1", Id = 1 };
        var objStr = obj1.ToJson();
        var sd = objStr.ToJsonNode().ToObj<TestJsonObj1>();
        Assert.True(sd.Age == 1);
        Assert.True(sd.UserName == "1");
        Assert.True(sd.Id == 1);
    }

    public class TestJsonObj1
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int Age { get; set; }
    }

    public class TestJsonObj2
    {
        public string UserName { get; set; }
        public int Age { get; set; }
    }
}

