using System.Runtime.CompilerServices;

namespace TellMeWhatToDo;


internal partial class Program {
    public partial class Menu {
        public static Menu Main = new Menu(
            isCommandsOnly: true,
            instructions: () => "",
            commands: Menu.GetMainMenuCommands(),
            inputHandler: null,
            isMode: MenuType.Main
        );


        private static IList<MenuCommand> GetMainMenuCommands() {
            var output = new List<MenuCommand>();

            output.Add( new MenuCommand(
                key: ConsoleKey.L,
                info: "Load file",
                goToMode: MenuType.Load
            ) );

            if( Program.Decider is not null ) {
                output.Add( new MenuCommand(
                    key: ConsoleKey.S,
                    info: "Save file",
                    goToMode: MenuType.Save
                ) );

                output.Add( new MenuCommand(
                    key: ConsoleKey.C,
                    info: "Pick an initial context",
                    goToMode: MenuType.Context
                ) );

                output.Add( new MenuCommand(
                    key: ConsoleKey.D,
                    info: "Make decision",
                    goToMode: MenuType.Decide
                ) );
            }

            output.Add( new MenuCommand(
                key: ConsoleKey.Q,
                info: "Quit",
                goToMode: MenuType.Quit
            ) );

            return output;
        }
    }
}
