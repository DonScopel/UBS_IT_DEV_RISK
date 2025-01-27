using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using static UBS_IT_DEV_RISK.Enums;

namespace UBS_IT_DEV_RISK
{
    public class Trade : ITrade
    {
        private const int _lateDays = 30;

        // Propriedades
        private double _value;
        public double Value
        { get { return _value; } }

        private string _clientSector;
        public string ClientSector
        { get { return _clientSector; } }

        private DateTime _nextPaymentDate;
        public DateTime NextPaymentDate
        { get { return _nextPaymentDate; } }

        private Enums.eCategory _category;
        public Enums.eCategory Category
        { get { return _category; } }

        public Trade(string line, DateTime refDate)
        {
            if (isValid(line))
            {
                string[] values = line.Trim().Split(' ');
                try
                {
                    _value = double.Parse(values[0].Trim());
                    _clientSector = values[1].Trim();
                    _nextPaymentDate = DateTime.ParseExact(values[2].Trim(), "MM/dd/yyyy", CultureInfo.InvariantCulture);

                    rank(refDate);
                }
                catch (Exception ex)
                {
                    throw new Exception("Linha Inválida.", ex);
                }
            }
            else
            {
                throw new Exception("Linha Inválida.");
            }
        }

        private bool isValid(string line)
        {
            //return true;
            string pattern = @"^\d+\s+(Private|Public)\s+(0[1-9]|1[0-2])/(0[1-9]|[12][0-9]|3[01])/\d{4}$";
            return Regex.IsMatch(line.Trim(), pattern);
        }

        private void rank(DateTime refDate)
        {
            //Atualmente há três categorias, as quais estão em ordem de precedência:
            //1.EXPIRED: Operações cuja próxima data de pagamento está atrasada há mais de 30 dias, baseado na  data de referência que será disponibilizada.  
            //2.HIGHRISK: Operações com valor superior a 1,000,000, com cliente do setor Privado.  
            //3.MEDIUMRISK: Operações com valor superior a 1,000,000, com cliente do setor Público. 

            Enums.eCategory cat = eCategory.NONE;
            if (_nextPaymentDate.AddDays(_lateDays) < refDate)
            {
                cat = eCategory.EXPIRED;
            }
            if (_clientSector.ToUpper().Trim() == eSector.PRIVATE.ToString() && _value > 1000000)
            {
                cat = eCategory.HIGHRISK;
            }
            if (_clientSector.ToUpper().Trim() == eSector.PUBLIC.ToString() && _value > 1000000)
            {
                cat = eCategory.MEDIUMRISK;
            }
            _category = cat;
        }
    }
}
