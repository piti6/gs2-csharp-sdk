/*
 * Copyright 2016 Game Server Services, Inc. or its affiliates. All Rights
 * Reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License").
 * You may not use this file except in compliance with the License.
 * A copy of the License is located at
 *
 *  http://www.apache.org/licenses/LICENSE-2.0
 *
 * or in the "license" file accompanying this file. This file is distributed
 * on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
 * express or implied. See the License for the specific language governing
 * permissions and limitations under the License.
 */

using Gs2.Core.Model;
using Gs2.Util.LitJson;

namespace Gs2.Core.Control
{
	public abstract class Gs2Request<T> : Gs2Request, IRequest where T : class
	{
	    /** GS2リクエストID */
	    public string RequestId { set; get; }

	    public T WithRequestId(string requestId)
	    {
		    this.RequestId = requestId;
		    return this as T;
	    }
        
	    /** コンテキストスタック */
	    public string ContextStack { set; get; }

	    public T WithContextStack(string contextStack)
	    {
		    this.ContextStack = contextStack;
		    return this as T;
	    }

	    public static Gs2Request<T> operator *(Gs2Request<T> x, int y) {
		    return (Gs2Request<T>) x.DoMultiple(y);
	    }

	    public static Gs2Request<T> operator +(Gs2Request<T> x, Gs2Request<T> y) {
		    return (Gs2Request<T>) x.DoAdd(y);
	    }
	}
	
	public abstract class Gs2Request
	{
		internal Gs2Request() { }
		
		public abstract JsonData ToJson();
	    
		public abstract string UniqueKey();

		protected abstract Gs2Request DoMultiple(int x);
		protected abstract Gs2Request DoAdd(Gs2Request x);
	    
		public static Gs2Request operator *(Gs2Request x, int y) {
			return x.DoMultiple(y);
		}

		public static Gs2Request operator +(Gs2Request x, Gs2Request y) {
			return x.DoAdd(y);
		}
	}
}