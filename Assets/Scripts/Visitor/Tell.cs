public interface Tell
{
    // Метод обработки посещения
    int Accept(Visitor v, int value);
}
