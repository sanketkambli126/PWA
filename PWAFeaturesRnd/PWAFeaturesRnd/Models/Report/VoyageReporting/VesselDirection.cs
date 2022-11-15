namespace PWAFeaturesRnd.Models.Report.VoyageReporting
{
    public class VesselDirection
    {
        public string Description { get; set; }
        public string Id { get; set; }
        public bool? PdrIsDiagonalWave { get; set; }
        public bool? PdrIsEastWestWave { get; set; }
        public int? PdrScale { get; set; }
        public decimal? PdrWaveAngle { get; set; }
        public decimal? PdrWindAngle { get; set; }
        public decimal? PdrWindDirDeg { get; set; }
    }
}
