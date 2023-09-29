using System.Linq;

namespace _00Project1_Protmusis_
{
    public class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string,int> usersDictionary = new Dictionary<string,int>();
            string currentUser = "";

            logIn(ref currentUser, ref usersDictionary);
            Thread.Sleep(1000);
            Console.Clear();

            string functionSelection = "";
            string back = "";


            while (functionSelection != "6")
            {
                Console.Clear();
                showMenu(currentUser);
                functionSelection = Console.ReadLine();

                if (functionSelection == "1")
                {
                    Console.Clear();
                    logIn(ref currentUser, ref usersDictionary);
                    Thread.Sleep(1000);
                    Console.Clear();

                }
                else if (functionSelection == "2")
                {
                    Console.WriteLine("Jus sekmingai atsijungete nuo paskyros!");
                    Thread.Sleep(1000);
                    Console.Clear();
                    logIn(ref currentUser, ref usersDictionary);
                }
                else if (functionSelection == "3")
                {
                    Console.Clear();
                    showGameRules();
                    backToMenu(back);
                    
                }
                else if (functionSelection == "4")
                {
                    Console.Clear();
                    Console.WriteLine("ZAIDIMO REZULTATU IR DALYVIU PERZIURA");
                    Console.WriteLine("MENIU:");
                    Console.WriteLine("1 - ZAIDIMO DALYVIU PERZIURA");
                    Console.WriteLine("2 - ZAIDIMO REZULTATU PERZIURA");
                    Console.Write("Pasirinkite funkcija: ");
                    string function4Selection = Console.ReadLine();

                    if(function4Selection == "1")
                    {
                        Console.Clear();
                        showUsers(usersDictionary);
                        backToMenu(back);
                    }
                    else if (function4Selection == "2")
                    {
                        Console.Clear();
                        showGameResult(usersDictionary);
                        backToMenu(back);
                    }
                }
                else if (functionSelection == "5")
                {
                    //////////////////////////Start game///////////////////////////////////
                    List<string> answeredQuestions = new List<string>();

                    for (int i = 0; i < 10; i++)
                    {
                        Dictionary<string, string> questionsDictionary = createQuestionsDictionary();
                        Dictionary<string, List<string>> optionsDictionary = createOptionsDictionary();
                        Dictionary<string, string> answersDictionary = createAnswersDictionary();
                        Dictionary<string, int> pointsDictionary = createPointsDictionary();

                        Console.Clear();
                        showQuestionsGenre();
                        string genreSelection = "";
                        string genreTopic = "";
                        string genreLetter = "";

                        Console.Write("Pasirinkite klausimo kategorija: ");

                        genreSelection = Console.ReadLine();
                        if (genreSelection == "q")
                        {
                            break;
                        }

                        genreLetter = numberToGenre(genreSelection);


                        int questionCount = questionsDictionary.Keys.Count(key => key.StartsWith(genreLetter));
                        int answeredQuestionsCount = answeredQuestions.Count(key => key.StartsWith(genreLetter));
                        if (answeredQuestionsCount == questionCount)
                        {
                            Console.WriteLine("Jusu pasirinkta kategorija nebeturi klausimu, prasome pasirinkti kita kategorija:");
                            genreSelection = Console.ReadLine();
                            if (genreSelection == "q")
                            {
                                break;
                            }
                            genreLetter = numberToGenre(genreSelection);
                        }

                        Random rand = new Random();
                        
                        genreTopic = genreLetter + rand.Next(0, questionCount);

                        while (answeredQuestions.Contains(genreTopic))
                        {
                            genreTopic = genreLetter + rand.Next(0, questionCount);
                        }
                        answeredQuestions.Add(genreTopic);

                        string question = questionsDictionary[genreTopic];

                        List<string> options = optionsDictionary[genreTopic];
                        string answer = answersDictionary[genreTopic];
                        int questionPoint = pointsDictionary[genreTopic];

                        showQuestion(question);

                        showOptions(options);

                        string guess = Console.ReadLine().ToUpper();

                        int guessNumber = letterToNumber(guess);
                        Console.Clear();

                        showQuestion(question);
                        bool correctAnswer = showAnswer(options, guessNumber, answer);

                        int pointForAnswer = calculatePoints(questionPoint, correctAnswer);
                        usersDictionary[currentUser] += pointForAnswer;

                        Console.ReadLine();
                    }

                }

            }

        }
        public static void showMenu(string currentUser)
        {
            Console.WriteLine($"------------------                          Prisijunges vartotojas: {currentUser}");
            Console.WriteLine("Meniu:");
            Console.WriteLine("1 - PRISIJUNGIMAS");
            Console.WriteLine("2 - ATSIJUNGIMAS");
            Console.WriteLine("3 - ZAIDIMO TAISYKLIU ATVAIZDAVIMAS");
            Console.WriteLine("4 - ZAIDIMO REZULTATU IR DALYVIU PERZIURA");
            Console.WriteLine("5 - DALYVAVIMAS");
            Console.WriteLine("6 - ISEJIMAS IS ZAIDIMO");
            Console.WriteLine();
            Console.Write("Pasirinkite funkcija: ");
        }
        public static void logIn(ref string currentUser, ref Dictionary<string,int> usersDictionary)
        {
            Console.WriteLine("Sveiki, prasome prisijungti prie protmusio programos");
            Console.WriteLine();
            Console.Write("Iveskite vartotojo varda: ");
            string firstName = Console.ReadLine();
            Console.Write("Iveskite vartotojo pavarde: ");
            string lastName = Console.ReadLine();

            string userName = firstName + " " + lastName;
            
            if (!usersDictionary.ContainsKey(userName))
            {
                usersDictionary.Add(userName, 0);
                Console.WriteLine("Jusu registracija sekminga, sveikiname prisijungus");
            }
            else
            {
                Console.WriteLine($"{firstName}, sveiki sugrize");
            }
            currentUser = userName;
        }
        public static void backToMenu(string back)
        {
            Console.WriteLine();
            Console.WriteLine("BACK-q");
            back = Console.ReadLine();
            if (back != "q")
            {
                back = Console.ReadLine();
            }
        }
        public static void showGameRules()
        {
            Console.WriteLine("Sveikiname prsijungus prie protmusio programos");
            Console.WriteLine("TAISYKLES:");
            Console.WriteLine("1. Klasuimai yra suskirstyti i 4 kategorijas");
            Console.WriteLine("2. Pries kiekviena klasuima turesite pasirinkti sekancio klausimo kategorija");
            Console.WriteLine("3. Is viso reikes atsakyti i 10 klausimu, kurie pateikiami atsitiktiniu budu");
            Console.WriteLine("4. Kiekvienas klausimas vertas nuo 1 iki 3 tasku, priklausomai nuo klausimo sudetingumo");
            Console.WriteLine("5. Atsakymui i klausima turesite pasirinkti is 4 pateiktu variantu");
            Console.WriteLine("6. Atsakius teisingai taskai uz atsakytus klausimus sumuosis ir gale zaidimo bus pateikti rezultatu lenteleje");
        }
        public static void showGameResult(Dictionary<string, int> usersDictionary)
        {
            foreach (var user in usersDictionary)
            {
                Console.WriteLine("DALYVIU REZULTATAI:");
                Console.WriteLine($"{user.Key} | {user.Value}");
                //Reikia surusiuoti.........................
            }

            usersDictionary.OrderBy(x => x.Value);

            foreach (var user in usersDictionary)
            {
                Console.WriteLine("DALYVIU REZULTATAI:");
                Console.WriteLine($"{user.Key} | {user.Value}");
                //Reikia surusiuoti.........................
            }

        }
        public static void showUsers(Dictionary<string,int> usersDictionary)
        {
            foreach (var user in usersDictionary)
            {
                Console.WriteLine(user.Key);
            }
            //galbut prideti kada buvo paskutini karta prisijunges ir kiek kartu prisijunges
        }

        public static void showQuestionsGenre()
        {
            Console.WriteLine("KATEGORIJOS:");
            Console.WriteLine("1 - FILMAI               2 - MUZIKA");
            Console.WriteLine("3 - MOKSLAS              4 - SPORTAS");

        }
        public static string numberToLetter(int number)
        {
            string letter = "";
            switch (number)
            {
                case 0:
                    letter = "A";
                    break;
                case 1: 
                    letter = "B";
                    break;
                case 2:
                    letter = "C";
                    break;
                case 3:
                    letter = "D";
                    break;
            }
            return letter;
        }
        public static int letterToNumber(string letter)
        {
            int number = 0;
            switch (letter)
            {
                case "A":
                    number = 0;
                    break;
                case "B":
                    number = 1;
                    break;
                case "C":
                    number = 2;
                    break;
                case "D":
                    number = 3;
                    break;
            }
            return number;
        }
        public static string numberToGenre(string genreSelection)
        {
            string genreLetter = "";
            switch (genreSelection)
            {
                case "1":
                    genreLetter = "F";
                    break;
                case "2":
                    genreLetter = "M";
                    break;
                case "3":
                    genreLetter = "S";
                    break;
                case "4":
                    genreLetter = "G";
                    break;
            }
            return genreLetter;
        }

        public static void showQuestion(string question)
        {
            Console.Clear();
            Console.WriteLine(question);
            Console.WriteLine();

        }
        public static void showOptions(List<string> options)
        {
            Console.WriteLine("ATSAKYMU VARIANTAI:");
            for (int j = 0; j < options.Count; j++)
            {
                string letter = numberToLetter(j);
                Console.WriteLine($"{letter} - {options[j]}");
            }
        }
        public static bool showAnswer(List<string> options,int guessNumber, string answer)
        {
            Dictionary<string, int> pointsDictionary = createPointsDictionary();

            bool correctAnswer = false;

            Console.WriteLine("ATSAKYMU VARIANTAI:");
            for (int j = 0; j < options.Count; j++)
            {
                if (j==guessNumber && options[guessNumber] == answer)
                {
                    string letter = numberToLetter(j);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{letter} - {options[j]}");
                    correctAnswer = true;
                }
                else if (j == guessNumber && options[guessNumber] != answer)
                {
                    string letter = numberToLetter(j);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{letter} - {options[j]}");
                }
                else
                {
                    string letter = numberToLetter(j);
                    Console.WriteLine($"{letter} - {options[j]}");
                }
                Console.ResetColor();

            }
            return correctAnswer;
        }
        public static int calculatePoints(int points, bool correctAnswer)
        {
            if (correctAnswer)
            {
                return points;
            }
            else
            {
                return 0;
            }
        }


        //F - movies (Filmai)
        //M - music (Muzika)
        //S - science (Mokslas)
        //G - sport (Sportas)

        public static Dictionary<string, string> createQuestionsDictionary()
        {
            Dictionary<string, string> questions = new Dictionary<string, string>()
            {
                {"F1","Kuriais metais krikštatėvis buvo išleistas pirmą kartą?" },
                {"F2","Kuris aktorius pelnė geriausią aktoriaus Oskarą už filmus „Filadelfija“ (1993) ir „Forrest Gump“ (1994)?" },
                {"F3","Amerikiečių aktorė, vaidinusi Tokijo požemio boso O-Ren Ishii vaidmenį filme „KillBill“" },
                {"F4","Kas vaidino Juno MacGuffą priešais Michaelą Cera 2007 m. Filme „Juno“?" },
                {"F5","Kuris britų aktorius vaidino Lee Christmas vaidmenį kartu su Sylvesteriu Stallone filme „The Expendables“?" },

                {"M1","Kokiais metais „The Beatles“ pirmą kartą išvyko į JAV?" },
                {"M2","Kurios alternatyvios roko grupės 1991 m. Hitas buvo „prarasti savo religiją“?" },
                {"M3","Koks yra grupės, kurioje yra šie nariai: Johnas Diakonas, Brianas May, Freddie Mercury, Rogeris Tayloras, vardas?" },
                {"M4","Kokia 1960-ųjų amerikiečių pop grupė sukūrė „bangos garsą“?" },
                {"M5","Kuris dainininkas, be kita ko, buvo žinomas kaip „Popo karalius“ ir „Pirštinė“?" },

                {"S1","Kiek širdžių turi aštuonkojis?" },
                {"S2","Kokia vidutinė Veneros paviršiaus temperatūra?" },
                {"S3","Kiek gramų druskos (natrio chlorido) yra litre tipiško jūros vandens?" },
                {"S4","Kiek metų užtruks iš Žemės paleidžiamas erdvėlaivis, kad pasiektų Plutono planetą?" },
                {"S5","Jei nukristumėte beorė, be trinties skylė, einanti visą kelią per Žemę, kiek laiko reikėtų kristi į kitą pusę?" },

                {"G1","Kokiomis sporto šakomis pasižymėjo Neilas Adamsas?" },
                {"G2","Kokį sporto žaidimą 1891 metais išrado Jamesas Naismithas?" },
                {"G3","Kiek žaidėjų yra olimpinėje garbanojimo komandoje?" },
                {"G4","Kur vyko Sandraugos žaidynės 1930 m." },
                {"G5","Kiek žaidėjų yra „Water Polo“ komandoje?" },
            };
            return questions;

        }
        public static Dictionary<string, string> createAnswersDictionary()
        {


            Dictionary<string, string> dictionary = new Dictionary<string, string>()
            {
                {"F1", "1972"},
                {"F2", "Tom Hanks"},
                {"F3", "Lucy Liu"},
                {"F4", "Ellen Page"},
                {"F5", "Jason Statham"},

                {"M1", "1964"},
                {"M2", "REM"},
                {"M3", "Queen"},
                {"M4", "Beach Boys"},
                {"M5", "Michael Jackson"},

                {"S1", "3"},
                {"S2", "460°C"},
                {"S3", "0"},
                {"S4", "9.5 metu"},
                {"S5", "42 minučių"},

                {"G1", "Dziudo"},
                {"G2", "Krepšinis"},
                {"G3", "4"},
                {"G4", "Hamiltonas, Kanada"},
                {"G5", "7"},
            };
            return dictionary;
        }

        public static Dictionary<string, List<string>> createOptionsDictionary()
        {
            Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>()
            {
                {"F1", new List<string>() { "1964", "1972", "1976", "1988" } },
                {"F2", new List<string>() { "Garry Oldman", "Leonardo Dicaprio", "Tom Hanks", "Brad Pit" }},
                {"F3", new List<string>() { "Lucy Liu", "Ana Peltrow", "Natali Kimen", "Kristi White" }},
                {"F4", new List<string>() { "Guillermina Valdés", "Graciela Borges", "Ellen Page", "Santiago Salomón" }},
                {"F5", new List<string>() { "Jason Statham", "Dolph Lundgren", "50 Cent", "Audrius Bruzas" }},

                {"M1", new List<string>() { "1964", "1968", "1975", "1981" }},
                {"M2", new List<string>() { "The Beatles", "Eagle", "FleetWood Mac", "REM" }},
                {"M3", new List<string>() { "Pink Floyd", "Led Zepelin", "Queen", "The Beatles" }},
                {"M4", new List<string>() { "Beach Boys", "Zentai", "Dream Street", "Black Eyed Peas" }},
                {"M5", new List<string>() { "Gitanas Nauseda", "Michael Jackson", "Justin Timberlake", "Zilvinas Zvagulis" }},

                {"S1", new List<string>() { "1", "2", "3", "4" }},
                {"S2", new List<string>() { "140°C", "320°C", "460°C", "680°C" }},
                {"S3", new List<string>() { "0", "10", "20", "30" }},
                {"S4", new List<string>() { "6 metus", "7.5 metu", "8 metus", "9.5 metu" }},
                {"S5", new List<string>() { "14 minučių", "25 minučių", "42 minučių", "56 minučių" }},

                {"G1", new List<string>() { "Dziudo", "Tenisas", "Boksas", "Futbolas" }},
                {"G2", new List<string>() { "Tenisas", "Krepšinis", "Golfas", "Futbolas" }},
                {"G3", new List<string>() { "2", "4", "6", "8" }},
                {"G4", new List<string>() { "Hamiltonas", "Sidnejus", "Pekinas", "Akronas" }},
                {"G5", new List<string>() { "3", "5", "7", "9" }},
            };

            return dictionary;

        }
        public static Dictionary<string, int> createPointsDictionary()
        {


            Dictionary<string, int> dictionary = new Dictionary<string, int>()
            {
                {"F1", 2},
                {"F2", 1},
                {"F3", 3},
                {"F4", 3},
                {"F5", 1},

                {"M1", 3},
                {"M2", 2},
                {"M3", 1},
                {"M4", 1},
                {"M5", 1},

                {"S1", 2},
                {"S2", 2},
                {"S3", 2},
                {"S4", 3},
                {"S5", 3},

                {"G1", 3},
                {"G2", 2},
                {"G3", 3},
                {"G4", 3},
                {"G5", 2},
            };
            return dictionary;
        }

    }
}