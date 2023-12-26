using LogFilter.entities;

namespace LogFilter.Services
{
    static public class LogDetector
    {
        public static Dictionary<string, List<Auth>> UserAttempts = new Dictionary<string, List<Auth>>();
        public static void AnalyzeUserAccountAttacks(List<Auth> logData, int maxAttempts, int timeWindowInSeconds)
        {
            // Sortowanie danych po czasie
            logData.Sort((a, b) => a.Time.CompareTo(b.Time));

            var timeSlotSize = 5; // Szerokość szeregu czasowego w sekundach
            var currentTimeSlot = 1; // Początkowy numer szeregu czasowego

            var currentSlotStartTime = logData[0].Time;
            var currentSlotEndTime = currentSlotStartTime + timeSlotSize;

            foreach (var logEntry in logData)
            {
                var currentTime = logEntry.Time;

                if (currentTime >= currentSlotStartTime && currentTime <= currentSlotEndTime)
                {
                    if (!UserAttempts.ContainsKey(logEntry.SourceUserDomain))
                    {
                        UserAttempts[logEntry.SourceUserDomain] = new List<Auth>();
                    }

                    UserAttempts[logEntry.SourceUserDomain].Add(logEntry);
                }
                else
                {
                    foreach (var userDomain in UserAttempts.Keys)
                    {
                        var attempts = UserAttempts[userDomain];

                        if (attempts.Count > maxAttempts)
                        {
                            Console.WriteLine($"Potencjalny atak użytkownika {userDomain} w oknie czasowym {currentSlotStartTime}-{currentSlotEndTime} sekundy. Liczba prób: {attempts.Count}");
                        }
                    }

                    // Przejdź do kolejnego szeregu czasowego
                    currentSlotStartTime += 1;
                    currentSlotEndTime += 1;
                    UserAttempts.Clear();

                    // Przesuń się do następnego szeregu czasowego
                    while (currentTime > currentSlotEndTime)
                    {
                        currentSlotStartTime += 1;
                        currentSlotEndTime += 1;
                    }

                    if (!UserAttempts.ContainsKey(logEntry.SourceUserDomain))
                    {
                        UserAttempts[logEntry.SourceUserDomain] = new List<Auth>();
                    }

                    UserAttempts[logEntry.SourceUserDomain].Add(logEntry);
                }
            }
        }
    }
}

