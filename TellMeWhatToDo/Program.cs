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
    private static DecisionsMaker? Decider = new DecisionsMaker( new DecisionsData {
        Options = new Dictionary<string, DecisionOption> {
            {
                "Square room",
                new DecisionOption {
                    Contexts = [ "Default" ],
                    Name = "Square room",
                    Description = null,
                    Weight = 1f,
                    RepeatMinimumAmount = null,
                    RepeatMaximumAmount = null,
                    IsRepeatingWeightScale = null,
                    IsRepeatingContiguouslyWeightScale = null,
                    RepeatIntermissionMinimumDelay = null,
                    RepeatIntermissionWeight = null,
                    SubOptionsSlots = [
                        new DecisionOption.SubOptionSlotPreference {
                            Name = "General door",
                            NonEmptyPreference = 1f
                        },
                        new DecisionOption.SubOptionSlotPreference {
                            Name = "General door",
                            NonEmptyPreference = 1f
                        }
                    ]
                }
            },
            {
                "Hallway",
                new DecisionOption {
                    Contexts = [ "Default" ],
                    Name = "Hallway",
                    Description = null,
                    Weight = 1f,
                    RepeatMinimumAmount = null,
                    RepeatMaximumAmount = null,
                    IsRepeatingWeightScale = null,
                    IsRepeatingContiguouslyWeightScale = null,
                    RepeatIntermissionMinimumDelay = null,
                    RepeatIntermissionWeight = null,
                    SubOptionsSlots = [
                        new DecisionOption.SubOptionSlotPreference {
                            Name = "General door",
                            NonEmptyPreference = 1f
                        },
                        new DecisionOption.SubOptionSlotPreference {
                            Name = "General door",
                            NonEmptyPreference = 1f
                        }
                    ]
                }
            }
        },
        SubOptionTypes = new Dictionary<string, DecisionSubOptionSlotDef> {
            {
                "General door",
                new DecisionSubOptionSlotDef {
                    ConnectionName = "General door",
                    ChildContextsPreferences = new Dictionary<string[], float> {
                        { ["Square room"], 1f },
                        { ["Hallway"], 1f }
                    }
                }
            }
        }
    } );

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
