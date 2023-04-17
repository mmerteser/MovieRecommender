using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRecommender.DataAccess.Context
{
    public static class MssqlColumnTypes
    {
        public const string Nvarchar = "nvarchar";
        public const string Binary = "varbinary";
        public const string Boolean = "bit";
        public const string SmallDateTime = "smalldatetime";
        public const string DateTime = "datetime";
        public const string Money = "money";
        public const string Int = "int";
        public const string Tinyint = "tinyint";
        public const string Double = "float";
        public const string DefaultLangCode = "TR";
    }
}
