using Quartz;

namespace Collapsenav.Net.Tool.Ext;

public class JobAndTrigger
{
    public IJobDetail Job { get; set; }
    public ITrigger Trigger { get; set; }
}