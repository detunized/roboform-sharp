// Copyright (C) 2017 Dmitry Yakimenko (detunized@gmail.com).
// Licensed under the terms of the MIT license. See LICENCE for details.

namespace RoboForm
{
    public class Account
    {
        public enum FieldKind
        {
            Text,
            Password
        }

        public struct Field
        {
            public readonly string Name;
            public readonly string Value;
            public readonly FieldKind Kind;

            public Field(string name, string value, FieldKind kind)
            {
                Name = name;
                Value = value;
                Kind = kind;
            }
        }

        public readonly string Name;
        public readonly string Path;
        public readonly string Url;
        public readonly Field[] Fields;

        public Account(string name,
                       string path,
                       string url,
                       Field[] fields)
        {
            Name = name;
            Path = path;
            Url = url;
            Fields = fields;
        }
    }
}
