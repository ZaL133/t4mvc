﻿using Microsoft.DiaSymReader;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using t4mvc.core;

namespace t4mvc.data
{
    public static class AuditInspector
    {
        public static AuditRecord GetCreateAuditRecord<T>(this T record, Guid recordId, Guid userId)
        {
            var rv = new AuditRecord
            {
                AuditRecordId   = Guid.NewGuid(),
                AuditType       = "Created",
                CreateDate      = DateTime.Now,
                RecordId        = recordId, 
                UserId          = userId
            };
            rv.ChangedFields    = JsonConvert.SerializeObject(record, Formatting.Indented);
            rv.RecordType       = record.GetType().Name;

            return rv;
        }

        public static AuditRecord GetUpdateAuditRecord<T>(this T oldRecord, T newRecord, IEnumerable<string> fieldsToIgnore)
        {
            string auditType = "Update";

            var rv = new AuditRecord
            {
                AuditRecordId   = Guid.NewGuid(),
                AuditType       = auditType,
                CreateDate      = DateTime.Now
            };

            var recordType = oldRecord.GetType();
            rv.RecordType = recordType.Name;

            var changedValues = new List<ChangeValue>();
            var allProperties = recordType.GetProperties(System.Reflection.BindingFlags.Instance | BindingFlags.Public)
                                                   // Ignore anything specified in the fieldsToIgnore list
                                                   // This will contain read-only properties as well as the create/modify user/date
                                                   .Where(x => !fieldsToIgnore.Contains(x.Name)
                                                                && x.Name != "ModifyDate");

            rv.RecordId = (Guid)(allProperties.First().GetValue(oldRecord));

            foreach (var prop in allProperties)
            {
                if (prop.Name == "ModifyUserId") rv.UserId = (Guid)prop.GetValue(newRecord);
                var oldValue = prop.GetValue(oldRecord);
                var newValue = prop.GetValue(newRecord);

                if (oldValue == null && newValue == null)
                {
                    // do nothing
                }
                else
                {
                    if (oldValue == null)
                    {
                        changedValues.Add(new ChangeValue
                        {
                            FieldName   = prop.Name,
                            OldValue    = oldValue?.ToString(),
                            NewValue    = newValue?.ToString()
                        });
                    }
                    else
                    {
                        if (!oldValue.Equals(newValue))
                        {
                            changedValues.Add(new ChangeValue
                            {
                                FieldName   = prop.Name,
                                OldValue    = oldValue?.ToString(),
                                NewValue    = newValue?.ToString()
                            });
                        }
                    }
                }
            }

            rv.ChangedFields = JsonConvert.SerializeObject(changedValues, Formatting.Indented);

            return rv;
        }

        public static AuditRecord GetDeleteAuditRecord<T>(this T oldRecord, Guid recordId, Guid userId)
        {
            var rv = new AuditRecord
            {
                AuditRecordId   = Guid.NewGuid(),
                AuditType       = "Deleted",
                CreateDate      = DateTime.Now,
                RecordId        = recordId, 
                UserId          = userId
            };
            rv.ChangedFields    = JsonConvert.SerializeObject(oldRecord, Formatting.Indented);
            rv.RecordType       = oldRecord.GetType().Name;

            return rv;
        }
    }

    public class ChangeValue
    {
        public string FieldName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
    }

}
