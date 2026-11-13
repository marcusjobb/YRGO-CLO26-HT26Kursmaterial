using AiChat.Tools;

namespace AiChat.Tests;

public class WorkspaceTests
{
    [Fact]
    public void Filer_inne_i_arbetsytan_godkanns()
    {
        var workspace = new Workspace(Directory.CreateTempSubdirectory().FullName);
        var path = workspace.Resolve("mapp/fil.txt");
        Assert.EndsWith(Path.Combine("mapp", "fil.txt"), path);
    }

    [Theory]
    [InlineData("../hemligt.txt")]
    [InlineData("mapp/../../hemligt.txt")]
    public void Sokvagar_utanfor_arbetsytan_avvisas(string path)
    {
        var workspace = new Workspace(Directory.CreateTempSubdirectory().FullName);
        Assert.Throws<ArgumentException>(() => workspace.Resolve(path));
    }

    [Fact]
    public void Tom_sokvag_avvisas()
    {
        var workspace = new Workspace(Directory.CreateTempSubdirectory().FullName);
        Assert.Throws<ArgumentException>(() => workspace.Resolve("  "));
    }
}
