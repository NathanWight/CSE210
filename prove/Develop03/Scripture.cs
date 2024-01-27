class ScriptureContent
{
    private readonly string _text;
    private readonly VerseReference _reference;

    public ScriptureContent(VerseReference reference, string text)
    {
        _reference = reference ?? throw new ArgumentNullException(nameof(reference));
        _text = text ?? throw new ArgumentNullException(nameof(text));
    }

    public VerseReference Reference => _reference;

    public string Text => _text;

    public override string ToString()
    {
        return $"{_text}";
    }
}
