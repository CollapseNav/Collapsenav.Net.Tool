using Xunit;

namespace Collapsenav.Net.Tool.Test;
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

        [Fact]
        public void HashTest()
        {
            var msg = "123123123";
            var msg2 = "233333";
            Assert.False(msg.Md5En() == msg2.Md5En());
            Assert.False(msg.Sha1En() == msg2.Sha1En());
            Assert.False(msg.Sha256En() == msg2.Sha256En());
        }
}
