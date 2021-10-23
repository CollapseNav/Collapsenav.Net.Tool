namespace Collapsenav.Net.Tool.Data
{
    public class PageRequest
    {
        public virtual int Index { get; set; } = 1;
        public virtual int Max { get; set; } = 20;
        public virtual int Skip
        {
            get => skip ?? (Index - 1) * Max; set => skip = value;
        }
        protected int? skip;
    }
}
