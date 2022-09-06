using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    internal class DB
    {
        private string path = Directory.GetCurrentDirectory() + @"\DB.txt";
        private readonly int GAP_TO_KR = '가' - ' ';

        public DB(string path = "N/A")
        {
            if (path == "N/A")
            {
                this.path = Directory.GetCurrentDirectory() + @"\DB.txt";
            }
            else
            {
                this.path = path;
            }
        }

        public void Create_NewDB()
        {
            string[] defalut = new string[]
            {
                DataManager.Version,
                Encryption($"Last Play Rule: \r\n" +
                           $"10\r\n" +
                           $"10\r\n" +
                           $"10"),
                Encryption($"Rank: "),
                Encryption($"Play: "),
                Encryption("0")
            };

            File.WriteAllLines(path, defalut);
            
        }

        public void WriteLine(bool win_game, bool lose_game, int now_sec = 0)
        {
            try
            {
                if (File.Exists(path))
                {
                    string[] file_data = File.ReadAllLines(path);
                    for (int i = 0; i < file_data.Length; i++)
                    {
                        file_data[i] = Decryption(file_data[i]);
                    }

                    string header = DataManager.Version;

                    file_data[0] = header;

                    file_data[Find_TextInArray(file_data, "Last Play Rule: ")] = $"Last Play Rule: ";
                    file_data[Find_TextInArray(file_data, "Last Play Rule: ") + 1] = $"{DataManager.main.SizeX}";
                    file_data[Find_TextInArray(file_data, "Last Play Rule: ") + 2] = $"{DataManager.main.SizeY}";
                    file_data[Find_TextInArray(file_data, "Last Play Rule: ") + 3] = $"{DataManager.main.Bombs}";

                    int now_play = (int)(DateTime.Now - DataManager.main.GameStart).TotalSeconds;
                    file_data[Find_TextInArray(file_data, "Play: ") + 1]
                        = (int.Parse(file_data[Find_TextInArray(file_data, "Play: ") + 1]) + now_play).ToString();
                    DataManager.main.GameStart = DateTime.Now;


                    if (win_game)
                    {
                        file_data[Find_TextInArray(file_data, "Play: ")]
                            = $"W\r\n" +
                              $"{DateTime.Now.ToString("yy.MM.dd.HH:mm:ss")}\r\n" +
                              $"{DataManager.main.SizeX}\r\n" +
                              $"{DataManager.main.SizeY}\r\n" +
                              $"{DataManager.main.Bombs}\r\n" +
                              $"{now_sec}\r\n" +
                              $"Play: ";
                    }
                    else if (lose_game)
                    {
                        file_data[Find_TextInArray(file_data, "Play: ")]
                            = $"L\r\n" +
                              $"{DateTime.Now.ToString("yy.MM.dd.HH.mm.ss")}\r\n" +
                              $"{DataManager.main.SizeX}\r\n" +
                              $"{DataManager.main.SizeY}\r\n" +
                              $"{DataManager.main.Bombs}\r\n" +
                              $"{now_sec}\r\n" +
                              $"Play: ";
                    }

                    foreach (string line in file_data) Debug.WriteLine(line);

                    for (int i = 0; i < file_data.Length; i++)
                    {
                        file_data[i] = Encryption(file_data[i]);
                    }

                    File.WriteAllLines(path, file_data);
                }
                else
                {
                    Create_NewDB();
                }
            }
            catch
            {
                Debug.WriteLine("Create New DB");

                Create_NewDB();
            }
        }

        public void ReadLine()
        {
            try
            {
                if (File.Exists(path))
                {
                    string[] file_data = File.ReadAllLines(path);

                    for (int i = 0; i < file_data.Length; i++)
                    {
                        file_data[i] = Decryption(file_data[i]);
                    }

                    int last = Find_TextInArray(file_data, "Last Play Rule: ");
                    int width = int.Parse(file_data[last + 1]),
                        height = int.Parse(file_data[last + 2]),
                        bombs = int.Parse(file_data[last + 3]);

                    DataManager.main.SizeX = width;
                    DataManager.main.SizeY = height;
                    DataManager.main.Bombs = bombs;
                }
                else
                {
                    Create_NewDB();

                    DataManager.main.SizeX = 10;
                    DataManager.main.SizeY = 10;
                    DataManager.main.Bombs = 10;
                }
            }
            catch
            {
                Debug.WriteLine("Create New DB");

                Create_NewDB();
            }
        }

        public List<Rank> Create_RankList()
        {
            try
            {
                if (File.Exists(path))
                {
                    string[] file_data = File.ReadAllLines(path);

                    for (int i = 0; i < file_data.Length; i++)
                    {
                        file_data[i] = Decryption(file_data[i]);
                    }

                    int first = Find_TextInArray(file_data, "Rank: ");

                    List<Rank> ranks = new List<Rank>();

                    for (int i = 1; file_data[first + i] == "W" || file_data[first + i] == "L"; i += 6)
                    {
                        ranks.Add(new Rank(new string[6]
                        {
                            file_data[first + i + 0],
                            file_data[first + i + 1],
                            file_data[first + i + 2],
                            file_data[first + i + 3],
                            file_data[first + i + 4],
                            file_data[first + i + 5]
                        }));
                    }
                    
                    return ranks;
                }
                else
                {
                    Create_NewDB();
                }
            }
            catch
            {
                Debug.WriteLine("Create New DB");

                Create_NewDB();
            }

            return null;
        }

        public int GetPlayTime()
        {
            try
            {
                if (File.Exists(path))
                {
                    string[] file_data = File.ReadAllLines(path);

                    for (int i = 0; i < file_data.Length; i++)
                    {
                        file_data[i] = Decryption(file_data[i]);
                    }

                    return int.Parse(file_data[Find_TextInArray(file_data, "Play: ") + 1]);
                }
                else
                {
                    Create_NewDB();
                }
            }
            catch
            {
                Debug.WriteLine("Create New DB");

                Create_NewDB();
            }

            return 0;
        }

        private string Encryption(string s)
        {
            string result = "";

            foreach(char c in s)
            {
                result += Encryption(c);
            }

            return result;
        }

        private char Encryption(char c)
        {
            if (c == '\n' || c == '\r') return c;
            else return (char)(c + GAP_TO_KR);
        }

        private string Decryption(string s)
        {
            string result = "";

            foreach (char c in s)
            {
                result += Decryption(c);
            }

            return result;
        }

        private char Decryption(char c)
        {
            if (c == '\n' || c == '\r') return c;
            else return (char)(c - GAP_TO_KR);
        }

        private int Find_TextInArray(string[] strs, string find)
        {
            for (int i = 0; i < strs.Length; i++)
            {
                if (strs[i].Contains(find))
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
