namespace Shared
{
    public interface IMyMessage
    {
        string Text { get; set; }
    }

    public class MyMessage : IMyMessage
    {
        public string Text { get; set; }
    }
}
