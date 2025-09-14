using MudBlazor;
using MudBlazor.Utilities;

namespace TodoListWeb;

public static  class Configurations
{
   public static MudTheme DarkTheme = new MudTheme()
   {
      PaletteDark = new PaletteDark()
      {
        Primary = Colors.Purple.Darken3,
        Secondary = Colors.Red.Lighten1,
        Background = Colors.Shades.Black,
        AppbarText = Colors.Shades.White,
        TextSecondary = Colors.Gray.Lighten1,
        Surface = Colors.Gray.Darken3,
        AppbarBackground = Colors.Purple.Darken3,
        Success = Colors.Green.Darken3
        
      }
   };
   
   public static MudTheme LightTheme = new MudTheme()
   {
      PaletteLight = new PaletteLight()
      {
         
      }
   };
   
}
