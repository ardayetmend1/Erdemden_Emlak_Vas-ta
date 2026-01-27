namespace Erdemden_Emlak_Vasıta_EntityLayer
{
    public enum UserRole
    {
        User,
        Admin
    }

    public enum ListingCategory
    {
        RealEstate,
        Vehicle
    }

    public enum ListingStatus
    {
        Satilik,
        Satildi,
        Opsiyonlu
    }

    public enum BuyerReason
    {
        Kendisi,
        Esi,
        Cocugu,
        Yatirimlik,
        SirketIcin,
        Diger
    }
}
