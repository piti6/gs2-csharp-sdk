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
using System.Text.RegularExpressions;
using Gs2.Core.Model;
using Gs2.Util.LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Project.Model
{

	[Preserve]
	public class Receipt : IComparable
	{
        public string ReceiptId { set; get; }
        public string AccountName { set; get; }
        public string Name { set; get; }
        public long? Date { set; get; }
        public string Amount { set; get; }
        public string PdfUrl { set; get; }
        public long? CreatedAt { set; get; }
        public long? UpdatedAt { set; get; }

        public Receipt WithReceiptId(string receiptId) {
            this.ReceiptId = receiptId;
            return this;
        }

        public Receipt WithAccountName(string accountName) {
            this.AccountName = accountName;
            return this;
        }

        public Receipt WithName(string name) {
            this.Name = name;
            return this;
        }

        public Receipt WithDate(long? date) {
            this.Date = date;
            return this;
        }

        public Receipt WithAmount(string amount) {
            this.Amount = amount;
            return this;
        }

        public Receipt WithPdfUrl(string pdfUrl) {
            this.PdfUrl = pdfUrl;
            return this;
        }

        public Receipt WithCreatedAt(long? createdAt) {
            this.CreatedAt = createdAt;
            return this;
        }

        public Receipt WithUpdatedAt(long? updatedAt) {
            this.UpdatedAt = updatedAt;
            return this;
        }

    	[Preserve]
        public static Receipt FromJson(JsonData data)
        {
            if (data == null) {
                return null;
            }
            return new Receipt()
                .WithReceiptId(!data.Keys.Contains("receiptId") || data["receiptId"] == null ? null : data["receiptId"].ToString())
                .WithAccountName(!data.Keys.Contains("accountName") || data["accountName"] == null ? null : data["accountName"].ToString())
                .WithName(!data.Keys.Contains("name") || data["name"] == null ? null : data["name"].ToString())
                .WithDate(!data.Keys.Contains("date") || data["date"] == null ? null : (long?)long.Parse(data["date"].ToString()))
                .WithAmount(!data.Keys.Contains("amount") || data["amount"] == null ? null : data["amount"].ToString())
                .WithPdfUrl(!data.Keys.Contains("pdfUrl") || data["pdfUrl"] == null ? null : data["pdfUrl"].ToString())
                .WithCreatedAt(!data.Keys.Contains("createdAt") || data["createdAt"] == null ? null : (long?)long.Parse(data["createdAt"].ToString()))
                .WithUpdatedAt(!data.Keys.Contains("updatedAt") || data["updatedAt"] == null ? null : (long?)long.Parse(data["updatedAt"].ToString()));
        }

        public JsonData ToJson()
        {
            return new JsonData {
                ["receiptId"] = ReceiptId,
                ["accountName"] = AccountName,
                ["name"] = Name,
                ["date"] = Date,
                ["amount"] = Amount,
                ["pdfUrl"] = PdfUrl,
                ["createdAt"] = CreatedAt,
                ["updatedAt"] = UpdatedAt,
            };
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if (ReceiptId != null) {
                writer.WritePropertyName("receiptId");
                writer.Write(ReceiptId.ToString());
            }
            if (AccountName != null) {
                writer.WritePropertyName("accountName");
                writer.Write(AccountName.ToString());
            }
            if (Name != null) {
                writer.WritePropertyName("name");
                writer.Write(Name.ToString());
            }
            if (Date != null) {
                writer.WritePropertyName("date");
                writer.Write(long.Parse(Date.ToString()));
            }
            if (Amount != null) {
                writer.WritePropertyName("amount");
                writer.Write(Amount.ToString());
            }
            if (PdfUrl != null) {
                writer.WritePropertyName("pdfUrl");
                writer.Write(PdfUrl.ToString());
            }
            if (CreatedAt != null) {
                writer.WritePropertyName("createdAt");
                writer.Write(long.Parse(CreatedAt.ToString()));
            }
            if (UpdatedAt != null) {
                writer.WritePropertyName("updatedAt");
                writer.Write(long.Parse(UpdatedAt.ToString()));
            }
            writer.WriteObjectEnd();
        }

        public int CompareTo(object obj)
        {
            var other = obj as Receipt;
            var diff = 0;
            if (ReceiptId == null && ReceiptId == other.ReceiptId)
            {
                // null and null
            }
            else
            {
                diff += ReceiptId.CompareTo(other.ReceiptId);
            }
            if (AccountName == null && AccountName == other.AccountName)
            {
                // null and null
            }
            else
            {
                diff += AccountName.CompareTo(other.AccountName);
            }
            if (Name == null && Name == other.Name)
            {
                // null and null
            }
            else
            {
                diff += Name.CompareTo(other.Name);
            }
            if (Date == null && Date == other.Date)
            {
                // null and null
            }
            else
            {
                diff += (int)(Date - other.Date);
            }
            if (Amount == null && Amount == other.Amount)
            {
                // null and null
            }
            else
            {
                diff += Amount.CompareTo(other.Amount);
            }
            if (PdfUrl == null && PdfUrl == other.PdfUrl)
            {
                // null and null
            }
            else
            {
                diff += PdfUrl.CompareTo(other.PdfUrl);
            }
            if (CreatedAt == null && CreatedAt == other.CreatedAt)
            {
                // null and null
            }
            else
            {
                diff += (int)(CreatedAt - other.CreatedAt);
            }
            if (UpdatedAt == null && UpdatedAt == other.UpdatedAt)
            {
                // null and null
            }
            else
            {
                diff += (int)(UpdatedAt - other.UpdatedAt);
            }
            return diff;
        }
    }
}