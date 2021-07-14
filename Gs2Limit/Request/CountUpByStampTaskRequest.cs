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
using Gs2.Gs2Limit.Model;
using Gs2.Util.LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Limit.Request
{
	[Preserve]
	[System.Serializable]
	public class CountUpByStampTaskRequest : Gs2Request<CountUpByStampTaskRequest>
	{
        public string StampTask { set; get; }
        public string KeyId { set; get; }

        public CountUpByStampTaskRequest WithStampTask(string stampTask) {
            this.StampTask = stampTask;
            return this;
        }

        public CountUpByStampTaskRequest WithKeyId(string keyId) {
            this.KeyId = keyId;
            return this;
        }

    	[Preserve]
        public static CountUpByStampTaskRequest FromJson(JsonData data)
        {
            if (data == null) {
                return null;
            }
            return new CountUpByStampTaskRequest()
                .WithStampTask(!data.Keys.Contains("stampTask") || data["stampTask"] == null ? null : data["stampTask"].ToString())
                .WithKeyId(!data.Keys.Contains("keyId") || data["keyId"] == null ? null : data["keyId"].ToString());
        }

        public JsonData ToJson()
        {
            return new JsonData {
                ["stampTask"] = StampTask,
                ["keyId"] = KeyId,
            };
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if (StampTask != null) {
                writer.WritePropertyName("stampTask");
                writer.Write(StampTask.ToString());
            }
            if (KeyId != null) {
                writer.WritePropertyName("keyId");
                writer.Write(KeyId.ToString());
            }
            writer.WriteObjectEnd();
        }
    }
}