using MudBlazor;
using MudBlazor.Utilities;

namespace FilmManagement.Web.Layout
{
    public sealed class FilmManagementPallete : PaletteDark
    {

        private FilmManagementPallete()
        {
            Primary = new MudColor("#9966FF");
            Secondary = new MudColor("#F6AD31");
            Tertiary = new MudColor("#8AE491");
        }

        public static FilmManagementPallete CreatePallete => new();
    }
}
