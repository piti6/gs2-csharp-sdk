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
using Gs2.Gs2Enhance.Model;
using Gs2.Util.LitJson;

#if UNITY_2017_1_OR_NEWER
using UnityEngine.Scripting;
#endif

namespace Gs2.Gs2Enhance.Request
{
#if UNITY_2017_1_OR_NEWER
	[Preserve]
#endif
	[System.Serializable]
	public class CreateProgressByUserIdRequest : Gs2Request<CreateProgressByUserIdRequest>
	{
        public string NamespaceName { set; get; }
        public string UserId { set; get; }
        public string RateName { set; get; }
        public string TargetItemSetId { set; get; }
        public Gs2.Gs2Enhance.Model.Material[] Materials { set; get; }
        public bool? Force { set; get; }
        public string DuplicationAvoider { set; get; }
        public CreateProgressByUserIdRequest WithNamespaceName(string namespaceName) {
            this.NamespaceName = namespaceName;
            return this;
        }
        public CreateProgressByUserIdRequest WithUserId(string userId) {
            this.UserId = userId;
            return this;
        }
        public CreateProgressByUserIdRequest WithRateName(string rateName) {
            this.RateName = rateName;
            return this;
        }
        public CreateProgressByUserIdRequest WithTargetItemSetId(string targetItemSetId) {
            this.TargetItemSetId = targetItemSetId;
            return this;
        }
        public CreateProgressByUserIdRequest WithMaterials(Gs2.Gs2Enhance.Model.Material[] materials) {
            this.Materials = materials;
            return this;
        }
        public CreateProgressByUserIdRequest WithForce(bool? force) {
            this.Force = force;
            return this;
        }

        public CreateProgressByUserIdRequest WithDuplicationAvoider(string duplicationAvoider) {
            this.DuplicationAvoider = duplicationAvoider;
            return this;
        }

#if UNITY_2017_1_OR_NEWER
    	[Preserve]
#endif
        public static CreateProgressByUserIdRequest FromJson(JsonData data)
        {
            if (data == null) {
                return null;
            }
            return new CreateProgressByUserIdRequest()
                .WithNamespaceName(!data.Keys.Contains("namespaceName") || data["namespaceName"] == null ? null : data["namespaceName"].ToString())
                .WithUserId(!data.Keys.Contains("userId") || data["userId"] == null ? null : data["userId"].ToString())
                .WithRateName(!data.Keys.Contains("rateName") || data["rateName"] == null ? null : data["rateName"].ToString())
                .WithTargetItemSetId(!data.Keys.Contains("targetItemSetId") || data["targetItemSetId"] == null ? null : data["targetItemSetId"].ToString())
                .WithMaterials(!data.Keys.Contains("materials") || data["materials"] == null ? new Gs2.Gs2Enhance.Model.Material[]{} : data["materials"].Cast<JsonData>().Select(v => {
                    return Gs2.Gs2Enhance.Model.Material.FromJson(v);
                }).ToArray())
                .WithForce(!data.Keys.Contains("force") || data["force"] == null ? null : (bool?)bool.Parse(data["force"].ToString()));
        }

        public override JsonData ToJson()
        {
            return new JsonData {
                ["namespaceName"] = NamespaceName,
                ["userId"] = UserId,
                ["rateName"] = RateName,
                ["targetItemSetId"] = TargetItemSetId,
                ["materials"] = Materials == null ? null : new JsonData(
                        Materials.Select(v => {
                            //noinspection Convert2MethodRef
                            return v.ToJson();
                        }).ToArray()
                    ),
                ["force"] = Force,
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
            if (RateName != null) {
                writer.WritePropertyName("rateName");
                writer.Write(RateName.ToString());
            }
            if (TargetItemSetId != null) {
                writer.WritePropertyName("targetItemSetId");
                writer.Write(TargetItemSetId.ToString());
            }
            writer.WriteArrayStart();
            foreach (var material in Materials)
            {
                if (material != null) {
                    material.WriteJson(writer);
                }
            }
            writer.WriteArrayEnd();
            if (Force != null) {
                writer.WritePropertyName("force");
                writer.Write(bool.Parse(Force.ToString()));
            }
            writer.WriteObjectEnd();
        }

        public override string UniqueKey() {
            var key = "";
            key += NamespaceName + ":";
            key += UserId + ":";
            key += RateName + ":";
            key += TargetItemSetId + ":";
            key += Materials + ":";
            key += Force + ":";
            return key;
        }

        protected override Gs2Request DoMultiple(int x) {
            return new CreateProgressByUserIdRequest {
                NamespaceName = NamespaceName,
                UserId = UserId,
                RateName = RateName,
                TargetItemSetId = TargetItemSetId,
                Materials = Materials,
                Force = Force,
            };
        }

        protected override Gs2Request DoAdd(Gs2Request x) {
            var y = (CreateProgressByUserIdRequest)x;
            if (NamespaceName != y.NamespaceName) {
                throw new ArithmeticException("mismatch parameter values CreateProgressByUserIdRequest::namespaceName");
            }
            if (UserId != y.UserId) {
                throw new ArithmeticException("mismatch parameter values CreateProgressByUserIdRequest::userId");
            }
            if (RateName != y.RateName) {
                throw new ArithmeticException("mismatch parameter values CreateProgressByUserIdRequest::rateName");
            }
            if (TargetItemSetId != y.TargetItemSetId) {
                throw new ArithmeticException("mismatch parameter values CreateProgressByUserIdRequest::targetItemSetId");
            }
            if (Materials != y.Materials) {
                throw new ArithmeticException("mismatch parameter values CreateProgressByUserIdRequest::materials");
            }
            if (Force != y.Force) {
                throw new ArithmeticException("mismatch parameter values CreateProgressByUserIdRequest::force");
            }
            return new CreateProgressByUserIdRequest {
                NamespaceName = NamespaceName,
                UserId = UserId,
                RateName = RateName,
                TargetItemSetId = TargetItemSetId,
                Materials = Materials,
                Force = Force,
            };
        }
    }
}