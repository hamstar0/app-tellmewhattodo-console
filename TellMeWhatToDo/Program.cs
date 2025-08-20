using System.Runtime.CompilerServices;

namespace TellMeWhatToDo;


internal partial class Program {
    public enum MenuType {
        Main,
        Load,
        Save,
        Context,
        Decide,
        Quit
    }

    public static readonly string DecisionFileExtension = "decide.json";



    private static string? CurrentDecisionsFileName = null;

    private static DecisionsMaker? Decider = null;
    //private static DecisionsMaker? Decider = new DecisionsMaker( new DecisionsData(
    //    [new DecisionsData.Option("Square room", 1f, 0, 9999, 1f, 0.9f, 3, Int32.MaxValue, 1f, 1f, [] )],
    //    [new DecisionsData.Context("Default", 1f, 0f, 1f, 5, 50, 10, new Dictionary<string, float>())]
    //) );

    private static MenuType CurrentMenu = MenuType.Main;



    static void Main( string[] args ) {
        Console.WriteLine( @"Tell Me What To Do v1.0" );

        while( true ) {
            Menu currentMenu = Program.Menus[ Program.CurrentMenu ];

            Console.Write( "\n"+Program.OutputMenu(currentMenu) );

            Program.HandleMenuInput( currentMenu );

            if( Program.CurrentMenu == MenuType.Quit ) {
                break;
            }
        }
    }
}
