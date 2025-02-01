namespace ACME.BL.Mapping
{
    using ACME.BL.Mapping.Profile;
    using AutoMapper;
    public class MapperConfig
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(static cfg =>
            {
                cfg.AddProfile(new BussinesProfile());
                cfg.AddProfile(new MasterDataBLProfile());
            });
        }
    }
}
