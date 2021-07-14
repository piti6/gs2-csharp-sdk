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

namespace Gs2.Gs2Inventory.Result
{
	[Preserve]
	[System.Serializable]
	public class DeleteInventoryModelMasterResult : IResult
	{
        public Gs2.Gs2Inventory.Model.InventoryModelMaster Item { set; get; }

        public DeleteInventoryModelMasterResult WithItem(Gs2.Gs2Inventory.Model.InventoryModelMaster item) {
            this.Item = item;
            return this;
        }

    	[Preserve]
        public static DeleteInventoryModelMasterResult FromJson(JsonData data)
        {
            if (data == null) {
                return null;
            }
            return new DeleteInventoryModelMasterResult()
                .WithItem(!data.Keys.Contains("item") || data["item"] == null ? null : Gs2.Gs2Inventory.Model.InventoryModelMaster.FromJson(data["item"]));
        }

        public JsonData ToJson()
        {
            return new JsonData {
                ["item"] = Item?.ToJson(),
            };
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if (Item != null) {
                Item.WriteJson(writer);
            }
            writer.WriteObjectEnd();
        }
    }
}