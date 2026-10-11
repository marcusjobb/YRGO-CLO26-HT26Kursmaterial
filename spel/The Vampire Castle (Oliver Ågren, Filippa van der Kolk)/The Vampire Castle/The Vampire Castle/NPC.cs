class NPC
{
    public string Name { get; private set; }
    public List<string> Dialog { get; private set; }

    private int currentLine;

    public NPC(string name, List<string> dialog)
    {
        Name = name;
        Dialog = dialog;
        currentLine = 0;
    }

    public string NextLine()
    {
        string line = Dialog[currentLine];

        currentLine++;

        if (currentLine >= Dialog.Count)
        {
            currentLine = 0;
        }

        return line;
    }
}