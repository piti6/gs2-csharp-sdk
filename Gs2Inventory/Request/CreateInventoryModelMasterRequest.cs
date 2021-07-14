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
using Gs2.Gs2Inventory.Model;
using Gs2.Util.LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Inventory.Request
{
	[Preserve]
	[System.Serializable]
	public class CreateInventoryModelMasterRequest : Gs2Request<CreateInventoryModelMasterRequest>
	{
        public string NamespaceName { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public string Metadata { set; get; }
        public int? InitialCapacity { set; get; }
        public int? MaxCapacity { set; get; }
        public bool? ProtectReferencedItem { set; get; }

        public CreateInventoryModelMasterRequest WithNamespaceName(string namespaceName) {
            this.NamespaceName = namespaceName;
            return this;
        }

        public CreateInventoryModelMasterRequest WithName(string name) {
            this.Name = name;
            return this;
        }

        public CreateInventoryModelMasterRequest WithDescription(string description) {
            this.Description = description;
            return this;
        }

        public CreateInventoryModelMasterRequest WithMetadata(string metadata) {
            this.Metadata = metadata;
            return this;
        }

        public CreateInventoryModelMasterRequest WithInitialCapacity(int? initialCapacity) {
            this.InitialCapacity = initialCapacity;
            return this;
        }

        public CreateInventoryModelMasterRequest WithMaxCapacity(int? maxCapacity) {
            this.MaxCapacity = maxCapacity;
            return this;
        }

        public CreateInventoryModelMasterRequest WithProtectReferencedItem(bool? protectReferencedItem) {
            this.ProtectReferencedItem = protectReferencedItem;
            return this;
        }

    	[Preserve]
        public static CreateInventoryModelMasterRequest FromJson(JsonData data)
        {
            if (data == null) {
                return null;
            }
            return new CreateInventoryModelMasterRequest()
                .WithNamespaceName(!data.Keys.Contains("namespaceName") || data["namespaceName"] == null ? null : data["namespaceName"].ToString())
                .WithName(!data.Keys.Contains("name") || data["name"] == null ? null : data["name"].ToString())
                .WithDescription(!data.Keys.Contains("description") || data["description"] == null ? null : data["description"].ToString())
                .WithMetadata(!data.Keys.Contains("metadata") || data["metadata"] == null ? null : data["metadata"].ToString())
                .WithInitialCapacity(!data.Keys.Contains("initialCapacity") || data["initialCapacity"] == null ? null : (int?)int.Parse(data["initialCapacity"].ToString()))
                .WithMaxCapacity(!data.Keys.Contains("maxCapacity") || data["maxCapacity"] == null ? null : (int?)int.Parse(data["maxCapacity"].ToString()))
                .WithProtectReferencedItem(!data.Keys.Contains("protectReferencedItem") || data["protectReferencedItem"] == null ? null : (bool?)bool.Parse(data["protectReferencedItem"].ToString()));
        }

        public JsonData ToJson()
        {
            return new JsonData {
                ["namespaceName"] = NamespaceName,
                ["name"] = Name,
                ["description"] = Description,
                ["metadata"] = Metadata,
                ["initialCapacity"] = InitialCapacity,
                ["maxCapacity"] = MaxCapacity,
                ["protectReferencedItem"] = ProtectReferencedItem,
            };
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if (NamespaceName != null) {
                writer.WritePropertyName("namespaceName");
                writer.Write(NamespaceName.ToString());
            }
            if (Name != null) {
                writer.WritePropertyName("name");
                writer.Write(Name.ToString());
            }
            if (Description != null) {
                writer.WritePropertyName("description");
                writer.Write(Description.ToString());
            }
            if (Metadata != null) {
                writer.WritePropertyName("metadata");
                writer.Write(Metadata.ToString());
            }
            if (InitialCapacity != null) {
                writer.WritePropertyName("initialCapacity");
                writer.Write(int.Parse(InitialCapacity.ToString()));
            }
            if (MaxCapacity != null) {
                writer.WritePropertyName("maxCapacity");
                writer.Write(int.Parse(MaxCapacity.ToString()));
            }
            if (ProtectReferencedItem != null) {
                writer.WritePropertyName("protectReferencedItem");
                writer.Write(bool.Parse(ProtectReferencedItem.ToString()));
            }
            writer.WriteObjectEnd();
        }
    }
}