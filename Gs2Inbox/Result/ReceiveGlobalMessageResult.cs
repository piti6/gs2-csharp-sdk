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
using Gs2.Gs2Inbox.Model;
using Gs2.Util.LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Inbox.Result
{
	[Preserve]
	[System.Serializable]
	public class ReceiveGlobalMessageResult : IResult
	{
        public Gs2.Gs2Inbox.Model.Message[] Item { set; get; }

        public ReceiveGlobalMessageResult WithItem(Gs2.Gs2Inbox.Model.Message[] item) {
            this.Item = item;
            return this;
        }

    	[Preserve]
        public static ReceiveGlobalMessageResult FromJson(JsonData data)
        {
            if (data == null) {
                return null;
            }
            return new ReceiveGlobalMessageResult()
                .WithItem(!data.Keys.Contains("item") || data["item"] == null ? new Gs2.Gs2Inbox.Model.Message[]{} : data["item"].Cast<JsonData>().Select(v => {
                    return Gs2.Gs2Inbox.Model.Message.FromJson(v);
                }).ToArray());
        }

        public JsonData ToJson()
        {
            return new JsonData {
                ["item"] = new JsonData(Item == null ? new JsonData[]{} :
                        Item.Select(v => {
                            //noinspection Convert2MethodRef
                            return v.ToJson();
                        }).ToArray()
                    ),
            };
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            writer.WriteArrayStart();
            foreach (var ite in Item)
            {
                if (ite != null) {
                    ite.WriteJson(writer);
                }
            }
            writer.WriteArrayEnd();
            writer.WriteObjectEnd();
        }
    }
}