using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UBS_IT_DEV_RISK
{
    public class FileIO
    {
        private DateTime _dateRef;
        public DateTime DateRef { get { return _dateRef; } }

        private int _numberLines = 0;
        public int NumberLines { get { return _numberLines; } }

        private List<Trade> _tradeList = new List<Trade>();

        public void Read(string path)
        {

            string[] lines;

            try
            {
                lines = File.ReadAllLines(path);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao ler o arquivo.", ex);
            }

            try
            {
                if (lines.Length > 0)
                {
                    bool isDateValid = DateTime.TryParseExact(lines[0].Trim(), "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _dateRef);
                    if (!isDateValid)
                    {
                        throw new Exception(string.Format("Data de Referência Inválida. ({0})", lines[0]));
                    }
                }
                if (lines.Length > 1)
                {
                    bool isNumberValid = int.TryParse(lines[1].Trim(), out _numberLines);
                    if (!isNumberValid)
                    {
                        throw new Exception(string.Format("Número de linhas informado inválido. ({0})", lines[1]));
                    }
                    if (_numberLines != lines.Length - 2)
                    {
                        throw new Exception(string.Format("Número de registros informado ({0}) diferente da quantidade de registros no arquivo ({1}).", _numberLines, lines.Length - 2));
                    }
                }

                if (lines.Length > 2)
                {
                    for (int i = 2; i < lines.Length; i++)
                    {
                        try
                        {
                            _tradeList.Add(new Trade(lines[i], _dateRef));
                        }
                        catch (Exception ex)
                        {
                            // Não vou parar o processo de leitura, apenas exibir qual linha é inválida

                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(string.Format("Linha Inválida: #{0} - {1}", i, lines[i]));
                            Console.ForegroundColor = ConsoleColor.Gray;
                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Ocorreu um erro: {0}", ex.Message), ex);
            }
        }

        public void Save(string path)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(path, append: false))
                {
                    foreach (Trade trade in _tradeList)
                    {
                        sw.WriteLine(trade.Category);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Ocorreu um erro: {0}", ex.Message), ex);
            }
        }
    }
}
