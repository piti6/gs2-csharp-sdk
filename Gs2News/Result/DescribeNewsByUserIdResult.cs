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
using Gs2.Gs2News.Model;
using Gs2.Util.LitJson;

#if UNITY_2017_1_OR_NEWER
using UnityEngine.Scripting;
#endif

namespace Gs2.Gs2News.Result
{
#if UNITY_2017_1_OR_NEWER
	[Preserve]
#endif
	[System.Serializable]
	public class DescribeNewsByUserIdResult : IResult
	{
        public Gs2.Gs2News.Model.News[] Items { set; get; }
        public string ContentHash { set; get; }
        public string TemplateHash { set; get; }

        public DescribeNewsByUserIdResult WithItems(Gs2.Gs2News.Model.News[] items) {
            this.Items = items;
            return this;
        }

        public DescribeNewsByUserIdResult WithContentHash(string contentHash) {
            this.ContentHash = contentHash;
            return this;
        }

        public DescribeNewsByUserIdResult WithTemplateHash(string templateHash) {
            this.TemplateHash = templateHash;
            return this;
        }

#if UNITY_2017_1_OR_NEWER
    	[Preserve]
#endif
        public static DescribeNewsByUserIdResult FromJson(JsonData data)
        {
            if (data == null) {
                return null;
            }
            return new DescribeNewsByUserIdResult()
                .WithItems(!data.Keys.Contains("items") || data["items"] == null ? new Gs2.Gs2News.Model.News[]{} : data["items"].Cast<JsonData>().Select(v => {
                    return Gs2.Gs2News.Model.News.FromJson(v);
                }).ToArray())
                .WithContentHash(!data.Keys.Contains("contentHash") || data["contentHash"] == null ? null : data["contentHash"].ToString())
                .WithTemplateHash(!data.Keys.Contains("templateHash") || data["templateHash"] == null ? null : data["templateHash"].ToString());
        }

        public JsonData ToJson()
        {
            return new JsonData {
                ["items"] = Items == null ? null : new JsonData(
                        Items.Select(v => {
                            //noinspection Convert2MethodRef
                            return v.ToJson();
                        }).ToArray()
                    ),
                ["contentHash"] = ContentHash,
                ["templateHash"] = TemplateHash,
            };
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            writer.WriteArrayStart();
            foreach (var item in Items)
            {
                if (item != null) {
                    item.WriteJson(writer);
                }
            }
            writer.WriteArrayEnd();
            if (ContentHash != null) {
                writer.WritePropertyName("contentHash");
                writer.Write(ContentHash.ToString());
            }
            if (TemplateHash != null) {
                writer.WritePropertyName("templateHash");
                writer.Write(TemplateHash.ToString());
            }
            writer.WriteObjectEnd();
        }
    }
}