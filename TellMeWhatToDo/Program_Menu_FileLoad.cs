using System.Runtime.CompilerServices;
using System.Text.Json;

namespace TellMeWhatToDo;


internal partial class Program {
    public partial class Menu {
        public static Menu Load = new Menu(
            isCommandsOnly: true,
            instructions: () => "Choose decisions file to load:",
            commands: Menu.GetFileLoadMenuCommands(),
            inputHandler: Program.LoadFile,
            isMode: MenuType.Load
        );


        private static IList<MenuCommand> GetFileLoadMenuCommands() {
            string currentDirectory = Directory.GetCurrentDirectory();

            // Get all files in the current directory
            string[] files = Directory.GetFiles( currentDirectory );

            int i = 1;
            IList<MenuCommand> commands = files
                .Where( f => f.EndsWith("."+Program.DecisionFileExtension) )
                .Take( 9 )
                .Select( f => new MenuCommand(
                    key: Enum.Parse<ConsoleKey>( "D"+(i++) ),
                    info: f.Split( Path.DirectorySeparatorChar ).Last(),
                    action: () => Program.LoadFile( f ),
                    goToMode: MenuType.Main
                ) )
                .ToList();
            commands.Add( new MenuCommand(
                key: ConsoleKey.Escape,
                info: "Return to main menu",
                goToMode: MenuType.Main
            ) );

            return commands;
        }
    }



    private static bool LoadFile( string path ) {
        try {
            string fileData = File.ReadAllText( path );
            DecisionOptionChoices? data = JsonSerializer.Deserialize<DecisionOptionChoices>( fileData );

            if( data is null ) {
                return false;
            }

            Program.Decider = new DecisionsMaker( data.Options );
        } catch {
            return false;
        }

        return true;
    }
}
