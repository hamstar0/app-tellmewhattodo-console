using System.Runtime.CompilerServices;
using System.Text.Json;

namespace TellMeWhatToDo;


internal partial class Program {
    public partial class Menu {
        public static Menu Save = new Menu(
            isCommandsOnly: false,
            instructions: () => "Enter name of save file",
            commands: [],
            inputHandler: Program.SaveFile,
            isMode: MenuType.Save
        );
    }



    private static bool SaveFile( string fileName ) {
        string currentDirectory = Directory.GetCurrentDirectory();
        string path = currentDirectory
            + Path.DirectorySeparatorChar
            + fileName+"."+Program.DecisionFileExtension;

        try {
            string fileData = JsonSerializer.Serialize(
                Program.Decider?.Data,
                new JsonSerializerOptions { WriteIndented = true }
            );

            File.WriteAllText( path, fileData );
        } catch {
            return false;
        }

        return true;
    }
}
