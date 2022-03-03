using System;
using NederlandseLoterij.KrasLoterij.Service.Contracts.DTO.Base;
using Newtonsoft.Json;

namespace NederlandseLoterij.KrasLoterij.Service.Extensions
{
    public static class JsonExtensions
    {
        public static BaseDTO<T> TryDeserialize<T>(this string json)
        {
            var result = new BaseDTO<T>();

            try
            {
                result.Data = JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                result.AddError($"Failed to deserialize json to type of {typeof(T)} - {ex.Message}");
            }

            return result;
        }
    }
}