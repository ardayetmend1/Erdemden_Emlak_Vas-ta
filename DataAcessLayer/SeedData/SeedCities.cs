using DataAcessLayer.Concrete;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAcessLayer.SeedData
{
    public static class SeedCities
    {
        public static async Task SeedAsync(Context context)
        {
            if (await context.Set<City>().AnyAsync())
                return;

            var locationData = GetLocationData();

            foreach (var cityEntry in locationData)
            {
                var city = new City
                {
                    Id = Guid.NewGuid(),
                    Name = cityEntry.Key,
                    CreatedAt = DateTime.UtcNow
                };

                context.Set<City>().Add(city);

                foreach (var districtEntry in cityEntry.Value)
                {
                    var district = new District
                    {
                        Id = Guid.NewGuid(),
                        CityId = city.Id,
                        Name = districtEntry.Key,
                        CreatedAt = DateTime.UtcNow
                    };

                    context.Set<District>().Add(district);

                    foreach (var neighborhoodName in districtEntry.Value)
                    {
                        var neighborhood = new Neighborhood
                        {
                            Id = Guid.NewGuid(),
                            DistrictId = district.Id,
                            Name = neighborhoodName,
                            CreatedAt = DateTime.UtcNow
                        };

                        context.Set<Neighborhood>().Add(neighborhood);
                    }
                }
            }

            await context.SaveChangesAsync();
        }

        public static async Task AddMissingDistrictsAndNeighborhoodsAsync(Context context)
        {
            var locationData = GetLocationData();
            var existingCities = await context.Set<City>()
                .Include(c => c.Districts)
                    .ThenInclude(d => d.Neighborhoods)
                .ToListAsync();

            bool hasChanges = false;

            foreach (var cityEntry in locationData)
            {
                var city = existingCities.FirstOrDefault(c => c.Name == cityEntry.Key);
                if (city == null)
                {
                    city = new City
                    {
                        Id = Guid.NewGuid(),
                        Name = cityEntry.Key,
                        CreatedAt = DateTime.UtcNow
                    };
                    context.Set<City>().Add(city);
                    hasChanges = true;
                }

                foreach (var districtEntry in cityEntry.Value)
                {
                    var district = city.Districts.FirstOrDefault(d => d.Name == districtEntry.Key);
                    if (district == null)
                    {
                        district = new District
                        {
                            Id = Guid.NewGuid(),
                            CityId = city.Id,
                            Name = districtEntry.Key,
                            CreatedAt = DateTime.UtcNow
                        };
                        context.Set<District>().Add(district);
                        hasChanges = true;
                    }

                    foreach (var neighborhoodName in districtEntry.Value)
                    {
                        if (!district.Neighborhoods.Any(n => n.Name == neighborhoodName))
                        {
                            context.Set<Neighborhood>().Add(new Neighborhood
                            {
                                Id = Guid.NewGuid(),
                                DistrictId = district.Id,
                                Name = neighborhoodName,
                                CreatedAt = DateTime.UtcNow
                            });
                            hasChanges = true;
                        }
                    }
                }
            }

            if (hasChanges)
                await context.SaveChangesAsync();
        }

        private static Dictionary<string, Dictionary<string, string[]>> GetLocationData()
        {
            return new Dictionary<string, Dictionary<string, string[]>>
            {
                {
                    "İstanbul", new Dictionary<string, string[]>
                    {
                        { "Adalar", new[] { "Burgazada", "Büyükada", "Heybeliada", "Kınalıada", "Maden", "Nizam" } },
                        { "Arnavutköy", new[] { "Arnavutköy Merkez", "Bolluca", "Çilingir", "Dursunköy", "Hacımaşlı", "Hadımköy", "Haraççı", "İmrahor", "Karlıbayır", "Karaburun", "Mavigöl", "Taşoluk", "Tayakadın", "Yeşilbayır", "Yassıören" } },
                        { "Ataşehir", new[] { "Aşıkpaşa", "Atatürk", "Barbaros", "Batı Ataşehir", "Esatpaşa", "Ferhatpaşa", "İçerenköy", "İnönü", "Kayışdağı", "Küçükbakkalköy", "Mevlana", "Mimar Sinan", "Mustafa Kemal", "Yeni Çamlıca", "Yenisahra" } },
                        { "Avcılar", new[] { "Ambarlı", "Cihangir", "Denizköşkler", "Firuzköy", "Gümüşpala", "Merkez", "Mustafa Kemal Paşa", "Tahtakale", "Üniversite", "Yeşilkent" } },
                        { "Bağcılar", new[] { "100. Yıl", "15 Temmuz", "Bağlar", "Barbaros", "Çınar", "Demirkapı", "Evren", "Fatih", "Fevzi Çakmak", "Güneşli", "Hürriyet", "İnönü", "Kazım Karabekir", "Kemalpaşa", "Kirazlı", "Mahmutbey", "Merkez", "Sancaktepe", "Yavuz Selim", "Yenigün", "Yenimahalle", "Yıldıztepe" } },
                        { "Bahçelievler", new[] { "Bahçelievler", "Çobançeşme", "Cumhuriyet", "Fevzi Çakmak", "Hürriyet", "Kocasinan Merkez", "Siyavuşpaşa", "Soğanlı", "Şirinevler", "Yenibosna Merkez", "Zafer" } },
                        { "Bakırköy", new[] { "Ataköy 1. Kısım", "Ataköy 2-5-6. Kısım", "Ataköy 3-4-11. Kısım", "Ataköy 7-8-9-10. Kısım", "Basınköy", "Cevizlik", "Florya", "Kartaltepe", "Osmaniye", "Sakızağacı", "Şenlikköy", "Yeşilköy", "Yeşilyurt", "Zeytinlik", "Zuhuratbaba" } },
                        { "Başakşehir", new[] { "Altınşehir", "Bahçeşehir 1. Kısım", "Bahçeşehir 2. Kısım", "Başak", "Başakşehir", "Güvercintepe", "Kayabaşı", "Onurkent", "Şahintepe", "Ziya Gökalp" } },
                        { "Bayrampaşa", new[] { "Altıntepsi", "Cevatpaşa", "İsmetpaşa", "Kocatepe", "Muratpaşa", "Orta", "Terazidere", "Vatan", "Yenidoğan", "Yıldırım" } },
                        { "Beşiktaş", new[] { "Abbasağa", "Akatlar", "Arnavutköy", "Balmumcu", "Bebek", "Cihannüma", "Dikilitaş", "Etiler", "Gayrettepe", "Konaklar", "Kuruçeşme", "Levent", "Levazım", "Mecidiye", "Muradiye", "Nisbetiye", "Ortaköy", "Sinanpaşa", "Türkali", "Ulus", "Vişnezade", "Yıldız" } },
                        { "Beykoz", new[] { "Acarlar", "Anadoluhisarı", "Baklacı", "Çavuşbaşı", "Çengeldere", "Çiğdem", "Çubuklu", "Gümüşsuyu", "İncirköy", "Kanlıca", "Kavacık", "Mahmutşevketpaşa", "Ortaçeşme", "Paşabahçe", "Polonezköy", "Rüzgarlıbahçe", "Soğuksu", "Tokatköy", "Yalıköy", "Yavuz Selim" } },
                        { "Beylikdüzü", new[] { "Adnan Kahveci", "Barış", "Büyükşehir", "Cumhuriyet", "Dereağzı", "Gürpınar", "Kavakli", "Kavaklı", "Marmara", "Sahil", "Yakuplu" } },
                        { "Beyoğlu", new[] { "Asmalımescit", "Bedrettin", "Bereketzade", "Bülbül", "Camiikebir", "Cihangir", "Çukur", "Evliya Çelebi", "Firuzağa", "Gümüşsuyu", "Hacıahmet", "Halıcıoğlu", "İstiklal", "Kamer Hatun", "Kalyoncukulluğu", "Katip Mustafa Çelebi", "Kemankeş Karamustafa Paşa", "Kılıçali Paşa", "Kulaksız", "Müeyyetzade", "Ömeravni", "Örnektepe", "Pürtelaş", "Şahkulu", "Sütlüce", "Tomtom", "Yahya Kahya", "Yenişehir" } },
                        { "Büyükçekmece", new[] { "Atatürk", "Batıköy", "Beykent", "Cumhuriyet", "Çakmaklı", "Fatih", "Hürriyet", "Kamiloba", "Mimarsinan", "Muratbey", "Pınartepe", "Türkoba", "Yenimahalle" } },
                        { "Çatalca", new[] { "Çatalca Merkez", "Ferhatpaşa", "Kaleiçi", "Kestanelik", "Muratbey", "Ovayenice", "Subaşı" } },
                        { "Çekmeköy", new[] { "Alemdağ", "Çamlık", "Ekşioğlu", "Hamidiye", "Hüseyinli", "Kirazlıdere", "Mehmet Akif", "Merkez", "Mimar Sinan", "Nişantepe", "Ömerli", "Reşadiye", "Sultaniye", "Taşdelen" } },
                        { "Esenler", new[] { "Atışalanı", "Birlik", "Davutpaşa", "Fatih", "Fevzi Çakmak", "Havaalanı", "Kazım Karabekir", "Kemer", "Menderes", "Mimarsinan", "Namık Kemal", "Nine Hatun", "Oruçreis", "Turgut Reis", "Yavuz Selim" } },
                        { "Esenyurt", new[] { "Ardıçlı", "Cumhuriyet", "Çakmaklı", "Esenkent", "Fatih", "Güzelyurt", "İnönü", "Kıraç", "Mehterçeşme", "Merkez", "Namık Kemal", "Necmi Kadıoğlu", "Örnek", "Pınar", "Saadetdere", "Talatpaşa", "Turgut Özal", "Üçevler", "Yenikent", "Zafer" } },
                        { "Eyüpsultan", new[] { "Akşemsettin", "Alibeyköy", "Çırçır", "Defterdar", "Düğmeciler", "Emniyettepe", "Esentepe", "Göktürk", "Güzeltepe", "İslambey", "Karadolap", "Kemerburgaz", "Merkez", "Mimarsinan", "Mithatpaşa", "Nişancı", "Pirinççi", "Rami Cuma", "Rami Yeni", "Sakarya", "Silahtarağa", "Topçular", "Yeşilpınar" } },
                        { "Fatih", new[] { "Aksaray", "Alemdar", "Atikali", "Balat", "Beyazıt", "Binbirdirek", "Cankurtaran", "Cerrahpaşa", "Demirtaş", "Derviş Ali", "Eminsinan", "Hacıkadın", "Haseki Sultan", "Hirkai Şerif", "Horhor", "İskenderpaşa", "Karagümrük", "Katip Kasım", "Kumkapı", "Mercan", "Mesihpaşa", "Mevlanakapı", "Mollafenari", "Mollahüsrev", "Nişanca", "Saraç İshak", "Seyyid Ömer", "Silivrikapı", "Sultanahmet", "Süleymaniye", "Sümbül Efendi", "Şehremini", "Tahtakale", "Topkapı", "Vefa", "Yavuz Sultan Selim", "Yedikule", "Zeyrek" } },
                        { "Gaziosmanpaşa", new[] { "Bağlarbaşı", "Barbaros Hayrettin Paşa", "Fevzi Çakmak", "Hürriyet", "Karadeniz", "Karayolları", "Karlıtepe", "Mevlana", "Merkez", "Sarıgöl", "Sultançiftliği", "Yenimahalle", "Yıldıztabya" } },
                        { "Güngören", new[] { "Abdurrahman Nafiz Gürman", "Akıncılar", "Gençosman", "Güneştepe", "Güven", "Haznedar", "Mareşal Çakmak", "Mehmet Nesih Özmen", "Merkez", "Sanayi", "Tozkoparan" } },
                        { "Kadıköy", new[] { "Acıbadem", "Bostancı", "Caddebostan", "Caferağa", "Dumlupınar", "Eğitim", "Erenköy", "Fenerbahçe", "Feneryolu", "Fikirtepe", "Göztepe", "Hasanpaşa", "Koşuyolu", "Kozyatağı", "Merdivenköy", "Moda", "Osmanağa", "Rasimpaşa", "Sahrayıcedit", "Suadiye", "Zühtüpaşa" } },
                        { "Kağıthane", new[] { "Çağlayan", "Çeliktepe", "Emniyet Evleri", "Gültepe", "Hamidiye", "Harmantepe", "Hürriyet", "Mehmet Akif Ersoy", "Merkez", "Nurtepe", "Ortabayır", "Sanayi", "Seyrantepe", "Şirintepe", "Talatpaşa", "Telsizler", "Yahya Kemal" } },
                        { "Kartal", new[] { "Atalar", "Cevizli", "Çavuşoğlu", "Esentepe", "Hürriyet", "Karlıktepe", "Kordonboyu", "Orhantepe", "Petrol İş", "Soğanlık Yeni", "Topselvi", "Uğur Mumcu", "Yakacık", "Yalı", "Yukarı" } },
                        { "Küçükçekmece", new[] { "Atakent", "Beşyol", "Cennet", "Cumhuriyet", "Fatih", "Fevzi Çakmak", "Gültepe", "Halkalı Merkez", "İnönü", "İstasyon", "Kanarya", "Kartaltepe", "Kemalpaşa", "Mehmet Akif", "Söğütlüçeşme", "Sultan Murat", "Tevfik Bey", "Yarımburgaz", "Yeşilova" } },
                        { "Maltepe", new[] { "Altayçeşme", "Altıntepe", "Aydınevler", "Bağlarbaşı", "Başıbüyük", "Cevizli", "Çınar", "Esenkent", "Feyzullah", "Fındıklı", "Girne", "Gülsuyu", "İdealtepe", "Küçükyalı", "Yalı", "Zümrütevler" } },
                        { "Pendik", new[] { "Ahmet Yesevi", "Bahçelievler", "Batı", "Çamçeşme", "Çınardere", "Doğu", "Dumlupınar", "Esenler", "Esenyalı", "Güllü Bağlar", "Güzelyalı", "Harmandere", "Kavakpınar", "Kurtköy", "Orta", "Ramazanoğlu", "Sapanbağları", "Sülüntepe", "Velibaba", "Yayalar", "Yenimahalle", "Yenişehir" } },
                        { "Sancaktepe", new[] { "Abdurrahman Gazi", "Akpınar", "Atatürk", "Emek", "Eyüp Sultan", "Fatih", "İnönü", "Kemal Türkler", "Meclis", "Merve", "Osmangazi", "Paşaköy", "Sarıgazi", "Veysel Karani", "Yenidoğan", "Yunusemre" } },
                        { "Sarıyer", new[] { "Ayazağa", "Bahçeköy Merkez", "Baltalimanı", "Büyükdere", "Cumhuriyet", "Çamlıtepe", "Darüşşafaka", "Derbent", "Emirgan", "Fatih Sultan Mehmet", "Ferahevler", "Garipçe", "Huzur", "İstinye", "Kireçburnu", "Kumsal", "Maslak", "Maden", "Pınar", "PTT Evleri", "Reşitpaşa", "Rumelihisarı", "Rumelikavağı", "Tarabya", "Uskumruköy", "Yeniköy", "Zekeriyaköy" } },
                        { "Silivri", new[] { "Alibey", "Cumhuriyet", "Fatih", "Gümüşyaka", "Mimar Sinan", "Ortaköy", "Piri Mehmet Paşa", "Selimpaşa", "Semizkumlar", "Yolçatı" } },
                        { "Sultanbeyli", new[] { "Abdurrahman Gazi", "Ahmet Yesevi", "Battalgazi", "Fatih", "Hamidiye", "Hasanpaşa", "Mecidiye", "Mehmet Akif", "Mimar Sinan", "Necip Fazıl", "Orhangazi", "Turgut Reis", "Yavuz Selim" } },
                        { "Sultangazi", new[] { "50. Yıl", "75. Yıl", "Cebeci", "Cumhuriyet", "Esentepe", "Gazi", "Habibler", "İsmetpaşa", "Malkoçoğlu", "Sultançiftliği", "Uğur Mumcu", "Yayla", "Yunusemre", "Zübeyde Hanım" } },
                        { "Şile", new[] { "Şile Merkez", "Ağva", "Balibey", "Kumbaba" } },
                        { "Şişli", new[] { "Bozkurt", "Cumhuriyet", "Duatepe", "Ergenekon", "Esentepe", "Feriköy", "Fulya", "Gülbahar", "Halaskargazi", "Halide Edip Adıvar", "Harbiye", "İnönü", "İzzet Paşa", "Kaptanpaşa", "Kuştepe", "Mahmut Şevket Paşa", "Mecidiyeköy", "Meşrutiyet", "Nişantaşı", "Osmanbey", "Paşa", "Teşvikiye", "Yayla" } },
                        { "Tuzla", new[] { "Aydınlı", "Aydıntepe", "Cami", "Evliya Çelebi", "Fatih", "İçmeler", "İstasyon", "Mescit", "Mimar Sinan", "Orhanlı", "Postane", "Şifa", "Yayla" } },
                        { "Ümraniye", new[] { "Altınşehir", "Armağanevler", "Aşağı Dudullu", "Atakent", "Çakmak", "Çamlık", "Dumlupınar", "Elmalıkent", "Esenevler", "Esenkent", "Esenşehir", "Hekimbaşı", "İnkılap", "İstiklal", "Kazım Karabekir", "Madenler", "Mehmet Akif", "Namık Kemal", "Parseller", "Saray", "Site", "Tantavi", "Tatlısu", "Topağacı", "Yamanevler", "Yukarı Dudullu" } },
                        { "Üsküdar", new[] { "Acıbadem", "Ahmediye", "Altunizade", "Aziz Mahmut Hüdayi", "Bahçelievler", "Barbaros", "Beylerbeyi", "Bulgurlu", "Burhaniye", "Çengelköy", "Ferah", "Güzeltepe", "İcadiye", "Kandilli", "Kirazlıtepe", "Kısıklı", "Kuzguncuk", "Mimar Sinan", "Salacak", "Selimiye", "Sultantepe", "Ünalan", "Validei Atik", "Yavuztürk", "Zeynep Kamil" } },
                        { "Zeytinburnu", new[] { "Beştelsiz", "Çırpıcı", "Gökalp", "Kazlıçeşme", "Maltepe", "Merkezefendi", "Nuripaşa", "Seyitnizam", "Sümer", "Telsiz", "Veliefendi", "Yenidoğan", "Yeşiltepe" } }
                    }
                },
                {
                    "Ankara", new Dictionary<string, string[]>
                    {
                        { "Çankaya", Array.Empty<string>() },
                        { "Keçiören", Array.Empty<string>() },
                        { "Mamak", Array.Empty<string>() },
                        { "Yenimahalle", Array.Empty<string>() }
                    }
                },
                {
                    "İzmir", new Dictionary<string, string[]>
                    {
                        { "Konak", Array.Empty<string>() },
                        { "Bornova", Array.Empty<string>() },
                        { "Karşıyaka", Array.Empty<string>() },
                        { "Alsancak", Array.Empty<string>() },
                        { "Çeşme", Array.Empty<string>() }
                    }
                },
                {
                    "Bursa", new Dictionary<string, string[]>
                    {
                        { "Nilüfer", Array.Empty<string>() },
                        { "Osmangazi", Array.Empty<string>() },
                        { "Yıldırım", Array.Empty<string>() }
                    }
                },
                {
                    "Antalya", new Dictionary<string, string[]>
                    {
                        { "Konyaaltı", Array.Empty<string>() },
                        { "Muratpaşa", Array.Empty<string>() },
                        { "Kepez", Array.Empty<string>() },
                        { "Alanya", Array.Empty<string>() }
                    }
                },
                {
                    "Muğla", new Dictionary<string, string[]>
                    {
                        { "Bodrum", Array.Empty<string>() },
                        { "Marmaris", Array.Empty<string>() },
                        { "Fethiye", Array.Empty<string>() },
                        { "Datça", Array.Empty<string>() }
                    }
                },
                {
                    "Bolu", new Dictionary<string, string[]>
                    {
                        { "Merkez", Array.Empty<string>() },
                        { "Abant", Array.Empty<string>() },
                        { "Gerede", Array.Empty<string>() }
                    }
                }
            };
        }
    }
}
