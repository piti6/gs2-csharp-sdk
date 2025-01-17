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
	public class DirectEnhanceByUserIdRequest : Gs2Request<DirectEnhanceByUserIdRequest>
	{
        public string NamespaceName { set; get; }
        public string RateName { set; get; }
        public string UserId { set; get; }
        public string TargetItemSetId { set; get; }
        public Gs2.Gs2Enhance.Model.Material[] Materials { set; get; }
        public Gs2.Gs2Enhance.Model.Config[] Config { set; get; }
        public string DuplicationAvoider { set; get; }
        public DirectEnhanceByUserIdRequest WithNamespaceName(string namespaceName) {
            this.NamespaceName = namespaceName;
            return this;
        }
        public DirectEnhanceByUserIdRequest WithRateName(string rateName) {
            this.RateName = rateName;
            return this;
        }
        public DirectEnhanceByUserIdRequest WithUserId(string userId) {
            this.UserId = userId;
            return this;
        }
        public DirectEnhanceByUserIdRequest WithTargetItemSetId(string targetItemSetId) {
            this.TargetItemSetId = targetItemSetId;
            return this;
        }
        public DirectEnhanceByUserIdRequest WithMaterials(Gs2.Gs2Enhance.Model.Material[] materials) {
            this.Materials = materials;
            return this;
        }
        public DirectEnhanceByUserIdRequest WithConfig(Gs2.Gs2Enhance.Model.Config[] config) {
            this.Config = config;
            return this;
        }

        public DirectEnhanceByUserIdRequest WithDuplicationAvoider(string duplicationAvoider) {
            this.DuplicationAvoider = duplicationAvoider;
            return this;
        }

#if UNITY_2017_1_OR_NEWER
    	[Preserve]
#endif
        public static DirectEnhanceByUserIdRequest FromJson(JsonData data)
        {
            if (data == null) {
                return null;
            }
            return new DirectEnhanceByUserIdRequest()
                .WithNamespaceName(!data.Keys.Contains("namespaceName") || data["namespaceName"] == null ? null : data["namespaceName"].ToString())
                .WithRateName(!data.Keys.Contains("rateName") || data["rateName"] == null ? null : data["rateName"].ToString())
                .WithUserId(!data.Keys.Contains("userId") || data["userId"] == null ? null : data["userId"].ToString())
                .WithTargetItemSetId(!data.Keys.Contains("targetItemSetId") || data["targetItemSetId"] == null ? null : data["targetItemSetId"].ToString())
                .WithMaterials(!data.Keys.Contains("materials") || data["materials"] == null ? new Gs2.Gs2Enhance.Model.Material[]{} : data["materials"].Cast<JsonData>().Select(v => {
                    return Gs2.Gs2Enhance.Model.Material.FromJson(v);
                }).ToArray())
                .WithConfig(!data.Keys.Contains("config") || data["config"] == null ? new Gs2.Gs2Enhance.Model.Config[]{} : data["config"].Cast<JsonData>().Select(v => {
                    return Gs2.Gs2Enhance.Model.Config.FromJson(v);
                }).ToArray());
        }

        public override JsonData ToJson()
        {
            return new JsonData {
                ["namespaceName"] = NamespaceName,
                ["rateName"] = RateName,
                ["userId"] = UserId,
                ["targetItemSetId"] = TargetItemSetId,
                ["materials"] = Materials == null ? null : new JsonData(
                        Materials.Select(v => {
                            //noinspection Convert2MethodRef
                            return v.ToJson();
                        }).ToArray()
                    ),
                ["config"] = Config == null ? null : new JsonData(
                        Config.Select(v => {
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
            if (RateName != null) {
                writer.WritePropertyName("rateName");
                writer.Write(RateName.ToString());
            }
            if (UserId != null) {
                writer.WritePropertyName("userId");
                writer.Write(UserId.ToString());
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
            writer.WriteArrayStart();
            foreach (var confi in Config)
            {
                if (confi != null) {
                    confi.WriteJson(writer);
                }
            }
            writer.WriteArrayEnd();
            writer.WriteObjectEnd();
        }

        public override string UniqueKey() {
            var key = "";
            key += NamespaceName + ":";
            key += RateName + ":";
            key += UserId + ":";
            key += TargetItemSetId + ":";
            key += Materials + ":";
            key += Config + ":";
            return key;
        }

        protected override Gs2Request DoMultiple(int x) {
            return new DirectEnhanceByUserIdRequest {
                NamespaceName = NamespaceName,
                RateName = RateName,
                UserId = UserId,
                TargetItemSetId = TargetItemSetId,
                Materials = Materials,
                Config = Config,
            };
        }

        protected override Gs2Request DoAdd(Gs2Request x) {
            var y = (DirectEnhanceByUserIdRequest)x;
            if (NamespaceName != y.NamespaceName) {
                throw new ArithmeticException("mismatch parameter values DirectEnhanceByUserIdRequest::namespaceName");
            }
            if (RateName != y.RateName) {
                throw new ArithmeticException("mismatch parameter values DirectEnhanceByUserIdRequest::rateName");
            }
            if (UserId != y.UserId) {
                throw new ArithmeticException("mismatch parameter values DirectEnhanceByUserIdRequest::userId");
            }
            if (TargetItemSetId != y.TargetItemSetId) {
                throw new ArithmeticException("mismatch parameter values DirectEnhanceByUserIdRequest::targetItemSetId");
            }
            if (Materials != y.Materials) {
                throw new ArithmeticException("mismatch parameter values DirectEnhanceByUserIdRequest::materials");
            }
            if (Config != y.Config) {
                throw new ArithmeticException("mismatch parameter values DirectEnhanceByUserIdRequest::config");
            }
            return new DirectEnhanceByUserIdRequest {
                NamespaceName = NamespaceName,
                RateName = RateName,
                UserId = UserId,
                TargetItemSetId = TargetItemSetId,
                Materials = Materials,
                Config = Config,
            };
        }
    }
}