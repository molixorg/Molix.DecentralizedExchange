namespace Common.Domain
{
    public class ConfigParam
    {
        public int OId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public bool Editable { get; set; }
        public bool Deleted { get; set; }
        public bool Edited { get; set; }
    }
}
