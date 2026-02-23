namespace EntityLayer.Entities;

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
    Opsiyonlu,
    Pasif
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

public enum RealEstateListingType
{
    Satilik,
    Kiralik
}

public enum QuoteStatus
{
    Pending,    // Beklemede
    OfferMade,  // Teklif Verildi
    Accepted,   // Kabul Edildi
    Rejected    // Reddedildi
}
