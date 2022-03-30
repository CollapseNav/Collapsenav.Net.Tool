using Snowflake.Core;
using Yitter.IdGenerator;

namespace Collapsenav.Net.Tool;
/// <summary>
/// 雪花算法
/// </summary>
public class SnowFlake
{
    /// <summary>
    /// 静态初始化
    /// </summary>
    static SnowFlake()
    {
        InitSnowFlake();
        InitClassicSnowFlake();
    }
    /// <summary>
    /// 初始化雪花算法(雪花漂移算法-更短)
    /// </summary>
    public static void InitSnowFlake(ushort workId = 1, byte bitlen = 6)
    {
        var options = new IdGeneratorOptions(workId)
        {
            SeqBitLength = bitlen
        };
        YitIdHelper.SetIdGenerator(options);
    }
    /// <summary>
    /// 获取Id(雪花漂移算法-更短)
    /// </summary>
    public static long NextId()
    {
        return YitIdHelper.NextId();
    }

    public static IdWorker Worker;
    /// <summary>
    /// 初始化雪花算法(经典雪花算法-更长)
    /// </summary>
    public static void InitClassicSnowFlake(long workId = 1, long datacenterId = 1)
    {
        Worker = new IdWorker(workId, datacenterId);
    }
    /// <summary>
    /// 获取Id(经典雪花算法-更长)
    /// </summary>
    public static long ClassicNextId()
    {
        return Worker.NextId();
    }
}