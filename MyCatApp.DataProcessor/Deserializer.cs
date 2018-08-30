using MyCarApp.Data;
using MyCarApp.Models.Vehicles;
using MyCatApp.DataProcessor.Dtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyCatApp.DataProcessor
{
    public static class Deserializer
    {
        public static void ImportColours(ApplicationDbContext context, string baseDir, string fileName)
        {
            if (!context.Colours.Any())
            {
                var jsonString = File.ReadAllText(baseDir + fileName);

                var dtos = JsonConvert.DeserializeObject<ColournDtoImport[]>(jsonString);

                List<Colour> newEntities = new List<Colour>();

                foreach (var dto in dtos)
                {
                    Colour entity = new Colour()
                    {
                        Name = dto.Name,
                    };

                    newEntities.Add(entity);
                }

                context.Colours.AddRange(newEntities);
                //context.SaveChanges();
            }
        }

        public static void ImportPictureValidExtensions(ApplicationDbContext context, string baseDir, string fileName)
        {
            if (!context.PictureValidExtensions.Any())
            {
                var jsonString = File.ReadAllText(baseDir + fileName);

                var dtos = JsonConvert.DeserializeObject<PictureExtensionDtoImport[]>(jsonString);

                List<PictureValidExtensions> newEntities = new List<PictureValidExtensions>();

                foreach (var dto in dtos)
                {
                    PictureValidExtensions entity = new PictureValidExtensions()
                    {
                        Extension = dto.Extension,
                    };

                    newEntities.Add(entity);
                }

                context.PictureValidExtensions.AddRange(newEntities);
            }
        }

        public static void ImportFuelTypes(ApplicationDbContext context, string baseDir, string fileName)
        {
            if (!context.FuelTypes.Any())
            {
                var jsonString = File.ReadAllText(baseDir + fileName);

                var dtos = JsonConvert.DeserializeObject<FuelTypeDtoImport[]>(jsonString);

                List<FuelType> newEntities = new List<FuelType>();

                foreach (var dto in dtos)
                {
                    FuelType entity = new FuelType()
                    {
                        Fuel = dto.Fuel,
                    };

                    newEntities.Add(entity);
                }

                context.FuelTypes.AddRange(newEntities);
                //context.SaveChanges();
            }
        }

        public static void ImportTransmissions(ApplicationDbContext context, string baseDir, string fileName)
        {
            if (!context.Transmissions.Any())
            {
                var jsonString = File.ReadAllText(baseDir + fileName);

                var dtos = JsonConvert.DeserializeObject<TransmissionDtoImport[]>(jsonString);

                List<Transmission> newEntities = new List<Transmission>();

                foreach (var dto in dtos)
                {
                    Transmission entity = new Transmission()
                    {
                        Type = dto.Type,
                    };

                    newEntities.Add(entity);
                }

                context.Transmissions.AddRange(newEntities);
                //context.SaveChanges();
            }
        }

        public static void ImportVehicleConditions(ApplicationDbContext context, string baseDir, string fileName)
        {
            if (!context.VehicleConditions.Any())
            {
                var jsonString = File.ReadAllText(baseDir + fileName);

                var dtos = JsonConvert.DeserializeObject<VehicleConditionDtoImport[]>(jsonString);

                List<VehicleCondition> newEntities = new List<VehicleCondition>();

                foreach (var dto in dtos)
                {
                    VehicleCondition entity = new VehicleCondition()
                    {
                        Condition = dto.Condition,
                    };

                    newEntities.Add(entity);
                }

                context.VehicleConditions.AddRange(newEntities);
                //context.SaveChanges();
            }
        }

        public static void ImportVehicleMakes(ApplicationDbContext context, string baseDir, string fileName)
        {
            if (!context.VehicleMakes.Any())
            {
                var jsonString = File.ReadAllText(baseDir + fileName);

                var dtos = JsonConvert.DeserializeObject<VehicleMakeDtoImport[]>(jsonString);

                List<VehicleMake> newEntities = new List<VehicleMake>();

                foreach (var dto in dtos)
                {
                    VehicleMake entity = new VehicleMake()
                    {
                        Name = dto.Name,
                    };

                    foreach (var dtoModel in dto.Models)
                    {
                        var model = new VehicleModel()
                        {
                            Name = dtoModel.Name
                        };

                        entity.Models.Add(model);
                    }

                    newEntities.Add(entity);
                }

                context.VehicleMakes.AddRange(newEntities);
                //context.SaveChanges();
            }
        }

        public static void ImportVehicleTypes(ApplicationDbContext context, string baseDir, string fileName)
        {
            if (!context.VehicleTypes.Any())
            {
                var jsonString = File.ReadAllText(baseDir + fileName);

                var dtos = JsonConvert.DeserializeObject<VehicleTypeDtoImport[]>(jsonString);

                List<VehicleType> newEntities = new List<VehicleType>();

                foreach (var dto in dtos)
                {
                    VehicleType entity = new VehicleType()
                    {
                        Type = dto.Type,
                    };

                    newEntities.Add(entity);
                }

                context.VehicleTypes.AddRange(newEntities);
                //context.SaveChanges();
            }
        }
    }
}
