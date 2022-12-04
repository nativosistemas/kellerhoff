using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace Kellerhoff.Codigo.clases.Generales
{
    public class Validaciones
    {
        public static bool isMail(string pMail)
        {
           return DKbase.web.generales.Validaciones_base.isMail(pMail);
        }
        public static System.Boolean IsNumeric(System.Object Expression)
        {
            return DKbase.web.generales.Validaciones_base.IsNumeric(Expression);
        }
    }
}