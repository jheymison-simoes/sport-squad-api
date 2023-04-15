using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SportSquad.Business.Tests.Utils;

public static class FakeDataGenerator
{
    private const string CultureFaker = "pt_BR";
    
    public static IList<T> GenerateFakeData<T>(int quantity, string cultureFaker = CultureFaker) where T : class, new()
    {
        return GenerateFakesData<T>(quantity);
    }
    
    public static T GenerateFakeData<T>(string cultureFaker = CultureFaker) where T : class, new()
    {
        return GenerateFakesData<T>().First();
    }
    
    private static IList<T> GenerateFakesData<T>(int? quantity = null, string cultureFaker = CultureFaker) where T : class, new()
    {
        var faker = new Faker<T>(cultureFaker);
        var properties = typeof(T).GetProperties();

        foreach (var property in properties)
        {
            var propertyType = property.PropertyType;
            
            var underlyingType = Nullable.GetUnderlyingType(propertyType);
            var isNullable = underlyingType != null;
            
            if (!isNullable)
            {
                switch (property.Name.ToUpper())
                {
                    case "EMAIL":
                        faker.RuleFor(property.Name, f => f.Internet.Email());
                        break;
                    case "NAME":
                        faker.RuleFor(property.Name, f => f.Name.FullName());
                        break;
                    case "PHONENUMBER":
                        faker.RuleFor(property.Name, f => f.Random.Replace("##########"));
                        break;
                    default:
                        faker.RuleFor(property.Name, f => GetRandomValue(propertyType, f));
                        break;
                }
            }
            else
            {
                switch (property.Name.ToUpper())
                {
                    case "EMAIL":
                        faker.RuleFor(property.Name, f => f.Internet.Email().OrNull(f));
                        break;
                    case "NAME":
                        faker.RuleFor(property.Name, f => f.Name.FullName().OrNull(f));
                        break;
                    case "PHONENUMBER":
                        faker.RuleFor(property.Name, f => f.Random.Replace("##########").OrNull(f));
                        break;
                    default:
                        faker.RuleFor(property.Name, f => GetRandomNullableValue(underlyingType, f).OrNull(f));
                        break;
                }
            }
        }
        
        return quantity.HasValue ? faker.Generate(quantity.Value) : faker.Generate(1);
    }
    
    private static object GetRandomValue(Type propertyType, Faker faker)
    {
        if (propertyType == typeof(string)) return faker.Random.String(10);
        
        if (propertyType == typeof(int)) return faker.Random.Int();
        
        if (propertyType == typeof(decimal)) return faker.Random.Decimal();
        
        if (propertyType == typeof(DateTime)) return faker.Date.Past();
        
        if (propertyType == typeof(bool)) return faker.Random.Bool();
        
        throw new Exception($"Tipo de propriedade não suportado: {propertyType}");
    }

    private static object GetRandomNullableValue(Type propertyType, Faker faker)
    {
        // Verifica o tipo subjacente da propriedade nulável
        var underlyingType = Nullable.GetUnderlyingType(propertyType);

        // Define um valor nulo aleatório para o tipo subjacente da propriedade nulável
        if (underlyingType == typeof(string))
        {
            return faker.Random.String(10).OrNull(faker);
        }
        else if (underlyingType == typeof(int))
        {
            return faker.Random.Int().OrNull(faker);
        }
        else if (underlyingType == typeof(decimal))
        {
            return faker.Random.Decimal().OrNull(faker);
        }
        else if (underlyingType == typeof(DateTime))
        {
            return faker.Date.Past().OrNull(faker);
        }
        else if (underlyingType == typeof(bool))
        {
            return faker.Random.Bool().OrNull(faker);
        }
        // Adicione outras verificações de tipos aqui

        throw new Exception($"Tipo de propriedade nulável não suportado: {propertyType}");
    }
}

// public static T GenerateFakeData<T>() where T : class, new()
// {
//     var faker = new Faker<T>();
//
//     // Obtém as propriedades da classe
//     var properties = typeof(T).GetProperties();
//
//     // Itera sobre as propriedades e define valores fake para cada uma
//     foreach (var property in properties)
//     {
//         var propertyType = property.PropertyType;
//
//         // Verifica se a propriedade é nulável
//         var underlyingType = Nullable.GetUnderlyingType(propertyType);
//         var isNullable = underlyingType != null;
//
//         // Define um valor aleatório para a propriedade com base em seu tipo
//         if (!isNullable)
//         {
//             faker.RuleFor(property.Name, f => GetRandomValue(propertyType, f));
//         }
//         // Define um valor nulo aleatório para a propriedade se ela for nulável
//         else
//         {
//             faker.RuleFor(property.Name, f => GetRandomNullableValue(underlyingType, f));
//         }
//     }
//
//     return faker.Generate();
// }
//
// private static object GetRandomValue(Type propertyType, Faker faker)
// {
//     if (propertyType == typeof(string))
//     {
//         return faker.Random.String(10);
//     }
//     else if (propertyType == typeof(int))
//     {
//         return faker.Random.Int();
//     }
//     else if (propertyType == typeof(decimal))
//     {
//         return faker.Random.Decimal();
//     }
//     else if (propertyType == typeof(DateTime))
//     {
//         return faker.Date.Past();
//     }
//     else if (propertyType == typeof(bool))
//     {
//         return faker.Random.Bool();
//     }
//     // Adicione outras verificações de tipos aqui
//
//     throw new Exception($"Tipo de propriedade não suportado: {propertyType}");
// }
//
// private static object GetRandomNullableValue(Type propertyType, Faker faker)
// {
//     // Verifica o tipo subjacente da propriedade nulável
//     var underlyingType = Nullable.GetUnderlyingType(propertyType);
//
//     // Define um valor nulo aleatório para o tipo subjacente da propriedade nulável
//     if (underlyingType == typeof(string))
//     {
//         return faker.Random.String(10).OrNull(faker);
//     }
//     else if (underlyingType == typeof(int))
//     {
//         return faker.Random.Int().OrNull(faker);
//     }
//     else if (underlyingType == typeof(decimal))
//     {
//         return faker.Random.Decimal().OrNull(faker);
//     }
//     else if (underlyingType == typeof(DateTime))
//     {
//         return faker.Date.Past().OrNull(faker);
//     }
//     else if (underlyingType == typeof(bool))
//     {
//         return faker.Random.Bool().OrNull(faker);
//     }
//     // Adicione outras verificações de tipos aqui
//
//     throw new Exception($"Tipo de propriedade nulável não suportado: {propertyType}");
// }
//
