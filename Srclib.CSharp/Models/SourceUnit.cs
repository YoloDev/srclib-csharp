using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Microsoft.Dnx.Runtime;

namespace Srclib.CSharp
{
  public class SourceUnit
  {
    public const string CSHARP_PACKAGE = "CSharpPackage";
    
    public SourceUnit()
    {
      Type = CSHARP_PACKAGE;
    }
  
    [JsonProperty]
    public string Name { get; set; }
    
    [JsonProperty]
    public string Type { get; set; }
    
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string Repo { get; set; }
    
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string CommitID { get; set; }
    
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string[] Globs { get; set; }
    
    [JsonProperty]
    public string[] Files { get; set; }
    
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string Dir { get; set; }
    
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public DependencyInfo[] Dependencies { get; set; }
    
    [JsonProperty]
    public object Data { get; set; }
    
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public Dictionary<string, string> Ops { get; set; }
    
    internal static SourceUnit FromProject(Project project, string root)
    {
      var su = new SourceUnit 
      {
        Name = project.Name,
        Dir = Utils.GetRelativePath(project.ProjectDirectory, root),
        Files = project.Files.SourceFiles.Select(p => Utils.GetRelativePath(p, root)).ToArray(),
        Dependencies = project.Dependencies.Select(DependencyInfo.FromLibraryDependency).ToArray(),
        Ops = new Dictionary<string, string> { { "depresolve", null } }
      };
      
      return su;
    }
  }
}