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

    //private static DecisionsMaker? Decider = null;
    private static DecisionsMaker? Decider = new DecisionsMaker( new DecisionsData(
        new Dictionary<string, DecisionOption>{ {
            "Square room",
            new DecisionOption(
                contexts: [],
                name: "Square room",
                description: null,
                weight: 1f,
                repeatMinimumAmount: null,
                repeatMaximumAmount: null,
                isRepeatingWeightScale: null,
                isRepeatingContiguouslyWeightScale: null,
                repeatIntermissionMinimumDelay: null,
                repeatIntermissionWeight: null,
                subOptionsSlotsByName: null
            )
        } },
        new Dictionary<string, DecisionSubOption>{ {
            "General door",
            new DecisionSubOption(
                connectionName: "General door",
                subOptionContextsPreferences: new Dictionary<string[], float> {
                    { ["Square room"], 1f }
                },
                maxDepthByContext: null
            )
        } }
    ) );

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
