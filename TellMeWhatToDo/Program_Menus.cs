using System.Runtime.CompilerServices;

namespace TellMeWhatToDo;


internal partial class Program {
    public partial class Menu(
                bool isCommandsOnly,
                Func<string>? instructions,
                IList<MenuCommand> commands,
                Func<string, bool>? inputHandler,
                MenuType isMode ) {
        public readonly bool IsCommandsOnly = isCommandsOnly;
        public readonly Func<string>? Instructions = instructions;
        public readonly IList<MenuCommand> Commands = commands;
        public readonly Func<string, bool>? InputHandler = inputHandler;
        public readonly MenuType IsMode = isMode;
    }


    public class MenuCommand( ConsoleKey key, string info, MenuType goToMode, Action? action=null ) {
        public readonly ConsoleKey Key = key;
        public readonly string Info = info;
        public readonly Action? Action = action;
        public readonly MenuType GoToMode = goToMode;
    }



    private static IDictionary<MenuType, Menu> Menus = new Dictionary<MenuType, Menu> {
        { MenuType.Main, Menu.Main },
        { MenuType.Load, Menu.Load },
        { MenuType.Save, Menu.Save },
        { MenuType.Context, Menu.Context },
        { MenuType.Decide, Menu.Decide }
    };


    private static string OutputMenu( Menu menu ) {
        string output = "\nCurrent file: " + (Program.CurrentDecisionsFileName ?? "-");
        string? instruction = menu.Instructions?.Invoke();

        if( menu.IsCommandsOnly && instruction is not null ) {
            output += "\n" + instruction;
        }

        foreach( MenuCommand c in menu.Commands ) {
            output += "\n" + c.Key.ToString() + ": " + c.Info;
        }

        if( menu.IsCommandsOnly ) {
            output += "\nEnter command: ";
        } else {
            output += "\n"+ instruction + ": ";
        }

        return output;
    }

    private static void HandleMenuInput( Menu currentMenu ) {
        if( currentMenu.IsCommandsOnly ) {
            Program.HandleCommandsMenuInput( currentMenu );
        } else {
            Program.HandleTextMenuInput( currentMenu );
        }
    }

    private static void HandleCommandsMenuInput( Menu currentMenu ) {
        ConsoleKey input = Console.ReadKey().Key;

        MenuCommand? cmd = currentMenu.Commands.FirstOrDefault( c => c.Key == input );

        Program.CurrentMenu = cmd?.GoToMode ?? Program.CurrentMenu;

        Console.WriteLine( cmd is null ? "\nInvalid command." : " " );
    }

    private static void HandleTextMenuInput( Menu currentMenu ) {
        string? input = Console.ReadLine();

        if( input is not null && (currentMenu.InputHandler?.Invoke(input) ?? true) ) {
            Console.WriteLine( " " );

            Program.CurrentMenu = MenuType.Main;
        } else {
            Console.WriteLine( "\nInvalid input." );
        }
    }
}
