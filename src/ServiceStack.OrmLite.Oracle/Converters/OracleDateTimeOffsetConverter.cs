﻿using System;
using ServiceStack.OrmLite.Converters;

namespace ServiceStack.OrmLite.Oracle.Converters
{
    //TODO: Custom DateTimeOffset logic from DialectProvider should be moved here
    public class OracleDateTimeOffsetConverter : DateTimeOffsetConverter
    {
        private OracleTimestampConverter _timestampConverter;

        public OracleDateTimeOffsetConverter(OracleTimestampConverter timestampConverter)
        {
            _timestampConverter = timestampConverter;
        }

        public override string ColumnDefinition
        {
            get { return "TIMESTAMP WITH TIME ZONE"; }
        }

        public OracleOrmLiteDialectProvider OracleDialect
        {
            get { return (OracleOrmLiteDialectProvider)DialectProvider; }
        }

        public override string ToQuotedString(Type fieldType, object value)
        {
            return OracleDialect.GetQuotedDateTimeOffsetValue((DateTimeOffset)value);
        }

        public override object ToDbParamValue(Type fieldType, object value)
        {
            return OracleDialect.GetQuotedDateTimeOffsetValue((DateTimeOffset)value);
        }

        public override object ToDbValue(FieldDefinition fieldDef, object value)
        {
            var timestamp = (DateTimeOffset)value;
            return _timestampConverter.ConvertToOracleTimeStampTz(timestamp);
        }

        public override object FromDbValue(FieldDefinition fieldDef, object value)
        {
            return Convert.ChangeType(value, typeof(DateTimeOffset));
        }
    }
}