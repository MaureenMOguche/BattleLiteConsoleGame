namespace MyBattleLite.Models
{
    public class PlayerInfoModel
    {
        public string UsersName { get; set; }
        public List<GridSpotModel> GridShotLocations { get; set; } = new List<GridSpotModel>();
        public List<GridSpotModel> ShipLocations { get; set; } = new List<GridSpotModel>();

        public int ShotCount { get; set; } = 0;
    }
}