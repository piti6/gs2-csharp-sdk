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
using Gs2.Gs2Lottery.Model;
using Gs2.Util.LitJson;

#if UNITY_2017_1_OR_NEWER
using UnityEngine.Scripting;
#endif

namespace Gs2.Gs2Lottery.Result
{
#if UNITY_2017_1_OR_NEWER
	[Preserve]
#endif
	[System.Serializable]
	public class DrawWithRandomSeedByUserIdResult : IResult
	{
        public Gs2.Gs2Lottery.Model.DrawnPrize[] Items { set; get; }
        public string TransactionId { set; get; }
        public string StampSheet { set; get; }
        public string StampSheetEncryptionKeyId { set; get; }
        public bool? AutoRunStampSheet { set; get; }

        public DrawWithRandomSeedByUserIdResult WithItems(Gs2.Gs2Lottery.Model.DrawnPrize[] items) {
            this.Items = items;
            return this;
        }

        public DrawWithRandomSeedByUserIdResult WithTransactionId(string transactionId) {
            this.TransactionId = transactionId;
            return this;
        }

        public DrawWithRandomSeedByUserIdResult WithStampSheet(string stampSheet) {
            this.StampSheet = stampSheet;
            return this;
        }

        public DrawWithRandomSeedByUserIdResult WithStampSheetEncryptionKeyId(string stampSheetEncryptionKeyId) {
            this.StampSheetEncryptionKeyId = stampSheetEncryptionKeyId;
            return this;
        }

        public DrawWithRandomSeedByUserIdResult WithAutoRunStampSheet(bool? autoRunStampSheet) {
            this.AutoRunStampSheet = autoRunStampSheet;
            return this;
        }

#if UNITY_2017_1_OR_NEWER
    	[Preserve]
#endif
        public static DrawWithRandomSeedByUserIdResult FromJson(JsonData data)
        {
            if (data == null) {
                return null;
            }
            return new DrawWithRandomSeedByUserIdResult()
                .WithItems(!data.Keys.Contains("items") || data["items"] == null ? new Gs2.Gs2Lottery.Model.DrawnPrize[]{} : data["items"].Cast<JsonData>().Select(v => {
                    return Gs2.Gs2Lottery.Model.DrawnPrize.FromJson(v);
                }).ToArray())
                .WithTransactionId(!data.Keys.Contains("transactionId") || data["transactionId"] == null ? null : data["transactionId"].ToString())
                .WithStampSheet(!data.Keys.Contains("stampSheet") || data["stampSheet"] == null ? null : data["stampSheet"].ToString())
                .WithStampSheetEncryptionKeyId(!data.Keys.Contains("stampSheetEncryptionKeyId") || data["stampSheetEncryptionKeyId"] == null ? null : data["stampSheetEncryptionKeyId"].ToString())
                .WithAutoRunStampSheet(!data.Keys.Contains("autoRunStampSheet") || data["autoRunStampSheet"] == null ? null : (bool?)bool.Parse(data["autoRunStampSheet"].ToString()));
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
                ["transactionId"] = TransactionId,
                ["stampSheet"] = StampSheet,
                ["stampSheetEncryptionKeyId"] = StampSheetEncryptionKeyId,
                ["autoRunStampSheet"] = AutoRunStampSheet,
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
            if (TransactionId != null) {
                writer.WritePropertyName("transactionId");
                writer.Write(TransactionId.ToString());
            }
            if (StampSheet != null) {
                writer.WritePropertyName("stampSheet");
                writer.Write(StampSheet.ToString());
            }
            if (StampSheetEncryptionKeyId != null) {
                writer.WritePropertyName("stampSheetEncryptionKeyId");
                writer.Write(StampSheetEncryptionKeyId.ToString());
            }
            if (AutoRunStampSheet != null) {
                writer.WritePropertyName("autoRunStampSheet");
                writer.Write(bool.Parse(AutoRunStampSheet.ToString()));
            }
            writer.WriteObjectEnd();
        }
    }
}