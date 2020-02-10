﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Ductulator.Model;

namespace Ductulator.Core
{
    public static class OverallSizes
    {

        public static List<string> elmSize(Element elm)
        {
            List<string> result = new List<string>();
            string elmShape = CurrentDuctShape.elmShape(elm);
            Connector elmConn = CurrentElmConn.elmConn(elm);

            if (elmShape == "Round")
            {
                var temresult = elmConn.Radius * 2;
                result.Add(temresult.ToString());
            }
            else
            {      
                double b_Side = elmConn.Height * factorConvertion(elm);
                result.Add(Math.Round(b_Side, 2).ToString());
                double a_Side = elmConn.Width * factorConvertion(elm);
                result.Add(Math.Round(a_Side, 2).ToString());
                result.Add(DuctDiamEquiv.Diam_equiv(a_Side, b_Side).ToString());
            }

            return result;
        }

        private static double factorConvertion(Element elm)
        {
            double result = 0;
            string NameUnits = null;
            string abrev = null;

            ModelUnits.unitsName(elm, ref NameUnits, ref result, ref abrev);

            return result;
        }

    }
}