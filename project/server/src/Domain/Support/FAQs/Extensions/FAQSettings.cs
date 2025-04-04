namespace server.src.Domain.Support.FAQs.Extensions;

public class FAQSettings
{
    public static int TitleLength { get; } = 50;
    public static int QuestionLength { get; } = 1024;
    public static int AnswerLength { get; } = 2048;
    public static int ViewCountDefaultValue { get; } = 0;
}