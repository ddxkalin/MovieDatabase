namespace MovieDatabase.Web.ViewModels.Settings
{
    using MovieDatabase.Common.Mapping;
    using MovieDatabase.Data.Models;

    public class SettingViewModel : IMapFrom<Setting>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }
    }
}
