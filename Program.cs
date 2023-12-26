using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LogFilter.entities;
using System;
using LogFilter.Services;



//1. Pobranie i zmapowanie danych

//2. Analiza statystyczna logów:
// 2.1 Ile średnio eventów dzieje się w jakichś ramach czasowych
// 2.2 Ile średnio eventów jednego uzytkownika dzieje się w jakichś ramach czasowych


// 3. Na podstawie analizy określamy róne TimeWindow per konkretny atak

// 4. Przeprowadzamy analize logów
string filePath = "data/auth.txt";
var authList = FileService.DownloadDataFromFile(filePath);

LogDetector.AnalyzeUserAccountAttacks(authList,3,5);

