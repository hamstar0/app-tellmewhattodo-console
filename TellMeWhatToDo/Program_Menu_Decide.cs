using System.Runtime.CompilerServices;

namespace TellMeWhatToDo;


internal partial class Program {
    public partial class Menu {
        public static Menu Decide = new Menu(
            isCommandsOnly: true,
            instructions: () => "",
            commands: [],
            inputHandler: null,
            isMode: MenuType.Decide
        );


        private static IList<MenuCommand> GetDecideMenuCommands() {
            var output = new List<MenuCommand>();

            if( Program.Decider is not null ) {
                output.Add( new MenuCommand(
                    key: ConsoleKey.P,
                    info: "Propose new idea",
                    action: Program.ProposeDecision,
                    goToMode: MenuType.Decide
                ) );

                if( Program.Decider.PendingDecision is not null ) {
                    output.Add( new MenuCommand(
                        key: ConsoleKey.D,
                        info: "Confirm proposed idea",
                        action: Program.ConfirmDecision,
                        goToMode: MenuType.Decide
                    ) );
                }
            }

            output.Add( new MenuCommand(
                key: ConsoleKey.Escape,
                info: "Return to main menu",
                action: () => {
                    if( Program.Decider is not null ) {
                        Program.Decider.PendingDecision = null;
                    }
                },
                goToMode: MenuType.Main
            ) );

            return output;
        }
    }



    private static void ProposeDecision() {
        if( Program.Decider is null ) {
            throw new FileNotFoundException( "No decisions file loaded." );
        }

        Program.Decider.PendingDecision = Program.Decider.ProposeDecision();

        Console.WriteLine( "Idea: "+Program.Decider.PendingDecision.Name );
    }


    private static void ConfirmDecision() {
        if( Program.Decider is null ) {
            throw new FileNotFoundException( "No decisions file loaded." );
        }

        Program.Decider.MakeDecision( Program.Decider.PendingDecision );

        Program.Decider.PendingDecision = null;

        Console.WriteLine( "Idea confirmed." );
    }
}
