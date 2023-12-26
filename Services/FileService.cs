using LogFilter.entities;

namespace LogFilter.Services
{
    public class FileService
    {
        public static IEnumerable<List<Auth>> DownloadDataInBatches(string path, int batchSize = 10000)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                List<string> lines = new List<string>();
                string line;
                int count = 0;

                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                    count++;

                    if (count % batchSize == 0)
                    {
                        List<Auth?> authList = lines.Select(line =>
                        {
                            var parts = line.Split(',');
                            if (parts.Length == 3)
                            {
                                return new Auth
                                {
                                    Time = int.Parse(parts[0]),
                                    SourceUserDomain = parts[1],
                                    DestinationUserDomain = parts[2],
                                };
                            }
                            else
                            {
                                return null;
                            }
                        })
                        .Where(auth => auth != null)
                        .ToList();

                        yield return authList;
                        lines.Clear();
                    }
                }

                // Sprawdź, czy są jakiekolwiek pozostałe wiersze
                if (lines.Any())
                {
                    List<Auth?> authList = lines.Select(line =>
                    {
                        var parts = line.Split(',');
                        if (parts.Length == 3)
                        {
                            return new Auth
                            {
                                Time = int.Parse(parts[0]),
                                SourceUserDomain = parts[1],
                                DestinationUserDomain = parts[2],
                            };
                        }
                        else
                        {
                            return null;
                        }
                    })
                    .Where(auth => auth != null)
                    .ToList();

                    yield return authList;
                }
            }
        }
        public static List<Auth?>? DownloadDataFromFile(string path, int limit = 50000){
            List<string> lines = File.ReadLines(path).Take(limit).ToList();
            if(lines.Any()){
                List<Auth?> authList = lines.Select(line =>
                {
                    var parts = line.Split(',');
                    if (parts.Length == 9)
                    {
                        return new Auth
                        {
                            Time = int.Parse(parts[0]),
                            SourceUserDomain = parts[1],
                            DestinationUserDomain = parts[2],
                        };
                    }
                    else
                    {
                        // Jeśli linia nie zawiera 8 pól, zwróć null (lub obsłuż błąd)
                        return null;
                    }
                })
                .Where(auth => auth != null) // Usuń null, jeśli wystąpił błąd
                .ToList();
                return authList;
            }
            return null;
        }
    }
}