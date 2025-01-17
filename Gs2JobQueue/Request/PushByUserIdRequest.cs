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
using Gs2.Gs2JobQueue.Model;
using Gs2.Util.LitJson;

#if UNITY_2017_1_OR_NEWER
using UnityEngine.Scripting;
#endif

namespace Gs2.Gs2JobQueue.Request
{
#if UNITY_2017_1_OR_NEWER
	[Preserve]
#endif
	[System.Serializable]
	public class PushByUserIdRequest : Gs2Request<PushByUserIdRequest>
	{
        public string NamespaceName { set; get; }
        public string UserId { set; get; }
        public Gs2.Gs2JobQueue.Model.JobEntry[] Jobs { set; get; }
        public string DuplicationAvoider { set; get; }
        public PushByUserIdRequest WithNamespaceName(string namespaceName) {
            this.NamespaceName = namespaceName;
            return this;
        }
        public PushByUserIdRequest WithUserId(string userId) {
            this.UserId = userId;
            return this;
        }
        public PushByUserIdRequest WithJobs(Gs2.Gs2JobQueue.Model.JobEntry[] jobs) {
            this.Jobs = jobs;
            return this;
        }

        public PushByUserIdRequest WithDuplicationAvoider(string duplicationAvoider) {
            this.DuplicationAvoider = duplicationAvoider;
            return this;
        }

#if UNITY_2017_1_OR_NEWER
    	[Preserve]
#endif
        public static PushByUserIdRequest FromJson(JsonData data)
        {
            if (data == null) {
                return null;
            }
            return new PushByUserIdRequest()
                .WithNamespaceName(!data.Keys.Contains("namespaceName") || data["namespaceName"] == null ? null : data["namespaceName"].ToString())
                .WithUserId(!data.Keys.Contains("userId") || data["userId"] == null ? null : data["userId"].ToString())
                .WithJobs(!data.Keys.Contains("jobs") || data["jobs"] == null ? new Gs2.Gs2JobQueue.Model.JobEntry[]{} : data["jobs"].Cast<JsonData>().Select(v => {
                    return Gs2.Gs2JobQueue.Model.JobEntry.FromJson(v);
                }).ToArray());
        }

        public override JsonData ToJson()
        {
            return new JsonData {
                ["namespaceName"] = NamespaceName,
                ["userId"] = UserId,
                ["jobs"] = Jobs == null ? null : new JsonData(
                        Jobs.Select(v => {
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
            if (UserId != null) {
                writer.WritePropertyName("userId");
                writer.Write(UserId.ToString());
            }
            writer.WriteArrayStart();
            foreach (var job in Jobs)
            {
                if (job != null) {
                    job.WriteJson(writer);
                }
            }
            writer.WriteArrayEnd();
            writer.WriteObjectEnd();
        }

        public override string UniqueKey() {
            var key = "";
            key += NamespaceName + ":";
            key += UserId + ":";
            key += Jobs + ":";
            return key;
        }

        protected override Gs2Request DoMultiple(int x) {
            return new PushByUserIdRequest {
                NamespaceName = NamespaceName,
                UserId = UserId,
                Jobs = Jobs,
            };
        }

        protected override Gs2Request DoAdd(Gs2Request x) {
            var y = (PushByUserIdRequest)x;
            if (NamespaceName != y.NamespaceName) {
                throw new ArithmeticException("mismatch parameter values PushByUserIdRequest::namespaceName");
            }
            if (UserId != y.UserId) {
                throw new ArithmeticException("mismatch parameter values PushByUserIdRequest::userId");
            }
            if (Jobs != y.Jobs) {
                throw new ArithmeticException("mismatch parameter values PushByUserIdRequest::jobs");
            }
            return new PushByUserIdRequest {
                NamespaceName = NamespaceName,
                UserId = UserId,
                Jobs = Jobs,
            };
        }
    }
}