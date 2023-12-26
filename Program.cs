using LogFilter.Services;

//1. Analiza statystyczna logów na jednej porcji danych:
// 1.1 Ile średnio eventów dzieje się w jakichś ramach czasowych
// 1.2 Ile średnio eventów jednego uzytkownika dzieje się w jakichś ramach czasowych

//2. Pobranie i zmapowanie danych
int dataSetIndex = 1;
foreach (var batch in FileService.DownloadDataInBatches("data/auth-00.txt", 100000))
{
    // 4. Przeprowadzamy analize logów
    Console.WriteLine($"Downloaded {dataSetIndex}");
    dataSetIndex++;
    LogDetector.AnalyzeUserAccountAttacks(batch,10,5);
}