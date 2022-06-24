using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace GeoCoordinateParser
{
    public class CoordinatesConverter
    {

        /// <summary>
        /// Convert a coordinates string with any format to decimal coordinates
        /// </summary>
        public static Coordinates convert(string coordsString)
        {
            coordsString = Regex.Replace(coordsString, "\\s+", " ");

            string ddLatStr = "";
            string ddLngStr = "";
            string latdir = "";
            string lngdir = "";

            string[] matches = new string[] { };

            
            string verbatimLat = "";
            string verbatimLng = "";

            double ddLat;
            double ddLng;

            if (dd_re.IsMatch(coordsString))
            {
                Match MM = dd_re.Match(coordsString);
                matches = matchesToArray(MM);
                if (checkMatch(matches))
                {
                    ddLatStr = matches[2];
                    ddLngStr = matches[6];

                    //need to fix if there are ','s instead of '.'
                    if (ddLatStr.Contains(','))
                    {
                        ddLatStr.Replace(',', '.');
                    }

                    if (ddLngStr.Contains(','))
                    {
                        ddLngStr.Replace(',', '.');
                    }

                    //the directions
                    if (!String.IsNullOrEmpty(matches[1]))
                    {
                        latdir = matches[1];
                        lngdir = matches[5];
                    }
                    else if (!String.IsNullOrEmpty(matches[4]))
                    {
                        latdir = matches[4];
                        lngdir = matches[8];
                    }
                }
                else
                {
                    throw new Exception("invalid decimal coordinates format");
                }
            }
            else if (dms_periods.IsMatch(coordsString))
            {
                Match MM = dms_periods.Match(coordsString);
                matches = matchesToArray(MM);
                if (checkMatch(matches))
                {
                    //this is slightly different to the javascript version
                    string latDeg = matches[2];
                    double latMins = 0;
                    double latSecs = 0;

                    string lngDeg = matches[9];
                    double lngMins = 0;
                    double lngSecs = 0;

                    //get the mins parts
                    try
                    {
                        if (!String.IsNullOrEmpty(matches[4]))
                        {
                            latMins = double.Parse(matches[4].Replace(",", "."));
                        }

                        if (!String.IsNullOrEmpty(matches[6]))
                        {
                            latSecs = double.Parse(matches[6].Replace(",", "."));
                        }

                        if (!String.IsNullOrEmpty(matches[11]))
                        {
                            lngMins = double.Parse(matches[11].Replace(",", "."));
                        }

                        if (!String.IsNullOrEmpty(matches[13]))
                        {
                            lngSecs = double.Parse(matches[13].Replace(",", "."));
                        }
                    }
                    catch
                    {
                        throw new Exception("invalid DMS coordinates format");
                    }

                    //calculate
                    string latDec = (latMins / 60 + latSecs / 3600).ToString();
                    string lngDec = (lngMins / 60 + lngSecs / 3600).ToString();

                    //put it back together
                    ddLatStr = latDec.Replace("0.", latDeg + ".");
                    ddLngStr = lngDec.Replace("0.", lngDeg + ".");

                    //the directions
                    if (!String.IsNullOrEmpty(matches[1]))
                    {
                        latdir = matches[1];
                        lngdir = matches[8];
                    }
                    else if (!String.IsNullOrEmpty(matches[7]))
                    {
                        latdir = matches[7];
                        lngdir = matches[14];
                    }
                    
                }
                else
                {
                    throw new Exception("invalid DMS coordinates format");
                }
            }
            else if (dms_abbr.IsMatch(coordsString))
            {
                Match MM = dms_abbr.Match(coordsString);
                matches = matchesToArray(MM);
                if (checkMatch(matches))
                {
                    //this is slightly different to the javascript version
                    string latDeg = matches[2];
                    double latMins = 0;
                    double latSecs = 0;

                    string lngDeg = matches[10];
                    double lngMins = 0;
                    double lngSecs = 0;

                    //get the mins parts
                    try
                    {
                        if (!String.IsNullOrEmpty(matches[4]))
                        {
                            latMins = double.Parse(matches[4].Replace(",", "."));
                        }

                        if (!String.IsNullOrEmpty(matches[6]))
                        {
                            latSecs = double.Parse(matches[6].Replace(",", "."));
                        }

                        if (!String.IsNullOrEmpty(matches[12]))
                        {
                            lngMins = double.Parse(matches[12].Replace(",", "."));
                        }

                        if (!String.IsNullOrEmpty(matches[14]))
                        {
                            lngSecs = double.Parse(matches[14].Replace(",", "."));
                        }
                    }
                    catch
                    {
                        throw new Exception("invalid DMS coordinates format");
                    }

                    //calculate
                    string latDec = (latMins / 60 + latSecs / 3600).ToString();
                    string lngDec = (lngMins / 60 + lngSecs / 3600).ToString();

                    //put it back together
                    ddLatStr = latDec.Replace("0.", latDeg + ".");
                    ddLngStr = lngDec.Replace("0.", lngDeg + ".");

                    //the directions
                    if (!String.IsNullOrEmpty(matches[1]))
                    {
                        latdir = matches[1];
                        lngdir = matches[9];
                    }
                    else if (!String.IsNullOrEmpty(matches[8]))
                    {
                        latdir = matches[8];
                        lngdir = matches[16];
                    }
                }
                else
                {
                    throw new Exception("invalid DMS coordinates format");
                }
            }
            else if(coords_other.IsMatch(coordsString))
            {
                Match MM = coords_other.Match(coordsString);
                matches = matchesToArray(MM);
                if (checkMatch(matches))
                {
                    //this is slightly different to the javascript version
                    string latDeg = matches[2];
                    double latMins = 0;
                    double latSecs = 0;

                    string lngDeg = matches[10];
                    double lngMins = 0;
                    double lngSecs = 0;

                    //get the mins parts
                    try
                    {
                        if (!String.IsNullOrEmpty(matches[4]))
                        {
                            latMins = double.Parse(matches[4].Replace(",", "."));
                        }

                        if (!String.IsNullOrEmpty(matches[6]))
                        {
                            latSecs = double.Parse(matches[6].Replace(",", "."));
                        }

                        if (!String.IsNullOrEmpty(matches[12]))
                        {
                            lngMins = double.Parse(matches[12].Replace(",", "."));
                        }

                        if (!String.IsNullOrEmpty(matches[14]))
                        {
                            lngSecs = double.Parse(matches[14].Replace(",", "."));
                        }
                    }
                    catch
                    {
                        throw new Exception("invalid DMS coordinates format");
                    }

                    //calculate
                    string latDec = (latMins / 60 + latSecs / 3600).ToString();
                    string lngDec = (lngMins / 60 + lngSecs / 3600).ToString();

                    //put it back together
                    ddLatStr = latDec.Replace("0.", latDeg + ".");
                    ddLngStr = lngDec.Replace("0.", lngDeg + ".");

                    //the directions
                    if (!String.IsNullOrEmpty(matches[1]))
                    {
                        latdir = matches[1];
                        lngdir = matches[9];
                    }
                    else if (!String.IsNullOrEmpty(matches[8]))
                    {
                        latdir = matches[8];
                        lngdir = matches[16];
                    }
                }
                else
                {
                    throw new Exception("invalid DMS coordinates format");
                }
            }

            //double check longitude
            try
            {
                double val = double.Parse(ddLngStr.Replace(",", ".")); //we need the replace because this is verbatim and might have the comma
                if (val >= 180)
                {
                    throw new Exception("invalid longitude value");
                }
            }
            catch
            {
                throw new Exception("invalid longitude value");
            }

            //lets calculate now
           
            try
            {
                ddLat = Math.Round(double.Parse(ddLatStr.Replace(",", ".")), 6); //replace needed as this is verbatim and may include comma
                ddLng = Math.Round(double.Parse(ddLngStr.Replace(",", ".")), 6);
            }
            catch
            {
                throw new Exception("error in conversion");
            }

            //back to strings and trim the trailing zeros. Apparent the G29 does this
            ddLatStr = ddLat.ToString("G29");
            ddLngStr = ddLng.ToString("G29");

            //and back to numbers again
            ddLat = double.Parse(ddLatStr);
            ddLng = double.Parse(ddLngStr);


            //check the directions, we need both or none
            if((!String.IsNullOrEmpty(latdir) || !String.IsNullOrEmpty(lngdir)) && (String.IsNullOrEmpty(latdir) || String.IsNullOrEmpty(lngdir))) {
                throw new Exception("invalid coordinates format");
            }

            if(!String.IsNullOrEmpty(latdir) && latdir == lngdir)
            {
                throw new Exception("invalid coordinates format");
            }

            //make sure the signs and cardinal directions are correct
            if (Regex.IsMatch(latdir, "S|SOUTH", RegexOptions.IgnoreCase))
            {
                if (ddLat > 0)
                {
                    ddLat = -1 * ddLat;
                }
            }

            if(Regex.IsMatch(lngdir, "W|WEST", RegexOptions.IgnoreCase))
            {
                if (ddLng > 0)
                {
                    ddLng = -1 * ddLng;
                }
            }


            //lets get the verbatim parts
            string sepChars = "[,/\u0020]";

            string[] seps = matchesToArray(Regex.Match(coordsString, sepChars, RegexOptions.IgnoreCase));

            if (seps.Length == 0) //make it from matches
            {
                //we have to shift matches
                matches = matches.Skip(1).ToArray();
                int half = matches.Length / 2;
                for(int i = 0; i < half; i++)
                {
                    verbatimLat += matches[i];
                }
                for (int i = half; i < matches.Length; i++)
                {
                    verbatimLng += matches[i];
                }
            }
            else
            {
                int middle;
                if(seps.Length % 2 == 1) // odd numbers
                {
                    double ind = seps.Length / 2;
                    middle = (int)Math.Floor(ind); //we need to cast back to int
                }
                else
                {
                    middle = (seps.Length / 2) - 1;
                }

                int splitIndex = 0;

                //it might only be one
                if (middle == 0)
                {
                    splitIndex = coordsString.IndexOf(seps[0]);
                    verbatimLat = coordsString.Substring(0, splitIndex).Trim();
                    verbatimLng = coordsString.Substring(splitIndex + 1).Trim();
                }
                else //we have to walk through them to find the middle
                {
                    int currSepIndex = 0;
                    int startSearchIndex = 0;
                    while(currSepIndex <= middle)
                    {
                        splitIndex = coordsString.IndexOf(seps[currSepIndex], startSearchIndex);
                        startSearchIndex = splitIndex + 1;
                        currSepIndex++;
                    }

                    verbatimLat = coordsString.Substring(0, splitIndex).Trim();
                    verbatimLng = coordsString.Substring(splitIndex + 1).Trim();
                }
            }

            Coordinates coords = new Coordinates();
            coords.decimalLatitude = ddLat;
            coords.decimalLongitude = ddLng;
            coords.verbatimLatitude = verbatimLat;
            coords.verbatimLongitude = verbatimLng;
            coords.decimalCoordinates = $"{ddLat},{ddLng}";
            coords.verbatimCoordinates = coordsString;

            return coords;

        }

        private static string[] matchesToArray(Match matches)
        {
            List<string> matchesList = new List<string>();
            if (matches.Success)
            {
                foreach (Group g in matches.Groups) //remember that matches is iterable. This is by default pointing to the first (and should be only) match
                {
                    matchesList.Add(g.ToString());
                }

                return matchesList.ToArray();
            }
            else
            {
                return new string[] { };
            }
            
        }

        private static bool checkMatch(string[] matches)
        {
            //check the first one is not a number, it should be a string (it's the full match)
            double p;
            if (double.TryParse(matches[0], out p))
            {
                return false;
            }
            
            //remove blanks and shift
            string[] filteredMatch = matches.Where(x => !String.IsNullOrEmpty(x)).ToArray();
            filteredMatch = filteredMatch.Skip(1).ToArray(); //this is shift()

            //make sure it's balanced
            if (filteredMatch.Length % 2 != 0)
            {
                return false;
            }

            //make sure that if it's a number one side it's a number on the other
            int halflen = filteredMatch.Length / 2;
            Regex numRegex = new Regex(@"^[-+]?\d+([\.,]\d+)?$");
            Regex dirsRegex = new Regex(@"[eastsouthnorthwest]+", RegexOptions.IgnoreCase);

            for (int i = 0; i < halflen; i++)
            {
                string leftside = filteredMatch[i];
                string rightside = filteredMatch[i + halflen];
                bool bothNumberic = numRegex.IsMatch(leftside) && numRegex.IsMatch(rightside);
                bool bothDirs = dirsRegex.IsMatch(leftside) && dirsRegex.IsMatch(rightside);
                bool bothEqual = leftside == rightside;
                if(bothNumberic || bothDirs || bothEqual)
                {
                    continue;
                }
                else
                {
                    return false;
                }
                
            }

            return true;

        }
        
        
        private static Regex dd_re = new Regex(@"(NORTH|SOUTH|[NS])?\s*([+-]?[0-8]?[0-9](?:[\.,]\d{3,}))\s*([•º°]?)[\s]*(NORTH|SOUTH|[NS])?\s*[,/;]?[\s]*(EAST|WEST|[EW])?\s*([+-]?[0-1]?[0-9]?[0-9](?:[\.,]\d{3,}))\s*([•º°]?)\s*(EAST|WEST|[EW])?", RegexOptions.IgnoreCase);

        private static Regex dms_periods = new Regex(@"(NORTH|SOUTH|[NS])?\s*([+-]?[0-8]?[0-9])\s*(\.)\s*([0-5]?[0-9])\s*(\.)\s*((?:[0-5]?[0-9])(?:[\.,]{1}\d{1,3})?)?\s*(NORTH|SOUTH|[NS])?(?:\s*[,/;]\s*|\s*)(EAST|WEST|[EW])?\s*([+-]?[0-1]?[0-9]?[0-9])\s*(\.)\s*([0-5]?[0-9])\s*(\.)\s*((?:[0-5]?[0-9])(?:[\.,]{1}\d{1,3})?)?\s*(EAST|WEST|[EW])?", RegexOptions.IgnoreCase);

        private static Regex dms_abbr = new Regex(@"(NORTH|SOUTH|[NS])?\s*([+-]?[0-8]?[0-9])\s*(D(?:EG)?(?:REES)?)\s*([0-5]?[0-9])\s*(M(?:IN)?(?:UTES)?)\s*((?:[0-5]?[0-9])(?:[\.,]{1}\d{1,3})?)?(S(?:EC)?(?:ONDS)?)?\s*(NORTH|SOUTH|[NS])?(?:\s*[,/;]\s*|\s*)(EAST|WEST|[EW])?\s*([+-]?[0-1]?[0-9]?[0-9])\s*(D(?:EG)?(?:REES)?)\s*([0-5]?[0-9])\s*(M(?:IN)?(?:UTES)?)\s*((?:[0-5]?[0-9])(?:[\.,]{1}\d{1,3})?)?(S(?:EC)?(?:ONDS)?)\s*(EAST|WEST|[EW])?", RegexOptions.IgnoreCase);

        private static Regex coords_other = new Regex(@"(NORTH|SOUTH|[NS])?\s*([+-]?[0-8]?[0-9])\s*([•º°\.:]|D(?:EG)?(?:REES)?)?\s*,?([0-5]?[0-9](?:[\.,]\d{1,})?)?\s*(['′´’\.:]|M(?:IN)?(?:UTES)?)?\s*,?((?:[0-5]?[0-9])(?:[\.,]\d{1,3})?)?\s*(''|′′|’’|´´|[""″”\.])?\s*(NORTH|SOUTH|[NS])?(?:\s*[,/;]\s*|\s*)(EAST|WEST|[EW])?\s*([+-]?[0-1]?[0-9]?[0-9])\s*([•º°\.:]|D(?:EG)?(?:REES)?)?\s*,?([0-5]?[0-9](?:[\.,]\d{1,})?)?\s*(['′´’\.:]|M(?:IN)?(?:UTES)?)?\s*,?((?:[0-5]?[0-9])(?:[\.,]\d{1,3})?)?\s*(''|′′|´´|’’|[""″”\.])?\s*(EAST|WEST|[EW])?", RegexOptions.IgnoreCase); //just have to double up the " from the javascript version
    }
}
