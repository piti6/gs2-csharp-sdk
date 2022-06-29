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
using Gs2.Gs2Formation.Model;
using Gs2.Util.LitJson;

#if UNITY_2017_1_OR_NEWER
using UnityEngine.Scripting;
#endif

namespace Gs2.Gs2Formation.Request
{
#if UNITY_2017_1_OR_NEWER
	[Preserve]
#endif
	[System.Serializable]
	public class AcquireActionsToPropertyFormPropertiesRequest : Gs2Request<AcquireActionsToPropertyFormPropertiesRequest>
	{
        public string NamespaceName { set; get; }
        public string UserId { set; get; }
        public string FormModelName { set; get; }
        public string PropertyId { set; get; }
        public Gs2.Gs2Formation.Model.AcquireAction AcquireAction { set; get; }
        public Gs2.Gs2Formation.Model.AcquireActionConfig[] Config { set; get; }
        public string DuplicationAvoider { set; get; }
        public AcquireActionsToPropertyFormPropertiesRequest WithNamespaceName(string namespaceName) {
            this.NamespaceName = namespaceName;
            return this;
        }
        public AcquireActionsToPropertyFormPropertiesRequest WithUserId(string userId) {
            this.UserId = userId;
            return this;
        }
        public AcquireActionsToPropertyFormPropertiesRequest WithFormModelName(string formModelName) {
            this.FormModelName = formModelName;
            return this;
        }
        public AcquireActionsToPropertyFormPropertiesRequest WithPropertyId(string propertyId) {
            this.PropertyId = propertyId;
            return this;
        }
        public AcquireActionsToPropertyFormPropertiesRequest WithAcquireAction(Gs2.Gs2Formation.Model.AcquireAction acquireAction) {
            this.AcquireAction = acquireAction;
            return this;
        }
        public AcquireActionsToPropertyFormPropertiesRequest WithConfig(Gs2.Gs2Formation.Model.AcquireActionConfig[] config) {
            this.Config = config;
            return this;
        }

        public AcquireActionsToPropertyFormPropertiesRequest WithDuplicationAvoider(string duplicationAvoider) {
            this.DuplicationAvoider = duplicationAvoider;
            return this;
        }

#if UNITY_2017_1_OR_NEWER
    	[Preserve]
#endif
        public static AcquireActionsToPropertyFormPropertiesRequest FromJson(JsonData data)
        {
            if (data == null) {
                return null;
            }
            return new AcquireActionsToPropertyFormPropertiesRequest()
                .WithNamespaceName(!data.Keys.Contains("namespaceName") || data["namespaceName"] == null ? null : data["namespaceName"].ToString())
                .WithUserId(!data.Keys.Contains("userId") || data["userId"] == null ? null : data["userId"].ToString())
                .WithFormModelName(!data.Keys.Contains("formModelName") || data["formModelName"] == null ? null : data["formModelName"].ToString())
                .WithPropertyId(!data.Keys.Contains("propertyId") || data["propertyId"] == null ? null : data["propertyId"].ToString())
                .WithAcquireAction(!data.Keys.Contains("acquireAction") || data["acquireAction"] == null ? null : Gs2.Gs2Formation.Model.AcquireAction.FromJson(data["acquireAction"]))
                .WithConfig(!data.Keys.Contains("config") || data["config"] == null ? new Gs2.Gs2Formation.Model.AcquireActionConfig[]{} : data["config"].Cast<JsonData>().Select(v => {
                    return Gs2.Gs2Formation.Model.AcquireActionConfig.FromJson(v);
                }).ToArray());
        }

        public JsonData ToJson()
        {
            return new JsonData {
                ["namespaceName"] = NamespaceName,
                ["userId"] = UserId,
                ["formModelName"] = FormModelName,
                ["propertyId"] = PropertyId,
                ["acquireAction"] = AcquireAction?.ToJson(),
                ["config"] = new JsonData(Config == null ? new JsonData[]{} :
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
            if (FormModelName != null) {
                writer.WritePropertyName("formModelName");
                writer.Write(FormModelName.ToString());
            }
            if (PropertyId != null) {
                writer.WritePropertyName("propertyId");
                writer.Write(PropertyId.ToString());
            }
            if (AcquireAction != null) {
                AcquireAction.WriteJson(writer);
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
    }
}