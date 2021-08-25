using System;
using Xunit;
using Collapsenav.Net.Tool;

namespace Collapsenav.Net.Tool.Test
{
    public class SecurityTest
    {
        [Fact]
        public void AesTest()
        {
            var msg = "123123123";
            var result = AESTool.Encrypt(msg);
            var tresult = AESTool.Decrypt(result);
            Assert.True(tresult == msg);
            // 再测一下扩展方法
            result = msg.AesEn();
            tresult = result.AesDe();
            Assert.True(tresult == msg);
        }
    }
}
