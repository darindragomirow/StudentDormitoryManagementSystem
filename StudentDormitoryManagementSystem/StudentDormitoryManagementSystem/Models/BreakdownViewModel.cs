using AutoMapper;
using StudentDormitoryManagementSystem.Data.Model.Models;
using StudentDormitoryManagementSystem.Infrastructure;
using System;

namespace StudentDormitoryManagementSystem.Models
{
    public class BreakdownViewModel : IMapFrom<Breakdown>, IHaveCustomMappings
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public short RoomNumber { get; set; }

        public string Sender { get; set; }

        public string Severity { get; set; }

        public bool Acknowledged { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Breakdown, BreakdownViewModel>()
                 .ForMember(breakdownViewModel => breakdownViewModel.Id, cfg => cfg.MapFrom(item => item.Id))
                 .ForMember(breakdownViewModel => breakdownViewModel.Title, cfg => cfg.MapFrom(item => item.Title))
                 .ForMember(breakdownViewModel => breakdownViewModel.Description, cfg => cfg.MapFrom(item => item.Description))
                 .ForMember(breakdownViewModel => breakdownViewModel.RoomNumber, cfg => cfg.MapFrom(item => item.RoomNumber))
                 .ForMember(breakdownViewModel => breakdownViewModel.Sender, cfg => cfg.MapFrom(item => item.Sender))
                 .ForMember(breakdownViewModel => breakdownViewModel.Severity, cfg => cfg.MapFrom(item => item.Severity.ToString()))
                 .ForMember(breakdownViewModel => breakdownViewModel.Acknowledged, cfg => cfg.MapFrom(item => item.Acknowledged));
        }
    }
}