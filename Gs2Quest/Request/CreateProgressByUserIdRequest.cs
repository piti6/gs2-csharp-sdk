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
using Gs2.Gs2Quest.Model;
using Gs2.Util.LitJson;

#if UNITY_2017_1_OR_NEWER
using UnityEngine.Scripting;
#endif

namespace Gs2.Gs2Quest.Request
{
#if UNITY_2017_1_OR_NEWER
	[Preserve]
#endif
	[System.Serializable]
	public class CreateProgressByUserIdRequest : Gs2Request<CreateProgressByUserIdRequest>
	{
        public string NamespaceName { set; get; }
        public string UserId { set; get; }
        public string QuestModelId { set; get; }
        public bool? Force { set; get; }
        public Gs2.Gs2Quest.Model.Config[] Config { set; get; }
        public string DuplicationAvoider { set; get; }
        public CreateProgressByUserIdRequest WithNamespaceName(string namespaceName) {
            this.NamespaceName = namespaceName;
            return this;
        }
        public CreateProgressByUserIdRequest WithUserId(string userId) {
            this.UserId = userId;
            return this;
        }
        public CreateProgressByUserIdRequest WithQuestModelId(string questModelId) {
            this.QuestModelId = questModelId;
            return this;
        }
        public CreateProgressByUserIdRequest WithForce(bool? force) {
            this.Force = force;
            return this;
        }
        public CreateProgressByUserIdRequest WithConfig(Gs2.Gs2Quest.Model.Config[] config) {
            this.Config = config;
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
                .WithQuestModelId(!data.Keys.Contains("questModelId") || data["questModelId"] == null ? null : data["questModelId"].ToString())
                .WithForce(!data.Keys.Contains("force") || data["force"] == null ? null : (bool?)bool.Parse(data["force"].ToString()))
                .WithConfig(!data.Keys.Contains("config") || data["config"] == null ? new Gs2.Gs2Quest.Model.Config[]{} : data["config"].Cast<JsonData>().Select(v => {
                    return Gs2.Gs2Quest.Model.Config.FromJson(v);
                }).ToArray());
        }

        public override JsonData ToJson()
        {
            return new JsonData {
                ["namespaceName"] = NamespaceName,
                ["userId"] = UserId,
                ["questModelId"] = QuestModelId,
                ["force"] = Force,
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
            if (UserId != null) {
                writer.WritePropertyName("userId");
                writer.Write(UserId.ToString());
            }
            if (QuestModelId != null) {
                writer.WritePropertyName("questModelId");
                writer.Write(QuestModelId.ToString());
            }
            if (Force != null) {
                writer.WritePropertyName("force");
                writer.Write(bool.Parse(Force.ToString()));
            }
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
            key += UserId + ":";
            key += QuestModelId + ":";
            key += Force + ":";
            key += Config + ":";
            return key;
        }

        protected override Gs2Request DoMultiple(int x) {
            return new CreateProgressByUserIdRequest {
                NamespaceName = NamespaceName,
                UserId = UserId,
                QuestModelId = QuestModelId,
                Force = Force,
                Config = Config,
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
            if (QuestModelId != y.QuestModelId) {
                throw new ArithmeticException("mismatch parameter values CreateProgressByUserIdRequest::questModelId");
            }
            if (Force != y.Force) {
                throw new ArithmeticException("mismatch parameter values CreateProgressByUserIdRequest::force");
            }
            if (Config != y.Config) {
                throw new ArithmeticException("mismatch parameter values CreateProgressByUserIdRequest::config");
            }
            return new CreateProgressByUserIdRequest {
                NamespaceName = NamespaceName,
                UserId = UserId,
                QuestModelId = QuestModelId,
                Force = Force,
                Config = Config,
            };
        }
    }
}