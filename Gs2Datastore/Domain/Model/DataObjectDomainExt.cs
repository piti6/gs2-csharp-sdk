using System;
using System.Collections;
#if UNITY_2017_1_OR_NEWER
#if GS2_ENABLE_UNITASK
using Cysharp.Threading.Tasks;
#endif
using UnityEngine.Networking;
#else
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
#endif
using Gs2.Core.Domain;
using Gs2.Core.Exception;
using Gs2.Core.Model;
using Gs2.Core.Net;
using Gs2.Gs2Datastore.Model;
using Gs2.Gs2Datastore.Request;

namespace Gs2.Gs2Datastore.Domain.Model
{
    public partial class DataObjectDomain
    {
#if UNITY_2017_1_OR_NEWER
	#if GS2_ENABLE_UNITASK
	    public async UniTask<byte[]> Download(
	#else
	    private class DownloadImplFuture : Gs2Future<byte[]>
	    {
		    private DataObjectDomain _domain;
		    private string _fileUrl;

		    public DownloadImplFuture(
			    DataObjectDomain domain,
			    string fileUrl
		    )
		    {
			    _domain = domain;
			    _fileUrl = fileUrl;
		    }

		    public override IEnumerator Action()
		    {
			    using var request = UnityWebRequest.Get(_fileUrl);
			    request.downloadHandler = new DownloadHandlerBuffer();
			    yield return request.SendWebRequest();
			    if (request.responseCode != 200)
			    {
				    OnError(new UnknownException(Array.Empty<RequestError>()));
			    }
			    OnComplete(request.downloadHandler.data);
		    }
	    }
	    
	    public class DownloadFuture : Gs2Future<byte[]>
	    {
		    private DataObjectDomain _domain;

		    public DownloadFuture(
			    DataObjectDomain domain
		    )
		    {
			    _domain = domain;
		    }

		    public override IEnumerator Action()
		    {
			    {
				    string fileUrl = null;
				    {
					    var task = _domain.PrepareDownloadByUserIdAndName(
						    new PrepareDownloadByUserIdAndDataObjectNameRequest()
					    );
					    yield return task;
					    if (task.Error != null)
					    {
						    OnError(task.Error);
						    yield break;
					    }

					    fileUrl = task.Result.FileUrl;
				    }
				    {
					    var task = new DownloadImplFuture(
						    _domain,
						    fileUrl
					    );
					    yield return task;
					    if (task.Error != null)
					    {
						    OnError(task.Error);
						    yield break;
					    }
					    OnComplete(task.Result);
				    }
			    }
		    }
	    }

        public DownloadFuture Download(
	#endif
#else
	    public async Task<byte[]> Download(
#endif
		)
        {
#if UNITY_2017_1_OR_NEWER
	#if GS2_ENABLE_UNITASK
	        var result = await this.PrepareDownloadByUserIdAndNameAsync(
		        new PrepareDownloadByUserIdAndDataObjectNameRequest()
	        );
	        using var request = UnityWebRequest.Get(result.FileUrl);
	        request.downloadHandler = new DownloadHandlerBuffer();
	        await request.SendWebRequest();
	        if (request.responseCode != 200) {
		        request.Dispose();
		        throw new UnknownException(Array.Empty<RequestError>());
	        }
	        var data = request.downloadHandler.data;
	        return data;
#else
	        return new DownloadFuture(
		        this
	        );
	#endif
#else
	        var result = await this.PrepareDownloadByUserIdAndNameAsync(
		        new PrepareDownloadByUserIdAndDataObjectNameRequest()
	        );
	        var response = await new HttpClient().GetAsync(result.FileUrl);
	        if (response.StatusCode != HttpStatusCode.OK)
	        {
		        throw new UnknownException(Array.Empty<RequestError>());
	        }
	        return await response.Content.ReadAsByteArrayAsync();
#endif
        }
	    
#if UNITY_2017_1_OR_NEWER
	#if GS2_ENABLE_UNITASK
	    public async UniTask<DataObject> ReUpload(
	#else
	    private class ReUploadImplFuture : Gs2Future<RestResult>
	    {
		    private DataObjectDomain _domain;
		    private string _uploadUrl;
		    private byte[] _data;

		    public ReUploadImplFuture(
			    DataObjectDomain domain,
			    string uploadUrl,
			    byte[] data
		    )
		    {
			    _domain = domain;
			    _uploadUrl = uploadUrl;
			    _data = data;
		    }

		    public override IEnumerator Action()
		    {
			    using var request = UnityWebRequest.Put(_uploadUrl, _data);
			    request.downloadHandler = new DownloadHandlerBuffer();
			    yield return request.SendWebRequest();
			    OnComplete(new RestResult(
				    (int) request.responseCode,
				    request.responseCode == 200 ? "{}" : string.IsNullOrEmpty(request.error) ? "{}" : request.error
			    ));
		    }
	    }

	    public class ReUploadFuture : Gs2Future<DataObject>
	    {
		    private DataObjectDomain _domain;
		    private byte[] _data;

		    public ReUploadFuture(
			    DataObjectDomain domain,
			    byte[] data
		    )
		    {
			    _domain = domain;
			    _data = data;
		    }

		    public override IEnumerator Action()
		    {
			    {
				    string dataObjectName = null;
				    string uploadUrl = null;
				    {
					    var task = _domain.PrepareReUpload(
						    new PrepareReUploadByUserIdRequest()
					    );
					    yield return task;
					    if (task.Error != null)
					    {
						    OnError(task.Error);
						    yield break;
					    }
						var task2 = task.Result.Model();
						yield return task2;

					    dataObjectName = task2.Result.Name;
					    uploadUrl = task.Result.UploadUrl;
				    }
				    {
					    var task = new ReUploadImplFuture(
						    _domain,
						    uploadUrl,
						    _data
					    );
					    yield return task;
					    if (task.Error != null)
					    {
						    OnError(task.Error);
						    yield break;
					    }
				    }
				    {
					    var task = _domain.DoneUpload(
						    new DoneUploadByUserIdRequest()
					    );
					    yield return task;
					    if (task.Error != null)
					    {
						    OnError(task.Error);
						    yield break;
					    }
						var task2 = task.Result.Model();
						yield return task2;
					    OnComplete(task2.Result);
				    }
			    }
		    }
	    }

        public ReUploadFuture ReUpload(
	#endif
#else
	    public async Task<DataObjectDomain> ReUpload(
#endif
	        byte[] data
        )
        {
#if UNITY_2017_1_OR_NEWER
	#if GS2_ENABLE_UNITASK
	        {
		        var result = await this.PrepareReUploadAsync(
			        new PrepareReUploadByUserIdRequest()
		        );

		        using var request = UnityWebRequest.Put(result.UploadUrl, data);
		        request.downloadHandler = new DownloadHandlerBuffer();
		        await request.SendWebRequest();
		        if (request.responseCode != 200) {
			        request.Dispose();
			        throw new UnknownException(Array.Empty<RequestError>());
		        }
	        }
	        {
		        var result = await (await this.DoneUploadAsync(
			        new DoneUploadByUserIdRequest()
		        )).Model();
		        return result;
	        }
	#else
	        return new ReUploadFuture(
		        this,
		        data
	        );
	#endif
#else
	        {
		        var result = await this.PrepareReUploadAsync(
			        new PrepareReUploadByUserIdRequest()
		        );
		        
		        var response = await new HttpClient().PutAsync(result.UploadUrl, new ByteArrayContent(data));
		        if (response.StatusCode != HttpStatusCode.OK)
		        {
			        throw new UnknownException(Array.Empty<RequestError>());
		        }
	        }
	        {
		        var result = await this.DoneUploadAsync(
			        new DoneUploadByUserIdRequest()
		        );
		        return result;
	        }
#endif
        }
    }
}