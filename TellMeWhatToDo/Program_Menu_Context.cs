using System.Runtime.CompilerServices;

namespace TellMeWhatToDo;


internal partial class Program {
    public partial class Menu {
        public static Menu Context = new Menu(
            isCommandsOnly: false,
            instructions: () => "Enter a context",
            commands: [],
            inputHandler: Program.AddContext,
            isMode: MenuType.Context
        );
    }



    private static bool AddContext( string context ) {
        Program.Decider?.AddInitialContext( context );

        return true;
    }
}
