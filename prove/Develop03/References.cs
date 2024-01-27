class VerseReference
{
    private readonly string _book, _chapter, _verse;

    public VerseReference(string book, string chapter, string verse)
    {
        _book = book ?? throw new ArgumentNullException(nameof(book));
        _chapter = chapter ?? throw new ArgumentNullException(nameof(chapter));
        _verse = verse ?? throw new ArgumentNullException(nameof(verse));
    }

    public string Book => _book;
    public string Chapter => _chapter;
    public string Verse => _verse;

    public override string ToString()
    {
        return $"{_book} {_chapter}:{_verse}";
    }
}
