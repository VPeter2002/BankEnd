// Data/ApplicationUser.cs
using Microsoft.AspNetCore.Identity;

namespace AD41HN_HFT_2022231.Db.Data
{
    // Itt megadhatod a namespace-t a projekted alapján
    // (pl. namespace AD41HN_HFT_2022231.Data)
    // namespace AD41HN_HFT_2022231.Data

    public class ApplicationUser : IdentityUser
    {
        // Itt tároljuk, hogy "Orvos" vagy "Páciens"    
        public string UserRole { get; set; }
    }
}