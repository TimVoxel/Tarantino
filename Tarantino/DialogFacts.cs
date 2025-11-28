using Tarantino;

public class DialogFacts
{
    public static IEnumerable<DialogNodeKind> ResponseDialogKinds
    {
        get
        {
            foreach (var kind in Enum.GetValues<DialogNodeKind>())
            {
                if (kind != DialogNodeKind.Dialog)
                {
                    yield return kind;
                }
            }
        }
    }
}
