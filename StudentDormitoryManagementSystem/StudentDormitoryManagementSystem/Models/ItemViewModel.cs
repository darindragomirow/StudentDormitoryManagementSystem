using System;
using AutoMapper;
using StudentDormitoryManagementSystem.Data.Model.Models;
using StudentDormitoryManagementSystem.Infrastructure;

namespace StudentDormitoryManagementSystem.Models
{
    public class ItemViewModel : IMapFrom<Item>, IHaveCustomMappings
    {
        public ItemViewModel() 
        {
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Count { get; set; }

        public string Material { get; set; }

        public string Model { get; set; }

        public string State { get; set; }

        public string Size { get; set; }

        public ItemCategory ItemCategory { get; set; }

        public Room Room { get; set; }

        public DateTime DateRegistered { get; set; }

        public DateTime ExpirationDate { get; set; }

        public bool canReserve { get; set; }

        public string CurrentStatus { get; set; }

        public string ImagePath { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Item, ItemViewModel>()
                .ForMember(itemViewModel => itemViewModel.Id, cfg => cfg.MapFrom(item => item.Id))
                .ForMember(itemViewModel => itemViewModel.Name, cfg => cfg.MapFrom(item => item.Name))
                .ForMember(itemViewModel => itemViewModel.ItemCategory, cfg => cfg.MapFrom(item => item.ItemCategory))
                .ForMember(itemViewModel => itemViewModel.Description, cfg => cfg.MapFrom(item => item.Description))
                .ForMember(itemViewModel => itemViewModel.Count, cfg => cfg.MapFrom(item => item.Count))
                .ForMember(itemViewModel => itemViewModel.Material, cfg => cfg.MapFrom(item => item.Material))
                .ForMember(itemViewModel => itemViewModel.Model, cfg => cfg.MapFrom(item => item.Model))
                .ForMember(itemViewModel => itemViewModel.Size, cfg => cfg.MapFrom(item => item.Size.ToString()))
                .ForMember(itemViewModel => itemViewModel.State, cfg => cfg.MapFrom(item => item.State.ToString()))
                .ForMember(itemViewModel => itemViewModel.Room, cfg => cfg.MapFrom(item => item.Room))
                .ForMember(itemViewModel => itemViewModel.DateRegistered, cfg => cfg.MapFrom(item => item.DateRegistered))
                .ForMember(itemViewModel => itemViewModel.ExpirationDate, cfg => cfg.MapFrom(item => item.ExpirationDate))
                .ForMember(itemViewModel => itemViewModel.canReserve, cfg => cfg.MapFrom(item => item.canReserve));
        }
    }
}