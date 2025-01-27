using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UBS_IT_DEV_RISK
{
    public class Enums
    {
        public enum eCategory
        {
            NONE = 0,
            EXPIRED = 1,
            HIGHRISK = 2,
            MEDIUMRISK = 4
        }

        public enum eSector
        {
            NONE = 0,
            PRIVATE = 1,
            PUBLIC = 2
        }
    }
}
