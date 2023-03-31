    using System.Threading;
    using System.Threading.Tasks;
    using UnityEngine;
    using UnityEngine.Networking;

    public class Loader
    {
        public async Task<string> LoadDataByTask(string path)  
        {
            var token = new CancellationTokenSource();
            var isWait = true;
            var task = new TaskCompletionSource<bool>();
            var request = UnityWebRequest.Get(path);
            var sender = request.SendWebRequest();
            sender.completed += operation => task.SetResult(true);
        

            await task.Task;
        
            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(request.error);
            }
            else
            {
                var text = request.downloadHandler.text;
                return text;
            }

            return null;
        }

    }

