public interface Visitor
{
    // Посетить аудео повествование
    int VisitAudioTell(int value);

    // Посетить текстовое повествование
    int VisitTextTell(int value);
}
