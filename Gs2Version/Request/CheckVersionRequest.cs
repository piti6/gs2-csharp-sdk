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
using Gs2.Gs2Version.Model;
using Gs2.Util.LitJson;

#if UNITY_2017_1_OR_NEWER
using UnityEngine.Scripting;
#endif

namespace Gs2.Gs2Version.Request
{
#if UNITY_2017_1_OR_NEWER
	[Preserve]
#endif
	[System.Serializable]
	public class CheckVersionRequest : Gs2Request<CheckVersionRequest>
	{
        public string NamespaceName { set; get; }
        public string AccessToken { set; get; }
        public Gs2.Gs2Version.Model.TargetVersion[] TargetVersions { set; get; }
        public string DuplicationAvoider { set; get; }
        public CheckVersionRequest WithNamespaceName(string namespaceName) {
            this.NamespaceName = namespaceName;
            return this;
        }
        public CheckVersionRequest WithAccessToken(string accessToken) {
            this.AccessToken = accessToken;
            return this;
        }
        public CheckVersionRequest WithTargetVersions(Gs2.Gs2Version.Model.TargetVersion[] targetVersions) {
            this.TargetVersions = targetVersions;
            return this;
        }

        public CheckVersionRequest WithDuplicationAvoider(string duplicationAvoider) {
            this.DuplicationAvoider = duplicationAvoider;
            return this;
        }

#if UNITY_2017_1_OR_NEWER
    	[Preserve]
#endif
        public static CheckVersionRequest FromJson(JsonData data)
        {
            if (data == null) {
                return null;
            }
            return new CheckVersionRequest()
                .WithNamespaceName(!data.Keys.Contains("namespaceName") || data["namespaceName"] == null ? null : data["namespaceName"].ToString())
                .WithAccessToken(!data.Keys.Contains("accessToken") || data["accessToken"] == null ? null : data["accessToken"].ToString())
                .WithTargetVersions(!data.Keys.Contains("targetVersions") || data["targetVersions"] == null ? new Gs2.Gs2Version.Model.TargetVersion[]{} : data["targetVersions"].Cast<JsonData>().Select(v => {
                    return Gs2.Gs2Version.Model.TargetVersion.FromJson(v);
                }).ToArray());
        }

        public JsonData ToJson()
        {
            return new JsonData {
                ["namespaceName"] = NamespaceName,
                ["accessToken"] = AccessToken,
                ["targetVersions"] = TargetVersions == null ? null : new JsonData(
                        TargetVersions.Select(v => {
                            //noinspection Convert2MethodRef
                            return v.ToJson();
                        }).ToArray()
                    ),
            };
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if (NamespaceName != null) {
                writer.WritePropertyName("namespaceName");
                writer.Write(NamespaceName.ToString());
            }
            if (AccessToken != null) {
                writer.WritePropertyName("accessToken");
                writer.Write(AccessToken.ToString());
            }
            writer.WriteArrayStart();
            foreach (var targetVersion in TargetVersions)
            {
                if (targetVersion != null) {
                    targetVersion.WriteJson(writer);
                }
            }
            writer.WriteArrayEnd();
            writer.WriteObjectEnd();
        }
    }
}