using System;
using System.Collections.Generic;
//using System.Data;

using System.Linq;

using System.Web;

using FreneValue.Models;

using System.Linq.Dynamic;

using System.Web.Caching;

namespace FreneValue.Infrastructure
{
    public static class Utilitaires 
    {
       private static arbredb db = new arbredb();     

       public static List<string> LireCodeValeurCache(string w_coddom)
        {
            List<string> valeurList = null;
            var cacheKey = "ValeurListKey";

           // if (HttpContext.Cache[cacheKey] == null)
            if  (HttpContext.Current.Cache[cacheKey] == null)
            {
                valeurList = db.valeurs
                           .Where(r => r.COD_DOM == w_coddom)
                           .OrderBy(r => r.VAL)
                           .Select(r => r.VAL).Distinct().ToList();

                HttpContext.Current.Cache.Insert(cacheKey,
                                                       valeurList,
                                                       null,
                                                       Cache.NoAbsoluteExpiration,
                                                       TimeSpan.FromMinutes(10)
                                                       );
            }
            else
            {
                valeurList = (List<string>)HttpContext.Current.Cache[cacheKey];
            }
            return (valeurList);
        }

    }
}