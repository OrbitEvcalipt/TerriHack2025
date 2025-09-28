namespace FunnyBlox
{
  public enum ETowerOwnerType
  {
    Neutral = 0,
    Player = 1,
    Enemy = 2,
  }

  public enum ETowerPurposeType
  {
    FactoryUnit_00 = 0,
    FactoryUnit_01 = 1,
    WatchTower = 2,
  }

  public enum EUnitType
  {
    UnitType_00 = 0,
    UnitType_01 = 1,
    UnitType_99 = 99,
  }

  public class CommonData
  {
    public const string PLAYERPREFS_CURRENTLEVEL="CurrentLevel";
  }
}