using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace MuhendisSozluk
{
    public class connectionStrings
    {
        //public static String webcfg_constr = ConfigurationManager.ConnectionStrings["webconfig_constring"].ConnectionString;
        public static String bedir2 = @"data source = DESKTOP-PIRF3HI\SQLEXPRESS; Database=SozlukDB; Integrated Security=True";
        public static String kodhamur = @"data source = sql.poseidon.domainhizmetleri.com; Database = kodhamur_sozluk; User id = kodhamur_sozluk; Password=q9Mc94x%;";
        public static String bedir = kodhamur;
    }
}