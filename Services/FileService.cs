using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogFilter.entities;

namespace LogFilter.Services
{
    public class FileService
    {
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
                            SourceComputer = parts[3],
                            DestinationComputer = parts[4],
                            AuthType = parts[5],
                            LogonType = parts[6],
                            AuthenticationOrientation = parts[7],
                            AuthStatus = parts[8]
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