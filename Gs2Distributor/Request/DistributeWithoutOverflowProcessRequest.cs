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
using System;
using System.Collections.Generic;
using System.Linq;
using Gs2.Core.Control;
using Gs2.Core.Model;
using Gs2.Gs2Distributor.Model;
using Gs2.Util.LitJson;

#if UNITY_2017_1_OR_NEWER
using UnityEngine.Scripting;
#endif

namespace Gs2.Gs2Distributor.Request
{
#if UNITY_2017_1_OR_NEWER
	[Preserve]
#endif
	[System.Serializable]
	public class DistributeWithoutOverflowProcessRequest : Gs2Request<DistributeWithoutOverflowProcessRequest>
	{
        public string UserId { set; get; }
        public Gs2.Gs2Distributor.Model.DistributeResource DistributeResource { set; get; }
        public DistributeWithoutOverflowProcessRequest WithUserId(string userId) {
            this.UserId = userId;
            return this;
        }
        public DistributeWithoutOverflowProcessRequest WithDistributeResource(Gs2.Gs2Distributor.Model.DistributeResource distributeResource) {
            this.DistributeResource = distributeResource;
            return this;
        }

#if UNITY_2017_1_OR_NEWER
    	[Preserve]
#endif
        public static DistributeWithoutOverflowProcessRequest FromJson(JsonData data)
        {
            if (data == null) {
                return null;
            }
            return new DistributeWithoutOverflowProcessRequest()
                .WithUserId(!data.Keys.Contains("userId") || data["userId"] == null ? null : data["userId"].ToString())
                .WithDistributeResource(!data.Keys.Contains("distributeResource") || data["distributeResource"] == null ? null : Gs2.Gs2Distributor.Model.DistributeResource.FromJson(data["distributeResource"]));
        }

        public override JsonData ToJson()
        {
            return new JsonData {
                ["userId"] = UserId,
                ["distributeResource"] = DistributeResource?.ToJson(),
            };
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if (UserId != null) {
                writer.WritePropertyName("userId");
                writer.Write(UserId.ToString());
            }
            if (DistributeResource != null) {
                DistributeResource.WriteJson(writer);
            }
            writer.WriteObjectEnd();
        }

        public override string UniqueKey() {
            var key = "";
            key += UserId + ":";
            key += DistributeResource + ":";
            return key;
        }

        protected override Gs2Request DoMultiple(int x) {
            if (x != 1) {
                throw new ArithmeticException("Unsupported multiply DistributeWithoutOverflowProcessRequest");
            }
            return this;
        }

        protected override Gs2Request DoAdd(Gs2Request x) {
            var y = (DistributeWithoutOverflowProcessRequest)x;
            return this;
        }
    }
}