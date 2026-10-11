class Vampire
{
    public string Name { get; private set; }
    public string Question { get; private set; }

    private int correctAnswer;

    public Vampire(string name, string question, int correctAnswer)
    {
        Name = name;
        Question = question;
        this.correctAnswer = correctAnswer;
    }

    public bool AskQuestion(int guess)
    {
        if (guess == correctAnswer)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}