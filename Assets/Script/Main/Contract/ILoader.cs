using System.Threading.Tasks;

public interface ILoader
{
    Task<string> LoadDataByTask(string path);
}