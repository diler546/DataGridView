using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGridView
{
    public static class EnumHelper
    {
        /// <summary>
        /// Получение всех атрибутов описания (descriptions)
        /// </summary>
        public static IEnumerable<string> GetEnumDescriptions(Type type)
        {
            var strings = new Collection<string>();
            foreach (Enum value in Enum.GetValues(type))
            {
                strings.Add(GetEnumDescription(value));
            }
            return strings;
        }

        /// <summary>
        /// Получение атрибута описания из Enum по одному из свойств
        /// </summary>
        public static string GetEnumDescription(Enum value)
        {
            var fields = value.GetType().GetField(value.ToString());
            var attributes = fields?.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (!(attributes is null))
            {
                return ((DescriptionAttribute)attributes[0]).Description;
            }

            return value.ToString();
        }
    }
}
