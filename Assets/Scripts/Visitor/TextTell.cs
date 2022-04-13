public class TextTell : Tell
{
    public int Accept(Visitor v, int hallID)
    {
        return v.VisitTextTell(hallID);
    }
}
