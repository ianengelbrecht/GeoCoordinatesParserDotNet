using System;

namespace GeoCoordinateParser
{
    public class Coordinates
    {
        public Coordinates()
        {

        }

        /// <summary>
        /// Test if an already converted coordinate string is equivalent to the results of this conversion. Good to identifying errors in datasets with already existing decimal coordinates.
        /// </summary>
        /// <param name="coordsToTest">A string with decimalLatitude and decimalLongitude separated by a comma</param>
        /// <returns>True if the tested coordinates are close enough (to six decimal places)</returns>
        public bool closeEnough(string coordsToTest)
        {
            if (coordsToTest.Contains(","))
            {
                string[] coords = coordsToTest.Split(',');
                try
                {
                    double.Parse(coords[0]);
                    double.Parse(coords[1]);
                }
                catch
                {
                    throw new Exception("coords are not valid decimals");
                }

                return decimalsCloseEnough(this.decimalLatitude, double.Parse(coords[0])) && decimalsCloseEnough(this.decimalLongitude, double.Parse(coords[1])); //this here will be the converted coordinates object

                                    
            }
            else
            {
                throw new Exception("coords being tested must be separated by a comma");
            }
        }

        private bool decimalsCloseEnough(double dec1, double dec2)
        {
            double diff = Math.Abs(dec1 - dec2);
            return diff < 0.0000019;
        }

        public string verbatimCoordinates { get; set; }
        public string verbatimLatitude { get; set; }
        public string verbatimLongitude { get; set; }
        public double decimalLatitude { get; set; }
        public double decimalLongitude { get; set; }
        public string decimalCoordinates { get; set; }
    }
}
